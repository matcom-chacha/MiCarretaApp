using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrendyShop.Models
{
    public class MOType
    {
        [Key]
        public int MOTypeId { get; set; }

        [Required]
        [Display(Name = "Tipo de operación")]
        public string Name { get; set; }
    }
}
