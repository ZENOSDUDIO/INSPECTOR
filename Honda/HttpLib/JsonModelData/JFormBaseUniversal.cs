using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Honda.Globals;

namespace Honda.HttpLib.JsonModelData
{
    /// <summary>
    /// 表单中的组的定义
    /// </summary>
    public class JFormGroup
    {
        /// <summary>
        /// 组的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 组的序号
        /// </summary>
        public string SerialNum { get; set; }

        /// <summary>
        /// 组的父级ID 
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 组名称 
        /// </summary>
        public string Name { get; set; }

        private List<object> list = null;

        /// <summary>
        /// 组里的单元项
        /// 因为组里的单元项格式有多种，所以入参为Object类型
        /// </summary>
        public List<object> ItemList
        {
            get
            {
                if (list == null)
                    list = new List<object>();
                return list;
            }
            private set { list = value; }
        }

        public JFormGroup()
        {
            ItemList = new List<object>();
        }

        public bool AddItem(object item)
        {
            ItemList.Add(item);
            return true;
        }
    }

    /// <summary>
    /// 用于描绘评价表单
    /// 1、每张表单是由多个Group来组成
    /// 2、每个Group内的Item类型有的一样，有的不一样
    /// 第一种类型
    /// </summary>
    public class JFormBaseUniversal
    {
        /// <summary>
        /// 页面表头描述
        /// </summary>
        public string pageHeadDesc { get; set; }

        /// <summary>
        /// 表单的ID 
        /// </summary>
        public string ID { get; set; }


        /// <summary>
        /// 表单Code 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 表单名称 
        /// </summary>
        public string Name { get; set; }

        private List<JFormGroup> list = null;

        public List<JFormGroup> GroupList
        {
            get
            {
                if (list == null)
                    list = new List<JFormGroup>();
                return list;
            }
            private set { list = value; }
        }

        public JFormBaseUniversal()
        {
        }

        /// <summary>
        /// 根据json结果，解析出表单的对象
        /// </summary>
        /// <param name="strJsonTxt"></param>
        /// <returns></returns>
        public bool ParseResult(string strJsonTxt)
        {
            try
            {
                var jsonObj = JObject.Parse(strJsonTxt);
                string retCode = jsonObj["code"].ToString();
                string msg = jsonObj["message"].ToString();

                if (retCode != "2")
                    return false;
                var retContent = jsonObj["result"];
                //result是一个数组 但是里面永远只有一个对象
                JArray jRetArray = JArray.Parse(retContent.ToString());
                retContent = JObject.Parse(jRetArray[0].ToString()); //直接取第一个对象
                ID = retContent["id"].ToString();
                Name = retContent["name"].ToString();
                Code = retContent["code"].ToString();
                //循环解析表单里的组
                JArray dataList = JArray.Parse(retContent["Group"].ToString());
                JFormGroup grp = null;
                object item = null;
                for (int i = 0; i < dataList.Count; i++)
                {
                    //解析“组”这个对象的信息
                    JObject jGroupObj = JObject.Parse(dataList[i].ToString());
                    grp = new JFormGroup();
                    grp.Name = jGroupObj["name"].ToString();
                    grp.ID = jGroupObj["code"].ToString();
                    grp.ParentID = jGroupObj["parentId"].ToString();
                    grp.SerialNum = jGroupObj["serialNum"].ToString();

                    JArray itemList = JArray.Parse(jGroupObj["children"].ToString());
                    for (int n = 0; n < itemList.Count; n++)
                    {
                        #region 相关说明

                        //解析“组里的单元项”这个对象的信息
                        /*组里的单元项是有好几种模板的，
                         *最简单的就是一个单元项就是一行表格，
                         *也可能一个单元项里面继续分组，而且分组的格式可能也不一样，
                         *所以会有多种模板去解析 
                         */

                        #endregion

                        JObject jItemObj = JObject.Parse(itemList[n].ToString());
                        //解析此处的数据时要根据模板类型来进行解析
                        string templateIndex = jItemObj["templateType"].ToString();

                        #region 根据不同的模板解析单元项

                        switch (templateIndex)
                        {
                                #region 模板1

                            case "0":
                            {
                                item = ParseTemFirst(jItemObj);
                                break;
                            }

                                #endregion

                            default:
                                item = null;
                                break;
                        }

                        #endregion

                        if (item != null)
                        {
                            grp.AddItem(item);
                        }
                    }
                    GroupList.Add(grp);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "ZDCEDCEXSDEC==344CED3CD4CEWAVSDT4H");
                Debug.WriteLine("***********************");
                Debug.WriteLine(">>>>>出错表单名称:" + Name);
                PrintFormRet();
                return false;
            }
        }

        #region 解析 模板1

        /// <summary>
        /// 模板1的解析
        /// </summary>
        /// <param name="jItemObj"></param>
        /// <returns></returns>
        private JFormItemFirst ParseTemFirst(JObject jItemObj)
        {
            JFormItemFirst itemFirst = new JFormItemFirst();
            itemFirst.ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.FIRST;
            itemFirst.SerialNum = jItemObj["serialNum"].ToString();
            itemFirst.Title = jItemObj["scoringDes"].ToString();
            itemFirst.ID = jItemObj["code"].ToString();
            itemFirst.ParentId = jItemObj["parentId"].ToString();
            itemFirst.ValueType = "1";

            itemFirst.LastResult = jItemObj["lastResult"].ToString();
            itemFirst.CurrentResult = jItemObj["shopsResult"].ToString();
            return itemFirst;
        }

        #endregion

        public string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken) enumerator.Current;
                if (jc is JObject || ((JProperty) jc).Value is JObject)
                {
                    return GetJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty) jc).Name == key)
                    {
                        return ((JProperty) jc).Value.ToString();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 打印分组信息
        /// </summary>
        private void PrintFormRet()
        {
            int grpCount = GroupList.Count;
            int itemCount = 0;
            Debug.WriteLine("打印表单开始---------" + Name + "-----------");
            for (int i = 0; i < GroupList.Count; i++)
            {
                JFormGroup grp = GroupList[i];
                Debug.WriteLine("  组" + i.ToString() + ":" + grp.Name);
                for (int n = 0; n < grp.ItemList.Count; n++)
                {
                    itemCount++;
                    var item = grp.ItemList[n] as JFormItemBase;
                    //item.EnumScoreType
                    if (item.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.THIRD)
                    {
                        var item2 = grp.ItemList[n] as JFormItemContentThird;
                    }

                    Debug.WriteLine("        " + n.ToString() + ":" + item.Title);
                }
            }
            Debug.WriteLine("组个数：" + grpCount.ToString() + "   " + "单元项个数：" + itemCount.ToString());
            Debug.WriteLine("打印表单结束---------" + Name + "-----------");
        }

        //public 
    }
}