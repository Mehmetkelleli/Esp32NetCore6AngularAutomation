using System.Reflection.Metadata.Ecma335;

namespace SmartBusinessApplication.Domain.Entity.BaseEntity
{
    public class BaseClass
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
