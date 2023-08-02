using System.ComponentModel.DataAnnotations;

namespace Saaya.Web.Db.Models
{
    public class SaayaUser : SaayaEntity
    {
        [Required]
        public string Username { get; set; }

        public ulong? Snowflake { get; set; } 

        //[Required]
        //public string PasswordHash { get; set; }

        //[Required]
        //public string Email { get; set; }

        //[Required]
        //public string Token { get; set; }

        public string Role { get; set; } = "User";

        //public List<Song> Songs { get; set; } = new List<Song>();
        //public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}