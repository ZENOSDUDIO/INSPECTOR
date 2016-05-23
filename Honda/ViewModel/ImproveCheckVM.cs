using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Honda.ViewModel
{
    public class ImproveCheckVM : ViewModelBase
    {
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
            set
            {
                if (_storeName != value)
                {
                    _storeName = value;
                    RaisePropertyChanged("StoreName");
                }
            }
        }


        private ObservableCollection<MImproveCheck> _listImproveCheck = new ObservableCollection<MImproveCheck>();

        public ObservableCollection<MImproveCheck> listImproveCheck
        {
            get { return _listImproveCheck; }
            set
            {
                if (_listImproveCheck != value)
                {
                    _listImproveCheck = value;
                    RaisePropertyChanged("listImproveCheck");
                }
            }
        }


        private ObservableCollection<MFileData> _attachmentList;

        /// <summary>
        /// 证据
        /// </summary>
        public ObservableCollection<MFileData> AttachmentList
        {
            get { return _attachmentList; }
            set
            {
                if (_attachmentList != value)
                {
                    _attachmentList = value;
                    RaisePropertyChanged("AttachmentList");
                }
            }
        }

        #region CMD

        /// <summary>
        /// 查询改善计划审核列表
        /// </summary>
        public RelayCommand LoadCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    DMImprove.INSTANCE.GetImproveCheckList((isSucceed, msg) =>
                    {
                        if (isSucceed)
                        {
                            listImproveCheck = DMImprove.INSTANCE.ItemCheckList;
                        }
                    });
                });
            }
        }

        /// <summary>
        /// 通过
        /// </summary>
        public RelayCommand<MImproveCheck> PassCommand
        {
            get { return new RelayCommand<MImproveCheck>((check) => { check.status = "3"; }); }
        }

        /// <summary>
        /// 不通过
        /// </summary>
        public RelayCommand<MImproveCheck> NoPassCommand
        {
            get { return new RelayCommand<MImproveCheck>((check) => { check.status = "4"; }); }
        }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand<MImproveCheck> CheckCommand
        {
            get { return new RelayCommand<MImproveCheck>((check) => { }); }
        }


        /// <summary>
        /// 上传改善计划审核
        /// </summary>
        public RelayCommand UploadCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    DMImprove.INSTANCE.UploadImproveCheck((isSucceed, msg) =>
                    {
                        if (isSucceed)
                        {
                            System.Windows.MessageBox.Show("上传成功");

                            //刷新巡回评价管理页面命令
                            GlobalValue.NEED_REFRESH_STORE_TORE = true;

                            Messenger.Default.Send("巡回评价管理", GlobalValue.COMMAND_MAIN_PAGE);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show(msg);
                        }
                    });
                });
            }
        }

        /// <summary>
        /// 打开证据窗口
        /// </summary>
        public RelayCommand<MImproveCheck> OpenDownFileWindowsCommand
        {
            get
            {
                return new RelayCommand<MImproveCheck>((improveCheck) =>
                {
                    //
                    AttachmentList = improveCheck.Attachment;
                    Messenger.Default.Send("改善计划审核", GlobalValue.IMPROVE_CHECK);
                });
            }
        }

        /// <summary>
        /// 关闭证据窗口
        /// </summary>
        public RelayCommand CloseEvidenceCommand
        {
            get
            {
                return new RelayCommand(() => { Messenger.Default.Send("证据", GlobalValue.IMPROVE_CHECK_ClOSE_EVIDECE); });
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public RelayCommand<MFileData> DownFileCommand
        {
            get
            {
                return new RelayCommand<MFileData>((fileData) =>
                {
                    FolderBrowserDialog SavFileDialog = new FolderBrowserDialog();
                    DialogResult result = SavFileDialog.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        return;
                    }

                    string FilePath = SavFileDialog.SelectedPath + "\\" + Path.GetFileName(fileData.FileUriOnServer);
                    fileData.FilePath = FilePath;
                    fileData.DownloadFile((isSucceed) =>
                    {
                        if (isSucceed)
                        {
                            System.Windows.MessageBox.Show("文件名为" + fileData.FilePath + "下载成功");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("文件名为" + fileData.FilePath + "下载失败");
                        }
                    });
                });
            }
        }

        #endregion
    }
}