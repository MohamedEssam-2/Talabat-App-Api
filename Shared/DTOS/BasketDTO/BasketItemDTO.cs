using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDTO
{
    public class BasketItemDTO
    {
        public int Id { get; set; } //Product have int id 

        public string ProductName { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        [Range(1,100000)]
        public decimal Price { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
