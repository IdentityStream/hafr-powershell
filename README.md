<div align="center">
	<img src="logo.png" width="320" height="320">
	<p>
		<b>A tiny templating language for writing email generation conventions.</b>    
	</p>
</div>

# Hafr PowerShell module

A PowerShell module for formatting objects using [Hafr](https://github.com/IdentityStream/Hafr).

# Installation

The Hafr powershell module is available via the Powershell Gallery:

```powershell
Install-Module Hafr
```

# Usage

The module consists of a CmdLet called `Format-Hafr`.

## Format-Hafr

### Parameters

#### Model

It accepts a PowerShell object. The object can be sent in via the pipeline.

#### Template

This is a Hafr template that will be applied to the model.

### Examples

#### Single object via pipeline

```powershell
$user = @{ Firstname = "John Alexander"; Lastname = "Doe" }
$user | Format-Hafr "{ Firstname | replace(' ', '.') }.{ Lastname }"
# Output:
# John.Alexander.Doe
```

#### Single object via parameters

```powershell
$user = @{ Firstname = "John Alexander"; Lastname = "Doe" }
Format-Hafr -Model $user -Template "{ Firstname | replace(' ', '.') }.{ Lastname }"
# Output:
# John.Alexander.Doe
```

#### Array of objects via pipeline

```powershell
$user1 = @{ Firstname = "John Alexander"; Lastname = "Doe" }
$user2 = @{ Firstname = "Carl Benjamin"; Lastname = "Lewis" }
$users = $user1, $user2
$users | Format-Hafr "{ Firstname | replace(' ', '.') }.{ Lastname }"
# Output:
# John.Alexander.Doe
# Carl.Benjamin.Lewis
```