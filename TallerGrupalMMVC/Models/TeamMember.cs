using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerGrupalMMVC.Models
{
    public class TeamMember
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string FavoriteSport { get; set; }
        public string PhotoPath { get; set; }
        public string Role { get; set; } // Rol en el equipo (opcional)
        public string Description { get; set; } // Descripción breve (opcional)
    }
}