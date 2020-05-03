using System;
using System.Collections.Generic;
using System.Text;

namespace VehiclePortal.Common.ServiceModels
{
    public class AddFundsServiceModel
    {
        public string UserId { get; set; }

        public decimal Balance { get; set; }
    }
}
