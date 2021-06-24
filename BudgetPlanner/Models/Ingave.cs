using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Models
{
    public class Ingave
    {
        [Key]
        public int IngaveId { get; set; }
        [DataType(DataType.Currency)]
        public decimal Bedrag { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public string Omschrijving { get; set; }
        [DisplayName("Product")]
        public string ProductImage { get; set; }
        public string Bedrijf { get; set; }
        public string Status { get; set; }
    }
}
