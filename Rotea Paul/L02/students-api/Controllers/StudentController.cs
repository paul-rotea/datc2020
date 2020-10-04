using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace students_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        
        [HttpGet]
        public IEnumerable<Student> Get()
        {
           return StudentRepo.Students;
        }
    }
}
