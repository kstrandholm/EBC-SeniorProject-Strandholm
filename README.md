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
Working with this project will require knowlege about how to use WPF, bindings, user controls,
the MVVM pattern, commanding, and how Prism connects the UI views with the logic in
the view models using Region Managers.

### Nuget Packages

The Downloader implements Logging in the form of Serilog and file parsing in the form of CsvHealper.
It also uses SSH.NET to create a connection to the 3rd party's FTP site, and implements Linq's DataContext 
class to create a database connection.
UI is done in WPF with the help of Prism.


### Understanding Prism

Arguably the hardest part about working with this code base is understanding the implementation of
the Prism Nuget package in the projects with a UI.

As defined on the github page:
> Prism is a framework for building loosely coupled, maintainable, and testable XAML applications

The main focus of Prism WPF is to make implementing the MVVM pattern easier and more understandable.
It uses View Model Locators that can automatically determine which view model is associated with which view,
based on the naming convention of __*[ViewName]*ViewModel.cs__.  Prism also allows convenient control
over the UI by using Region Managers that pass views back and forth, basically like a mini window inside
of a window, similar to the idea of an iFrame in html.  To make property binding easier, Prism automatically
implements the INotifyPropertyChanged interface, removing the need to manually call PropertyChanged on
every property connected to the UI.  Additionally, Prism includes Events that can be
published and subscribed to, which enable passing information and status updates between views.  Finally,
Prism takes the Command concept and enhances that as well by using Delegate Commands that can automatically
determine if a command can be executed or not.

Please note that this is just an overview of the Prism features this solution implements and is not
a substitute for reading the Prism documentation to get a good understanding of how it works.

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
