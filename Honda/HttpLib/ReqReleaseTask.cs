using Honda.Model;
using Honda.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib
{
    /*    
     * 发布任务接口  - by luyonghe 2014/11/4    
     */

    public class ReqReleaseTask : HttpBase
    {
        private MTask _task;
        private string _jsonTxt;

        public ReqReleaseTask(MTask task, Action<object> callback)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqReleaseTask);
                //"/imp/tourEvaluation/releaseTask";
            //m_strInterfaceUrl = "/impInterface/tourEvaluation/releaseTask";
            _task = task;
            BuildParam();
        }

        public override void BuildParam()
        {
            try
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("logId"); //巡回员ID
                m_jsonWriter.WriteValue(_task.Account);
                m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
                m_jsonWriter.WriteValue(GetMD5Str(_task.Account));
                m_jsonWriter.WritePropertyName("timestamp"); //时间戳
                m_jsonWriter.WriteValue(_timestamp);

                m_jsonWriter.WritePropertyName("data");
                m_jsonWriter.WriteStartArray();
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("creatorId"); //任务创建人ID
                m_jsonWriter.WriteValue(DMUser.INSTANCE.CurrentMUser.UserAccount);
                m_jsonWriter.WritePropertyName("taskName"); //任务名称
                m_jsonWriter.WriteValue(_task.TaskName);
                m_jsonWriter.WritePropertyName("taskType"); //任务类型
                m_jsonWriter.WriteValue(_task.taskType);
                m_jsonWriter.WritePropertyName("executorId"); //执行该评价任务的巡回员Id
                m_jsonWriter.WriteValue(_task.executorId);
                m_jsonWriter.WritePropertyName("taskStartTime"); //任务开始时间
                m_jsonWriter.WriteValue(_task.TaskBeginTime);
                m_jsonWriter.WritePropertyName("taskEndTime"); //任务结束时间
                m_jsonWriter.WriteValue(_task.TaskEndTime);
                m_jsonWriter.WritePropertyName("shopId"); //执行该任务的特约店ID
                m_jsonWriter.WriteValue(_task.shopId);
                m_jsonWriter.WritePropertyName("shopName"); //执行该任务的特约店名称
                m_jsonWriter.WriteValue(_task.ShopName);
                m_jsonWriter.WritePropertyName("taskDescription"); //任务描述
                m_jsonWriter.WriteValue(_task.taskDescription);
                m_jsonWriter.WriteEndObject();
                m_jsonWriter.WriteEndArray();
                m_jsonWriter.WriteEndObject();

                _jsonTxt = m_strBuilderJson.ToString();
                string tsss = _jsonTxt;
                JObject jobj = JObject.Parse(tsss);
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

                if (code == SUCCESS_CODE)
                {
                    m_bIsSuccess = true;
                }
                else
                {
                    m_bIsSuccess = false;
                    m_strErrorMsg = msg;
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