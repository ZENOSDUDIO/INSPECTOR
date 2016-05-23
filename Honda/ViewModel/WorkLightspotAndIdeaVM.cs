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

namespace Honda.ViewModel
{
    /*
     * 工作亮点与意见需求
     */


    public class WorkLightspotAndIdeaVM : ViewModelBase
    {
        #region Var 、Construction Fun

        /// <summary>
        /// 工作亮点与意见需求列表
        /// </summary>
        private ObservableCollection<MWorkLightspot> _listWorkLightspot;

        public ObservableCollection<MWorkLightspot> listWorkLightspot
        {
            get
            {
                if (_listWorkLightspot == null)
                {
                    _listWorkLightspot = new ObservableCollection<MWorkLightspot>();
                }

                return _listWorkLightspot;
            }

            set
            {
                if (_listWorkLightspot != value)
                {
                    _listWorkLightspot = value;
                    RaisePropertyChanged("listWorkLightspot");
                }
            }
        }


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
                    _storeName = "";
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

        /// <summary>
        /// 记录删除的ID,提交给服务端批量删除
        /// </summary>
        private string _removedIDs;

        public string RemovedIDs
        {
            get
            {
                if (string.IsNullOrEmpty(_removedIDs))
                {
                    _removedIDs = "";
                }

                return _removedIDs;
            }
            set
            {
                if (_removedIDs != value)
                {
                    _removedIDs = value;
                }
            }
        }


        public WorkLightspotAndIdeaVM()
        {
        }

        #endregion

        public void InitStoreData()
        {
            if (DMStoreTour.INSTANCE.CurrentMStore != null)
            {
                StoreName = DMStoreTour.INSTANCE.CurrentMStore.StoreName;
            }

            GetWorkLightspotList();
        }


        /// <summary>
        /// 获取工作亮点与意见需求列表
        /// </summary>
        public void GetWorkLightspotList()
        {
            //如果已经保存在本地
            if (File.Exists(DirectoryHelper.INSTANCE.WorkLightspotAndIdeaSignatureList))
            {
                listWorkLightspot =
                    (ObservableCollection<MWorkLightspot>)
                        SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.WorkLightspotAndIdeaListPath);
            }
            else
            {
                DMWorkLightspotAndIdea.INSTANCE.GetWorkLightspotAndIdeaList(DMUser.INSTANCE.CurrentMUser.UserAccount,
                    DMStoreTour.INSTANCE.CurrentMStore.shopId, (isSucceed, msg) =>
                    {
                        if (isSucceed)
                        {
                            listWorkLightspot = DMWorkLightspotAndIdea.INSTANCE.listWorkLightspot;
                            Tools.DeleteDir(DirectoryHelper.INSTANCE.WorkLightspotAndIdeaPath);
                        }
                        else
                        {
                            listWorkLightspot =
                                (ObservableCollection<MWorkLightspot>)
                                    SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.WorkLightspotAndIdeaListPath);
                        }
                    });
            }
        }

        /// <summary>
        /// 提交工作亮点与意见需求列表
        /// </summary>
        public void CommitWorkLightspotList()
        {
            DMWorkLightspotAndIdea.INSTANCE.CommitWorkLightspotList(DMUser.INSTANCE.CurrentMUser.UserAccount,
                DMStoreTour.INSTANCE.CurrentMStore.shopId, listWorkLightspot, (isSucceed, msg) =>
                {
                    if (isSucceed)
                    {
                        listWorkLightspot = DMWorkLightspotAndIdea.INSTANCE.listWorkLightspot;
                    }
                    else
                    {
                        MessageBox.Show(msg);
                    }
                });
        }

        #region CMD

        public RelayCommand LoadedCommand
        {
            get { return new RelayCommand(() => { InitStoreData(); }); }
        }


        /// <summary>
        /// 新增一条工作亮点与意见需求
        /// </summary>
        public RelayCommand AddWorkLightCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (listWorkLightspot == null)
                    {
                        listWorkLightspot = new ObservableCollection<MWorkLightspot>();
                    }
                    MWorkLightspot item = new MWorkLightspot();
                    item.projectId = "1";
                    item.categoryId = "4";
                    listWorkLightspot.Add(item);
                    item.strNo = (listWorkLightspot.Count).ToString();
                    if (string.IsNullOrWhiteSpace(item.ImaName0))
                    {
                    }
                });
            }
        }

        /// <summary>
        /// 删除一条工作亮点与意见需求
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletWorkLightCommand
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    if (listWorkLightspot.Contains(workLigh))
                    {
                        this.RemovedIDs += string.IsNullOrEmpty(workLigh.ID) ? "" : workLigh.ID + ",";

                        listWorkLightspot.Remove(workLigh);

                        RearrangementNO();
                    }
                });
            }
        }


        /// <summary>
        /// 重新排列工作亮点与需求意见的序号
        /// </summary>
        private void RearrangementNO()
        {
            for (int i = 0; i < listWorkLightspot.Count; i++)
            {
                listWorkLightspot[i].strNo = (i + 1).ToString();
            }
        }

        private MWorkLightspot GetWorkLightspot(string strNo)
        {
            for (int i = 0; i < listWorkLightspot.Count; i++)
            {
                if (listWorkLightspot[i].strNo == strNo)
                {
                    return listWorkLightspot[i];
                }
            }

            return null;
        }


        /// <summary>
        /// 选择项目类型
        /// </summary>
        public RelayCommand<ComboBox> ComboboxLoadedCommand
        {
            get
            {
                return new RelayCommand<ComboBox>((obj) =>
                {
                    //ComboBox com = obj as ComboBox;
                    //if (com == null)
                    //{
                    //    return;
                    //}

                    //MWorkLightspot work = com.Tag as MWorkLightspot;
                    //if (work == null)
                    //{
                    //    return;
                    //}

                    //if (work.projectId == "1")
                    //{
                    //    com.SelectedIndex = 0;

                    //}
                    //else if (work.projectId == "2")
                    //{
                    //    com.SelectedIndex = 1;
                    //}
                    //else if (work.projectId == "3")
                    //{
                    //    com.SelectedIndex = 2;
                    //}
                });
            }
        }

        /// <summary>
        /// 选择类别类型
        /// </summary>
        public RelayCommand<ComboBox> ComboboxLoadedCommand2
        {
            get
            {
                return new RelayCommand<ComboBox>((obj) =>
                {
                    //ComboBox com = obj as ComboBox;
                    //if (com == null)
                    //{
                    //    return;
                    //}

                    //MWorkLightspot work = com.Tag as MWorkLightspot;
                    //if (work == null)
                    //{
                    //    return;
                    //}
                    //if (work.categoryId == "4")
                    //{
                    //    com.SelectedIndex = 0;
                    //}
                    //else if (work.categoryId == "5")
                    //{
                    //    com.SelectedIndex = 1;
                    //}
                    //else if (work.categoryId == "6")
                    //{
                    //    com.SelectedIndex = 2;
                    //}
                    //else if (work.categoryId == "7")
                    //{
                    //    com.SelectedIndex = 0;
                    //}
                    //else if (work.categoryId == "8")
                    //{
                    //    com.SelectedIndex = 1;
                    //}
                    //else if (work.categoryId == "9")
                    //{
                    //    com.SelectedIndex = 0;
                    //}
                    //else if (work.categoryId == "10")
                    //{
                    //    com.SelectedIndex = 1;
                    //}
                });
            }
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        public RelayCommand<MWorkLightspot> UploadPictrueCommand
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    AddPictrueWindow pictrueW = new AddPictrueWindow();
                    if ((bool)pictrueW.ShowDialog())
                    {
                        MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                        if (string.IsNullOrWhiteSpace(workLigh.ImaPath0))
                        {
                            if (_work != null)
                            {
                                _work.ImaPath0 = CopyFileToAppDirecty(pictrueW.pictruePath);
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(workLigh.ImaPath1))
                        {
                            if (_work != null)
                            {
                                _work.ImaPath1 = CopyFileToAppDirecty(pictrueW.pictruePath);
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(workLigh.ImaPath2))
                        {
                            if (_work != null)
                            {
                                _work.ImaPath2 = CopyFileToAppDirecty(pictrueW.pictruePath);
                            }
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 删除图片1
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletPictrue0Command
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                    if (_work != null)
                    {
                        _work.ImaPath0 = null;
                    }
                });
            }
        }

        /// <summary>
        /// 删除图片2
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletPictrue1Command
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                    if (_work != null)
                    {
                        _work.ImaPath1 = null;
                    }
                });
            }
        }

        /// <summary>
        /// 删除图片3
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletPictrue2Command
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                    if (_work != null)
                    {
                        _work.ImaPath2 = null;
                    }
                });
            }
        }


        /// <summary>
        /// 上传音频
        /// </summary>
        public RelayCommand<MWorkLightspot> UploadAudioCommand
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.CheckFileExists = true;
                    dlg.Filter = "All files (*.*)|*.*|" + "(*.mp3)|*.mp3|" + "(*.mpeg)|*.mpeg|" + "(*.wma)|*.wma|" +
                                 "(*.wav)|*.wav|" + "(*.dvf)|*.dvf|" + "(*.msv)|*.msv|" + "(*.amr)|*.amr|" +
                                 "(*.m4a)|*.m4a";

                    if ((bool)dlg.ShowDialog())
                    {
                        try
                        {
                            if (dlg.FileName.ToLower().EndsWith(".mp3") ||
                                dlg.FileName.ToLower().EndsWith(".mpeg") ||
                                dlg.FileName.ToLower().EndsWith(".wma") ||
                                dlg.FileName.ToLower().EndsWith(".wav") ||
                                dlg.FileName.ToLower().EndsWith(".amr") ||
                                dlg.FileName.ToLower().EndsWith(".dvf") ||
                                dlg.FileName.ToLower().EndsWith(".msv") ||
                                dlg.FileName.ToLower().EndsWith(".m4a"))
                            {
                                MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                                if (string.IsNullOrWhiteSpace(workLigh.audioPath))
                                {
                                    if (_work != null)
                                    {
                                        _work.audioPath = CopyFileToAppDirecty(dlg.FileName);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("音频格式错误");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("上传音频出错\n" + ex.Message);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 删除音频
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletAudioCommand
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                    if (_work != null)
                    {
                        _work.audioPath = null;
                    }
                });
            }
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        public RelayCommand<MWorkLightspot> UploadVideoCommand
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.CheckFileExists = true;
                    dlg.Filter = "All files (*.*)|*.*|" + "(*.MP4)|*.MP4|" + "(*.avi)|*.avi|" + "(*.mov)|*.mov|" +
                                 "(*.3gp)|*.3gp|" + "(*.mkv)|*.mkv|" + "(*.mpeg)|*.mpeg|" + "(*.rmvb)|*.rmvb";

                    if ((bool)dlg.ShowDialog())
                    {
                        try
                        {
                            if (dlg.FileName.ToLower().EndsWith(".mp4") ||
                                dlg.FileName.ToLower().EndsWith(".avi") ||
                                dlg.FileName.ToLower().EndsWith(".mov") ||
                                dlg.FileName.ToLower().EndsWith(".3gp") ||
                                dlg.FileName.ToLower().EndsWith(".mkv") ||
                                dlg.FileName.ToLower().EndsWith(".mpeg") ||
                                dlg.FileName.ToLower().EndsWith(".rmvb"))
                            {
                                MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                                if (string.IsNullOrWhiteSpace(workLigh.videoPath))
                                {
                                    if (_work != null)
                                    {
                                        _work.videoPath = CopyFileToAppDirecty(dlg.FileName);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("视频格式错误");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("上传视频出错\n" + ex.Message);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletVideoCommand
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                    if (_work != null)
                    {
                        _work.videoPath = null;
                    }
                });
            }
        }

        /// <summary>
        /// 上传文档
        /// </summary>
        public RelayCommand<MWorkLightspot> UploadDocCommand
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.CheckFileExists = true;
                    dlg.Filter = "All files (*.*)|*.*|" + "(*.doc)|*.doc|" + "(*.docx)|*.docx|" + "(*.xls)|*.xls|" +
                                 "(*.ppt)|*.ppt";

                    if ((bool)dlg.ShowDialog())
                    {
                        try
                        {
                            if (dlg.FileName.ToLower().EndsWith(".doc") ||
                                dlg.FileName.ToLower().EndsWith(".docx") ||
                                dlg.FileName.ToLower().EndsWith(".xls") ||
                                dlg.FileName.ToLower().EndsWith(".ppt"))
                            {
                                MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                                if (string.IsNullOrWhiteSpace(workLigh.docPath0))
                                {
                                    if (_work != null)
                                    {
                                        _work.docPath0 = CopyFileToAppDirecty(dlg.FileName);
                                    }
                                }
                                else if (string.IsNullOrWhiteSpace(workLigh.docPath1))
                                {
                                    if (_work != null)
                                    {
                                        _work.docPath1 = CopyFileToAppDirecty(dlg.FileName);
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
        /// 删除文档1
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletDoc0Command
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                    if (_work != null)
                    {
                        _work.docPath0 = null;
                    }
                });
            }
        }

        /// <summary>
        /// 删除文档2
        /// </summary>
        public RelayCommand<MWorkLightspot> DeletDoc1Command
        {
            get
            {
                return new RelayCommand<MWorkLightspot>((workLigh) =>
                {
                    MWorkLightspot _work = GetWorkLightspot(workLigh.strNo);
                    if (_work != null)
                    {
                        _work.docPath1 = null;
                    }
                });
            }
        }

        /// <summary>
        /// 提交工作亮点与意见需求
        /// </summary>
        public RelayCommand CommitCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    foreach (MWorkLightspot work in listWorkLightspot)
                    {
                        if (string.IsNullOrWhiteSpace(work.ContentDes))
                        {
                            System.Windows.MessageBox.Show("序号为" + work.strNo + "未填写完整");
                            return;
                        }
                    }
                    DMWorkLightspotAndIdea.INSTANCE.UploadWorkLightspotList(listWorkLightspot, RemovedIDs,
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
            string newFilePath = DirectoryHelper.INSTANCE.WorkLightspotAndIdeaPath +
                                 Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(oldFile));
            File.Copy(oldFile, newFilePath, true);
            return newFilePath;
        }

        #endregion
    }
}