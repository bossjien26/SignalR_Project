// See https://aka.ms/new-console-template for more information
using SignalClient;

// Console.WriteLine("Hello, World!");
ClientService clientService = new ClientService();
await clientService.Call("Group1");