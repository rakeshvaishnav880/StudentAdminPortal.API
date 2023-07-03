namespace StudentAdminPortal.API.Repositories
{
    public class LocalImageRepositoryStorage : IImageRepository
    {
        public async Task<string> UploadProfileImage(IFormFile file, string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream filestream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(filestream);
            return GetServerRelativePath(fileName);
        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
