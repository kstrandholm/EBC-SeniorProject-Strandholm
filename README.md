# Dubuque Code Camp Manager
> Managing your code camp

## Getting started

The manager has been broken down into 3 main projects:

- Downloader gets the latest data from the 3rd party

- Scheduler creates a proposed schedule

- Registration enables on-site registration in the form of a kiosk window

### Initial Configuration

#### Downloader

Document the database connection string and the FTP site information in the app.config file

#### Scheduler

Document the database connection string in the app.config file

#### Registration

Document the database connection string in the app.config file

## Developing

### Important!

This project has a few Nuget packages that rely on other now-outdated Nuget packages.  
If a package has not been updated, do not update it without having a rollback plan.

### Basics

This project uses WPF to create the UI and C# for the logic, using the MVVM pattern to separate
the UI from the logic and from the data models.  Additionally, it implements Prism and Unity to
enable easier to understand and more concise code, as well as better control over the UI.

Working on this project will require knowlege about how to use WPF, bindings, user controls,
the MVVM pattern, commanding, and how Prism and Unity connect the UI views with the logic in
the view models using Region Managers.

### Nuget Packages

The Downloader implements Logging in the form of Serilog and file parsing in the form of CsvHealper.  
It also uses SSH.NET to create a connection to the 3rd party's FTP site, and implements Linq's DataContext 
class to create a database connection.

UI is done in WPF with the help of Prism.


### Understanding Prism

Arguably the hardest part about working with this code base is understanding the implementation of
of the Prism Nuget package in the projects with a UI.  This solution also uses Unity as a controller
that manages dependency injections.



## Features

### Downloader

Get and save the registrant information from the 3rd party!

### Scheduler

Add Sessions and Talks, then create the proposed schedule, all from the same window!

### Registration

Enable on-site registration with the option to select interesting talks!

## Links



- Unity: https://github.com/unitycontainer/unity
- Prism: https://github.com/PrismLibrary/Prism
- Serilog: https://github.com/serilog/serilog


## Licensing

One really important part: Give your project a proper license. Here you should
state what the license is and how to find the text version of the license.
Something like:

"The code in this project is licensed under MIT license."
