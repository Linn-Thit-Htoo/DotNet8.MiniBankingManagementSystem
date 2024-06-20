using DotNet8.MiniBankingManagementSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.MiniBankingManagementSystem.Models.Features
{
    public class ReportModel<T>
    {
        public string DataSetName { get { return ReportFileName + "DataSet"; } }
        public string ReportFileName { get; set; }
        public string ExportFileName { get; set; }
        public EnumFileType ReportType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public List<T> DataLst { get; set; }
    }
}
