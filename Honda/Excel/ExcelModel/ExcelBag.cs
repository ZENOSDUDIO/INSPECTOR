using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Excel.ExcelModel
{
   public class ExcelBag
    {
        public Dictionary<string,string> WorkSheetTitle { get; set; }

        public string workSheetName { get; set; }
        public string fileName { get; set; }
        public List<ExcelTable> tables { get; set; }
    }
}
