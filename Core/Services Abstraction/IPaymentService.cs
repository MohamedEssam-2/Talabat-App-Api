using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.BasketDTO;

namespace Services_Abstraction
{
    public interface IPaymentService
    {
        // Create Payment Intent
        // Test
        Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId);

        public Task UpdatePaymentStatus(string jsonRequest, string stripeHeader);


    }
}
