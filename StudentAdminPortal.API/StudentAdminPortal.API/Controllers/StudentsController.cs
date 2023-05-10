using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
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

        [HttpGet("get-student/{id}")]       
        public async Task<IActionResult> GetStudentAsync([FromRoute]int id)
        {
            var student = await studentRepository.GetStudentAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }
    }
}
