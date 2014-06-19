## Configr-and-AutoFac-Playpen

A .sln containing multiple, modular OWIN applications using ConfigR for dynamic configuration and IoC registration.

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
2. Is this ConfigR functionality really intended? http://goo.gl/lDFiSC
           
           

