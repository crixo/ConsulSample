using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Infrastructure;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers.API
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly DataStore _dataStore;

        public StudentsController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (_dataStore.Students != null)
                return Ok(_dataStore.Students);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _dataStore.Students.SingleOrDefault(c => c.ID == id);
            if (student != null)
            {
                return Ok(student);
            }

            return NotFound();
        }
    }
}
