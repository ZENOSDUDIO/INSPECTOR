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
    //public enum _Enum_Group_Form
    //{
    //    /// <summary>
    //    /// 普通类型
    //    /// </summary>
    //    _universal = 0,

    //    /// <summary>
    //    /// 人员类型A
    //    /// </summary>
    //    _peopleA = 1,

    //    /// <summary>
    //    /// 人员类型B
    //    /// </summary>
    //    _peopleB = 2
    //}
    ///// <summary>
    ///// 表单中的组的定义
    ///// </summary>
    //public class JFormGroup
    //{
    //    /// <summary>
    //    /// 组的ID 
    //    /// </summary>
    //    public string ID { get; set; }

    //    /// <summary>
    //    /// 组的父级ID 
    //    /// </summary>
    //    public string ParentID { get; set; }
    //    /// <summary>
    //    /// 组名称 
    //    /// </summary>
    //    public string Name { get; set; }

    //    private List<object> list = null;
    //    /// <summary>
    //    /// 组里的单元项
    //    /// 因为组里的单元项格式有多种，所以入参为Object类型
    //    /// </summary>
    //    public List<object> ItemList
    //    {
    //        get
    //        {
    //            if (list == null)
    //                list = new List<object>();
    //            return list;
    //        }
    //        private set
    //        {
    //            list = value;
    //        }
    //    }
    //    public JFormGroup()
    //    {
    //        ItemList = new List<object>();
    //    }
    //    public bool AddItem(object item)
    //    {
    //        ItemList.Add(item);
    //        return true;
    //    }

    //    /// <summary>
    //    /// 表单中组的类型
    //    /// </summary>
    //    public _Enum_Group_Form _enum_group_form { get; set; }
    //}
    ///// <summary>
    ///// 用于描绘评价表单
    ///// 1、每张表单是由多个Group来组成
    ///// 2、每个Group内的Item类型有的一样，有的不一样
    ///// 第一种类型
    ///// </summary>
    public class JFormBase
    {
        /// <summary>
        /// 表单的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 表单的父级ID 
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 表单编码 二级代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 表单名称 二级表单名称
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
            private set
            {
                list = value;
            }
        }

        public JFormBase()
        {
           
        }

        /// <summary>
        /// 根据json结果，解析出表单的对象
        /// </summary>
        /// <param name="strJsonTxt"></param>
        /// <returns></returns>
        public bool ParseResult(string strJsonTxt)
        {
            string strFormName = "";
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
                retContent = JObject.Parse(jRetArray[0].ToString());//直接取第一个对象
                Code = retContent["code"].ToString();
                Name = retContent["name"].ToString();
                ID = retContent["id"].ToString();
                ParentID = retContent["parentId"].ToString();
             
                //循环解析表单里的组
                JArray dataList = JArray.Parse(retContent["Group"].ToString());
                JFormGroup grp = null;
                object item = null;
                for (int i = 0; i < dataList.Count; i++ )
                {
                    //解析“组”这个对象的信息
                    JObject jGroupObj = JObject.Parse(dataList[i].ToString());
                    grp = new JFormGroup();
                    grp.Name = jGroupObj["name"].ToString();
                    grp.ID = jGroupObj["id"].ToString();
                    grp.ParentID = jGroupObj["parentId"].ToString();
                    if(Code == "PLUSES")//如果表单为“建议加分项--加分项”则需要做特殊处理
                    {
                        item = ParseTemFifth(jGroupObj);
                        grp.AddItem(item);
                    }
                    else
                    {
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
                                #region 模板2
                                case "1":
                                    {
                                        item = ParseTemSecond(jItemObj);
                                        break;
                                    }
                                #endregion
                                #region 模板3
                                case "2":
                                    {
                                        item = ParseTemThird(jItemObj);
                                        break;
                                    }
                                #endregion
                                #region 模板4
                                case "3":
                                    {
                                        item = ParseTemFourth(jItemObj);
                                        break;
                                    }
                                #endregion
                                #region 模板5
                                case "4"://建议加分项---加分项
                                    {
                                        item = ParseTemFifth(jItemObj);
                                        break;
                                    }
                                #endregion
                                #region 模板6
                                case "5"://建议加分项---五星级仓库评价表
                                    {
                                        item = ParseTemSixth(jItemObj);
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
                    }
                    GroupList.Add(grp);
                }
                PrintFive();
                //PrintFormRet();
                return true; 
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message, "ZDCEDCEXSDEC==344CED3CD4CEWAVSDT4H");
                Debug.WriteLine("***********************");
                Debug.WriteLine(">>>>>出错表单名称:" + Name + "Code:" + Code);
                PrintFormRet();
                return false;
            }
        }

        /// <summary>
        /// 打印五星级仓库所有小项的id
        /// </summary>
        private void PrintFive()
        {
            if (Code == Const.CODE_FSW)
            {
                List<string> ids = new List<string>();
                for (int i = 0; i < GroupList.Count; i++) //领域
                {
                    Debug.WriteLine("=====领域=========" + GroupList[i].Name);
                    List<object> items = GroupList[i].ItemList;
                    //GroupList[i].ItemList
                    JFormItemSixth itemSix;
                    for (int n = 0; n < items.Count; n++)//项目
                    {

                        itemSix = items[n] as JFormItemSixth;
                        Debug.WriteLine("  >>>项目:" + itemSix.Title);
                        //itemSix.ItemList
                        JFormItemContentSixth jics;
                        for (int m = 0; m < itemSix.ItemList.Count; m++)//分项
                        {
                            jics = itemSix.ItemList[m];
                            Debug.WriteLine("    >>>分项:" + jics.Title);
                            //string id = jics.ID;
                            //string title = jics.Title;
                            JFormItemContentSixthItem jicsi;
                            for (int x = 0; x < jics.Detail.Count; x++)//小项
                            {
                                jicsi = jics.Detail[x];
                                string name = jicsi.Name;
                                string id = jicsi.ID;
                                ids.Add(id);
                                Debug.WriteLine("      >>>小项:" + jicsi.Name + "id:" + id);

                            }
                        }
                    }
                }
                Debug.WriteLine("所有小项的id");
                for (int y = 0; y < ids.Count; y++)
                {
                    Debug.WriteLine(ids[y].ToString());
                }
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
            if(Name =="硬件")
            {
                Debug.WriteLine("   ");
            }
            JFormItemFirst itemFirst = new JFormItemFirst();
            itemFirst.ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.FIRST;
            itemFirst.SerialNum = jItemObj["serialNum"].ToString();
            itemFirst.Title = jItemObj["scoringDes"].ToString();
            itemFirst.ID = jItemObj["id"].ToString();
            itemFirst.ParentId = jItemObj["parentId"].ToString();
            itemFirst.ValueType = jItemObj["valueType"].ToString();
            string va = jItemObj["valueType"].ToString();
            if (va == "3")
            {
                Debug.WriteLine(va);
            }
            itemFirst.LastResult = jItemObj["lastResult"].ToString();
            itemFirst.CurrentResult = jItemObj["shopsResult"].ToString();
            return itemFirst;
        }
        #endregion

        #region 解析 模板2
        /// <summary>
        /// 模板2的解析
        /// </summary>
        /// <param name="jItemObj"></param>
        /// <returns></returns>
        private JFormItemSecond ParseTemSecond(JObject jItemObj)
        {
            if(Name == "硬件")
            {
                Debug.WriteLine("");
            }
            JFormItemSecond itemSecond = new JFormItemSecond();
            itemSecond.ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.SECOND;
            itemSecond.SerialNum = jItemObj["serialNum"].ToString();
            itemSecond.Title = jItemObj["content"].ToString();
            itemSecond.ID = jItemObj["id"].ToString();
            itemSecond.ParentId = jItemObj["parentId"].ToString();
            //修改格式后将ValueType放到了下面的子项中，所以此处无法或得到评分方式
            //itemSecond.ValueType = jItemObj["valueType"].ToString();
            string valueType = "";
            JArray jChildren = JArray.Parse(jItemObj["children"].ToString());
            JFormItemContentSecond jict = null;
            for (int p = 0; p < jChildren.Count; p++)
            {
                jict = new JFormItemContentSecond();
                JObject jTem2 = JObject.Parse(jChildren[p].ToString());
                jict.Title = jTem2["title"].ToString();
                jict.ID = jTem2["id"].ToString();
                jict.ParentID = jTem2["parentId"].ToString();
                jict.Detail = jTem2["detail"].ToString();
                jict.LastResult = jTem2["lastResult"].ToString();
                jict.CurrentResult = jTem2["shopsResult"].ToString();
                if(string.IsNullOrEmpty(valueType))
                    valueType =  jTem2["valueType"].ToString();
                itemSecond.ItemList.Add(jict);
            }
            itemSecond.ValueType = valueType;
            return itemSecond;
        }
        #endregion

        #region 解析 模板3
        /// <summary>
        /// 模板3的解析
        /// 服务基础评价-->硬件-->钣喷工具
        /// 服务基础评价-->人员-->人员配置1--10项
        /// 零部件评价-->人员管理-->人员配置1--3项
        /// </summary>
        /// <param name="jItemObj"></param>
        /// <returns></returns>
        private JFormItemThird ParseTemThird(JObject jItemObj)
        {
            JFormItemThird itemThird = new JFormItemThird();
            itemThird.ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.THIRD;
            itemThird.Title = jItemObj["content"].ToString();
            itemThird.SerialNum = jItemObj["serialNum"].ToString();
            if(itemThird.SerialNum == "11")
            {
                Debug.WriteLine("");
            }
            itemThird.ID = jItemObj["id"].ToString();
            itemThird.ParentId = jItemObj["parentId"].ToString();
            string valueType = "";
            
            JArray jChildren = JArray.Parse(jItemObj["children"].ToString());
            JFormItemContentThird jict = null;
            for (int m = 0; m < jChildren.Count; m++)//如果是人员配置 则此处数组的个数为1、如果是钣喷工具 则为12
            {
                jict = new JFormItemContentThird();
                JObject jTem2 = JObject.Parse(jChildren[m].ToString());
                jict.Title = jTem2["content"].ToString();
         
                jict.ID = jTem2["id"].ToString();//不用自身的id而是用后面跟着的数组中最后一个数字的id
                jict.ParentID = jTem2["parentId"].ToString();

                jict.LastResult = jTem2["lastResult"].ToString();
                jict.CurrentResult = jTem2["shopsResult"].ToString();
                if (string.IsNullOrEmpty(valueType))
                    valueType = jTem2["valueType"].ToString();
                JArray jItem = JArray.Parse(jTem2["scoreGrp"].ToString());
                for (int j = 0; j < jItem.Count; j++)
                {
                    JObject jDetail = JObject.Parse(jItem[j].ToString());
                    jict.Detail.Add(jDetail["name"].ToString());
                    //此处取最后一个数字的id作为当前项的id
                    if(j == jItem.Count -1)
                    {
                        if(Code == Const.CODE_HW)
                        {
                            jict.ID = jDetail["id"].ToString();
                        }
                        else
                        {
                            itemThird.ID = jDetail["id"].ToString();
                        }
                       
                    }
                }
                itemThird.ValueType = valueType; ;
                itemThird.ItemList.Add(jict);
            }
            return itemThird;
        }
        #endregion

        public string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;
                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    return GetJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty)jc).Name == key)
                    {
                        return ((JProperty)jc).Value.ToString();
                    }
                }
            } return null;
        }

        #region 解析 模板4
        /// <summary>
        /// 模板4的解析
        /// </summary>
        /// <param name="jItemObj"></param>
        /// <returns></returns>
        private JFormItemFourth ParseTemFourth(JObject jItemObj)
        {
            JFormItemFourth itemFourth = new JFormItemFourth();
            itemFourth.ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.FOURTH;
            itemFourth.SerialNum = jItemObj["serialNum"].ToString();
            itemFourth.Title = jItemObj["content"].ToString();
            itemFourth.ID = jItemObj["id"].ToString();
            itemFourth.ParentId = jItemObj["parentId"].ToString();
            string valueType = "";
            JArray jChildren = JArray.Parse(jItemObj["children"].ToString());
            JFormItemContentFourth jict = null;
            for (int p = 0; p < jChildren.Count; p++)
            {
                #region json对象
                /*
                 * {
      "id": "257",
      "parentId": 256,
      "title": "工具设备",
      "children": [
        {
          "id": "258",
          "parentId": 257,
          "valueType": "1",
          "describe": "常用工具是否齐全完好：\r\n\r\n  ·  扳手10-19（1套）/两工位\r\n\r\n  ·  套筒（1盒）/两工位\r\n\r\n  ·  扭矩扳手1把（18N·m-110N·m）/两工位\r\n",
          "shopsResult": "0|1",
          "lastResult": ""
        },
        {
          "id": "259",
          "parentId": 257,
          "valueType": "1",
          "describe": "工量具:\r\n\r\n  ·  万用表/百分表/千分尺/游标卡尺(每类至少有一个可正常使用)\r\n",
          "shopsResult": "0|1",
          "lastResult": ""
        },
        {
          "id": "260",
          "parentId": 257,
          "valueType": "1",
          "describe": "专用工具:\r\n\r\n  ·  机油滤清器拆卸工具/球头拆卸工具/汽油滤清器拆卸专用工具/氧传感器拆卸工具/转向节轴承拆卸工具/起步离合器安装工具倒档制动器安装工具(每类至少有一个可正常使用)\r\n",
          "shopsResult": "0|1",
          "lastResult": ""
        },
        {
          "id": "261",
          "parentId": 257,
          "valueType": "1",
          "describe": "作业管理系统配置的电脑及扫描枪是否齐全完好（已导入电子作业管理系统）",
          "shopsResult": "0|1",
          "lastResult": ""
        },
        {
          "id": "262",
          "parentId": 257,
          "valueType": "1",
          "describe": "在机修车间员工休息区放置最新期次技术月报",
          "shopsResult": "0|1",
          "lastResult": ""
        }
      ]
    }
                 */
                #endregion
                jict = new JFormItemContentFourth();
                JObject jTem2 = JObject.Parse(jChildren[p].ToString());
                jict.Title = jTem2["title"].ToString();
                jict.ID = jTem2["id"].ToString();
                jict.ParentID = jTem2["parentId"].ToString();
                JArray jItem = JArray.Parse(jTem2["children"].ToString());
                JFormItemContentFourthDetail detail;
                for (int x = 0; x < jItem.Count; x++)
                {
                    detail = new JFormItemContentFourthDetail();
                    JObject jDetail = JObject.Parse(jItem[x].ToString());
                    detail.Title = jDetail["describe"].ToString();
                    detail.CurrentResult = jDetail["shopsResult"].ToString();
                    detail.LastResult = jDetail["lastResult"].ToString();
                    detail.ID = jDetail["id"].ToString();
                    detail.ParentID = jDetail["parentId"].ToString();
                    if (string.IsNullOrEmpty(valueType))
                        valueType = jDetail["valueType"].ToString();
                    jict.Detail.Add(detail);
                }
                itemFourth.ValueType = valueType;
                itemFourth.ItemList.Add(jict);
            }
            return itemFourth;
        }
        #endregion

        #region 解析 模板5
        /// <summary>
        /// 模板5的解析
        /// </summary>
        /// <param name="jItemObj"></param>
        /// <returns></returns>
        private JFormItemFifth ParseTemFifth(JObject jItemObj)
        {
            JFormItemFifth item = new JFormItemFifth();
            item.ID = jItemObj["id"].ToString();
            item.ParentId = jItemObj["parentId"].ToString();
            item.Title = jItemObj["name"].ToString();
            item.SerialNum = jItemObj["serialNum"].ToString();
            string va = jItemObj["valueType"].ToString();
            if(va == "3")
            {
                Debug.WriteLine(va);
            }
            item.ValueType = jItemObj["valueType"].ToString();
            item.Content = jItemObj["content"].ToString();
            item.Score = jItemObj["score"].ToString();
            item.LastResult = jItemObj["lastResult"].ToString();
            item.CurrentResult = jItemObj["shopsResult"].ToString();
            return item;
        }
        #endregion

        #region 解析 模板6
        /// <summary>
        /// 模板6的解析
        /// </summary>
        /// <param name="jItemObj"></param>
        /// <returns></returns>
        private JFormItemSixth ParseTemSixth(JObject jItemObj)
        {
            JFormItemSixth item = new JFormItemSixth();
            item.ID = jItemObj["id"].ToString();
            item.ParentId = jItemObj["parentId"].ToString();
            item.Title = jItemObj["name"].ToString();//仪态
           
            JArray jChildren = JArray.Parse(jItemObj["children"].ToString());
            JFormItemContentSixth jict = null;
            for (int i = 0; i < jChildren.Count; i++)
            {
                jict = new JFormItemContentSixth();
                JObject jTem2 = JObject.Parse(jChildren[i].ToString());
                jict.Title = jTem2["name"].ToString();
                jict.ID = jTem2["id"].ToString();
                jict.ParentID = jTem2["parentId"].ToString();

                JArray jChildren2 = JArray.Parse(jTem2["children"].ToString());
                JFormItemContentSixthItem childItem;
                for (int n = 0; n < jChildren2.Count; n++)
                {
                    childItem = new JFormItemContentSixthItem();
                    JObject jTem3 = JObject.Parse(jChildren2[n].ToString());
                    childItem.ID = jTem3["id"].ToString();
                    childItem.ParentID = jTem3["parentId"].ToString();
                    childItem.SmallItem = jTem3["small"].ToString();
                    childItem.Classify = jTem3["classify"].ToString();
                    childItem.Name = jTem3["name"].ToString();
                    childItem.Score = jTem3["score"].ToString();
                    childItem.ValueType = jTem3["valueType"].ToString();
                    childItem.Content = jTem3["content"].ToString();
                    childItem.LastResult = jTem3["lastResult"].ToString();
                    childItem.CurrentResult = jTem3["shopsResult"].ToString();

                    jict.Detail.Add(childItem);
                }
                if(item.ItemList.Count > 0 && item.ItemList[0].Detail.Count > 0)
                    item.ValueType = item.ItemList[0].Detail[0].ValueType;
                item.ItemList.Add(jict);
            }
           
            //item.LastResult = jItemObj["lastResult"].ToString();
            //item.CurrentResult = jItemObj["shopsResult"].ToString();
            return item;
        }
        #endregion

        /// <summary>
        /// 打印分组信息
        /// </summary>
        private void PrintFormRet()
        {
            int grpCount = GroupList.Count;
            int itemCount = 0;
            Debug.WriteLine("打印表单开始---------" + Name + "-----------");
            for(int i = 0 ; i < GroupList.Count; i++)
            {
                JFormGroup grp = GroupList[i];
                Debug.WriteLine("  组" + i.ToString() + ":" + grp.Name);
                for(int n = 0; n < grp.ItemList.Count; n++)
                {
                    itemCount++;
                    JFormItemBase item = grp.ItemList[n] as JFormItemBase;
                    //item.EnumScoreType
                    if(item.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.THIRD)
                    {
                        JFormItemContentThird item2 = grp.ItemList[n] as JFormItemContentThird;
                    }
                       
                    Debug.WriteLine("        "+n.ToString() + ":" + item.Title);
                }
               
            }
            Debug.WriteLine("组个数：" + grpCount.ToString() + "   " + "单元项个数：" + itemCount.ToString());
            Debug.WriteLine("打印表单结束---------" + Name + "-----------");
        }

        //public 
    }

}
