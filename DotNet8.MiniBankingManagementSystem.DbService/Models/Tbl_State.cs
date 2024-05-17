using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    [Table("State")]
    public class Tbl_State
    {
        [Key]
        public long StateId { get; set; }
        public string StateCode { get; set; } = null!;
        public string StateName { get; set; } = null!;
    }
}