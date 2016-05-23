using Honda.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib
{
    /*    
     * 查询未完成任务接口  
     */

    public class ReqGetUnfinishedTask : HttpBase
    {
        private string _logId;

        /// <summary>
        /// 未完成的任务列表
        /// </summary>
        public ObservableCollection<MTask> listTask;

        /// <summary>
        /// 已过期总数
        /// </summary>
        public string haveExpiredTotal { get; set; }

        /// <summary>
        /// 即将过期总数
        /// </summary>
        public string willExpireTotal { get; set; }

        private string _jsonTxt;

        public ReqGetUnfinishedTask(string logId, Action<object> callback)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;
            m_strInterfaceUrl =
                Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqGetUnfinishedTask);
                //"/imp/tourEvaluation/queryTaskStatus";
            _logId = logId;
            BuildParam();
        }

        public override void BuildParam()
        {
            try
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("logId"); //巡回员ID
                m_jsonWriter.WriteValue(_logId);
                m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
                m_jsonWriter.WriteValue(GetMD5Str(_logId));
                m_jsonWriter.WritePropertyName("timestamp"); //时间戳
                m_jsonWriter.WriteValue(_timestamp);

                m_jsonWriter.WriteEndObject();
                _jsonTxt = m_strBuilderJson.ToString();
                JObject obj = JObject.Parse(_jsonTxt);
                _jsonTxt = PROTOCAL_PARAM + _jsonTxt;
                m_byteRequestData = Encoding.UTF8.GetBytes(_jsonTxt);
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                Debug.WriteLine(err);
            }
        }

        protected override void ConvertPostDataToBytesData()
        {
        }

        public override void ParseParam()
        {
            string str = Encoding.UTF8.GetString(m_byteResponseData);

            try
            {
                JObject resultObject = JObject.Parse(str);
                string code = resultObject["code"].ToString();
                string msg = resultObject["message"].ToString();
                haveExpiredTotal = resultObject["hasExpiredTotal"].ToString();
                willExpireTotal = resultObject["wilExpireTotal"].ToString();
                var result = resultObject["result"];

                if (code == SUCCESS_CODE)
                {
                    m_bIsSuccess = true;
                }
                else
                {
                    m_bIsSuccess = false;
                    m_strErrorMsg = msg;
                }

                if (m_bIsSuccess)
                {
                    listTask = new ObservableCollection<MTask>();
                    JArray dataList = JArray.Parse(result.ToString());
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        JObject jobj = JObject.Parse(dataList[i].ToString());
                        MTask task;
                        string TaskName = jobj["taskName"].ToString();
                        string TaskStatus = jobj["taskStauts"].ToString();
                        if (TaskStatus == "0")
                        {
                            task = new MTask(TaskName, ENUM_TASK.outDate);
                        }
                        else
                        {
                            task = new MTask(TaskName, ENUM_TASK.willOutDate);
                        }

                        task.TaskID = jobj["taskId"].ToString();
                        listTask.Add(task);
                    }
                }
            }
            catch (System.Exception ex)
            {
                string UriMessage = "请求地址： " + m_strRequestUrl + "\r\n";
                string requestMsg = "请求参数：" + _jsonTxt + "\r\n";
                string errMsg = UriMessage + requestMsg + m_strErrorMsg + "\r\n" + ex.Message;
                Debug.WriteLine(errMsg);
            }
        }
    }
}