using System.ComponentModel.DataAnnotations;

namespace SSW.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
