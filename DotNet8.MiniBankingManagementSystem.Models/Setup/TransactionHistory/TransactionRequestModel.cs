using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory
{
    public class TransactionRequestModel
    {
        public string FromAccountNo { get; set; } = null!;
        public string ToAccountNo { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
