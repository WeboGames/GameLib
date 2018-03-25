#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var buildDir = Directory("./src/GameLib/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() => 
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./src/GameLib.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./src/GameLib.sln", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./src/GameLib.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3("./src/**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {
        NoResults = true
        });
});

//////////////////////////////////////////////////////////////////////
// PACKAGE
//////////////////////////////////////////////////////////////////////

var RootFiles = new FilePath[]
{
    "LICENSE",
    "README.md"
};

var FilesToPackage = new FilePath[]
{
    "GameLib.dll",
    "Newtonsoft.Json.dll",
    "Newtonsoft.Json.xml"
};

Task("CreateImage")
    .IsDependentOn("Run-Unit-Tests")
    .Description("Copies files")
    .Does(() =>
    {
        var dir = "./src/GameLib/images/";
        var binDir = dir + "bin/";
        var sourceDir = "./src/GameLib/bin/Release/";

        CleanDirectory(dir);
        CopyFiles(RootFiles, dir);
        CreateDirectory(binDir);
        Information("Created directory " + binDir);

        foreach (FilePath file in FilesToPackage) {
            var sourcePath = sourceDir + file;
            if (FileExists(sourcePath)) {
                CopyFileToDirectory(sourcePath, binDir);
            }
        }
    });

Task("Package")
    .Description("Creates NuGet package")
    .IsDependentOn("CreateImage")
    .Does(() =>
    {
        var dir = "./src/GameLib/images/";
        var packageDir = "./src/GameLib/packages/";
        CreateDirectory(packageDir);
        NuGetPack("./nuget/GameLib.nuspec", new NuGetPackSettings()
        {
            Version = "0.0.8",
            BasePath = dir,
            OutputDirectory = packageDir
        });
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
