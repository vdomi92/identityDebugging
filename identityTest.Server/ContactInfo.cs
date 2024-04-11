namespace identityTest.Server
{
    public class ContactInfo : Entity
    {
        public required string Details { get; set; }

        public required string UserStringId { get; set; }

        public  required AppUser User { get; set; }
    }
}
