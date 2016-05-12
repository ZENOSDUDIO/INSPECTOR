using Honda.HttpLib;
using Honda.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.ViewModel
{
    /// <summary>
    /// 商务政策
    /// </summary>
    public class DMBusinessPolicy
    {
        /// <summary>
        /// 商务政策-非纯正零部件列表
        /// </summary>
        public ObservableCollection<MNotPureComponent> listNotPureComponent;

        /// <summary>
        /// 商务政策-零部件对外销售属性详情列表1
        /// </summary>
        public ObservableCollection<MComponentESDepartment> listComponentESDepartment;

        /// <summary>
        /// 商务政策-零部件对外销售属性详情列表2
        /// </summary>
        public ObservableCollection<MComponentESDepartment2> listComponentESDepartment2;

        /// <summary>
        /// 商务政策-零部件价格执行列表
        /// </summary>
        public ObservableCollection<MComponentPrice> listComponentPrice;

        public MSignature[] ListSignature;

        public static DMBusinessPolicy INSTANCE = new DMBusinessPolicy();

        private DMBusinessPolicy()
        {
        }

        /// <summary>
        /// 上传商务政策表格
        /// </summary>
        /// <param name="listNotPureComponent"></param>
        /// <param name="listComponentESDepartment"></param>
        /// <param name="listComponentESDepartment2"></param>
        /// <param name="listComponentPrice"></param>
        public void UpLoadBusinessPolicy(ObservableCollection<MNotPureComponent> listNotPureComponent,
            ObservableCollection<MComponentESDepartment> listComponentESDepartment,
            ObservableCollection<MComponentESDepartment2> listComponentESDepartment2,
            ObservableCollection<MComponentPrice> listComponentPrice,
            MSignature[] ListSignature, Action<string, bool> action)
        {
            ReqUploadBusinessPolicyList _cmdBusinessPolicyList = new ReqUploadBusinessPolicyList(listNotPureComponent,
                listComponentESDepartment,
                listComponentESDepartment2, listComponentPrice, ListSignature, (obj) =>
                {
                    ReqUploadBusinessPolicyList req = obj as ReqUploadBusinessPolicyList;
                    if (!req.m_bIsExistException)
                    {
                        req.ParseParam();
                        if (req.m_bIsSuccess)
                        {
                            action("操作成功！", true);
                        }
                        else
                        {
                            action(req.m_strErrorMsg, false);
                        }
                    }
                    else
                    {
                        action(req.m_strErrorMsg, false);
                    }
                });
            _cmdBusinessPolicyList.SendHttpRequest();
        }
    }
}