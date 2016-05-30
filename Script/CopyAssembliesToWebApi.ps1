<#
   Running this script will copy plugins to serveice host location;Don't add refference for concreat class in pulgin project. 
   How To: Go to Build Event in host service project and in Post Build Event section add the command, Powershell.exe -file "$(SolutionDir)scripts\CopyAssembliesToWebApi.ps1" $(ConfigurationName)
#>
param(
    [Parameter(Mandatory=$true)][string] $buildConfiguration
)

$sourcePath = Split-Path -parent (Split-Path -parent $MyInvocation.MyCommand.Definition)
$mvcWebApiPath = Join-Path $sourcePath "\SimpleUintiy.WebApiMvc4\bin"
$projectsToCopy = @("SimpleUnity.Domain.Services", "SimpleUnity.Repo.Impl.sql", "SimpleUnity.Repo.Impl.Isilion", "SimpleUnity.Repo.Impl.Sql")

$includeFilter = @("*.dll", "*.pdb", "*.xml")

foreach ($project in $projectsToCopy)
{
    $projectPath = Join-Path $sourcePath ($project + "\bin\" + $buildConfiguration + "\")
    #Does the directory exist for this project and configuration?
    if (!(Test-Path $projectPath))
    {
        Write-Warning ($projectPath + " does not exist.")
        continue
    }

    #Add * to make sure we get files back!
    $projectPath = Join-Path $projectPath "*"

    foreach ($projectFile in Get-ChildItem $projectPath -Include $includeFilter)
    {
        $destinationFile = Join-Path $mvcWebApiPath $projectFile.Name
        if ((!(Test-Path $destinationFile)) -or ($projectFile.LastWriteTimeUtc -gt (Get-Item $destinationFile).LastWriteTimeUtc ))
        {
            ("Copying " + $projectFile.FullName + " to " + $destinationFile)
            Copy-Item $projectFile.FullName $destinationFile
        }
    }
}