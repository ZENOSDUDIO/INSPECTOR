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
    public class ExportEvaluationHandle : ExcelHandleBase
    {
        #region Var Constructor
        string _imgLogoPath;
        string _imgSignaturePath;

        string _headerPosition1;
        string _headerPosition2;
        //表头位置
        int _headerRowIndex { get; set; }

        //第一行数据位置
        string _firstDPosition { get; set; }
        int _firstDRowIndex { get; set; }

        string storename;

        public ExportEvaluationHandle(Excel.ExcelModel.ExcelBag ebag)
            : base(ebag)
        { }

        #endregion

        #region FUN
        //初始化数据
        protected override void ExcelInit()
        {
            this._templatePath = Environment.CurrentDirectory + @"\Excel\Template\2015年客服现场检查表.xlsx";
            this._imgLogoPath = Environment.CurrentDirectory + @"\Excel\Img\image003.png";
            this._imgSignaturePath = Environment.CurrentDirectory + @"\Excel\Img\image006.png";

            //表头位置
            this._headerPosition1 = "A3:K3";
            this._headerPosition2 = "A4:K4";
            this._headerRowIndex = 3;

            //第一行数据位置
            this._firstDPosition = "A5:K5";
            this._firstDRowIndex = 5;

            //表头行高
            this._headRowHight = 18;

            //数据行高
            this._dataRowHeight = 13.5;

            //当前行位置
            this._currentRowIndex = 6;

            storename = this._title["shopname"];
        }

        //画表格（行数）
        protected override void DrawTable()
        {
            if (_ws == null || _tables == null) return;
            foreach (var tb in _tables)
            {
                //设置数据行数
                if (tb.TbData != null)
                {
                    tb.RowsCount = tb.TbData.Count();
                }

                var tbDrawRowIndex = _currentRowIndex;
                //记录表头起始行
                tb.TbHeaderStartRowIndex = _currentRowIndex;

                //绘制表头
                _ws.InsertRow(_currentRowIndex, 1, _headerRowIndex);
                _ws.Row(_currentRowIndex).CustomHeight = true;
                _ws.Row(_currentRowIndex).Height = _headRowHight;
                _ws.InsertRow(_currentRowIndex + 1, 1, _headerRowIndex + 1);
                _ws.Row(_currentRowIndex + 1).CustomHeight = true;
                _ws.Row(_currentRowIndex + 1).Height = _headRowHight;
                _currentRowIndex += 2;

                tbDrawRowIndex = _currentRowIndex;
                //记录表头结束行
                tb.TbHeaderEndRowIndex = _currentRowIndex - 1;

                //拷贝表头
                _ws.Cells[_headerPosition1].Copy(_ws.Cells[string.Format("A{0}:K{1}", tb.TbHeaderStartRowIndex, tb.TbHeaderStartRowIndex)]);
                _ws.Cells[string.Format("A{0}:K{1}", tb.TbHeaderStartRowIndex, tb.TbHeaderStartRowIndex)].Merge = true;
                _ws.Cells[_headerPosition2].Copy(_ws.Cells[string.Format("A{0}:K{1}", tb.TbHeaderEndRowIndex, tb.TbHeaderEndRowIndex)]);

                //设置表头列名
                _ws.Cells[string.Format("A{0}:K{1}", tb.TbHeaderStartRowIndex, tb.TbHeaderEndRowIndex)].LoadFromCollection(tb.TbHeads);

                //绘制数据行
                _ws.InsertRow(_currentRowIndex, tb.RowsCount, _firstDRowIndex);

                //记录数据部分起始行
                tb.TbDataStartRowIndex = tbDrawRowIndex;

                for (int r = 0; r < tb.RowsCount; r++)
                {
                    _ws.Cells[_firstDPosition].Copy(_ws.Cells[string.Format("A{0}:K{0}", _currentRowIndex)]);
                    var list1 = _ws.Cells["K" + _currentRowIndex].DataValidation.AddListDataValidation();
                    list1.Formula.Values.Add(ExcelValues.PassValue);
                    list1.Formula.Values.Add(ExcelValues.UnpassValue);
                    list1.ShowErrorMessage = true;
                    list1.Error = "Please select a value from the list";

                    _ws.Row(_currentRowIndex).CustomHeight = true;
                    _ws.Row(_currentRowIndex).Height = _dataRowHeight;

                    _currentRowIndex++;
                }

                //记录数据部分结束行
                tb.TbDataEndRowIndex = _currentRowIndex - 1;

                //设置数据校验
                var cellIsAddress = new ExcelAddress(string.Format("K{0}:K{1}", tb.TbDataStartRowIndex, tb.TbDataEndRowIndex));
                var cf = _ws.ConditionalFormatting.AddContainsText(
           cellIsAddress);
                cf.Text = ExcelValues.UnpassValue;
                cf.Style.Font.Color.Color = Color.OrangeRed;
                cf.Style.Fill.BackgroundColor.Color = Color.Pink;

                tbDrawRowIndex = _currentRowIndex;
            }

            //删除模板数据行
            for (int i = 3; i < 6; i++)
            {
                _ws.Row(i).Hidden = true;
            }

            //加载图片资源
            var picLogo = _ws.Drawings.AddPicture("logo", new FileInfo(_imgLogoPath));
            picLogo.SetPosition(0, 0);
            var picSign = _ws.Drawings.AddPicture("Signture", new FileInfo(_imgSignaturePath));
            picSign.SetPosition(_currentRowIndex, 10, 2, 10);

            //设置打印区域
            _ws.PrinterSettings.PrintArea = _ws.Cells[1, 1, 7 + _currentRowIndex, 11];

            //设置表题
            _ws.Cells["E2"].Value = storename;
        }

        //填数据
        protected override void FillData()
        {
            if (_ws == null || _tables == null) return;
            foreach (var tb in _tables)
            {
                if (tb.TbGroups == null) continue;

                //合并单元格
                foreach (var gp in tb.TbGroups)
                {
                    _ws.Cells[gp.startRowIndex + tb.TbDataStartRowIndex, gp.startColIndex, gp.endRowIndex + tb.TbDataStartRowIndex, gp.endColIndex].Merge = true;
                }

                if (tb.TbData == null) continue;

                //加载数据
                if (tb.TbData.Count > 0)
                    _ws.Cells[string.Format("A{0}:K{1}", tb.TbDataStartRowIndex, tb.TbDataEndRowIndex)].LoadFromCollection(tb.TbData);
            }
        }
        #endregion
    }
}
