# Sitecore.Glimpse

Sitecore.Glimpse is an Glimpse extension that provides runtime web diagnostics for Sitecore sites.

This project includes the following NuGet packages:

* Sitecore.Glimpse
* Sitecore.Glimpse.Mvc3 (extension for those wanting to use Sitecore's MVC capability)
* Sitecore.Glimpse.Mvc4 (supports the ASP.NET MVC4 release)

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


### Uninstalling the Package

Run the following commands:

    uninstall-package Sitecore.Glimpse



## Contributing to the Project

If you are interested in contributing to the growth and development of Sitecore.Glimpse in even a small way, please read the notes below.

The project can be built and tested from the command line by entering:

    .\build

Please ensure that there are no compilation or test failures and no code analysis warnings are being reported.

### Running the Smoke Tests

The `build.proj` file contains a set of smoke tests to verify that the Sitecore.Glimpse features all run successfully when the package has been installed in a Sitecore website.

In order to run these smoke tests you will need to:

* Have a local install of Sitecore.

* Set the *TestWebsitePath* and *TestWebsiteUrl* in the **build\environment.props** to reference the local Sitecore website.

* Ensure that the test website has the Sitecore.Glimpse package installed by running the following in the Package Manager Console:

    install-package Sitecore.Glimpse -Source `<path to folder containing your sitecore.glimpse nupkg file>`

You can then run the smoke tests by entering:

    .\build RunSmokeTests

Curl commands will fire off HTTP requests to the test website to verify that the site is still responding as intended. These commands should execute successfully before you send a pull request back to the main project.

Your participation in the project is very much welcomed.