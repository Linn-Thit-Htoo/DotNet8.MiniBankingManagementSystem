using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    [Table("TransactionHistory")]
    public class Tbl_TransactionHistory
    {
        [Key]
        public long TransactionHistoryId { get; set; }
        public string FromAccountNo { get; set; } = null!;
        public string ToAccountNo { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
