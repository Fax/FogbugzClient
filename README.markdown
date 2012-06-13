# Fogbugz client

A simple client implemented in C# that access Fogbugz by allowing you to send command objects to a library.

## A simple example

    var client = new FogbugzClient("https://mybugtracer.fogbugz.com/api.asp");
    client.Logon("user", "password");
    var command = new ListFiltersCommand();
    var xml = client.ExecuteCommand(command);

    // or you can pass a Func to ExecuteCommand that will convert an XDocument to another type
    // ListFiltersCommand implements one for you

    var filterList = client.ExecuteCommand(command, command.CreateFilterList);
