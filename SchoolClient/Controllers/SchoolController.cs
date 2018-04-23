using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SchoolClient.Models;

namespace SchoolClient.Controllers
{
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {
        private readonly ILogger<SchoolController> _logger;
        private readonly IConfiguration _configurationRoot;


        public SchoolController(ILogger<SchoolController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configurationRoot = configuration;
        }

        // GET api/values
        [HttpGet("students")]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            var apiClient = new ApiClient(_configurationRoot, _logger);

            await apiClient.Initialize();

            var students = await apiClient.GetStudents();

            return students;
        }
    }
}
