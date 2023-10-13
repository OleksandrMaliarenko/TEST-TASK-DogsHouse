using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DogsHouse.Models.Views
{
    public class DogView
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public double Tail_Length { get; set; }
        public double Weight { get; set; }
    }
}
