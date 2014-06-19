#load "Api.Debug.csx"
#load "Core.Debug.csx"

bool overwriteComponentConfigs = true; // we have control over whether we inherit base component configurations or overwrite them

if(overwriteComponentConfigs)
{ //overwrite any component values here
	string websiteInfo = "From Web.Debug.csx";
	bool authenticationEnabled = false;

	apiConfiguration.Info                   = websiteInfo;
	coreConfiguration.Info                  = websiteInfo;
	apiConfiguration.AuthenticationEnabled  = authenticationEnabled;
	coreConfiguration.AuthenticationEnabled = authenticationEnabled;
}