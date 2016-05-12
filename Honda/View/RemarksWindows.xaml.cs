using Honda.Globals;
using Honda.Model.Form;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Honda.View
{
    /// <summary>
    /// RemarksWindows.xaml 的交互逻辑
    /// </summary>
    public partial class RemarksWindows : BaseWindow
    {
        /// <summary>
        /// 备注图片地址列表
        /// </summary>
        private List<string> lstpictrue;

        //备注内容
        private MRemarks remaks;

        public RemarksWindows(MRemarks remaks)
        {
            InitializeComponent();
            SetOwner();

            this.remaks = remaks;
            if (remaks != null && remaks.ImagePathList != null)
            {
                lstpictrue = Tools.CloneList(remaks.ImagePathList);
            }
            else
            {
                lstpictrue = new List<string>();
            }
            imageListUCT.ImageList = lstpictrue;
            imageListUCT.SetAddAtion(() => { AddPictrue(); });


            this.Loaded += RemarksWindows_Loaded;
        }

        #region 单击添加按钮，弹出选择图片

        /// <summary>
        /// 单击添加按钮，弹出选择图片窗口
        /// </summary>
        private void AddPictrue()
        {
            //PictrueWindows pictrueWindows = new PictrueWindows();
            //pictrueWindows.iSelected = lstpictrue.Count;

            //if ((bool)pictrueWindows.ShowDialog())
            //{
            //   lstpictrue.AddRange(pictrueWindows._lstSelectPictrue);
            //   imageListUCT.ImageList = lstpictrue;
            //}

            double left = this.Left;
            this.Left = 2000;
            AddPictrueWindow pictrueW = new AddPictrueWindow();
            if ((bool)pictrueW.ShowDialog())
            {
                lstpictrue.Add(pictrueW.pictruePath);
                imageListUCT.ImageList = lstpictrue;
            }
            this.Left = left;
        }

        #endregion

        #region Event

        private void RemarksWindows_Loaded(object sender, RoutedEventArgs e)
        {
            if (remaks != null)
            {
                tbContent.Text = remaks.content;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbContent.Text))
            {
                MessageBox.Show("请填写文字说明");
                return;
            }
            remaks.content = tbContent.Text;
            List<string> tempPictrueLst = new List<string>();
            if (lstpictrue != null && lstpictrue.Count > 0)
            {
                for (int i = 0; i < lstpictrue.Count; i++)
                {
                    if (!File.Exists(lstpictrue[i]))
                    {
                        continue;
                    }

                    string fileName = System.IO.Path.GetFileName(lstpictrue[i]);
                    string targetPath = DirectoryHelper.INSTANCE.pictrue_base_path + fileName;

                    //拷贝图片文件到指定路径
                    if (!File.Exists(targetPath))
                    {
                        System.IO.File.Copy(lstpictrue[i], targetPath);
                    }
                    if (!tempPictrueLst.Contains(targetPath))
                        tempPictrueLst.Add(targetPath);
                }
            }

            remaks.ImagePathList = tempPictrueLst;
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}