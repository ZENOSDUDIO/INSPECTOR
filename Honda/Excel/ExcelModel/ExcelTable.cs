using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Excel.ExcelModel
{
    public class ExcelTable
    {
        public int RowsCount { get; set; }

        protected int ColsCount { get; set; }

        public List<DataHead> TbHeads { get; set; }

        public List<DataGroup> TbGroups { get; set; }

        public List<DataRow> TbData { get; set; }

        public int TbHeaderStartRowIndex { get; set; }
        public int TbHeaderEndRowIndex { get; set; }
        public int TbDataStartRowIndex { get; set; }
        public int TbDataEndRowIndex { get; set; }
    }
}
