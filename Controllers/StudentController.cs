using System;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using web_api.Logging;
using web_api.Model;

namespace web_api.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
	public class StudentController : ControllerBase
	{
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

		[HttpGet] //this is the verb attribute
        [Route("All", Name = "GetAllStudents")] //this is the route attribute
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
		{
            _logger.LogInformation("GetStudents method started");
               
            //var students = new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Address = item.Address,
            //        Email = item.Email
            //    };
            //    students.Add(obj);
            //}

            //using Linq query
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email
            });

            //Ok - 200 Success
            return Ok(students);
		}

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            //BadRequest - 400 - client error
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
             

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            //NotFound - 404 - NotFound - client error
            if (student == null)
            {
                _logger.LogError("Student not found with given Id");
                return NotFound($"Student with id {id} not found!");
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };

            //Ok - 200 Success
            return Ok(student);
        }

        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(201)] //RecordCreated statuscode
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null)
                return BadRequest();

            // First way of using Custom validation
            //if (model.AdmissionDate < DateTime.Now)
            //{
            //    ModelState.AddModelError("AdmissionDate Error","Admission date must be greater than or equal to todays date");
            //    return BadRequest(ModelState);
            //}

            //Second way is using attribute

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };
            CollegeRepository.Students.Add(student);

            model.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            //return Ok(model);
        }

        [HttpPut]
        [Route("Update")]
        ////api/student/update
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                BadRequest();

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;

            //return Ok(existingStudent);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}UpdatePartial")]
        ////api/student/1/updatepartial
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;

            //return Ok(existingStudent
            //
            // 204 - NoContent
            return NoContent();
        }

        //This is also fine without using Route
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            //BadRequest - 400 - client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();

            //NotFound - 404 - NotFound client error
            if (student == null)
                return NotFound($"Student with name {name} not found");

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };

            return Ok(student);
        }

        [HttpDelete]
        [Route("{id:int}", Name = "DeleteStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<bool> DeleteStudentById(int id)
        {
            //BadRequest - 400 - client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            //NotFound - 404 - NotFound - client error
            if (student == null)
                return NotFound($"Student with id {id} not found!");

            CollegeRepository.Students.Remove(student);

            return Ok(true);
        }
    }
}

