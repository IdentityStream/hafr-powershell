using System.Management.Automation;
using Hafr.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace HafrPsModule
{
    [Cmdlet(VerbsCommon.Format, "Hafr")]
    [OutputType(typeof(string), typeof(string[]))]
    public class HafrCmdlet : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true)]
        public string Template { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public PSObject Model { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            if (Parser.TryParse(Template, out var expression, out var errorMessage, out var errorPosition))
            {
                IEnumerable<string> resultIterator;
                // Parsing succeeded, it's safe to access the expression in here...
                if (Model.BaseObject is PSCustomObject)
                {
                    resultIterator = expression.EvaluateProperties(GetProperties(Model));
                }
                else if (Model.BaseObject is Hashtable hashTable) {
                    resultIterator = expression.EvaluateProperties(GetProperties(hashTable));
				}
                else
                {
                    resultIterator = expression.EvaluateModel(Model.BaseObject);
                }
                var results = resultIterator.ToArray();
                if(results.Length == 1){
                    WriteObject(results[0]);
                }else{
                    WriteObject(results);
                }
            }
            else
            {
                throw new TemplateParsingException(errorMessage, errorPosition);
            }
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }

        private static IDictionary<string, object> GetProperties(PSObject model)
        {
            var result = new Dictionary<string, object>();

            foreach (var property in model.Properties)
            {
                result[property.Name] = property.Value;
            }

            return result;
        }

        private static IDictionary<string, object> GetProperties(Hashtable model) {
            var result = new Dictionary<string, object>();

            foreach (var property in model.Keys) {
                result[property.ToString()] = model[property];
            }

            return result;
        }
    }
}
