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

This project uses WPF to create the UI and C# for the logic, using the MVVM pattern to separate
the UI from the logic and from the data models.  Additionally, it implements Prism and Unity to
enable easier to understand and more concise code, as well as better control over the UI.

Working on this project will require knowlege about how to use WPF, bindings, user controls,
the MVVM pattern, commanding, and how Prism and Unity connect the UI views with the logic in
the view models.



## Features

### Downloader

Get and save the registrant information from the 3rd party!

### Scheduler

Add Sessions and Talks, then create the proposed schedule, all from the same window!

### Registration

Enable on-site registration with the option to select interesting talks!

## Links



- Project homepage: https://your.github.com/awesome-project/
- Repository: https://github.com/your/awesome-project/
- Issue tracker: https://github.com/your/awesome-project/issues
  - In case of sensitive bugs like security vulnerabilities, please contact
    my@email.com directly instead of using issue tracker. We value your effort
    to improve the security and privacy of this project!
- Related projects:
  - Your other project: https://github.com/your/other-project/
  - Someone else's project: https://github.com/someones/awesome-project/


## Licensing

One really important part: Give your project a proper license. Here you should
state what the license is and how to find the text version of the license.
Something like:

"The code in this project is licensed under MIT license."
