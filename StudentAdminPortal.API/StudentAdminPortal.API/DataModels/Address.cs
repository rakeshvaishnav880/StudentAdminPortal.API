namespace StudentAdminPortal.API.DataModels
{
    public class Address
    {
        public int Id { get; set; }

        public string  PhysicalAddress { get; set; }

        public string PostalAddress { get; set; }

        //Navigation Property

        public int StudentId { get; set; }
    }
}
