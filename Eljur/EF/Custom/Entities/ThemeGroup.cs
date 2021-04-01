using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eljur.Context.Tables
{
    public class ThemeGroup
    {   [Key]
        [ForeignKey("Theme")]
        public int Id { get; set; }

        public double UsedHours { get; set; }
        public virtual Theme Theme { get; set; }
        public int ThemeId { get; set; }
    }
}