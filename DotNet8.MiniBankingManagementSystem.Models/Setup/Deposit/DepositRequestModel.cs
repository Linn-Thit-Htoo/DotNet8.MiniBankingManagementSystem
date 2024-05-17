using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Setup.Deposit
{
    public class DepositRequestModel
    {
        public string AccountNo { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
