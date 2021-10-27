using System.ComponentModel.DataAnnotations;

namespace Sapiens.Core.Entities
{
    public class EntityBase
    {
        [Key]
        public long Id { get; set; }
    }
}
