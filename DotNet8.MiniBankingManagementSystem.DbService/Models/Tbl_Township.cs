using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    [Table("Township")]
    public class Tbl_Township
    {
        [Key]
        public long TownshipId { get; set; }
        public string TownshipCode { get; set; } = null!;
        public string TownshipName { get; set; } = null!;
        public string StateCode { get; set; } = null!;
    }
}
