## Configr-and-AutoFac-Playpen

A .sln containing multiple, modular OWIN applications using ConfigR for dynamic configuration and IoC registration; serves as a conceptual model for the author to exercises modularity, abstraction and simplicity in a sandbox environment.
Proof of viability here can then be modeled in other codebases.

### Overview of Projects
#### API
  Web API 
  
  The API .csproj can either be executed as an OWIN self-hosted console application (in VS right-click & "Set as StartUp Project") or
  as OWIN middleware of an IIS web application via Website.csproj.
  
  When self-hosting (right-click, "Set as StartUp Project") is accessible via http://localhost:4445/api/info

#### Core
  NancyFx
  
  The Core .csproj can either be executed as an OWIN self-hosted console application (in VS right-click & "Set as StartUp Project") or
  as OWIN middleware of an IIS web application via Website.csproj.
  
  When self-hosting (right-click, "Set as StartUp Project") is accessible via http://localhost:4446/info

#### Website
  The website project daisy-chains the other two applications via OWIN.
  
      Website.csproj  :  Website.WebsiteStartup (see reference from AssemblyInfo)
        | 
        |__Api.csproj :  Api.ApiStartup 
        | 
        |__Core.csproj : Core.CoreStartup
        
  accessible via http://localhost:4444/api/info
                 http://localhost:4444/info
               
#### Configr Implementation
  Each of the projects, above, consumes a Configr/Scriptcs .csx configuration file which drives dependencyInjection type registration. Depending on the exeuction context of the application (self-hosted or a component of the Website) configuration settings can be inherited and overridden.
  
#### AutoFac Implementation
The .sln OWIN entrypoints consume configR .csx configurations in registering types and instances with AutoFac container(s) and strategy. From here the OWIN component applications execute, having been assigned their runtime configurations and type bindings.

            
##Questions: 
1. How to test the Website.csproj while using its own .csx configuration files?

Regarding the whitebox test, what I would do is actually spin up the real website. This is usually best done using a build script. In my day job we use Rake for this kind of thing (with an eye on switching to https://github.com/bau-build/bau in the future). We have an 'accept' task which runs the acceptance tests and takes a dependency on a 'start' task which starts the app. The 'accept' task ensures that the 'stop' task is invoked before exiting. For running tests in VS, I execut rake start from the console, run my tests in VS and then execute rake stop in the console. For debugging tests, I usually start the app in one instance of VS and then run the acceptance tests in another. With this approach the website is a real running instance of the website which uses its config in the normal manner. This, of course, isn't the only possible approach.
Regarding the loading of the nested config file, using #load foo2.csx is very different to using LoadScriptFile("foo2.csx");.
''''#load'''' is built into scriptcs itself and effectively it combines the contents of foo1.csx and foo2.csx into one file. This is why you are able to access local variables from foo2.csx in foo1.csx.
LoadScriptFile() on the other hand, is a ConfigR concept. It will execute the specified file, the config it defines will be loaded, and then exits the method, unloading the specified file. This means foo1.csx would have no knowledge of the code inside foo2.csx. What this allows is 'cascading config'. I.e. I define some config in a central file and then I override that config locally. The way it works is that the values in the outermost file always override the values in the inner files which are loaded with a chain of LoadScriptFile(). Note, the rules for how the cascading config works are under scrutiny and may be changed, see #141.
You may find that using #load is good enough for you. In fact, I'm now considering deprecating LoadScriptFile() altogether and relying on #load instead as it's more intuitive once you understand how #load works. I'd probably have to override the handling of ''''#load'''' to allow it work with HTTP endpoints to replace LoadWebScript() but that's easy enough to do.
As an aside, I'm also considering deprecating Add() in favour of Set() (just a name change) as I think it conveys the semantics better. Each call to Add() does not in fact add the item, it just sets the value of that item, overriding it if previously set.
Donations are always gladly accepted! That's very kind of you. My PayPal account is registered under adam@adamralph.com. Is that all the info you need for it?
—

2. Is this ConfigR functionality really intended? http://goo.gl/lDFiSC
           
When you add the object to config, ConfigR doesn't take a copy of the object. It's still the same object, so if you maintain a reference to it, as you do in your foo variable, and then change a property, you are changing the same object that is stored in the config.
I recieved the payment, thank you very much! It will be put to good use tonight when I go and watch the England game :wink:. In fact, it did inspire me to register the configr.org domain, so when I eventually get a website up and point that domain at it, it will be result of your donation. Thanks again.           

