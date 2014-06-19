#load "bin/Api.Debug.csx"
#load "bin/Core.Debug.csx"

bool overwriteComponentConfigs = false; // we have control over whether we inherit base component configurations or overwrite them
bool discreteLifetimeScope     = true;
Add("DiscreteLifetimeScope", discreteLifetimeScope);

if(overwriteComponentConfigs)
{ //overwrite any component values here
	string websiteInfo = "From Web.Debug.csx";
	bool authenticationEnabled = false;

	apiConfiguration.Info                   = websiteInfo;
	coreConfiguration.Info                  = websiteInfo;
	apiConfiguration.AuthenticationEnabled  = authenticationEnabled;
	coreConfiguration.AuthenticationEnabled = authenticationEnabled;
}