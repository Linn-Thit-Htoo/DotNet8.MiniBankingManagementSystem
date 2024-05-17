using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Setup.WithDraw
{
    public class WithDrawRequestModel
    {
        public string AccountNo { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
