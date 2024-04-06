using System.ComponentModel.DataAnnotations;

namespace Commerce.Domain.Entitie
{
    public class Entity
    {
        [Key]
        public int Id { get; protected set; }
    }
}
