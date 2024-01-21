// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using SignalClient;

ClientService clientService = new ClientService();
await clientService.Call();