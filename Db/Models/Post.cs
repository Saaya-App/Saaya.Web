#nullable disable
using System.ComponentModel.DataAnnotations;

namespace Saaya.Web.Db.Models
{
    public class Post : SaayaEntity
    {
        public string? Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}