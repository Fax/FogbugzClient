# Fogbugz client

A simple client implemented in C# that accesses Fogbugz by allowing you to send command objects to a client object.

## A simple example

``` C#
var client = new FogbugzClient("https://mybugtracer.fogbugz.com/api.asp");
client.Logon("user", "password");
var command = new ListFiltersCommand();
var xml = client.ExecuteCommand(command);

// or you can pass a Func to ExecuteCommand that will convert an XDocument to another type
// ListFiltersCommand implements one for you
var filterList = client.ExecuteCommand(command, command.CreateFilterList);
```

## LICENSE

FogbugzClient is copyright Fourth Hospitality 2012 and is released under the MIT license:

* http://www.opensource.org/licenses/MIT