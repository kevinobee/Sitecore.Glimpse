# Sitecore.Glimpse

Sitecore.Glimpse is an Glimpse extension that provides runtime web diagnostics for Sitecore sites.

This project includes the following NuGet packages:

* Sitecore.Glimpse
* Sitecore.Glimpse.Mvc3 (extension for those wanting to use Sitecore's MVC capability)
* Sitecore.Glimpse.Mvc4 (supports the ASP.NET MVC4 release)

## View the GitHub Pages

Take a look at the [Sitecore.Glimpse](http://kevinobee.github.io/Sitecore.Glimpse/) GitHub pages for all of the documentation on the extension and information on how to participate in the project.


## Instructions for Use

Either pull the package(s) from the NuGet gallery or clone the GitHub repos to get the latest code.


### Building the Package

* Clone this repository to your local file system

* You will need to specify the location of your Sitecore assemblies. This can be down in the `build\build.proj` file. Set the LibsSrcPath to where your Sitecore assemblies are located.

* From a command prompt type `.\build` and press Enter

### Installing the Package

Ensure that the website project is set to run with .NET Framework 4.0

Run the following powershell command in the package manager console of the Visual Studio solution for the target website:

    install-package Sitecore.Glimpse -Source <path>


Where `<path>` is the path to the  `artifacts\Packages\` folder that was produced by the build command.

Installing the package will do the following:

* Add a module into the ASP.NET pipeline
* Add a handler endpoint that by default be addressable via /Glimpse.axd
* A configuration section at the end of your web.config where the behaviour of the extension can be customised