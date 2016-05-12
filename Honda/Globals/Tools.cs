using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Honda.Globals
{
    public class Tools
    {
        public static bool IsConnected()
        {
            return true;
        }

        #region 调出或者隐藏虚拟键盘

        /// <summary>
        /// 检测虚拟键盘是否正在调出
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool processIsRunning(string process)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(process);

            return (processes.Length != 0);
        }

        /// <summary>
        /// 打开虚拟键盘
        /// </summary>
        public static void OpenKeyBoard()
        {
            if (!processIsRunning("TabTip"))
            {
                Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
            }
        }

        /// <summary>
        /// 关闭虚拟键盘
        /// </summary>
        public static void CloseKeyBoard()
        {
            if (processIsRunning("TabTip"))
            {
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("TabTip");
                foreach (System.Diagnostics.Process proc in processes)
                {
                    proc.Kill();
                }
            }
        }

        #endregion

        #region 深拷贝对象

        /// <summary>
        /// 深拷贝对象（前提是T 要标记[Serializable]）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(objectStream);
            }
        }

        /// <summary>
        /// 深拷贝列表（前提是T 要标记[Serializable]）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourcelist"></param>
        /// <returns></returns>
        public static List<T> CloneList<T>(List<T> sourcelist)
        {
            List<T> list = new List<T>();
            foreach (T t in sourcelist)
            {
                T tt = Clone<T>(t);
                list.Add(tt);
            }
            return list;
        }

        #endregion

        public static byte[] Bitmapimagetobytearray(BitmapImage bmp)
        {
            byte[] bytearray = null;

            try
            {
                Stream smarket = bmp.StreamSource;

                if (smarket != null && smarket.Length > 0)
                {
                    //很重要，因为position经常位于stream的末尾，导致下面读取到的长度为0。
                    smarket.Position = 0;

                    using (BinaryReader br = new BinaryReader(smarket))
                    {
                        bytearray = br.ReadBytes((int) smarket.Length);
                    }
                }
            }
            catch
            {
                //other exception handling
            }

            return bytearray;
        }

        public static MainWindow mainWindow;


        public static bool DeleteDir(string strPath)
        {
            try
            {
                //清空空格
                strPath = @strPath.Trim().ToString();
                //判断文件夹是否存在
                if (System.IO.Directory.Exists(strPath))
                {
                    string[] strDirs = System.IO.Directory.GetDirectories(strPath);
                    //获得文件数组
                    string[] strFiles = System.IO.Directory.GetFiles(strPath);

                    //遍历所有子文件夹
                    foreach (string strFile in strFiles)
                    {
                        //删除所有文件夹
                        System.IO.File.Delete(strFile);
                    }

                    foreach (string strDir in strDirs)
                    {
                        //删除所有文件夹
                        System.IO.Directory.Delete(strDir, true);
                    }
                }

                return true;
            }
            catch (Exception exp)
            {
                Debug.Write(exp.Message.ToString());
                return false;
            }
        }

        public static void MakeSureDirectory(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
        }

        public static string GetConfigValue(CONFIG_SETTING configkey)
        {
            switch (configkey)
            {
                case CONFIG_SETTING.IMP_ServerHost:
                    return ConfigurationManager.AppSettings["IMP_ServerHost"];

                case CONFIG_SETTING.IMP_ServerPort:
                    return ConfigurationManager.AppSettings["IMP_ServerPort"];

                case CONFIG_SETTING.IMP_VIEW_STOREKPI_URL:
                    return ConfigurationManager.AppSettings["IMP_VIEW_STOREKPI_URL"];

                case CONFIG_SETTING.IMP_IF_ReqCommitWorkLightspotList:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqCommitWorkLightspotList"];

                case CONFIG_SETTING.IMP_IF_ReqExportFile:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqExportFile"];

                case CONFIG_SETTING.IMP_IF_ReqGetFormList:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqGetFormList"];

                case CONFIG_SETTING.IMP_IF_ReqGetImproveCheckList:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqGetImproveCheckList"];

                case CONFIG_SETTING.IMP_IF_ReqGetImproveList:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqGetImproveList"];

                case CONFIG_SETTING.IMP_IF_ReqGetStoreList:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqGetStoreList"];

                case CONFIG_SETTING.IMP_IF_ReqGetUnfinishedTask:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqGetUnfinishedTask"];

                case CONFIG_SETTING.IMP_IF_ReqGetWorkLightspotAndIdeaList:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqGetWorkLightspotAndIdeaList"];

                case CONFIG_SETTING.IMP_IF_ReqLogin:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqLogin"];

                case CONFIG_SETTING.IMP_IF_ReqGetEvaluateMenu:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqGetEvaluateMenu"];

                case CONFIG_SETTING.IMP_IF_ReqReleaseTask:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqReleaseTask"];

                case CONFIG_SETTING.IMP_IF_ReqTestMulityForm:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqTestMulityForm"];

                case CONFIG_SETTING.IMP_IF_ReqUploadBusinessPolicy:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqUploadBusinessPolicy"];

                case CONFIG_SETTING.IMP_IF_ReqUploadFormScore:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqUploadFormScore"];

                case CONFIG_SETTING.IMP_IF_ReqUploadImproves:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqUploadImproves"];

                case CONFIG_SETTING.IMP_IF_ReqUploadImprovesCheck:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqUploadImprovesCheck"];

                case CONFIG_SETTING.IMP_IF_ReqUploadSummary:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqUploadSummary"];

                case CONFIG_SETTING.IMP_IF_ReqUploadtWorkLightspotAndIdeaList:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqUploadtWorkLightspotAndIdeaList"];

                case CONFIG_SETTING.IMP_IF_ReqBIReportPath:
                    return ConfigurationManager.AppSettings["IMP_IF_ReqBIReportPath"];

                default:
                    return string.Empty;
            }
        }
    }
}