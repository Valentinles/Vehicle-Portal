﻿using System;
using System.Collections.Generic;
using System.Text;
using VehiclePortal.Models;

namespace VehiclePortal.Common.ViewModels
{
    public class AddFundsBindingModel
    {
        public string UserId { get; set; }

        public decimal Balance { get; set; }
    }
}