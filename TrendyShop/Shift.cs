using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop
{
    public class Shift
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        //Si es necesario comparar minutos habra que añadir dos campos mas
    }
}
