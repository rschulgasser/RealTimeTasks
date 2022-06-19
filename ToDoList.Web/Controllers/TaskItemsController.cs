using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;

namespace ToDoList.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly IHubContext<TasksHub> _context;
        private string _connectionString;

        public TaskItemsController(IHubContext<TasksHub> context, IConfiguration configuration)
        {
            _context = context; 
            _connectionString = configuration.GetConnectionString("ConStr");
        }

    }
}
