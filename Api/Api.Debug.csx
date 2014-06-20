#r "Common.dll"

using Common;

ApiConfiguration apiConfiguration = new ApiConfiguration();// type is defined in Common

apiConfiguration.Info = "From Api.Debug.csx";
apiConfiguration.AuthenticationEnabled = false;

Add("ApiConfiguration", apiConfiguration);