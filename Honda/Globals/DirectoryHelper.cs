using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.Globals
{
    /*
     设计思路：该应用要离线使用，故需保存一些商店的离线数据，
     * 根据每个商店的id号创建一个文件夹用以保存数据
     * 
     */

    public class DirectoryHelper
    {
        #region 文件存储的目录

        /// <summary>
        /// 系统缓存文件保存根目录
        /// </summary>
        public const string APP_DATA_BASE_PATH = @"C:\Users\Public\Documents\Honda";

        /// <summary>
        /// 应用图片保存的根目录
        /// </summary>
        private const string CAMERA_PICTRUE_PATH = @"C:\Users\Public\Pictures\";

        /// <summary>
        /// 相机拍照图片名
        /// </summary>
        public string newPictrueName
        {
            get
            {
                string path = CAMERA_PICTRUE_PATH + DateTime.Now.ToString("yyyymmddhhmmss") + ".png";
                return path;
            }
        }

        public string GetFileNewName(string fileName)
        {
            string newFileName = null;
            //扩展名
            //string extensionName =  Path.GetExtension(fileName); 
            //路径不包括文件名
            string _filePath = Path.GetDirectoryName(fileName);

            newFileName = _filePath + DateTime.Now.ToString("yyyymmddhhmmss") + new Random().Next(3, 300).ToString() +
                          new Random().Next(3, 300).ToString() + Path.GetFileName(fileName);
            // newFileName = MD5.Create(newFileName).ToString();
            return newFileName;
        }

        /// <summary>
        /// 系统缓存文件-用户基本信息
        /// </summary>
        public string CUSTOMER_PATH = APP_DATA_BASE_PATH + @"\Customer\Customer.data";

        /// <summary>
        /// 图片所存放的文件夹
        /// </summary>
        public string pictrue_base_path
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(STORE_PATH))
                {
                    path = STORE_PATH + @"\pictrue\";

                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                    {
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                    }
                }

                return path;
            }
        }

        public string PictruePath
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(pictrue_base_path))
                {
                    path = pictrue_base_path + @"\pictrueList.data";
                }

                return path;
            }
        }


        //public string STORE_PATH = APP_DATA_BASE_PATH + @"\1283402";
        private string STORE_PATH = APP_DATA_BASE_PATH;
            

        /// <summary>
        /// 签名图片所存放的文件夹
        /// </summary>
        public string Signature_base_path
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(STORE_PATH))
                {
                    path = STORE_PATH + @"\signature";
                }

                return path;
            }
        }


        /// <summary>
        /// 评价表生产页面中-评价员1签名存放路径
        /// </summary>
        public string Signature_produce_valuator1_path
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(Signature_base_path))
                {
                    path = Signature_base_path + @"\signature_valuator1.bmp";
                    path = GetFileNewName(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 评价表生产页面中-评价员2签名存放路径
        /// </summary>
        public string Signature_produce_valuator2_path
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(Signature_base_path))
                {
                    path = Signature_base_path + @"\signature_valuator2.bmp";
                    path = GetFileNewName(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 评价表生产页面中--零部件经理签名存放路径
        /// </summary>
        public string Signature_produce_componentManager_path
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(Signature_base_path))
                {
                    path = Signature_base_path + @"\signature_component.bmp";
                    path = GetFileNewName(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 评价表生产页面中--服务经理签名存放路径
        /// </summary>
        public string Signature_produce_servesManager_path
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(Signature_base_path))
                {
                    path = Signature_base_path + @"\signature_serve.bmp";
                    path = GetFileNewName(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 评价表生产页面中--总经理签名存放路径
        /// </summary>
        public string Signature_produce_generalManager_path
        {
            get
            {
                string path = null;
                if (!string.IsNullOrWhiteSpace(Signature_base_path))
                {
                    path = Signature_base_path + @"\signature.bmp";
                    path = GetFileNewName(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 工作亮点
        /// </summary>
        public string WorkLightspotAndIdeaListPath
        {
            get
            {
                string path = WorkLightspotAndIdeaPath + "WorkLightspot.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 商务政策数据路径
        /// </summary>
        public string BusinessPolicyList1Path
        {
            get
            {
                string path = BusinessPolicyDir + "BusinessPolicyList1Path.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 商务政策数据路径
        /// </summary>
        public string BusinessPolicyList2Path
        {
            get
            {
                string path = BusinessPolicyDir + "BusinessPolicyList2Path.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }
        /// <summary>
        /// 商务政策数据路径
        /// </summary>
        public string BusinessPolicyList2_1Path
        {
            get
            {
                string path = BusinessPolicyDir + "BusinessPolicyList2_1Path.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 商务政策数据路径
        /// </summary>
        public string BusinessPolicyList3Path
        {
            get
            {
                string path = BusinessPolicyDir + "BusinessPolicyList3Path.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 改善计划
        /// </summary>
        public string ImprovePlanPath
        {
            get
            {
                string path = ImprovePlanDir + "ImprovePlanPath.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 改善计划（忽略项）
        /// </summary>
        public string ImprovePlanIgnorePath
        {
            get
            {
                string path = ImprovePlanDir + "ImprovePlanIgnorePath.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 工作亮点签名
        /// </summary>
        public string WorkLightspotAndIdeaSignatureList
        {
            get
            {
                string path = WorkLightspotAndIdeaPath + "WorkSignatureList.data";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 工作亮点
        /// </summary>
        public string WorkLightspotAndIdeaPath
        {
            get
            {
                string path = STORE_PATH + "\\" + "WorkLight\\";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        /// <summary>
        /// 商务政策
        /// </summary>
       public string BusinessPolicyDir
        {
            get
            {
                string path = STORE_PATH + "\\" + "BusinessPolicy\\";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }


       /// <summary>
       /// 改善计划目录
       /// </summary>
       public string ImprovePlanDir
       {
           get
           {
               string path = STORE_PATH + "\\" + "ImprovePlan\\";
               if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
               {
                   Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
               }
               return path;
           }
       }

        /// <summary>
        /// 巡回总结报表文件
        /// </summary>
        public string SummaryPath
        {
            get
            {
                string path = STORE_PATH + "\\" + "Summary\\";
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                }
                return path;
            }
        }

        #endregion

        public static DirectoryHelper INSTANCE = new DirectoryHelper();

        private DirectoryHelper()
        {
        }

        /// <summary>
        /// 创建文件目录:当保存店数据的时候创建文件夹
        /// </summary>
        /// <param name="StoreId"></param>
        public void CreateStoreFileDirectory(string StoreId)
        {
            string filePath = APP_DATA_BASE_PATH + @"\" + StoreId;
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));
            }
            STORE_PATH = filePath;
        }


        /// <summary>
        /// 根据店的id号获取存放该店的目录路径
        /// </summary>
        /// <param name="StoreId"></param>
        /// <returns></returns>
        public string GetFileDirectoryPath(string StoreId)
        {
            string filePath = APP_DATA_BASE_PATH + @"\" + StoreId;
            return filePath;
        }

        /// <summary>
        /// 根据店id号，删除该店里面的所有缓存数据
        /// </summary>
        public void ClearStoreFile(string StoreId)
        {
            string filePath = APP_DATA_BASE_PATH + @"\" + StoreId;
            DirectoryInfo di = new DirectoryInfo(filePath);
            di.Delete(true);
        }

        #region 保存当前特约店列表到本地

        // 特约店的文件保存路径

        public string EVALUATION_SOURCE
        {
            get { return STORE_PATH + @"\" + "EVALUATION_SOURCE.data"; }
            set { }
        }


        public string EVALUATION_SOURCE_MENU 
        {
            get { return STORE_PATH + @"\" + "EVALUATION_SOURCE_MENU.data"; }
            set { }
        }

        public string STORE_PATH_DATA //店列表
        {
            get { return STORE_PATH + @"\" + "STORE_PATH_DATA.data"; }
            set { }
        }

        public string SIGNATURE_DATA //签名signature
        {
            get { return STORE_PATH + @"\" + "SIGNATURE_DATA.data"; }
            set { }
        }

        public string STORE_FORM_STATE //店状态
        {
            get { return STORE_PATH + @"\" + "MStore_state.data"; }
            set { }
        }

        public string HARDWARE_LIST //硬件
        {
            get { return STORE_PATH + @"\" + "hardware_Source.data"; }
            set { }
        }

        public string PERSONNEL_LIST //人员
        {
            get { return STORE_PATH + @"\" + "personnel_Source.data"; }
            set { }
        }


        public string RECEIVE_GUESTS_FLOW_LIST //接待流程
        {
            get { return STORE_PATH + @"\" + "ReceiveGuestsFlow_Source.data"; }
            set { }
        }

        public string ALL_UPLOAD_FILE_DATA //上传的文件数据
        {
            get { return STORE_PATH + @"\" + "UploadFile.data"; }
            set { }
        }

        public string QUICK_SERVICE_LIST //快修流程
        {
            get { return STORE_PATH + @"\" + "QuickService_Source.data"; }
            set { }
        }

        public string BP_LIST //bp流程
        {
            get { return STORE_PATH + @"\" + "BP_Source.data"; }
            set { }
        }

        public string DATA_ACCURACY_LIST //数据准确性
        {
            get { return STORE_PATH + @"\" + "Data_Source.data"; }
            set { }
        }


        public string SATISFACTION_LIST //满意度管理
        {
            get { return STORE_PATH + @"\" + "SatisfactionManage.data"; }
            set { }
        }

        public string CLIENT_MANAGE_LIST //客户维系管理
        {
            get { return STORE_PATH + @"\" + "ClientMange.data"; }
            set { }
        }

        public string FACTORYMANAGE_LIST //来厂促进管理
        {
            get { return STORE_PATH + @"\" + "FactoryMange.data"; }
            set { }
        }

        public string EMPHASIS_BUSINESS_LIST //重点业务
        {
            get { return STORE_PATH + @"\" + "EmphasisBusiness.data"; }
            set { }
        }

        public string BASE_BUSINESS_LIST //基础业务
        {
            get { return STORE_PATH + @"\" + "BaseBusiness.data"; }
            set { }
        }

        public string MAR_MARKETING_BUSINESS_LIST //营销管理
        {
            get { return STORE_PATH + @"\" + "MarMarketingManage.data"; }
            set { }
        }

        public string REPERTORYMANAGE_LIST //库存管理M_StoreManagementSource
        {
            get { return STORE_PATH + @"\" + "M_RepertoryManage.data"; }
            set { }
        }

        public string STORE_MANAGEMENT_LIST //仓库管理
        {
            get { return STORE_PATH + @"\" + "StoreManagement.data"; }
            set { }
        }

        public string PERSONNELEX_LIST //人员
        {
            get { return STORE_PATH + @"\" + "personnel_ex.data"; }
            set { }
        }

        public string SUGGEST_PLUS_PROJECT_LIST //加分项
        {
            get { return STORE_PATH + @"\" + "Suggest_PlusProject.data"; }
            set { }
        }

        public string SUGGEST_WAREHOUSE_LIST //五星级仓库
        {
            get { return STORE_PATH + @"\" + "Suggest_Warehouse.data"; }
            set { }
        }


        public string STORE_NEED_LOAD_OFFLINE_DATA //五星级仓库
        {
            get { return APP_DATA_BASE_PATH + @"\" + "StoreNeedOfflineState.data"; }
            set { }
        }

        public string BI_REPORT_URL_INFO
        {
            get { return APP_DATA_BASE_PATH + @"\" + "BiReportUrlInfo.data"; }
        }

        public static string DOWNLOAD_ERROR_DIRECTORY
        {
            get { return APP_DATA_BASE_PATH + @"\DOWNLOADERROR"; }
            set { }
        }

        /// <summary>
        /// 保存当前特约店列表到本地
        /// </summary>
        private void SaveForm()
        {
            //try
            //{
            //    //创建该特约店的保存的目录，并且设置当前特约店的保存目录
            //    CreateStoreFileDirectory(DMStoreTour.INSTANCE.CurrentMStore.shopId);

            //    //保存特约店评价列表
            //    SerialHelp.SerialObject(EVALUATION_SOURCE, DMUnivesalEvaluate.INSTANCE.ListBaseUniversal);
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("保存特约店数据错误\n" + ex.Message);
            //}
        }

        #endregion
    }
}