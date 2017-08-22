# GameLib

[![Build status](https://ci.appveyor.com/api/projects/status/middixk3cpev8eyk/branch/master?svg=true)](https://ci.appveyor.com/project/moisesejimenezg/gamelib/branch/master) [![NuGet Version and Downloads count](https://buildstats.info/nuget/GameLib)](https://www.nuget.org/packages/GameLib)

GameLib is a pure C# library intended to speed up game development. Primarily thought as a companion to Unity3D, because it has no dependencies to it GameLib can be used independently of Unity3D.

It currently provides the following features:

- Save File Management.
- Text Resource Management.
- Inventory Management.

## Dependencies

The dependencies in the project currently are:

- NUnit v3.5*.
- Json.NET v9.0*.
- CakeBuild.

\*: Kept at older versions due to Unity3D using .NET 3.5.

## Installing GameLib

### Downloading via NuGet

GameLib is currently available through the NuGet package manager and can be installed through it.

### Building GameLib From Scratch (Windows)

GameLib is a [CakeBuild](https://cakebuild.net/) project. Running the provided build.ps1 script should be enough. After which you can add a reference to it in your project.

### Using with Unity3D

In order to be able to use GameLib with Unity3D, simply drop the GameLib.\*.dll and its dependencies' .dlls  somewhere under your Assets directory.
