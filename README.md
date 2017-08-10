# Demo: WebApi: People Repository
A little demo of some WebAPI 2 Best Practices, Using Swashbuckle to make Swagger, Some nice error handling, and using SQLLite with a little POCO Class. 

## How to build

In Visual Studio 2017, build, and then run

## Things to notice

### General structure best practices

- In Visual Studio I started with an empty web project and added WebAPI for a minimal amount of stuff I did not want
- I then added some useful NuGet packages (See: `packages.config`):
	- Swashbuckle - for swagger generation
	- CORS so I can call the API from anywhere (For demo purposes)
	- Newtonsoft.Json - because it's handy
- I put the repository code in it's own assembly and made unit tests for it, that will allow me to re-use the repository for other demos. Sqlite is nice because it creates the file for the db3 DB if it does not exist, and adding a "table" using POCO classes decorated with a few attributes is great. Take a look at the `Models.Person` class to see
- Building your own facade over data access with DTO or simple parameter contracts is nice as well. 
- Speaking of DTOs, a handy trick is to override the `ToString()` method to provide yourself a nice way of seeing the entity for logging/debugging, you may want to consider overriding `Equals` and other bases as well.
- Notice all assemblies use the same base `GlobalAssemblyInfo.cs` linked into each project, this allows them to share a bunch of attributes and is a best practice
- Lastly, things that implement `IDisposable` should be in a `using` block

## WebAPI 2 Specific niceties

### XML Code Comments to make Swagger Better

- Turn on XML code comments 
	- change the output to a named file in your root directory
	- make sure it gets copied-always to the output folder
- tweak your swagger configuration to use XML comments (see example: `~\Demo.People.WebAPI\App_Start\SwaggerConfig.cs`)
	- Think hard about the other swagger configuration settings

### Do not use the default routes and decorate your methods

- Look in `~\Demo.People.WebAPI\App_Start\WebApiConfig.cs` and see that I have removed the default route, it is IMHO, evil
- Instead notice that each controller has a `RoutePrefix` and each method has an explicit `Route`
- Always specify the `[Http{methood}]`
- Always supply a `[ResponseType(typeof({my type goes here})]` so that the generated swagger will document the type returned
	- Especially important, if like me you use the preferred return method type of `IHttpActionResult`
- Put useful XML code comments on each controller and method it will help tremendously

### Other WebAPI start up considerations

- Put in CORS in your start up (in in web.config) if your API needs to be called cross origin
- Consider a global error handler (see: `~\Demo.People.WebAPI\Library\GlobalExceptionHandler.cs`), and notice that in the WebAPI start up you are REPLACING the existing one
	- You can play with it using the WebAPI test methods (see: `~\Demo.People.WebAPI\Controllers\ErrorSimulatorController.cs`)
- Consider a global error trace as well  (see: `~\Demo.People.WebAPI\Library\TraceExceptionLogger.cs`), notice this is ADDED
- `System.Diagnostics` is your friend, start using that instead of some fancy logging framework to start with, there are a ton of community trace listeners, and any management framework that understand the Windows O/S will know how to capture it's output

### Always provide a way to query your APIs version

- See the version controller for an idea
- The DTO returned breaks the version up numerically and provide the composed string as well, and the copyright. This allows automated checking of version
- As for versioning, please use Semantic Versioning (http://semver.org/) for your API, in the case of .NET Major.Minor.Build.Private where 
	- a change to the Major number indicates a **BREAKING Change**
	- a change to the Minor number is a release that is non-breaking
	- a change to Build or Private is a new version, always make the numbers ascending!


## HTTP vs. REST vs. ...

To REST or not, up to you. In the case of this demo, as it is not REST-IVE, it is RPC over HTTP-ish, I have not done so, but you could easily change the WebAPI contracts to make this look more like REST.

BTW: I hate separate insert vs. update statements, I am an **UPSERT** guy if the thing is an entity and idempotent. 

## Side Observations

- Sqlite is very nice for demos as it is server-less and small
- The NuGet package https://www.nuget.org/packages/sqlite-net-pcl make working with SQLite very easy
- A shout out to the genius who made switching on types easy: http://stackoverflow.com/questions/11277036/typeswitching-in-c-sharp (see: `~\Demo.People.WebAPI\Library\TypeSwitch.cs`)
- Lastly, why does .NET not have a generic validation exception? I fixed it (see: `~\Demo.People.WebAPI\Library\ValidationException.cs`)

## YouTube Channel

https://www.youtube.com/user/spookdejur1962/videos 

## GitHub

https://github.com/BlitzkriegSoftware

## LinkedIn

https://www.linkedin.com/in/stuart-williams-81555a1