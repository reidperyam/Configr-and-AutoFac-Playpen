## ConfigrRepro

A .sln containing multiple, modular OWIN applications consuming a common configr .csx for the sake of reproducing unexpected functionality.

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
  The website project daisy chains the other two applications via OWIN.
  
      Website.csproj  : Website.WebsiteStartup (see reference from AssemblyInfo)
        |
        |__Api.csproj : Api.ApiStartup 
        |
        |__Core.csproj : Core.CoreStartup
        
  accessible via http://localhost:4444/api/info
                 http://localhost:4444/info
               
#### Configr Implementation
  Each of the projects, above, consumes a Configr/Scriptcs .csx configuration file which drives dependencyInjection type registration. The implementation simply chooses a .csx file based on DEBUG/RELEASE config.
  
  See 
  
  Configuration.csproj (Configr)
  DependencyInjection.csproj (AutoFac)


#### Configuration & DI Flow
```
On Start: Configuration.Configurator.cs ( loads .csx configuration )
  |
  | Depending on application context:
  |__> DependencyInjection.RegisterTypes.ForApi() (registers type bindings for self-hosted API)
  | or
  |__> DependencyInjection.RegisterTypes.ForCore() (registers type bindings for NancyFx)
  | or
  |__> DependencyInjection.RegisterTypes.ForWebsite() (registers type bindings for Website)
        |
        |
        |______\ OWIN Startup class:
               / Api.ApiStartup
                  or
                 Core.CoreStartup
                  or
                 Website.WebsiteStartup(calls ApiStartup, CoreStartup)
```
### Issues
Repro Steps For Encountered Issues:

  (Before proceeding verify successful build of ConfigrRepro.sln projects in DEBUG mode)
  
  Issue 1 : Configr .csx files required by Configuration.Configurator.cs (see above) are removed from output directory on application start.
  
      1a. The most consistent way to reproduce this is to execute the Unit tests from within Visual Studio.
          TEST -> WINDOWS -> TEST EXPLORER -> RUN ALL TESTS
      
      2a. These automated tests will intermittantly fail on startup as the dependant .csx files that drive their internals are removed
          from the AppDomain's base directory when the process starts (inexplicably).
          
      3a. If a single *.Test.csproj or the entire .sln is manually, explicitly rebuilt it will force said .csx file to 
          be once again copied to the output directory allowing tests to pass.
       
  Issue 2 : IIS will not load .csx files from the /bin/ directory. (manual repro because I didn't know how to write this test)
  
      1b. Within Visual Studio right-click the "Website" project and select "Set as StartUp Project".
      
      2b. Navigate to http://localhost:4444/ to invoke the hosted website.
      
      EXPECTED: The application to load dependant .csx file from the Website's bin directory just as other consumed dependancies
      RESULT: The application attempts to load its .csx file from the Website's base directory 
              Since the .csx is copied into the bin directory along with it's parent Configuration.dll, the Website cannot load it
              FileNotFoundException: Could not find file '...\ConfigrRepro\Website\devConfig.csx'.
      3b.
      Work around that does not work: copying the .csx to the base directory of the website solves this issue but creates another.
      The referenced, #r "Common.dll" assembly within the .csx cannot be loaded by the website's base directory and yields this error:
      
               'System.IO.FileNotFoundException' Assembly not found : Common.dll
      4b.
      At this point another work around might be to hard-code the location of this referenced assembly from within the .csx file ('#r bin/Common.dll')
      This however breaks the other, self-hosted console apps that consume the .csx file from their bin directories: Api.cspoj and Core.csproj.

            
##Questions: 
1. Is there an elegant work around that allows both the Website.csproj and other, shared consumers of the .csx files to load them?
2. Can Configr be enhanced to support loading from either /bin/ or base directory?
3. What process causes .csx files to be intermittantly purged on application startup?
4. Why does simply Building a .csproj not copy the dependant, "Copy Always" content to the application's execution directory?
           
           
           

