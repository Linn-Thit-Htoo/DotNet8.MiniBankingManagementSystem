using System;
using System.Collections.Generic;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models;

public partial class TransactionHistory
{
    public string TransactionHistoryId { get; set; } = null!;

    public string FromAccountNo { get; set; } = null!;

    public string ToAccountNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }
}
