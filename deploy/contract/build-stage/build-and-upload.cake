#addin "Cake.FileHelpers"

Environment.CurrentDirectory = Directory("../../../");
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var buildDir = MakeAbsolute(Directory("build"));
var buildBinDir = buildDir + Directory("/bin");
var projectDir = "src\\ApplicationAcquisitionSubmissions.Contract\\";
var projectPath = projectDir + "ApplicationAcquisitionSubmissions.Contract.csproj";
var version = EnvironmentVariable("GO_PIPELINE_LABEL") ?? "NOVERSIONSPECIFIED";
var nexusAddress = EnvironmentVariable("NEXUS_ADDRESS");
var nexusApiKey = EnvironmentVariable("NEXUS_APIKEY");

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("UpdateAssemblyInfo")
    .Does(() =>
{
    var assemblyInfo = projectDir + "Properties/AssemblyInfo.cs";
    ReplaceRegexInFiles(assemblyInfo,  "AssemblyVersion(\"1.0.0.0\")", "AssemblyVersion(\""+ version + "\")");
	ReplaceRegexInFiles(assemblyInfo,  "AssemblyFileVersion(\"1.0.0.0\")", "AssemblyFileVersion(\""+ version + "\")");
	ReplaceRegexInFiles(assemblyInfo,  "AssemblyCompany(\"\")", "AssemblyFileVersion(\"Zuto\")");
});


Task("Build")    
    .Does(() =>
{
	Console.WriteLine(projectPath);
	MSBuild(projectPath, settings =>
        settings.SetConfiguration(configuration)		  
		  .WithProperty("OutputPath", "\"" + buildBinDir + "\"")
          .SetVerbosity(Verbosity.Quiet));       

    CopyFile(projectDir + "ApplicationAcquisitionSubmissions.Contract.nuspec", buildBinDir + "\\ApplicationAcquisitionSubmissions.Contract.nuspec");
});

Task("Package")    
    .Does(() =>
{		
    NuGetPack(buildBinDir + "\\ApplicationAcquisitionSubmissions.Contract.nuspec", new NuGetPackSettings
	{        
		Verbosity = NuGetVerbosity.Detailed,		
		IncludeReferencedProjects = true,
		Version = version
    });
});

Task("Upload")    
    .Does(() =>
{
	var package = "ApplicationAcquisitionSubmissions.Contract." + version + ".nupkg";
	NuGetPush(package, new NuGetPushSettings 
	{ 
		Source = nexusAddress,
		ApiKey = nexusApiKey, 
		Verbosity = NuGetVerbosity.Detailed
	});
});


Task("Default")    
	.IsDependentOn("Clean")
	.IsDependentOn("UpdateAssemblyInfo")
	.IsDependentOn("Build")
	.IsDependentOn("Package")	
	.IsDependentOn("Upload");

RunTarget(target);
