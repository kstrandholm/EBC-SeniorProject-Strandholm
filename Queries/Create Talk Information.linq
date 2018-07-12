<Query Kind="Statements">
  <Connection>
    <ID>76e8a54c-d3ed-416f-bfe9-6df181e63e5f</ID>
    <Persist>true</Persist>
    <Server>tcp:pltnm-dev-testing.database.windows.net,1433</Server>
    <SqlSecurity>true</SqlSecurity>
    <Database>DCC_Kelly</Database>
    <UserName>kstrandholm</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA3OCcWARJzkS50yxLSIqc4wAAAAACAAAAAAADZgAAwAAAABAAAAALOYKQcsBuXwegTbHCzZymAAAAAASAAACgAAAAEAAAAF7ByM+uNJsferum3TUGYe8YAAAA1XVlJaeslvOOFC5so7OcA4c9IxO+PVhLFAAAAMylMsgdxIxK6UhK6Wv/40oTy2fL</Password>
    <DbVersion>Azure</DbVersion>
  </Connection>
  <NuGetReference>com.pltnm.package.linqpadlibrary</NuGetReference>
  <NuGetReference>com.pltnm.windows.commonlibraries</NuGetReference>
  <NuGetReference>com.pltnm.windows.mothermodel</NuGetReference>
  <Namespace>CommonLibs.Extensions</Namespace>
  <Namespace>CommonLibs.Misc</Namespace>
  <Namespace>LinqPadLibrary</Namespace>
  <Namespace>LinqPadLibrary.dba_Utilities</Namespace>
  <Namespace>LinqPadLibrary.dbe_Utilities</Namespace>
  <Namespace>LinqPadLibrary.Helper_Classes</Namespace>
  <Namespace>LinqPadLibrary.Statements</Namespace>
  <Namespace>LinqPadLibrary.Variable_Utilities</Namespace>
  <Namespace>MotherModel</Namespace>
  <Namespace>MotherModel.EntityFramework</Namespace>
  <Namespace>MotherModel.Models</Namespace>
  <Namespace>MotherModel.Models.Custom</Namespace>
  <Namespace>MotherModel.Models.Custom.Interfaces</Namespace>
  <Namespace>MotherModel.Models.Enums</Namespace>
  <Namespace>MotherModel.Models.Interfaces</Namespace>
  <Namespace>MotherModel.Models.Mappings</Namespace>
  <Namespace>MotherModel.Utilities</Namespace>
  <Namespace>MotherModel.Utilities.Enums</Namespace>
  <Namespace>PremiumCompute</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity.Core</Namespace>
  <Namespace>System.Data.Entity.Core.Common</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial</Namespace>
  <Namespace>System.Data.Entity.Core.Common.EntitySql</Namespace>
  <Namespace>System.Data.Entity.Core.EntityClient</Namespace>
  <Namespace>System.Data.Entity.Core.Mapping</Namespace>
  <Namespace>System.Data.Entity.Core.Metadata.Edm</Namespace>
  <Namespace>System.Data.Entity.Core.Objects</Namespace>
  <Namespace>System.Data.Entity.Core.Objects.DataClasses</Namespace>
  <Namespace>System.Data.Entity.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Annotations</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.DependencyResolution</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Design</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Interception</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.MappingViews</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Pluralization</Namespace>
  <Namespace>System.Data.Entity.Migrations</Namespace>
  <Namespace>System.Data.Entity.Migrations.Builders</Namespace>
  <Namespace>System.Data.Entity.Migrations.Design</Namespace>
  <Namespace>System.Data.Entity.Migrations.History</Namespace>
  <Namespace>System.Data.Entity.Migrations.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Migrations.Model</Namespace>
  <Namespace>System.Data.Entity.Migrations.Sql</Namespace>
  <Namespace>System.Data.Entity.Migrations.Utilities</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Configuration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
  <Namespace>System.Data.Entity.Spatial</Namespace>
  <Namespace>System.Data.Entity.SqlServer</Namespace>
  <Namespace>System.Data.Entity.SqlServer.Utilities</Namespace>
  <Namespace>System.Data.Entity.Utilities</Namespace>
  <Namespace>System.Data.Entity.Validation</Namespace>
</Query>

var diagnosticInfo = "Creating Talks";
var speakerIDs = from reg in Speakers
                    select new { reg.ID, reg.FirstName, reg.LastName };
var dateGiven = DateTime.Parse("12/20/2018");

var newTalkRecords = new List<Talks>
{
    new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Mike" && reg.LastName == "Cole").ID,
        Title = "The Trials And Tribulations Of Being A Fully Remote Developer",
        Summary = "Imagine working from home full-time. Your job choices are not limited geographically. You have a nice quiet workspace in your comfortable home with limited distractions. Lunch break in your easy chair. What's a dress code? You don't have to go outside in the morning during a frigid Iowa winter. Sounds perfect. Now imagine this actually happening to you and nothing goes to plan. How do you stay motivated? How do you deal with communication breakdowns? The feelings of isolation? Of feeling like a second rate employee of the company? In this presentation, Mike will review the tips and techniques he has learned over the past several years while being a full-time remote developer. This session is geared both towards developers and managers of remote development teams.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
    new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Nate" && reg.LastName == "Adams").ID,
        Title = "Your brain is broken and you suck at making decisions",
        Summary = "The human brain is really good at lots of things, but living and making decisions in our modern world typically isn't one of them. Learn about some of the ways our brain works less than optimally in decision making scenarios and how to stack the deck in favor of not totally messing things up. After exploring some of these concepts (they're features, not bugs, am I right?) we'll talk about why thinking in increments and iterations, and using empirical decision making can help us be more awesome.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Eugen" && reg.LastName == "Burianov").ID,
        Title = "Introduction to Blockchain",
        Summary = "If you want to know what Blockchain is, how it relates to crypto-currencies like Bitcoin and why people compare it to early days of the Internet then this session is for you. I will give an overview and practical ways to get started based on my own experience and share other resources if you want to dive deeper.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Spencer" && reg.LastName == "Herzberg").ID,
        Title = "Logging is not for Humans",
        Summary = "Stop logging for the humans, log for computers. When debugging issues or looking for anomalies, finding context and metadata in human readable logs is never easy. In this talk, I will show how logging for computers from your applications and servers will make your life easier and get you quicker to finding the things you are looking for.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Matthew" && reg.LastName == "Morrison").ID,
        Title = "Debate Me",
        Summary = "For about 15 years I have been developing software and in that time have settled on certain opinions and preferences based on my experiences. Confirmation bias and the bubble that I am in reinforce those opinions and preferences. In this talk audience participation is required. I will be presenting a list of my personal opinions and preferences and, for each one, will open up the floor for friendly debate. My hope is that I can learn something and at the same time impart some useful information to all who attend.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Daniel" && reg.LastName == "Juliano").ID,
        Title = "Beginner's Guide to Refactoring Code",
        Summary = "Imagine that you have just started working on a large established legacy code base with little test support. Documentation doesn't exist and the development culture has been of a â€˜git er done' mentality, so there are reams of bugs, unused code, copy pasta, and so on. When you are tasked to make enhancements to this code, it seems like anything you do will likely cause it to break. In this session we will take a codebase of questionable quality and walk through a series of refactorings. I will lay out a consistent approach that you can apply to such code, and we will cover the common refactorings that are available in Eclipse, Intellij, and Visual Studio. If you bring your laptop along, I will be using Eclipse and code I find on Github, so you should be able to follow the presentation as we go through it.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Greg" && reg.LastName == "Sohl").ID,
        Title = "A DSL for Your API",
        Summary = "Have you ever wanted to allow your users to be able to write scripts to execute actions within your application? Have you ever wondered how applications that do this accomplish it? Have you ever been sitting around with too much time on your hands and needed something interesting to think about? If so, then this talk is for you. During this talk we'll look at an app with a simple, easy to Grok API, and build up our own scripting language using the ANTLR4 Parser/Lexer generator, with which to drive it. All this, faster than you can say 'The Dragon Book'.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Luke" && reg.LastName == "Amdor").ID,
        Title = "'We'll do it live!': Monitoring and Debugging in Production",
        Summary = "That big 'P' word: Production. That new piece of shiny code you just wrote with a hundred percent test coverage goes ka-put once it's deployed once deployed there. What broken, and why? Sometimes the errors are a little more subtle, lying and growing there until you reach the right conditions. Either way, when users experience problems, it's not good. Maybe we need to check our assumptions a bit and figure out how to lower the risk if things go sideways. We'll go over my experience in a highly regulated industry to apply the OODA loop, continuous delivery, ownership, and observability to embrace failure to lower risk of production incidents.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Michael" && reg.LastName == "Liendo").ID,
        Title = "An Advocates Guide: You Just Got Hired. Now What?",
        Summary = "Great! You landed a new job and are now on a team of developers. Who is bringing you up to speed? What are you doing in your down time? What questions do you ask? This talk aims to demystify those questions, decrease imposter syndrome and give less experienced developers a fair chance at becoming leaders. Juniors to tech leads will find this talk valuable.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Scott" && reg.LastName == "Sauber").ID,
        Title = "ASP.NET Core 2 Fundamentals",
        Summary = "In this talk we'll go over the fundamentals of ASP.NET Core 2 and what you need to know to get started and be productive. We'll discuss the latest changes and improvements made in ASP.NET Core 2 over the 1.x versions including the brand new Razor Pages.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Brian" && reg.LastName == "Hogan").ID,
        Title = "Bash and Command Line for Developers",
        Summary = "Have you noticed all of the command line tools used in modern frameworks? There's a simple reason for that. The command line interface is incredibly powerful, once you understand how it works. With the CLI, you can manage your files, manipulate data, and automate your operating system, creating workflows that work for you.  And with the Windows Subsystem for Linux, Windows users have the same access to tools that Linux and Mac users have enjoyed for years. In this beginner-focused hands-on workshop, you'll get comfortable with the command line environment. You'll manage files and directories, learning shortcuts to create complex structures for your projects. Then you'll do a little network debugging. Then you'll search and replace text in files, learn how to redirect data to files or other programs, and finally write scripts to automate it all. When you're done you'll be able to incorporate your new skills into your daily workflow. Specific instructions will be sent to all registered attendees one week before the event to ensure that you can participate fully.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Dana" && reg.LastName == "Hart").ID,
        Title = "Finding Your Way to a Microservices Architecture",
        Summary = "We have all encountered those “wild” applications – those monolithic applications built years ago. It’s a mystery as to how they are still running and still processing today. These applications are only touched and deployed for bug fixes. That old deployment process alone makes you want to run and hide. How can we turn these wild beasts into tame creatures that are not scary and are more approachable? Well, by breaking down the monolith into micro units. Microservice architecture is a collection of independently deployable small services, each running a fine-grained process and having lightweight protocols. We’ll break down what that all means and how to see the forest through the trees to get from mono to micro.We’ll assess the path to take and discuss some useful tools you might need along the way to create this suite of services.As always, there are some pros and cons to cover.In the end, you should leave with a better impression on how to tear your monolith into microservices.Let’s make developing and deploying enjoyable again!",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Ken" && reg.LastName == "Sodemann").ID,
        Title = "Increasing Code Reuse Via Web Components",
        Summary = "Imagine spending a summer day spent hiking through the woods. The fresh air, the views; there's nothing better. Now imagine returning to the campsite to set up your things and start the campfire, only to realize that the tent isn't compatible with the sleeping bag, the firewood and matches don't mix, and the flashlight takes 9-volt batteries but you only packed AA. For too long now, this has been the reality of web development. With an overwhelming amount of frameworks to choose from, and even more choices to pick for libraries, most devs spend their time picking their tools than actually building awesome apps. What if there was a standard that all browser vendors agree on for creating reusable, encapsulated, and fast components? Enter Web Components. Shipping in almost every major browser today, Web Components aim to solve the age-old question of web development; 'What tool do I use?' We'll start off with the basics and dive into how adopting web components can speed up development, improve your code quality, and increase code reuse across your company.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Doc" && reg.LastName == "Norton").ID,
        Title = "The Technical Debt Trap",
        Summary = "Technical Debt has become a catch-all phrase for any code that needs to be re-worked. Much like Refactoring has become a catch-all phrase for any activity that involves changing code. These fundamental misunderstandings and comfortable yet mis-applied metaphors have resulted in a plethora of poor decisions. What is technical debt? What is not technical debt? Why should we care? What is the cost of misunderstanding? What do we do about it? Doc discusses the origins of the metaphor, what it means today, and how we properly identify and manage technical debt.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Benjamin" && reg.LastName == "Finkel").ID,
        Title = "Creating a Culture of Learning",
        Summary = "As the fast-paced world of IT continually changes around us we're constantly faced with an uphill battle:  Either learn or be left behind. Establishing a 'culture of learning', one that exists both within ourselves as well as in our personal and professional lives, is a critical part of creating a path up that learning slope. While most of us enjoy the process of leaning new skills and technologies, we need something more formal and proactive in our professional careers.  Drawing on over 20 years of professional experience, including the past four as an IT educator and adviser, this session aims to help us understand how we can create a culture that doesn't just foster and encourage learning but actively directs towards successful goals. We'll discuss why we learn and how to learn, and what we should aim to learn about. We'll also talk about specific activities and policies for engaging our employers and the organizations we're a part of to make our learning experiences consistent, enjoyable, applicable, and successful.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Duane" && reg.LastName == "Newman").ID,
        Title = "Adaptive User Interfaces with Xamarin.Forms",
        Summary = "Let's take Xamarin.Forms beyond Hello World and explore how to create apps that adapt to your users devices in the real-world. As an app developer, you don't want your apps to just look great in portrait or landscape on a phone, you want to provide a larger display experience option. In this session we will explore how to build an app that spans platforms and device types, giving users a rich experience, regardless of platform or form factor. Stop making ugly one-layout-fits-all UI apps, and start making single code-base apps your users love! Everybody wins!",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Jilea" && reg.LastName == "Hemmings").ID,
        Title = "Dare To Fail Up",
        Summary = "Dare to Fail - Growing up, my parents engineered in my brain the importance of striving for success and minimizing failures. Why?  It's only through failure that we can find out what we're truly made of and most successful entrepreneurs unfortunately, don't talk about the mistakes they've made before becoming successful. Most successful entrepreneurs don't explain their true story of mishaps, misfortunes, losses, depression or just a good old fashion crying it out session. During my discussion, I will explain how I failed in my first entrepreneurship venture and how that failure lead me to a new path of success. We should not be afraid to fail, because honestly 'Fear Kills More Dreams, Than Failure.' Some Entrepreneurs don't even try to make an attempt in building their businesses, because they are so afraid of failing. By the end of my session, the hesitation of being afraid to fail, will come to a complete halt. Join me and 'Dare To Fail.'",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Brian" && reg.LastName == "Hogan").ID,
        Title = "Automate Production Deployments with Terraform and Ansible",
        Summary = "You've built your app, and now you need to get it in front of people. But setting everything up by hand is tedious. In this talk, you'll see how to build a load-balanced web server using code.   We'll explore how to use Terraform to define and set up a cloud server, and then use Ansible to install the server software and upload the web site. Then we'll modify our infrastructure code to scale out our solution with a load balancer. We'll do all of this without ever directly logging in to the server. You'll get a taste of immutable infrastructure, and we'll discuss the benefits and limitations of this approach. Best of all, you'll be able to put this technique to use right away, because when we're done, you'll have scripts you can run to set up your own environment.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    },
     new Talks
    {        SpeakerID = speakerIDs.Single(reg => reg.FirstName == "Nicholas" && reg.LastName == "Jaeger").ID,
        Title = "Launching with Now, Next, and React",
        Summary = "This year we are seeing a continuing trend of React dominate the JavaScript scene. In this session, we're going to focus on what's hot this year - from the popularity and advancements React made last year, to server-rendered React applications with Next.js this year, and wrapping up with an overview of using Now, the command line interface, to perform instant immutable deployments of your app to the cloud. This will be a high-level talk aimed at beginners and seasoned early adopters alike! We'll go over high-level steps to use these together when launching apps, as well as some of the decision-making points you'll want to know about before adopting these technologies.",
        DateGiven = dateGiven,
        UpdateTime = DateTime.Now,
        DiagnosticInformation = diagnosticInfo
    }
};

//newTalkRecords.Dump();

if (true)
{
    Talks.InsertAllOnSubmit(newTalkRecords);
    SubmitChanges();
}

Talks.Dump();