using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DogsHouse.Models.FilterModels
{
    public abstract class QueryBase
    {
        public int pageNumber { get; set; } = 1;

        public int pageSize { get; set; } = 10;
    }
}
