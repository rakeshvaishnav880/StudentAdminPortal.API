using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentAdminPortal.API.DataModels;
//using Student = StudentAdminPortal.API.DataModels.Student;
namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext _context;
        public SqlStudentRepository(StudentAdminContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Tbl_SA_Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<List<Student>> GetStudentsByJoinsAsync()
        {

            List<Student> allstudents = new List<Student>();
                
            var students = await (from std in _context.Tbl_SA_Student 
                                  join gen in _context.Tbl_SA_Gender on std.GenderId equals gen.Id
                            select new
                            {                                                              
                                id = std.Id,
                                firstName = std.FirstName,
                                lastName = std.LastName,
                                dateofBirth = std.DateOfBirth,
                                email = std.Email,
                                mobile = std.Mobile,
                                ProfileImageUrl = std.ProfileImageUrl,
                                genderId = gen.Id,
                                genderdescription = gen.Discription                               
                            }).ToListAsync();
            foreach (var s in students)
            {


                var newstudnent = new Student()
                {
                    Id = s.id,
                    FirstName = s.firstName,
                    LastName = s.lastName,
                    DateOfBirth = s.dateofBirth,
                    Email = s.email,
                    Mobile = s.mobile,
                    Gender = new Gender
                    {
                        Id = s.genderId,
                        Discription = s.genderdescription
                    },
                    ProfileImageUrl = s.ProfileImageUrl,                    
                };
                allstudents.Add(newstudnent);
                //};
            }
                 return allstudents;
                // return await _context.Tbl_SA_Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
             var student =await _context.Tbl_SA_Student
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(x=>x.Id == id);
            if(student== null)
            {
                return null;
            }
            else
            {
                return student;
            }
        }

        public async Task<List<Student>> GetStudentsFromSpAsync(string FirstName)
        {
            return await _context.Tbl_SA_Student.FromSqlRaw($"GetStudentByName {FirstName}").ToListAsync();
                
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Tbl_SA_Gender.ToListAsync();
        }

        public async Task<Student> UpdateStudentById(int id, Student updatedstudent)
        {
            var existingstudent = await GetStudentAsync(id);
            if (existingstudent == null)
            {
                return null;
            }
            else
            {
                existingstudent.FirstName = updatedstudent.FirstName;
                existingstudent.LastName = updatedstudent.LastName;
                existingstudent.DateOfBirth = updatedstudent.DateOfBirth;
                existingstudent.Mobile = updatedstudent.Mobile;
                existingstudent.Email = updatedstudent.Email;
                existingstudent.GenderId = updatedstudent.GenderId;
                existingstudent.Address.PhysicalAddress = updatedstudent.Address.PhysicalAddress;
                existingstudent.Address.PostalAddress = updatedstudent.Address.PostalAddress;
                await _context.SaveChangesAsync();
                return existingstudent;
            }
        }

        public async Task<bool> Exists(int id)
        {
            var student = await _context.Tbl_SA_Student.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Student> DeleteStudentAsync(int id)
        {
            var student = await GetStudentAsync(id);
            if(student!=null)
            {
                _context.Tbl_SA_Student.Remove(student);
                await _context.SaveChangesAsync();
                return student;
            }
            return null;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var newstudent = await _context.Tbl_SA_Student.AddAsync(student);
            await _context.SaveChangesAsync();
            if(newstudent!=null)
            {
                return newstudent.Entity;
            }
            else
            {
                return null;
            }
        }
    }
}
