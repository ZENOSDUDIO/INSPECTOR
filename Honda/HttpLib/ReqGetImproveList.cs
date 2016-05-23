using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.Model;
using Honda.ViewModel;
using System.Collections.ObjectModel;

namespace Honda.HttpLib
{
    /// <summary>
    /// 获取改善计划接口
    /// <author>xiang</author>
    ///<date>2014/12/8</date>
    ///<summary>
    public class ReqGetImproveList : HttpBase
    {
        private string _jsonTxt;
        private string _code;

        public ObservableCollection<MImprove> Items = new ObservableCollection<MImprove>();

        public ReqGetImproveList(Action<Object> act)
            : base(RequestType.POST, act)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;

            //打包版本
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqGetImproveList);
                //"/imp/tourEvaluation/betterPlanBasics";
            //调试版本
            //m_strInterfaceUrl = "/impInterface/tourEvaluation/queryBetterPlan";


            BuildParam();
        }

        /// <summary>
        /// 构建参数
        /// </summary>
        public override void BuildParam()
        {
            /*
              {
                    "logId": "001",
                    "timestamp": "2014-01-0100: 00:00000",
                    "md5string": "401388F4C7978E2B5C1767FF8933E573",
                    "data": 
                     [
                        {
	                       "appriaseId": "001",  //评价ID
                           "shopId": "2222",//特约店ID	
                        }
                     ]
                }

             */

            m_jsonWriter.WriteStartObject();
            m_jsonWriter.WritePropertyName("logId"); //巡回员ID DMUser
            m_jsonWriter.WriteValue(DMUser.INSTANCE.CurrentMUser.UserAccount);


            m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
            m_jsonWriter.WriteValue(GetMD5Str(DMUser.INSTANCE.CurrentMUser.UserAccount));
            m_jsonWriter.WritePropertyName("timestamp"); //时间戳
            m_jsonWriter.WriteValue(_timestamp);

            m_jsonWriter.WritePropertyName("appriaseId"); //评价id
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.appriaseId); //需要修改 还原为真实数据
            //m_jsonWriter.WriteValue("66494afc84f44fe289d66b0444baa884");
            m_jsonWriter.WritePropertyName("shopId"); //巡回店id
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.shopId);
            //m_jsonWriter.WriteValue("DHEN04");

            m_jsonWriter.WriteEndObject();
            _jsonTxt = m_strBuilderJson.ToString();
            JObject obj = JObject.Parse(_jsonTxt);
            _jsonTxt = PROTOCAL_PARAM + _jsonTxt;
            m_byteRequestData = Encoding.UTF8.GetBytes(_jsonTxt);
        }

        protected override void ConvertPostDataToBytesData()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 解析接口返回
        /// </summary>
        public override void ParseParam()
        {
            if (!m_bIsSuccess)
            {
                return;
            }
            string str = Encoding.UTF8.GetString(m_byteResponseData);
            try
            {
                var resultObject = JObject.Parse(str);
                string code = resultObject["code"].ToString();
                if (code != "0")
                    return;
                var ret = resultObject["result"].ToString();
                MImprove item;
                JArray items = JArray.Parse(ret);
                for (int i = 0; i < items.Count; i++)
                {
                    item = new MImprove();
                    item.id = items[i]["id"].ToString();
                    item.pgrId = items[i]["pgrId"].ToString();
                    item.minName = items[i]["minName"].ToString();
                    item.smallName = items[i]["smallName"].ToString();
                    item.middName = items[i]["middName"].ToString();
                    item.strNo = (i + 1).ToString();
                    Items.Add(item);
                }
            }
            catch (System.Exception ex)
            {
                //string errMsg = "请求参数：" + _caseJson + "\r\n";
                //errMsg += "返回数据：" + str + "\r\n";
                //Log.PrintErrorLog("ReqAddOrUpdateCase", "解析数据失败：" + errMsg+"\r\n" + ex.Message);
            }
        }
    }
}