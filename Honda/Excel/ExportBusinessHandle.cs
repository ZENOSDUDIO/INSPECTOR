namespace Honda.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OfficeOpenXml;
    using OfficeOpenXml.Drawing;
    using Honda.ViewModel;
    using System.Windows.Forms;
    using System.IO;
    using OfficeOpenXml.Drawing.Vml;
    using OfficeOpenXml.ConditionalFormatting;
    using System.Drawing;
    using OfficeOpenXml.Style.Dxf;
    using Honda.Excel.ExcelModel;
    public class ExportBusinessHandle : ExcelHandleBase
    {
        #region Var Constructor

        string _headerPosition;

        const int columnOffset = 1;

        int _header1RowIndex;
        int _header2RowIndex;
        int _header2_1RowIndex;
        int _header3RowIndex;
        Queue<int> _headerRowIndexQ = new Queue<int>();

        string checkDate = string.Format("{0}/{1}/{2}", System.DateTime.Now.Date.Month, System.DateTime.Now.Date.Day, System.DateTime.Now.Date.Year);
        const string checkDatePosition = "O3";
        string shopCode;
        const string shopCodePosition = "D3";
        string shopName;
        const string shopNamePosition = "D4";
        string beginDate;
        const string beginDatePosition = "O4";

        public ExportBusinessHandle(Excel.ExcelModel.ExcelBag ebag)
            : base(ebag)
        { }

        #endregion

        #region FUN
        //初始化数据
        protected override void ExcelInit()
        {
            this._templatePath = Environment.CurrentDirectory + @"\Excel\Template\巡回评价商务政策表.xlsx";

            //表头位置
            this._headerPosition = "B{0}:Q{0}";

            this._header1RowIndex = 7;
            this._header2RowIndex = 10;
            this._header2_1RowIndex = 12;
            this._header3RowIndex = 15;

            shopName = this._title["shopname"];
            shopCode = this._title["shopcode"];

            _headerRowIndexQ.Enqueue(_header1RowIndex);
            _headerRowIndexQ.Enqueue(_header2RowIndex);
            _headerRowIndexQ.Enqueue(_header2_1RowIndex);
            _headerRowIndexQ.Enqueue(_header3RowIndex);

            //表头行高
            this._headRowHight = 22;
            this._dataRowHeight = 22;
        }

        //画表格（行数）
        protected override void DrawTable()
        {
            if (_ws == null || _tables == null) return;

            int insertedRowCount = 0;
            foreach (var tb in _tables)
            {
                _currentRowIndex = insertedRowCount + _headerRowIndexQ.Dequeue() + 1;
                var tbDrawRowIndex = _currentRowIndex;

                //设置数据行数
                if (tb.TbData != null)
                {
                    tb.RowsCount = tb.TbData.Count();

                    if (tb.TbData.Count == 0)
                    {
                        _ws.Row(_currentRowIndex - 1).Hidden = true;
                        _ws.Row(_currentRowIndex - 2).Hidden = true;
                    }
                }

                //绘制数据行
                _ws.InsertRow(_currentRowIndex, tb.RowsCount, tbDrawRowIndex - 1);
                insertedRowCount += tb.RowsCount;

                //记录数据部分起始行
                tb.TbDataStartRowIndex = tbDrawRowIndex;

                var header = string.Format(_headerPosition, tbDrawRowIndex - 1);
                for (int r = 0; r < tb.RowsCount; r++)
                {
                    _ws.Cells[header].Copy(_ws.Cells[string.Format("B{0}:Q{0}", _currentRowIndex)]);

                    _ws.Row(_currentRowIndex).CustomHeight = true;
                    _ws.Row(_currentRowIndex).Height = _dataRowHeight;

                    _currentRowIndex++;
                }

                //记录数据部分结束行
                tb.TbDataEndRowIndex = _currentRowIndex - 1;
            }

            //设置打印区域
            _ws.PrinterSettings.PrintArea = _ws.Cells[1, 1, 3 + _currentRowIndex, 17];

            //设置表题
            _ws.Cells[checkDatePosition].Value = checkDate;
            _ws.Cells[shopCodePosition].Value = shopCode;
            _ws.Cells[shopNamePosition].Value = shopName;
            _ws.Cells[beginDatePosition].Value = beginDate;
        }

        //填数据
        protected override void FillData()
        {
            if (_ws == null || _tables == null) return;
            foreach (var tb in _tables)
            {
                if (tb.TbGroups != null)
                {
                    //合并单元格
                    foreach (var gp in tb.TbGroups)
                    {
                        _ws.Cells[gp.startRowIndex + tb.TbDataStartRowIndex, gp.startColIndex + columnOffset, gp.endRowIndex + tb.TbDataStartRowIndex, gp.endColIndex + columnOffset].Merge = true;
                    }
                }

                if (tb.TbData == null) continue;

                //加载数据
                if (tb.TbData.Count > 0)
                    _ws.Cells[string.Format("B{0}:Q{1}", tb.TbDataStartRowIndex, tb.TbDataEndRowIndex)].LoadFromCollection(tb.TbData);
            }
        }
        #endregion
    }
}
