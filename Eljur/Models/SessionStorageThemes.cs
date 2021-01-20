using Eljur.Context.Tables;

namespace Eljur.Controllers
{
    public partial class VisitController
    {
        public class SessionStorageThemes
        {
            public int ThemeId { get; set; }
            public double Reserved { get; set; }
            public ThemeGroup ThemeGroup { get; set; }
        }

    }
}
