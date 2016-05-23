using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.Globals
{
    /// <summary>
    /// 调用系统软件打开文件
    /// </summary>
    public class FileHelp
    {
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="filePath"> 文件路径</param>
        public static void OpenFile(string filePath)
        {
            try
            {
                //判断文件是否存在
                if (filePath != null && File.Exists(filePath))
                {
                    System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo(filePath);
                    System.Diagnostics.Process Pro = System.Diagnostics.Process.Start(Info);
                }
                else
                {
                    var msg = string.Format("路径{0}文件不存在", filePath);
                    Debug.WriteLine(msg);
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("路径{0}文件不存在", ex.Message));
                MessageBox.Show(ex.Message);
            }
        }
    }
}