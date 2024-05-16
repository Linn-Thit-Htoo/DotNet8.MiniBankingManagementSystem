namespace DotNet8.MiniBankingManagementSystem.Models.Setup.Account
{
    public class AccountRequestModel
    {
        public string CustomerName { get; set; } = null!;
        public decimal AccountLevel { get; set; }
        public decimal Balance { get; set; }
        public string? CustomerCode { get; set; }
        public string StateCode { get; set; } = null!;
        public string TownshipCode { get; set; } = null!;
    }
}