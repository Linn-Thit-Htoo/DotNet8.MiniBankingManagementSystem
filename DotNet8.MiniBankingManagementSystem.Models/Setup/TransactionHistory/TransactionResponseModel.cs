using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Setup.TransactionHistory
{
    public class TransactionResponseModel
    {
        public string TransactionHistoryId { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReceiverName { get; set; } = null!;
    }
}