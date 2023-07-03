namespace StudentAdminPortal.API.Repositories
{
    //Image upload
    public interface IImageRepository
    {
        Task<string> UploadProfileImage(IFormFile file, string fileName);
    }
}
