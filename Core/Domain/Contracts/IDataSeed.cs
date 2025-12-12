using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDataSeed
    {
        //public void SeedData(); //old seeeding without async
        public /*async*/ Task SeedDataAsync();//Cant use async here because interface methods cant have body
        public Task IdentityDataSeeding();
    }
}
