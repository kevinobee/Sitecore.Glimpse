# Sitecore.Glimpse

Sitecore.Glimpse is ...


## Instructions for Use

This is an early proof of concept and as such packages have not been pushed to NuGet.org

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

TODO


### Uninstalling the Package

Run the following commands:

    uninstall-package Sitecore.Glimpse




### Tools

POSTMAN is a powerful HTTP client that runs as a Chrome browser extension and greatly helps with testing test REST web services. Find out more <http://www.getpostman.com/> 

References:

http://curl.haxx.se/docs/httpscripting.html - see section 4.3 File Upload POST 


## Contributing to the Project

If you are interested in contributing to the growth and development of Sitecore.Ship in even a small way, please read the notes below.

The project can be built and tested from the command line by entering:

    .\build

Please ensure that there are no compilation or test failures and no code analysis warnings are being reported.

### Running the Smoke Tests

The `build.proj` file contains a set of smoke tests to verify that the Sitecore.Glimpse features all run successfully when the package has been installed in a Sitecore website.

In order to run these smoke tests you will need to:

* Have a local install of Sitecore 6.6.

* Set the *TestWebsitePath* and *TestWebsiteUrl* in the **build\Sitecore.Glimpse.environment.props** to reference the local Sitecore 6.6 website.

* Ensure that the test website has the Ship package installed by running the following in the Package Manager Console:

    install-package Sitecore.Glimpse -Source `<path to folder containing your sitecore.glimpse nupkg file>`

You can then run the smoke tests by entering:

    .\build RunSmokeTests

A series of curl commands fire off HTTP requests to the Sitecore.Ship service routes and the results are printed out to the console. Each of these commands should execute successfully before you send a pull request back to the main project.

Your participation in the project is very much welcomed.