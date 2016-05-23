using Honda.Globals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.Model
{
    /// <summary>
    /// 工作亮点与意见需求
    /// </summary>
    [Serializable]
    public class MWorkLightspot : INotifyPropertyChanged
    {
        #region 图片

        /// <summary>
        /// 第一张图片地址
        /// </summary>
        private string _imaPath0;

        /// <summary>
        /// 第1张图片地址
        /// </summary>
        public string ImaPath0
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_imaPath0))
                {
                    ImaName0 = "";
                }
                else
                {
                    ImaName0 = Path.GetFileName(_imaPath0);
                }

                return _imaPath0;
            }
            set
            {
                if (_imaPath0 != value)
                {
                    _imaPath0 = value;
                }

                if (string.IsNullOrWhiteSpace(_imaPath0))
                {
                    ImaName0 = "";
                }
                else
                {
                    ImaName0 = Path.GetFileName(_imaPath0);
                }
            }
        }

        private string _imaPath1;

        /// <summary>
        /// 第二张图片地址
        /// </summary>
        public string ImaPath1
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_imaPath1))
                {
                    ImaName1 = "";
                }
                else
                {
                    ImaName1 = Path.GetFileName(_imaPath1);
                }

                return _imaPath1;
            }
            set
            {
                if (_imaPath1 != value)
                {
                    _imaPath1 = value;
                }

                if (string.IsNullOrWhiteSpace(_imaPath1))
                {
                    ImaName1 = "";
                }
                else
                {
                    ImaName1 = Path.GetFileName(_imaPath1);
                }
            }
        }

        private string _imaPath2;

        /// <summary>
        /// 第3张图片地址
        /// </summary>
        public string ImaPath2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_imaPath2))
                {
                    ImaName2 = "";
                }
                else
                {
                    ImaName2 = Path.GetFileName(_imaPath2);
                }

                return _imaPath2;
            }
            set
            {
                if (_imaPath2 != value)
                {
                    _imaPath2 = value;
                }

                if (string.IsNullOrWhiteSpace(_imaPath2))
                {
                    ImaName2 = "";
                }
                else
                {
                    ImaName2 = Path.GetFileName(_imaPath2);
                }
            }
        }


        /// <summary>
        /// 第一张图片名
        /// </summary>
        private string _imaName0;

        public string ImaName0
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_imaName0))
                {
                    _imaName0 = "";
                }
                ShowOrHidenCtrOfPictrue();
                return _imaName0;
            }

            set
            {
                if (_imaName0 != value)
                {
                    _imaName0 = value;
                    ShowOrHidenCtrOfPictrue();
                    NotifyPropertyChanged("ImaName0");
                }
            }
        }

        /// <summary>
        /// 第2张图片名
        /// </summary>
        private string _imaName1;

        public string ImaName1
        {
            get
            {
                if (_imaName1 == null)
                {
                    _imaName1 = "";
                }
                ShowOrHidenCtrOfPictrue();
                return _imaName1;
            }

            set
            {
                if (_imaName1 != value)
                {
                    _imaName1 = value;
                    ShowOrHidenCtrOfPictrue();
                    NotifyPropertyChanged("ImaName1");
                }
            }
        }

        /// <summary>
        /// 第3张图片名
        /// </summary>
        private string _imaName2;

        public string ImaName2
        {
            get
            {
                if (_imaName2 == null)
                {
                    _imaName2 = "";
                }
                ShowOrHidenCtrOfPictrue();
                return _imaName2;
            }

            set
            {
                if (_imaName2 != value)
                {
                    _imaName2 = value;
                    ShowOrHidenCtrOfPictrue();
                    NotifyPropertyChanged("ImaName2");
                }
            }
        }

        /// <summary>
        /// 是否显示图片项中的上传按钮
        /// </summary>
        private Visibility _IsShowPictrueUploadBtn = Visibility.Collapsed;

        public Visibility IsShowPictrueUploadBtn
        {
            get { return _IsShowPictrueUploadBtn; }
            set
            {
                if (_IsShowPictrueUploadBtn != value)
                {
                    _IsShowPictrueUploadBtn = value;
                    NotifyPropertyChanged("IsShowPictrueUploadBtn");
                }
            }
        }

        /// <summary>
        /// 是否显示图片项中的“未上传”TextBlock
        /// </summary>
        private Visibility _IsShowPictrueNotUpload = Visibility.Collapsed;

        public Visibility IsShowPictrueNotUpload
        {
            get { return _IsShowPictrueNotUpload; }
            set
            {
                if (_IsShowPictrueNotUpload != value)
                {
                    _IsShowPictrueNotUpload = value;
                    NotifyPropertyChanged("IsShowPictrueNotUpload");
                }
            }
        }

        /// <summary>
        /// 是否显示图片项中的“第一张图片”
        /// </summary>
        private Visibility _IsShowPictrue0 = Visibility.Collapsed;

        public Visibility IsShowPictrue0
        {
            get { return _IsShowPictrue0; }
            set
            {
                if (_IsShowPictrue0 != value)
                {
                    _IsShowPictrue0 = value;
                    NotifyPropertyChanged("IsShowPictrue0");
                }
            }
        }

        /// <summary>
        /// 是否显示图片项中的“第二张图片”
        /// </summary>
        private Visibility _IsShowPictrue1 = Visibility.Collapsed;

        public Visibility IsShowPictrue1
        {
            get { return _IsShowPictrue1; }
            set
            {
                if (_IsShowPictrue1 != value)
                {
                    _IsShowPictrue1 = value;
                    NotifyPropertyChanged("IsShowPictrue1");
                }
            }
        }

        /// <summary>
        /// 是否显示图片项中的“第三张图片”
        /// </summary>
        private Visibility _IsShowPictrue2 = Visibility.Collapsed;

        public Visibility IsShowPictrue2
        {
            get { return _IsShowPictrue2; }
            set
            {
                if (_IsShowPictrue2 != value)
                {
                    _IsShowPictrue2 = value;
                    NotifyPropertyChanged("IsShowPictrue2");
                }
            }
        }

        /// <summary>
        /// 根据条件隐藏或者显示图片项控件
        /// </summary>
        private void ShowOrHidenCtrOfPictrue()
        {
            //是否显示“未上传”
            if (string.IsNullOrWhiteSpace(_imaName0) && string.IsNullOrWhiteSpace(_imaName1) &&
                string.IsNullOrWhiteSpace(_imaName2))
            {
                IsShowPictrueNotUpload = Visibility.Visible;
            }
            else
            {
                IsShowPictrueNotUpload = Visibility.Collapsed;
            }

            //是否显示 “上传按钮”
            if (!string.IsNullOrWhiteSpace(_imaName0) && !string.IsNullOrWhiteSpace(_imaName1) &&
                !string.IsNullOrWhiteSpace(_imaName2))
            {
                IsShowPictrueUploadBtn = Visibility.Collapsed;
            }
            else
            {
                IsShowPictrueUploadBtn = Visibility.Visible;
            }

            //是否显示第一张图片名字
            if (string.IsNullOrWhiteSpace(_imaName0))
            {
                IsShowPictrue0 = Visibility.Collapsed;
            }
            else
            {
                IsShowPictrue0 = Visibility.Visible;
            }

            //是否显示第2张图片名字
            if (string.IsNullOrWhiteSpace(_imaName1))
            {
                IsShowPictrue1 = Visibility.Collapsed;
            }
            else
            {
                IsShowPictrue1 = Visibility.Visible;
            }

            //是否显示第3张图片名字
            if (string.IsNullOrWhiteSpace(_imaName2))
            {
                IsShowPictrue2 = Visibility.Collapsed;
            }
            else
            {
                IsShowPictrue2 = Visibility.Visible;
            }
        }

        #endregion

        #region 音频

        private string _audioPath;

        /// <summary>
        /// 音频路径
        /// </summary>
        public string audioPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_audioPath))
                {
                    AudioName = "";
                }
                else
                {
                    AudioName = Path.GetFileName(_audioPath);
                }
                return _audioPath;
            }
            set
            {
                if (value != _audioPath)
                {
                    _audioPath = value;
                    if (string.IsNullOrWhiteSpace(_audioPath))
                    {
                        AudioName = "";
                    }
                    else
                    {
                        AudioName = Path.GetFileName(_audioPath);
                    }
                }
            }
        }

        /// <summary>
        /// 音频名
        /// </summary>
        private string _audioName;

        public string AudioName
        {
            get
            {
                if (_audioName == null)
                {
                    _audioName = "";
                }
                ShowOrHidenCtrOfAudio();
                return _audioName;
            }

            set
            {
                if (_audioName != value)
                {
                    _audioName = value;
                    ShowOrHidenCtrOfAudio();
                    NotifyPropertyChanged("AudioName");
                }
            }
        }

        /// <summary>
        /// 是否显示音频项中的上传按钮
        /// </summary>
        private Visibility _IsShowAudioUploadBtn = Visibility.Collapsed;

        public Visibility IsShowAudioUploadBtn
        {
            get { return _IsShowAudioUploadBtn; }
            set
            {
                if (_IsShowAudioUploadBtn != value)
                {
                    _IsShowAudioUploadBtn = value;
                    NotifyPropertyChanged("IsShowAudioUploadBtn");
                }
            }
        }

        /// <summary>
        /// 是否显示音频项中的“未上传”TextBlock
        /// </summary>
        private Visibility _IsShowAudioNotUpload = Visibility.Collapsed;

        public Visibility IsShowAudioNotUpload
        {
            get { return _IsShowAudioNotUpload; }
            set
            {
                if (_IsShowAudioNotUpload != value)
                {
                    _IsShowAudioNotUpload = value;
                    NotifyPropertyChanged("IsShowAudioNotUpload");
                }
            }
        }

        /// <summary>
        /// 是否显示音频项中的“音频”
        /// </summary>
        private Visibility _IsShowAudio = Visibility.Collapsed;

        public Visibility IsShowAudio
        {
            get { return _IsShowAudio; }
            set
            {
                if (_IsShowAudio != value)
                {
                    _IsShowAudio = value;
                    NotifyPropertyChanged("IsShowAudio");
                }
            }
        }


        /// <summary>
        /// 根据条件隐藏或者显示音频项控件
        /// </summary>
        private void ShowOrHidenCtrOfAudio()
        {
            //是否显示“未上传”
            if (string.IsNullOrWhiteSpace(_audioName))
            {
                IsShowAudioNotUpload = Visibility.Visible;
            }
            else
            {
                IsShowAudioNotUpload = Visibility.Collapsed;
            }

            //是否显示 “上传按钮”
            if (string.IsNullOrWhiteSpace(_audioName))
            {
                IsShowAudioUploadBtn = Visibility.Visible;
            }
            else
            {
                IsShowAudioUploadBtn = Visibility.Collapsed;
            }

            //是否显示音频名字
            if (string.IsNullOrWhiteSpace(_audioName))
            {
                IsShowAudio = Visibility.Collapsed;
            }
            else
            {
                IsShowAudio = Visibility.Visible;
            }
        }

        #endregion

        #region 视频

        private string _videoPath;

        /// <summary>
        /// 视频路径
        /// </summary>
        public string videoPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_videoPath))
                {
                    VideoName = "";
                }
                else
                {
                    VideoName = Path.GetFileName(_videoPath);
                }
                return _videoPath;
            }
            set
            {
                if (_videoPath != value)
                {
                    _videoPath = value;
                    if (string.IsNullOrWhiteSpace(_videoPath))
                    {
                        VideoName = "";
                    }
                    else
                    {
                        VideoName = Path.GetFileName(_videoPath);
                    }
                }
            }
        }

        /// <summary>
        /// 视频名
        /// </summary>
        private string _videoName;

        public string VideoName
        {
            get
            {
                if (_videoName == null)
                {
                    _videoName = "";
                }
                ShowOrHidenCtrOfVideo();
                return _videoName;
            }

            set
            {
                if (_videoName != value)
                {
                    _videoName = value;
                    ShowOrHidenCtrOfVideo();
                    NotifyPropertyChanged("VideoName");
                }
            }
        }

        /// <summary>
        /// 是否显示视频项中的上传按钮
        /// </summary>
        private Visibility _IsShowVideoUploadBtn = Visibility.Collapsed;

        public Visibility IsShowVideoUploadBtn
        {
            get { return _IsShowVideoUploadBtn; }
            set
            {
                if (_IsShowVideoUploadBtn != value)
                {
                    _IsShowVideoUploadBtn = value;
                    NotifyPropertyChanged("IsShowVideoUploadBtn");
                }
            }
        }

        /// <summary>
        /// 是否显示视频项中的“未上传”TextBlock
        /// </summary>
        private Visibility _IsShowVideoNotUpload = Visibility.Collapsed;

        public Visibility IsShowVideoNotUpload
        {
            get { return _IsShowVideoNotUpload; }
            set
            {
                if (_IsShowVideoNotUpload != value)
                {
                    _IsShowVideoNotUpload = value;
                    NotifyPropertyChanged("IsShowVideoNotUpload");
                }
            }
        }

        /// <summary>
        /// 是否显示视频项中的“视频”
        /// </summary>
        private Visibility _IsShowVideo = Visibility.Collapsed;

        public Visibility IsShowVideo
        {
            get { return _IsShowVideo; }
            set
            {
                if (_IsShowVideo != value)
                {
                    _IsShowVideo = value;
                    NotifyPropertyChanged("IsShowVideo");
                }
            }
        }


        /// <summary>
        /// 根据条件隐藏或者显示视频项控件
        /// </summary>
        private void ShowOrHidenCtrOfVideo()
        {
            //是否显示“未上传”
            if (string.IsNullOrWhiteSpace(_videoName))
            {
                IsShowVideoNotUpload = Visibility.Visible;
            }
            else
            {
                IsShowVideoNotUpload = Visibility.Collapsed;
            }

            //是否显示 “上传按钮”
            if (!string.IsNullOrWhiteSpace(_videoName))
            {
                IsShowVideoUploadBtn = Visibility.Collapsed;
            }
            else
            {
                IsShowVideoUploadBtn = Visibility.Visible;
            }

            //是否显示视频频名字
            if (string.IsNullOrWhiteSpace(_videoName))
            {
                IsShowVideo = Visibility.Collapsed;
            }
            else
            {
                IsShowVideo = Visibility.Visible;
            }
        }

        #endregion

        #region 文档

        private string _docPath0;

        /// <summary>
        /// 文档路径1
        /// </summary>
        public string docPath0
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_docPath0))
                {
                    DocName0 = "";
                }
                else
                {
                    DocName0 = Path.GetFileName(_docPath0);
                }

                return _docPath0;
            }
            set
            {
                if (value != _docPath0)
                {
                    _docPath0 = value;
                    if (string.IsNullOrWhiteSpace(_docPath0))
                    {
                        DocName0 = "";
                    }
                    else
                    {
                        DocName0 = Path.GetFileName(_docPath0);
                    }
                }
            }
        }

        private string _docPath1;

        /// <summary>
        /// 文档路径2
        /// </summary>
        public string docPath1
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_docPath1))
                {
                    DocName1 = "";
                }
                else
                {
                    DocName1 = Path.GetFileName(_docPath1);
                }

                return _docPath1;
            }
            set
            {
                if (value != _docPath1)
                {
                    _docPath1 = value;
                    if (string.IsNullOrWhiteSpace(_docPath1))
                    {
                        DocName1 = "";
                    }
                    else
                    {
                        DocName1 = Path.GetFileName(_docPath1);
                    }
                }
            }
        }

        /// <summary>
        /// 文档名1
        /// </summary>
        private string _docName0;

        public string DocName0
        {
            get
            {
                if (_docName0 == null)
                {
                    _docName0 = "";
                }
                ShowOrHidenCtrOfDoc();
                return _docName0;
            }

            set
            {
                if (_docName0 != value)
                {
                    _docName0 = value;
                    ShowOrHidenCtrOfDoc();
                    NotifyPropertyChanged("DocName0");
                }
            }
        }


        /// <summary>
        /// 文档名1
        /// </summary>
        private string _docName1;

        public string DocName1
        {
            get
            {
                if (_docName1 == null)
                {
                    _docName1 = "";
                }
                ShowOrHidenCtrOfDoc();
                return _docName1;
            }

            set
            {
                if (_docName1 != value)
                {
                    _docName1 = value;
                    ShowOrHidenCtrOfDoc();
                    NotifyPropertyChanged("DocName1");
                }
            }
        }

        /// <summary>
        /// 是否显示文档项中的上传按钮
        /// </summary>
        private Visibility _IsShowDocUploadBtn = Visibility.Collapsed;

        public Visibility IsShowDocUploadBtn
        {
            get { return _IsShowDocUploadBtn; }
            set
            {
                if (_IsShowDocUploadBtn != value)
                {
                    _IsShowDocUploadBtn = value;
                    NotifyPropertyChanged("IsShowDocUploadBtn");
                }
            }
        }

        /// <summary>
        /// 是否显示文档项中的“未上传”TextBlock
        /// </summary>
        private Visibility _IsShowDocNotUpload = Visibility.Collapsed;

        public Visibility IsShowDocNotUpload
        {
            get { return _IsShowDocNotUpload; }
            set
            {
                if (_IsShowDocNotUpload != value)
                {
                    _IsShowDocNotUpload = value;
                    NotifyPropertyChanged("IsShowDocNotUpload");
                }
            }
        }

        /// <summary>
        /// 是否显示文档项中的“文档”
        /// </summary>
        private Visibility _IsShowDoc0 = Visibility.Collapsed;

        public Visibility IsShowDoc0
        {
            get { return _IsShowDoc0; }
            set
            {
                if (_IsShowDoc0 != value)
                {
                    _IsShowDoc0 = value;
                    NotifyPropertyChanged("IsShowDoc0");
                }
            }
        }

        /// <summary>
        /// 是否显示文档项中的“文档”
        /// </summary>
        private Visibility _IsShowDoc1 = Visibility.Collapsed;

        public Visibility IsShowDoc1
        {
            get { return _IsShowDoc1; }
            set
            {
                if (_IsShowDoc1 != value)
                {
                    _IsShowDoc1 = value;
                    NotifyPropertyChanged("IsShowDoc1");
                }
            }
        }


        /// <summary>
        /// 根据条件隐藏或者显示文档项控件
        /// </summary>
        private void ShowOrHidenCtrOfDoc()
        {
            //是否显示“未上传”
            if (string.IsNullOrWhiteSpace(_docName0) && string.IsNullOrWhiteSpace(_docName1))
            {
                IsShowDocNotUpload = Visibility.Visible;
            }
            else
            {
                IsShowDocNotUpload = Visibility.Collapsed;
            }

            //是否显示 “上传按钮”
            if (!string.IsNullOrWhiteSpace(_docName0) && !string.IsNullOrWhiteSpace(_docName1))
            {
                IsShowDocUploadBtn = Visibility.Collapsed;
            }
            else
            {
                IsShowDocUploadBtn = Visibility.Visible;
            }

            //是否显示文档名字1
            if (string.IsNullOrWhiteSpace(_docName0))
            {
                IsShowDoc0 = Visibility.Collapsed;
            }
            else
            {
                IsShowDoc0 = Visibility.Visible;
            }

            //是否显示文档名字2
            if (string.IsNullOrWhiteSpace(_docName1))
            {
                IsShowDoc1 = Visibility.Collapsed;
            }
            else
            {
                IsShowDoc1 = Visibility.Visible;
            }
        }

        #endregion

        public bool IsRemoved { get; set; }

        private string _strID;

        /// <summary>
        /// ID
        /// </summary>
        [DefaultValue("")]
        public string ID
        {
            get { return _strID; }
            set
            {
                if (_strID != value)
                {
                    _strID = value;
                    NotifyPropertyChanged("strID");
                }
            }
        }

        private string _strNo;

        /// <summary>
        /// 序号
        /// </summary>
        public string strNo
        {
            get { return _strNo; }
            set
            {
                if (_strNo != value)
                {
                    _strNo = value;
                    NotifyPropertyChanged("strNo");
                }
            }
        }


        private string _projectId;
        ///// <summary>
        ///// 项目ID
        ///// </summary>
        public string projectId
        {
            get { return _projectId; }
            set
            {
                if (value == "1")
                {
                    SelectProject = Listprojecct[0];
                }
                else if (value == "2")
                {
                    SelectProject = Listprojecct[1];
                }
                else if (value == "3")
                {
                    SelectProject = Listprojecct[2];
                }
            }
        }

        private string _categoryId;

        /// <summary>
        /// 类别ID
        /// </summary>
        public string categoryId
        {
            get { return _categoryId; }
            set
            {
                if (value == "4")
                {
                    SelectCategory = Listcategory[0];
                }
                else if (value == "5")
                {
                    SelectCategory = Listcategory[1];
                }
                else if (value == "6")
                {
                    SelectCategory = Listcategory[2];
                }
                else if (value == "7")
                {
                    SelectCategory = Listcategory[0];
                }
                else if (value == "8")
                {
                    SelectCategory = Listcategory[1];
                }
                else if (value == "9")
                {
                    SelectCategory = Listcategory[0];
                }
                else if (value == "10")
                {
                    SelectCategory = Listcategory[1];
                }
            }
        }

        private string _contentDes;

        /// <summary>
        /// 内容描述
        /// </summary>
        public string ContentDes
        {
            get { return _contentDes; }
            set
            {
                if (_contentDes != value)
                {
                    _contentDes = value;
                    NotifyPropertyChanged("ContentDes");
                }
            }
        }

        /// <summary>
        /// 答复内容
        /// </summary>
        public string repContent { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string picture { get; set; }

        /// <summary>
        /// 音频
        /// </summary>
        public string audio { get; set; }

        /// <summary>
        /// 视频
        /// </summary>
        public string video { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public string taskId { get; set; }


        private ObservableCollection<MProject> _listprojecct = null;

        /// <summary>
        /// 项目列表
        /// </summary>
        public ObservableCollection<MProject> Listprojecct
        {
            get
            {
                if (_listprojecct == null)
                {
                    _listprojecct = new ObservableCollection<MProject>()
                    {
                        new MProject() {projectName = "工作亮点", projectId = "1"},
                        new MProject() {projectName = "需求", projectId = "2"},
                        new MProject() {projectName = "建议", projectId = "3"}
                    };
                }

                return _listprojecct;
            }
            set
            {
                if (_listprojecct != value)
                {
                    _listprojecct = value;
                    NotifyPropertyChanged("Listprojecct");
                }
            }
        }

        private ObservableCollection<MCategory> _listcategory;

        /// <summary>
        /// 类别列表
        /// </summary>
        public ObservableCollection<MCategory> Listcategory
        {
            get
            {
                if (_listcategory == null)
                {
                    _listcategory = new ObservableCollection<MCategory>
                    {
                        new MCategory() {categoryName = "欣喜服务", categoryId = "4"},
                        new MCategory() {categoryName = "服务营销", categoryId = "5"},
                        new MCategory() {categoryName = "收益提升", categoryId = "6"}
                    };
                }
                return _listcategory;
            }
            set
            {
                if (_listcategory != value)
                {
                    _listcategory = value;
                    NotifyPropertyChanged("Listcategory");
                }
            }
        }


        private MProject _SelectProject;

        /// <summary>
        /// 当前选中的项目
        /// </summary>
        public MProject SelectProject
        {
            get
            {
                if (_SelectProject != null)
                {
                    _projectId = _SelectProject.projectId;
                }

                return _listprojecct.FirstOrDefault(n => n.projectId == _projectId);
            }
            set
            {
                if (_SelectProject != value && value != null)
                {
                    _SelectProject = value;

                    SetCategoryList(_SelectProject);
                    NotifyPropertyChanged("SelectProject");
                }
            }
        }

        private MCategory _selectCategory;

        /// <summary>
        /// 当前选中的类别
        /// </summary>
        public MCategory SelectCategory
        {
            get
            {
                if (_selectCategory != null)
                {
                    _categoryId = _selectCategory.categoryId;
                }
                return _listcategory.FirstOrDefault(n => n.categoryId == _categoryId);
            }
            set
            {
                if (_selectCategory != value && value != null)
                {
                    _selectCategory = value;

                    if (_selectCategory != null)
                    {
                        _categoryId = _selectCategory.categoryId;
                    }
                    NotifyPropertyChanged("SelectCategory");
                }
            }
        }

        private void SetCategoryList(MProject SelectProject)
        {
            if (SelectProject == null) return;
            switch (SelectProject.projectId)
            {
                case GlobalValue._WORK_LIGHTS_SPOT:
                    Listcategory = new ObservableCollection<MCategory>
                    {
                        new MCategory() {categoryName = "欣喜服务", categoryId = "4"},
                        new MCategory() {categoryName = "服务营销", categoryId = "5"},
                        new MCategory() {categoryName = "收益提升", categoryId = "6"}
                    };
                    //SelectCategory = Listcategory[0];
                    break;
                case GlobalValue._WORK_LIGHTS_NEED:
                    Listcategory = new ObservableCollection<MCategory>
                    {
                        new MCategory() {categoryName = "业务指导", categoryId = "7"},
                        new MCategory() {categoryName = "CS改善", categoryId = "8"}
                    };
                    //SelectCategory = Listcategory[0];
                    break;
                case GlobalValue._WORK_LIGHTS_SUGGEST:
                    Listcategory = new ObservableCollection<MCategory>
                    {
                        new MCategory() {categoryName = "硬件标准", categoryId = "9"},
                        new MCategory() {categoryName = "活动策划", categoryId = "10"}
                    };
                    //SelectCategory = Listcategory[0];
                    break;
            }
        }

        public MWorkLightspot()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 用于通知属性的改变
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    /// <summary>
    /// 工作亮点-项目
    /// </summary>
    [Serializable]
    public class MProject
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public string projectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectName { get; set; }
    }


    /// <summary>
    /// 工作亮点-类别
    /// </summary>
    [Serializable]
    public class MCategory
    {
        /// <summary>
        /// 类别id
        /// </summary>
        public string categoryId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string categoryName { get; set; }
    }
}