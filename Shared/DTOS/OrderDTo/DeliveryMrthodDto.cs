using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTo
{
    public class DeliveryMrthodDto
    {
        public int id { get; set; }
        public string ShortName { get; set; } = null!; //Campany Name
        public string Description { get; set; } = null!; //Details
        public string DeliveryTime { get; set; } = null!;//Estimated Time
        public decimal Cost { get; set; }



    }
}
