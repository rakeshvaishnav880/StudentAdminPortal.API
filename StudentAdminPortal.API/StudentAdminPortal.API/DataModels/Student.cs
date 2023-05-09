namespace StudentAdminPortal.API.DataModels
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

       // public string  Email { get; set; }

        public long Mobile { get; set; }

        public string? ProfileImageUrl { get; set; }

        public int GenderId { get; set; }

        //Navigation Property
        public Gender Gender { get; set; }

        public Address Address { get; set; }
        
    }
}
