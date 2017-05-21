using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.Categories.Dto.Out
{
    public class CategoryDtoOut
    {
        public int Id { get; set; }
        public Guid GuidId { get; set; }
        public string CategoryName { get; set; }
        public double Budget { get; set; }
    }
}
