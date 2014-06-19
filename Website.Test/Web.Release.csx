#load "Api.Release.csx"
#load "Core.Release.csx"

bool overwriteComponentConfigs = false; // we have control over whether we inherit base component configurations or overwrite them

if(overwriteComponentConfigs)
{ //overwrite any component values here
	string websiteInfo = "From Web.Release.csx";
	bool authenticationEnabled = true;

	apiConfiguration.Info                   = websiteInfo;
	coreConfiguration.Info                  = websiteInfo;
	apiConfiguration.AuthenticationEnabled  = authenticationEnabled;
	coreConfiguration.AuthenticationEnabled = authenticationEnabled;
}

