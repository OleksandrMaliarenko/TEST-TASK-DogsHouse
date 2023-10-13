using BLL_DogsHouse.Models.FilterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DogsHouse.Models.Queries
{
    public class DogQuery : QueryBase
    {
        [MinLength(3), MaxLength(50, ErrorMessage = "Incorrect Attribute")]
        public string attribute { get; set; } = "name";
        [MinLength(3), MaxLength(50, ErrorMessage = "Incorrect Order")]
        public string order { get; set; } = "asc";
    }
}
