param($ProjectDir)

# Script assumes its in the project folder
# Use the following to call from pre build:
#
# powershell.exe -Command $(ProjectDir)\version.ps1 -ProjectDir:'$(ProjectDir)'
#
# Requires powershell script execution permissions (and likely powershell w32)
# to set execute permissions, run the following in admin powershell:
#
# Set-ExecutionPolicy RemoteSigned
#

# The following variables are used for the final version output.
#    String Version:  Major.Minor.Fix.Stage#[.Patches.SHA1[.dirty]]
$strFILE_VERSION= $null

#    Digital Version: Major, Minor, Fix, Patches
$nbMAJOR_PART=0
$nbMINOR_PART=0
$nbFIX_PART=0
$nbPATCHES_PART=0

# Supporting StringFileInfo - not used for clean release builds.
$strConfiguration=$null
$releaseTag=$null

# Precedence is Git, VERSION_FILE, then DEFAULT_VERSION.
# Check if git is available by testing git describe.
git describe
if ($LASTEXITCODE -ne 1) {
  $strFILE_VERSION = git describe HEAD
  
  $tmp = git diff-index --name-only HEAD --

  if ([bool]$tmp) {
	$strConfiguration="Custom Build for "
	$strConfiguration+=git config user.name
	$strConfiguration+=" "
  }
  # The min version is X.Y.Z and the max is X.Y.Z.Stage#.Commits.SHA.dirty
  # strTMP_STAGE_PART is a holder for anything past 'X.Y.Z.'.
  $parts = $strFILE_VERSION.split(".")
  $nbMAJOR_PART=$parts[0].trim("v")
  $nbMINOR_PART=$parts[1]
  
  $finalparts=$parts[2].split("-")
  $nbFIX_PART=$finalparts[0]
  
  if ([bool]$finalparts[1] -And ![bool]$finalparts[2]) {
    $releaseTag = $finalparts[1]
    $strVersion = "v$nbMAJOR_PART.$nbMINOR_PART.$nbFIX_PART-$releaseTag.."
	switch ($releaseTag)
	{
	  "a" {$strConfiguration+="Alpha " }
	  "b" {$strConfiguration+="Beta " }
	  "rc" {$strConfiguration+="Release Candidate " }
	}
  }
  else {
    $strVersion = "v$nbMAJOR_PART.$nbMINOR_PART.$nbFIX_PART.."
  }

  $nbPATCHES_PART = git rev-list --count $strVersion
  $hash = git rev-parse --short HEAD
  
  if (![bool]$strConfiguration) {
    if ($nbPATCHES_PART -gt 0){
	  $strConfiguration = "Patched "
	}
	else{
	  $strConfiguration = "Release "
	}
  }
  # Put hash in config string
  $strConfiguration+="(git-$hash)"
  
  #Full version number major.minor.fix.patch
  $fullVersion = "$nbMAJOR_PART.$nbMINOR_PART.$nbFIX_PART.$nbPATCHES_PART"
}
else
{
  'an error occurred'
  $LASTEXITCODE
}

(gc $ProjectDir\AssemblyInfo.template) -replace 'assembly: AssemblyConfiguration\(".*"\)',('assembly: AssemblyConfiguration("{0}")' -f ($strConfiguration)) | Out-String | Out-File -Encoding UTF8 $ProjectDir\Properties\AssemblyInfo.cs
(gc $ProjectDir\Properties\AssemblyInfo.cs) -replace 'assembly: AssemblyFileVersion\(".*"\)',('assembly: AssemblyFileVersion("{0}")' -f ($fullVersion)) | Out-String | Out-File -Encoding UTF8 $ProjectDir\Properties\AssemblyInfo.cs
(gc $ProjectDir\Properties\AssemblyInfo.cs) -replace 'assembly: AssemblyVersion\(".*"\)',('assembly: AssemblyVersion("{0}")' -f ($fullVersion)) | Out-String | Out-File -Encoding UTF8 $ProjectDir\Properties\AssemblyInfo.cs
(gc $ProjectDir\Properties\AssemblyInfo.cs) -replace 'assembly: AssemblyInformationalVersion\(".*"\)',('assembly: AssemblyInformationalVersion("{0}")' -f ($strFILE_VERSION)) | Out-String | Out-File -Encoding UTF8 $ProjectDir\Properties\AssemblyInfo.cs
(gc -Raw $ProjectDir\Properties\AssemblyInfo.cs) -replace '(\r\n){3,}','$1' | Out-File -Encoding UTF8 $ProjectDir\Properties\AssemblyInfo.cs