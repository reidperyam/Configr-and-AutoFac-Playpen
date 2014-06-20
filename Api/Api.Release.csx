#r "Common.dll"

using Common;

ApiConfiguration apiConfiguration = new ApiConfiguration();// type is defined in Common

apiConfiguration.Info = "From Api.Release.csx";
apiConfiguration.AuthenticationEnabled = true;

Add("ApiConfiguration", apiConfiguration);