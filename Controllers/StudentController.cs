/*using System;
using System.Net;*/
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Org.BouncyCastle.Crypto.Engines;
using web_api.Data;
// using web_api.Logging;
using web_api.Model;

namespace web_api.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
	public class StudentController : ControllerBase
	{
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDbContext _dbContext;
        private readonly IMapper _mapper;

        public StudentController(ILogger<StudentController> logger, CollegeDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

		[HttpGet] //this is the verb attribute
        [Route("All", Name = "GetAllStudents")] //this is the route attribute
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async  Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
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
            
            //Get students
            // var students = await _dbContext.Students.ToListAsync();

            //If you want to return just particular columns, use StudentDTO
            //using Linq query

            // var students = await _dbContext.Students.ToListAsync();
            return await _dbContext.Students.ProjectTo<StudentDTO>(_mapper.ConfigurationProvider).ToListAsync();

            // var studentDtoData = _mapper.Map<List<StudentDTO>>(students);
            
            //With AutoMapper with don't need code below
            // var students = await _dbContext.Students.Select(s => new StudentDTO()
            // {
            //     Id = s.Id,
            //     StudentName = s.StudentName,
            //     Address = s.Address,
            //     Email = s.Email,
            //     DOB = s.DOB
            // }).ToListAsync();  
            
            

            //Ok - 200 Success
            // return Ok(studentDtoData);
		}

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentDTO>> GetStudentByIdAsync(int id)
        {
            //BadRequest - 400 - client error
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
             

            var student = await _dbContext.Students.Where(n => n.Id == id)
                // .ProjectTo<StudentDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            //NotFound - 404 - NotFound - client error
            if (student == null)
            {
                _logger.LogError("Student not found with given Id");
                return NotFound($"Student with id {id} not found!");
            }

            var studentDto = _mapper.Map<StudentDTO>(student);

            //Ok - 200 Success
            return Ok(studentDto);
        }

        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(201)] //RecordCreated Status code
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody]StudentDTO dto)
        {
            if (dto == null)
                return BadRequest();

            // First way of using Custom validation
            //if (model.AdmissionDate < DateTime.Now)
            //{
            //    ModelState.AddModelError("AdmissionDate Error","Admission date must be greater than or equal to today's date");
            //    return BadRequest(ModelState);
            //}

            //Second way is using attribute
            //create object for student
            Student student = _mapper.Map<Student>(dto);
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            dto.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new { id = dto.Id }, dto);
            //return Ok(model);
        }
 
        [HttpPut]
        [Route("Update")]
        ////api/student/update
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentDTO>> UpdateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                BadRequest();

            var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == dto.Id).FirstOrDefaultAsync();

            if (existingStudent == null)
                return NotFound();

            var newRecord = _mapper.Map<Student>(dto);
            
            _dbContext.Students.Update(newRecord);

            // existingStudent.StudentName = model.StudentName;
            // existingStudent.Email = model.Email;
            // existingStudent.Address = model.Address;
            // existingStudent.DOB = model.DOB;
            
            await _dbContext.SaveChangesAsync();

            //return Ok(existingStudent);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}UpdatePartial")]
        ////api/student/1/UpdateStudentPartial
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateStudentPartialAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == id).FirstOrDefaultAsync();

            if (existingStudent == null)
                return NotFound();

            var studentDto = _mapper.Map<StudentDTO>(existingStudent); 

            patchDocument.ApplyTo(studentDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent = _mapper.Map(studentDto, existingStudent);

            _dbContext.Students.Update(existingStudent);
    
            await _dbContext.SaveChangesAsync();
            
            // existingStudent = _mapper.Map<Student>(studentDto);
            
            /*await _dbContext.UpdateAsync(existingStudent);*/
            
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        //This is also fine without using Route
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentDTO>> GetStudentByNameAsync(string name)
        {
            //BadRequest - 400 - client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = await _dbContext.Students.Where(n => n.StudentName == name).FirstOrDefaultAsync();

            //NotFound - 404 - NotFound client error
            if (student == null)
                return NotFound($"Student with name {name} not found");

            var studentDto = _mapper.Map<StudentDTO>(student);

            return Ok(studentDto);
        }

        [HttpDelete]
        [Route("{id:int}", Name = "DeleteStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<bool>> DeleteStudentByIdAsync(int id)
        {
            //BadRequest - 400 - client error
            if (id <= 0)
                return BadRequest();

            var student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();

            //NotFound - 404 - NotFound - client error
            if (student == null)
                return NotFound($"Student with id {id} not found!");

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return Ok(true);
        }
    }
}

