﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    [Table("Deposit")]
    public class Tbl_Deposit
    {
        [Key]
        public long DepositId { get; set; }
        public string AccountNo { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime DepositDate { get; set; }
    }
}