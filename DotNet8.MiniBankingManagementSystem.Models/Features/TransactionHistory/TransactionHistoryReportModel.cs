using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Features.TransactionHistory
{
    public class TransactionHistoryReportModel
    {
        public string TransactionHistoryId { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string SenderName { get; set; } = null!;
        public string ReceiverName { get; set; } = null!;
    }
}
