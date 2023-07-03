using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [Route("[controller]")]
    [ApiController]    public class GendersController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        private readonly IMapper mapper;

        public GendersController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }
       
        //get-all-gender
        [HttpGet("get-allgenders")]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList = await studentRepository.GetGendersAsync();
            if (genderList == null || !genderList.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<List<Gender>>(genderList));
            }
            
        }
    }
}
