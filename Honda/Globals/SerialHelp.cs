using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Globals
{
    /// <summary>
    /// 对象序列化/ 反序列化 工具类
    /// </summary>
    public class SerialHelp
    {
        /// <summary>
        /// 序列化问题管理器
        /// </summary>
        /// <param name="filePath">保存路径(包括文件名)</param>
        /// <param name="objGraph">序列化对象</param>
        public static void SerialObject(string filePath, object objGraph)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = new FileStream(filePath,
                    FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, objGraph);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SerialHelp", string.Format("保存序列化文件{0} 失败：{1}", filePath, ex.Message));
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="fileName">文件保存的路径</param>
        public static object LoadFromBinaryFile(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                else
                {
                    BinaryFormatter binFormat = new BinaryFormatter();
                    using (Stream fStream = File.OpenRead(fileName))
                    {
                        return binFormat.Deserialize(fStream);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("SerialHelp", string.Format("提取序列化文件{0} 失败：{1}", fileName, ex.Message));
                return null;
            }
        }

        /// <summary>
        /// CheckFile
        /// </summary>
        /// <param name="fileName">文件保存的路径</param>
        public static bool CheckFileExsists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}