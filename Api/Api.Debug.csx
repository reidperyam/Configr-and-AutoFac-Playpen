#r "Common.dll"

using Common;

Configuration apiConfiguration = new Configuration();// type is defined in Common

apiConfiguration.Info = "From Api.Debug.csx";
apiConfiguration.AuthenticationEnabled = false;

Add("ApiConfiguration", apiConfiguration);