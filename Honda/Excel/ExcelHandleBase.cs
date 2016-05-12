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

    public abstract class ExcelHandleBase
    {
        #region Var Construction
        //标题
        protected Dictionary<string, string> _title { get; set; }

        //文件名称
        string _fileName;
        protected string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = string.Format("{0}{1}{2}{3}{4}{5}",
                    value, DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
            }
        }

        //ExcelPackage
        protected ExcelPackage _ep { get; set; }

        //操作的工作簿
        protected ExcelWorkbook _wb { get; set; }

        //操作的工作表名称
        protected ExcelWorksheet _ws { get; set; }

        //表格
        protected List<ExcelTable> _tables { get; set; }

        protected string _currentWorkSheetName { get; set; }


        //表头行高
        protected double _headRowHight { get; set; }

        //数据行高
        protected double _dataRowHeight { get; set; }

        //当前行位置
        protected int _currentRowIndex { get; set; }

        //模板路径
        protected string _templatePath { get; set; }

        //是否有错误
        public bool IsError { get; set; }

        //处理中消息
        public string Message { get; set; }

        protected ExcelHandleBase(Excel.ExcelModel.ExcelBag ebag)
        {
            try
            {
                this.FileName = ebag.fileName;
                this._tables = ebag.tables;
                this._title = ebag.WorkSheetTitle;
                this._currentWorkSheetName = ebag.workSheetName;
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                this.IsError = false;
            }
        }


        #endregion

        #region FUN
        public void Work()
        {
            try
            {
                //初始化资源
                ExcelInit();

                //ExcelPackage
                _ep = new OfficeOpenXml.ExcelPackage(new FileInfo(_templatePath));

                //操作的工作簿
                _wb = _ep.Workbook;

                //操作的工作表名称
                _ws = _wb.Worksheets.FirstOrDefault(n => n.Name == this._currentWorkSheetName);

                //画表格
                DrawTable();

                //填数据
                FillData();

                //导出文件
                ExportFile();
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                this.IsError = false;
            }
        }

        //初始化关键模板数据
        protected abstract void ExcelInit();

        //画表格
        protected abstract void DrawTable();

        //填数据
        protected abstract void FillData();

        //导出文件
        protected void ExportFile()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "*.xls|*.xlsx";
                sfd.FileName = this.FileName;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileInfo file = new FileInfo(sfd.FileName);
                    _ep.File = file;
                    _ep.Save();
                    MessageBox.Show("保存成功!");
                }
            }
        }

        #endregion
    }
}
