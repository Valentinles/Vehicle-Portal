using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Data;

namespace VehiclePortal.Services.Implementations
{
    public class DataService
    {
        protected readonly VehiclePortalDbContext context;

        public DataService(VehiclePortalDbContext context)
        {
            this.context = context;
        }
    }
}
