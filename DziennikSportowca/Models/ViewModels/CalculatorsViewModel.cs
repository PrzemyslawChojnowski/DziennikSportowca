using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class CalculatorsViewModel
    {
        public Gender Gender { get; set; }

        public double? Weight { get; set; }


    }
}
