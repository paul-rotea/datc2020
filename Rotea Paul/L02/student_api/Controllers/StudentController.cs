using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace student_api.Controllers
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

         [HttpGet("{id}")] // [HttpGet("students/{id}")]
         public Student Get(int id){
             return StudentRepo.Students.FirstOrDefault(s => s.Id == id);
         }
    }
}
