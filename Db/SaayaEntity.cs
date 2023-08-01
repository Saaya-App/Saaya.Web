using System.ComponentModel.DataAnnotations;

namespace Saaya.Web.Db
{
    public class SaayaEntity
    {
        [Key]
        public int Id { get; set; }

        public long DateCreated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}