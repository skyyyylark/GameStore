using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CategoryDetailsDTO : CategoryDTO
    {
        public ICollection<GameDTO>? Games { get; set; }
    }
}
