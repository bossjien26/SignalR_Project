using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SignalRClient_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        public ClientService clientService = new ClientService();

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("MailAPI/{mes}")]
        public async Task<string> Get(string mes)
        {
            Console.WriteLine(mes);
            string message = await clientService.Call("Group1", "client1_", "client_");
            string groupNum = message.Replace("client1_", "");
            Console.WriteLine("Group_" + groupNum);
            message = await clientService.Call("Group_" + groupNum, "client1_", mes);

            return message;
        }
    }
}