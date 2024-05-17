﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Setup.State
{
    public class StateModel
    {
        public long StateId { get; set; }
        public string StateCode { get; set; } = null!;
        public string StateName { get; set; } = null!;
    }
}