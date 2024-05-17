using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Setup.Township
{
    public class TownshipModel
    {
        public long TownshipId { get; set; }
        public string TownshipCode { get; set; } = null!;
        public string TownshipName { get; set; } = null!;
        public string StateCode { get; set; } = null!;
    }
}
