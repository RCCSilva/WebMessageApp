using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.Models
{
    public class StoredMessage
    {
        public int Id { get; set; }

        [Required, Column(TypeName = "varchar(128)")]
        public string UserId { get; set; }

        [Required] 
        public string ChatMessage { get; set; }
    }
}