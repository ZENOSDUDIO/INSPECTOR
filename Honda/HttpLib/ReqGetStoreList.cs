using Honda.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.HttpLib
{
    /*    
     * 获取店列表接口  - by luyonghe 2014/11/4    
     */

    public class ReqGetStoreList : HttpBase
    {
        private string _tourId; //巡回员编号
        public ObservableCollection<MStore> lstStore; //店列表
        private string _jsonTxt;

        public ReqGetStoreList(string tourId, Action<object> callback)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;

            //打包地址
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqGetStoreList);
                // "/imp/tourEvaluation/getContributingShop";
            // m_strInterfaceUrl = "/impInterface/tourEvaluation/getContributingShop";

            _tourId = tourId;
            BuildParam();
        }

        public override void BuildParam()
        {
            try
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("logId"); //巡回员ID
                m_jsonWriter.WriteValue(_tourId);
                m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
                m_jsonWriter.WriteValue(GetMD5Str(_tourId));
                m_jsonWriter.WritePropertyName("timestamp"); //时间戳
                m_jsonWriter.WriteValue(_timestamp);
                m_jsonWriter.WriteEndObject();

                _jsonTxt = m_strBuilderJson.ToString();
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
                    lstStore = new ObservableCollection<MStore>();
                    JArray dataList = JArray.Parse(result.ToString());
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        JObject jobj = JObject.Parse(dataList[i].ToString());
                        MStore store = new MStore();
                        if (jobj.Property("appriaseId") != null)
                        {
                            store.appriaseId = jobj["appriaseId"].ToString();
                        }
                        store.StoreName = jobj["shopName"].ToString();
                        store.shopId = jobj["shopId"].ToString();
                        store.strShopType = jobj["shopType"].ToString();
                        if (store.strShopType == "0")
                        {
                            store._bIsShowCrossDistrict = Visibility.Collapsed;
                            store._bIsShowNormal = Visibility.Visible;
                        }
                        else if (store.strShopType == "1")
                        {
                            store._bIsShowCrossDistrict = Visibility.Visible;
                            store._bIsShowNormal = Visibility.Collapsed;
                        }

                        store.taskStatus = jobj["taskStatus"].ToString();

                        if (store.taskStatus == "1")
                        {
                            //店列表右边的六个菜单的状态
                            //评价表
                            string status1 = jobj["appraiseStatus"].ToString();
                            store._TOUR_STATE = GetState(status1);

                            //商务政策
                            string status2 = jobj["bpStatus"].ToString();
                            store._BUSEINESS_STATE = GetState(status2);

                            //工作亮点
                            string status3 = jobj["jobStatus"].ToString();
                            store._LIGHT_SPOT_STATE = GetState(status3);

                            //改善计划
                            string status4 = jobj["planStatus"].ToString();
                            store._IMPROVE_STATE = GetState(status4);


                            //巡回评价总结报告
                            string status5 = jobj["srStatus"].ToString();
                            store._OVERALL_RATING_REPORT = GetState(status5);
                        }


                        lstStore.Add(store);
                    }
                }
            }
            catch (System.Exception ex)
            {
                m_bIsSuccess = false;
                string UriMessage = "请求地址： " + m_strRequestUrl + "\r\n";
                string requestMsg = "请求参数：" + _jsonTxt + "\r\n";
                string errMsg = UriMessage + requestMsg + m_strErrorMsg + "\r\n" + ex.Message;
                Debug.WriteLine(errMsg);
            }
        }

        /// <summary>
        /// 返回菜单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private ITEM_STATE GetState(string status)
        {
            if (status == "0") //待提交状态
            {
                return ITEM_STATE.TO_SUBMIT;
            }
            else if (status == "1")
            {
                return ITEM_STATE.NOT_START;
            }
            else if (status == "2") //待提交附件状态
            {
                return ITEM_STATE.TO_SUBMIT_ACCESSORY;
            }
            else if (status == "3") //已完成
            {
                return ITEM_STATE.SUBMITED;
            }
            else if (status == "4") //待审核
            {
                return ITEM_STATE.CHECK_PENDING;
            }
            else if (status == "5") //结束
            {
                return ITEM_STATE.CHECK_PENDING_FINISH;
            }

            else
            {
                return ITEM_STATE.NONE;
            }
        }
    }
}