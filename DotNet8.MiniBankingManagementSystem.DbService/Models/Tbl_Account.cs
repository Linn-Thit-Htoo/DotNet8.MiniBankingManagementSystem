using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    [Table("Account")]
    public class Tbl_Account
    {
        [Key]
        public long AccountId { get; set; }
        public string AccountNo { get; set; } = null!;
        public decimal Balance { get; set; }
        public decimal AccountLevel { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public string TownshipCode { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}