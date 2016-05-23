using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Excel.ExcelModel
{
    public class DataGroup
    {
        public int startRowIndex { get; set; }
        public int startColIndex { get; set; }
        public int endRowIndex { get; set; }
        public int endColIndex { get; set; }
    }
}
