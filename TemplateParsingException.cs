using Superpower.Model;
using System;

namespace HafrPsModule {
	public class TemplateParsingException : Exception {
		public TemplateParsingException(string message, Position position) : base(message) {
			Position = position;
		}

		public Position Position { get; }
	}
}
