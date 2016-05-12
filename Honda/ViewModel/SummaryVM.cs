using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Navigation;
using System.Linq;

namespace Honda.ViewModel
{
    /*
     * 巡回汇总报告
     */

    public class SummaryVM : ViewModelBase
    {
        #region Var 、Construction Fun

        /// <summary>
        /// 商店名
        /// </summary>
        private string _storeName;

        public string StoreName
        {
            get
            {
                if (string.IsNullOrEmpty(_storeName))
                {
                    _storeName = DMStoreTour.INSTANCE.CurrentMStore.StoreName;
                }

                return _storeName;
            }
        }

        /// <summary>
        /// 巡回总结报告列表
        /// </summary>
        private ObservableCollection<MSummary> _listSummary;

        public ObservableCollection<MSummary> listSummary
        {
            get
            {
                if (_listSummary == null)
                {
                    _listSummary = new ObservableCollection<MSummary>();
                }

                return _listSummary;
            }

            set
            {
                if (_listSummary != value)
                {
                    _listSummary = value;
                    RaisePropertyChanged("listSummary");
                }
            }
        }

        public SummaryVM()
        {
        }

        private void InitStoreData()
        {
            //清空数据
            listSummary.Clear();
        }

        #endregion

        #region CMD

        /// <summary>
        /// 新增一条巡回总结报告
        /// </summary>
        public RelayCommand AddSummary
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (listSummary == null)
                    {
                        listSummary = new ObservableCollection<MSummary>();
                    }
                    MSummary item = new MSummary();
                    listSummary.Add(item);
                    item.strNo = (listSummary.Count).ToString();
                });
            }
        }

        /// <summary>
        /// 删除一条巡回总结报告
        /// </summary>
        public RelayCommand<MSummary> DeletSummary
        {
            get
            {
                return new RelayCommand<MSummary>((summay) =>
                {
                    if (listSummary.Contains(summay))
                    {
                        listSummary.Remove(summay);
                        RearrangementNO();
                    }
                });
            }
        }


        /// <summary>
        /// 重新排列巡回总结报告意见的序号
        /// </summary>
        private void RearrangementNO()
        {
            for (int i = 0; i < listSummary.Count; i++)
            {
                listSummary[i].strNo = (i + 1).ToString();
            }
        }

        public RelayCommand LoadedCommand
        {
            get { return new RelayCommand(() => { InitStoreData(); }); }
        }

        private MSummary GetSummary(string strNo)
        {
            for (int i = 0; i < listSummary.Count; i++)
            {
                if (listSummary[i].strNo == strNo)
                {
                    return listSummary[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 上传文档
        /// </summary>
        public RelayCommand<MSummary> UploadDocCommand
        {
            get
            {
                return new RelayCommand<MSummary>((summay) =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.CheckFileExists = true;
                    dlg.Filter = "All files (*.*)|*.*|" + "(*.doc)|*.doc|" + "(*.docx)|*.docx|" + "(*.xls)|*.xls|" +
                                 "(*.ppt)|*.ppt";

                    if ((bool) dlg.ShowDialog())
                    {
                        try
                        {
                            if (dlg.FileName.ToLower().EndsWith(".doc") ||
                                dlg.FileName.ToLower().EndsWith(".docx") ||
                                dlg.FileName.ToLower().EndsWith(".xls") ||
                                dlg.FileName.ToLower().EndsWith(".xlsx") ||
                                dlg.FileName.ToLower().EndsWith(".ppt") ||
                                dlg.FileName.ToLower().EndsWith(".pptx"))
                            {
                                MSummary _work = GetSummary(summay.strNo);
                                if (string.IsNullOrWhiteSpace(summay.docPath))
                                {
                                    if (_work != null)
                                    {
                                        if (
                                            listSummary.Where(n => n.oldName == Path.GetFileName(dlg.FileName))
                                                .ToList()
                                                .Count > 0)
                                        {
                                            MessageBox.Show("该文件已经上传，请重新选择！");
                                            return;
                                        }
                                        _work.oldName = Path.GetFileName(dlg.FileName);
                                        _work.docPath = CopyFileToAppDirecty(dlg.FileName);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("文档格式错误");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("上传文档出错\n" + ex.Message);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 删除文档
        /// </summary>
        public RelayCommand<MSummary> DeletDocCommand
        {
            get
            {
                return new RelayCommand<MSummary>((summay) =>
                {
                    MSummary _work = GetSummary(summay.strNo);
                    if (_work != null)
                    {
                        _work.docPath = null;
                    }
                });
            }
        }

        /// <summary>
        /// 提交巡回总结报告
        /// </summary>
        public RelayCommand CommitCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (listSummary.Count() == 0)
                    {
                        System.Windows.MessageBox.Show("请选择上传的文档！");
                        return;
                    }
                    foreach (MSummary work in listSummary)
                    {
                        if (string.IsNullOrWhiteSpace(work.docPath))
                        {
                            System.Windows.MessageBox.Show("序号为" + work.strNo + "未上传文档。");
                            return;
                        }
                    }
                    DMSummary.INSTANCE.UploadSummary(listSummary,
                        (isSuccedd, msg) =>
                        {
                            if (isSuccedd)
                            {
                                MessageBox.Show("操作成功！");

                                //刷新巡回评价管理页面命令
                                GlobalValue.NEED_REFRESH_STORE_TORE = true;

                                Messenger.Default.Send("巡回评价管理", GlobalValue.COMMAND_MAIN_PAGE);
                            }
                            else
                            {
                                MessageBox.Show("失败");
                            }
                        });
                });
            }
        }

        private string CopyFileToAppDirecty(string oldFile)
        {
            string newFilePath = DirectoryHelper.INSTANCE.SummaryPath +
                                 Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(oldFile));
            File.Copy(oldFile, newFilePath, true);
            return newFilePath;
        }

        #endregion
    }
}