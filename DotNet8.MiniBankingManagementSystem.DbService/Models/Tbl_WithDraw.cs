using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    [Table("WithDraw")]
    public class Tbl_WithDraw
    {
        [Key]
        public long WithDrawId { get; set; }
        public string AccountNo { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime WithDrawDate { get; set; }
    }
}
