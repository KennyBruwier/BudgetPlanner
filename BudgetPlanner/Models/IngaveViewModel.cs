using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Models
{
    public class IngaveViewModel : Ingave
    {
        [DisplayName("Foto product")]
        public IFormFile ProductNewImage { get; set; }
        public IEnumerable<SelectListItem>ListStatus { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem("Betaald","Betaald"),
            new SelectListItem("Niet betaald","Niet Betaald"),
            new SelectListItem("Dringend te betalen","Dringed te betalen"),
            new SelectListItem("Wachten","Wachten"),
            new SelectListItem("Geen idee","Geen idee")
        };
    }
}
