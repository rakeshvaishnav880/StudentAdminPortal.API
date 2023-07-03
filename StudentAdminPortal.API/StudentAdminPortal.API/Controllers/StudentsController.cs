using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;
using Student = StudentAdminPortal.API.DomainModels.Student;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper,IImageRepository imageRepository)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }
        
        [HttpGet("get-studentByNameFromSp")]
        public async Task<IActionResult> GetStudentsFromSpAsync(string FirstName)
        {
            var students = await studentRepository.GetStudentsFromSpAsync(FirstName);           
            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet("get-allstudentByJoins")]
        public async Task<IActionResult> GetallStudentsByJoins()
        {
            var students = await studentRepository.GetStudentsByJoinsAsync();
            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet("get-allstudents")]       
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetStudentsAsync();
            
            //var domainModelStudents = mapper.Map<List<Student>>(students);
            //var domainModelStudents = new List<Student>();
            //foreach (var student in students)
            //{
            //    domainModelStudents.Add(new Student()
            //    {
            //        Id = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName,
            //        DateOfBirth = student.DateOfBirth,
            //        Email = student.Email,
            //        Mobile = student.Mobile,
            //        ProfileImageUrl = student.ProfileImageUrl,
            //        GenderId = student.GenderId,
            //        Address = new Address()
            //        {
            //            Id = student.Address.Id,
            //            PhysicalAddress = student.Address.PhysicalAddress,
            //            PostalAddress = student.Address.PostalAddress,
            //        },
            //        Gender = new Gender()
            //        {
            //            Id = student.Gender.Id,
            //            Discription = student.Gender.Discription
            //        }
            //    });
            //}
            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet("get-student/{id}"),ActionName("GetStudentAsync")]       
        public async Task<IActionResult> GetStudentAsync([FromRoute]int id)
        {
            var student = await studentRepository.GetStudentAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));

        }

        [HttpPut("update-studentByid/{id}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute]int id,[FromBody]UpdateStudentRequest student)
        {
            if(await studentRepository.Exists(id))
            {
               var updatedstudent = await studentRepository.UpdateStudentById(id, mapper.Map<DataModels.Student>(student));
                if (updatedstudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedstudent));
                }
            }           
                return NotFound();
        }

        [HttpDelete("delete-studentByid/{id}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] int id)
        {
            if (await studentRepository.Exists(id))
            {
                var student = await studentRepository.DeleteStudentAsync(id);
                return Ok(mapper.Map<Student>(student));
            }
            return NotFound();
        }

        [HttpPost("add-newstudent")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            //Add new student service call
            var newstudent = 
                await studentRepository.AddStudentAsync(mapper.Map<DataModels.Student>(request));
            if(newstudent!=null)
            {
                return CreatedAtAction(nameof(GetStudentAsync),new {id = newstudent.Id }, mapper.Map<Student>(newstudent));
            }
            return NotFound();
        }

        [HttpPost("upload-image/{id}")]
        public async Task<IActionResult> UploadImage([FromRoute] int id,IFormFile profileimage)
        {
            //Check if student exist
            if (await studentRepository.Exists(id))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(profileimage.FileName);
                var fileImagePath = await imageRepository.UploadProfileImage(profileimage, fileName);
                if(await studentRepository.UploadProfileImage(id,fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                //Upload the image to local storage
                
                //Update the profile image path in the database
            }
            return NotFound();


        }
    }
}
