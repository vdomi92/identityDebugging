using System.ComponentModel.DataAnnotations;

namespace identityTest.Server
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
