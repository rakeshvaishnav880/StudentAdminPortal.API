using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsByJoinsAsync();

        Task<List<Student>> GetStudentsAsync();

        Task<Student> GetStudentAsync(int id);

        Task<List<Student>> GetStudentsFromSpAsync(string FirstName);

        Task<List<Gender>> GetGendersAsync();

        Task<Student> UpdateStudentById(int id,Student updatedstudent);

        Task<bool> Exists(int id);

        Task<Student> DeleteStudentAsync(int id);

        Task<Student> AddStudentAsync(Student student);       
    }
}
