using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.HttpLib;
using Honda.HttpLib.JsonModelData;
using System.Threading;
using System.Diagnostics;
using Honda.Interface;
using Honda.Globals;
using System.Windows;
using Honda.Model.Form.baseModel;
using System.Collections.ObjectModel;
using Honda.Model;


namespace Honda.ViewModel
{
    /// <summary>
    /// 零部件评价
    /// </summary>
    public class DMUnivesalEvaluate
    {
        /*
        * 1、当所有请求都有返回数据时，页面在能操作。
        * 2、每当发一次请求时LoadDataCount加1，数据返回一次时，LoadDataCount减1，当LoadDataCount==0时，
        * 表示所有请求完成
        */
        private int LoadDataCount = 0;

        /// <summary>
        /// 当数据都请求下来完成时，回调
        /// </summary>
        private Action<bool> action_finish;

        public static DMUnivesalEvaluate INSTANCE = new DMUnivesalEvaluate();

        private Queue<string> CODES;


        /// <summary>
        /// 评估二级页面数据源
        /// </summary>
        public M_BaseUnivesalsSource CurrentBaseUniversal { get; set; }

        private ObservableCollection<M_BaseUnivesalsSource> _lstBaseUniversal;

        /// <summary>
        /// 缓存所有二级页面表单数据
        /// </summary>
        private ObservableCollection<M_BaseUnivesalsSource> ListBaseUniversal
        {
            get
            {
                if (_lstBaseUniversal == null)
                {
                    _lstBaseUniversal = new ObservableCollection<M_BaseUnivesalsSource>();
                    return _lstBaseUniversal;
                }
                return _lstBaseUniversal;
            }
            set { _lstBaseUniversal = value; }
        }

        private Dictionary<string, ObservableCollection<M_BaseUnivesalsSource>> _dataBaseUniversal;

        /// <summary>
        /// 缓存特约店数据
        /// </summary>
        public Dictionary<string, ObservableCollection<M_BaseUnivesalsSource>> DataBaseUniversal
        {
            get
            {
                if (_dataBaseUniversal == null)
                {
                    _dataBaseUniversal = new Dictionary<string, ObservableCollection<M_BaseUnivesalsSource>>();
                    return _dataBaseUniversal;
                }
                return _dataBaseUniversal;
            }
            set
            {
                _dataBaseUniversal = value;
            }
        }

        /// <summary>
        ///  评估一级菜单数据
        /// </summary>
        public ObservableCollection<MEvaluateMenu> ListUniversalMenu { get; set; }

        public void GetUniversalMenuList(Action<bool, string> action)
        {
            ReqGetEvaluateMenu _cmdUpload = new ReqGetEvaluateMenu((obj) =>
            {
                var req = obj as ReqGetEvaluateMenu;
                if (!req.m_bIsExistException)
                {
                    req.ParseParam();
                    if (req.m_bIsSuccess)
                    {
                        ListUniversalMenu = req._lstEvulateMenu;
                        action(true, "操作成功！");
                    }
                    else
                    {
                        action(false, req.m_strErrorMsg);
                    }
                }
                else
                {
                    action(false, req.m_strErrorMsg);
                }
            });
            _cmdUpload.SendHttpRequest();
        }

        private DMUnivesalEvaluate()
        {
        }

        public void GetFormListFromServer(Action<bool> action)
        {
            this.ListBaseUniversal = new ObservableCollection<M_BaseUnivesalsSource>();

            this.GetUniversalMenuList((result, msg) =>
            {
                if (!result)
                {
                    Debug.WriteLine(
                        "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%店id为" +
                        DMStoreTour.INSTANCE.CurrentMStore.shopId + "\n评估页面一级菜单获取失败");
                    return;
                }

                //获取评估二级表单
                CODES = new Queue<string>();

                foreach (var item in ListUniversalMenu)
                {
                    CODES.Enqueue(item.EvaluateCode);
                }

                LoadDataCount = CODES.Count;

                action_finish = action;

                GetSecondForm(CODES.Dequeue());
            });
        }

        /// <summary>
        /// 根据一级表单代码获取二级表单
        /// </summary>
        /// <param name="code"></param>
        private void GetSecondForm(string code)
        {
            ReqGetFormList cmd = new ReqGetFormList(code, (obj) =>
            {
                ReqGetFormList rf = obj as ReqGetFormList;
                rf.ParseParam();
                var fb = new JFormBaseUniversal();
                bool isSuccess = fb.ParseResult(rf.ContentOfJsonResult);
                LoadDataCount--;
                if (isSuccess)
                {
                    Adapter(fb, code);
                    Thread.Sleep(200);
                    if (CODES.Count > 0)
                        GetSecondForm(CODES.Dequeue());
                }
                else
                {
                    string str = " 店id为" + DMStoreTour.INSTANCE.CurrentMStore.shopId + "\n二级表带code为" + code + "获取失败";
                    MessageBox.Show(str);
                    Debug.WriteLine(
                        "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%店id为" +
                        DMStoreTour.INSTANCE.CurrentMStore.shopId + "\n一级级表带code为" + code + "的评估页面二级表单内容获取失败");
                    if (action_finish != null)
                        action_finish(false);
                }
            });
            cmd.SendHttpRequest();
        }

        #region 适配

        /// <summary>
        /// 适配
        /// </summary>
        /// <param name="fb"></param>
        private void Adapter(JFormBaseUniversal fb, string code)
        {
            try
            {
                CurrentBaseUniversal = new M_BaseUnivesalsSource();
                CurrentBaseUniversal.ID = fb.ID;
                CurrentBaseUniversal._sourceIdentify = code;


                ListBaseUniversal.Add(CurrentBaseUniversal);

                if (DataBaseUniversal.ContainsKey(DMStoreTour.INSTANCE.CurrentMStore.shopId))
                {

                    DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId] = ListBaseUniversal;
                }
                else
                {
                    DataBaseUniversal.Add(DMStoreTour.INSTANCE.CurrentMStore.shopId, ListBaseUniversal);
                }

                List<JFormGroup> GroupList = fb.GroupList;

                for (int i = 0; i < GroupList.Count; i++)
                {
                    JFormGroup group = GroupList[i];

                    AdapterCommonGroup(group);
                }
                CurrentBaseUniversal.InitFormData();

                if (LoadDataCount == 0)
                {
                    if (action_finish != null)
                        action_finish(true);
                }
            }
            catch (Exception ex)
            {
                if (LoadDataCount == 0)
                {
                    if (action_finish != null)
                        action_finish(false);
                }
                Debug.WriteLine("类DMUnivesalEvaluate-》Adapter_PS->Error\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 适配正常格式的表单的组数据
        /// </summary>
        /// <param name="?"></param>
        private void AdapterCommonGroup(JFormGroup group)
        {
            //组
            M_Common_Groupcs _configuration_Group = new M_Common_Groupcs();
            _configuration_Group._EvaluationGroupContent = group.Name;
            _configuration_Group.ID = group.ID;
            _configuration_Group.ParentID = group.ParentID;

            var gIndex = 0;
            int.TryParse(group.SerialNum, out gIndex);
            _configuration_Group._index = gIndex;

            CurrentBaseUniversal.ListGroup.Add(_configuration_Group);

            //组里的单元项
            List<object> lstObj = group.ItemList;
            for (int j = 0; j < lstObj.Count; j++)
            {
                JFormItemFirst first = (JFormItemFirst)lstObj[j];
                MItemNormal cell = new MItemNormal(first.SerialNum, first.Title, IsPassOrNot(first.LastResult),
                    IsPassOrNot(first.CurrentResult), true);
                cell.ID = first.ID;
                cell.ParentId = first.ParentId;
                cell.ShopID = first.ShopID;
                _configuration_Group.LstItem.Add(cell);
                if (cell.ID == null)
                {
                    Debug.WriteLine("########Adapter_common_group############################");
                }
            }
        }

        private bool IsPassOrNot(string scoreCode)
        {
            if (GlobalValue.PASS == scoreCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}