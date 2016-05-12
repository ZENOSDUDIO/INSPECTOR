using System.Collections.ObjectModel;
using System.Globalization;
using Honda.Globals;
using Honda.HttpLib;
using Honda.HttpLib.JsonInputData;
using Honda.Interface;
using Honda.Model;
using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.ViewModel
{
    /*
     * 评价表生成页管理
     */

    public class DMPreview
    {
        public static DMPreview INSTANCE = new DMPreview();

        public MSignature[] ListSignature;


        private DMPreview()
        {
            ListSignature = new MSignature[4];
        }

        /// <summary>
        /// 上传评价表
        /// </summary>
        public void UploadFormScore(Action<bool, string> action, bool isUpLoadFile = false)
        {
            try
            {
                EvaluationForUpload eva = GetUploadScoreData();
                ReqUploadFormScore _cmdUploadFormScore = new ReqUploadFormScore(eva, DMPreview.INSTANCE.ListSignature,
                    (obj) =>
                    {
                        ReqUploadFormScore req = obj as ReqUploadFormScore;
                        if (!req.m_bIsExistException)
                        {
                            req.ParseParam();
                            if (req.m_bIsSuccess)
                            {
                                DMStoreTour.INSTANCE.GetStoreList(DMUser.INSTANCE.CurrentMUser.UserAccount,
                                    (isSucceed, msg) =>
                                    {
                                        if (isSucceed)
                                        {
                                        }
                                    });
                                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.ALL_UPLOAD_FILE_DATA, eva);

                                if (isUpLoadFile)
                                {
                                    UploadAttachment(eva, action); //需修改 需完成函数体的内容
                                }
                                else
                                {
                                    action(true, "操作成功！");
                                }
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
                    }, isUpLoadFile);

                _cmdUploadFormScore.SendHttpRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("******************************************err*******DMPreview***********\r\n" +
                                "UploadFormScore:" + ex.Message);
            }
        }

        /// <summary>
        /// 上传附件，如果提交评分时用户勾选的提交附件，则调用此函数将附件提交 
        /// <author>xiang</author>
        /// </summary>
        public void UploadAttachment(EvaluationForUpload eva, Action<bool, string> action)
        {
            ReqTestMulityForm cmd = new ReqTestMulityForm((obj) =>
            {
                ReqTestMulityForm req = obj as ReqTestMulityForm;
                if (!req.m_bIsExistException)
                {
                    req.ParseParam();
                    if (req.m_bIsSuccess)
                    {
                        DMStoreTour.INSTANCE.GetStoreList(DMUser.INSTANCE.CurrentMUser.UserAccount, (isSucceed, msg) =>
                        {
                            if (isSucceed)
                            {
                            }
                        });
                        action(true, "上传表格成功");
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
            }, eva.Items);
            cmd.SendHttpRequest();
        }

        #region  获取需要上传的表格的数据

        /// <summary>
        /// 获取需要上传的表格的分数数据
        /// </summary>
        /// <returns></returns>
        public EvaluationForUpload GetUploadScoreData()
        {
            var eva = new EvaluationForUpload();
            var groups = new List<GroupDataForUpload>();
            var items = new List<ItemDataForUpload>();
            eva.Groups = groups;
            eva.Items = items;
            foreach (var data in DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId])
            {
                Upload_Common_Group(groups, items, data.ListGroup);
            }
            string str = "pppp";
            return eva;
        }

        #region UploadFormData

        /// <summary>
        /// 一般类型的组
        /// </summary>
        private void Upload_Common_Group(List<GroupDataForUpload> Groups, List<ItemDataForUpload> Items,
            List<M_Common_Groupcs> ListGroup)
        {
            for (int i = 0; i < ListGroup.Count; i++)
            {
                GroupDataForUpload group = new GroupDataForUpload();
                Groups.Add(group);
                group.ID = ListGroup[i].ID;
                group.Score = ListGroup[i].LstItem.Count.ToString(CultureInfo.InvariantCulture);
                //小项
                foreach (MItemNormal cell in ListGroup[i].LstItem)
                {
                    ItemDataForUpload item = FiveSItemToUploadItem(cell);
                    item.GroupId = group.ID;
                    Items.Add(item);
                    if (string.IsNullOrEmpty(item.ID))
                    {
                        Debug.WriteLine("&&&&&&&&&&&&&&Upload_Common_Group&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 获取附件中文件数据
        /// </summary>
        /// <param name="fileNameList">附件中的图片列表表</param>
        /// <returns></returns>
        private List<FileDataForUpload> GetFileDataForUpload(List<string> fileNameList)
        {
            List<FileDataForUpload> Files = new List<FileDataForUpload>();
            foreach (string filepath in fileNameList)
            {
                FileDataForUpload fileDataForUpload = new FileDataForUpload();
                Files.Add(fileDataForUpload);
                fileDataForUpload.OldName = Path.GetFileName(filepath);
                fileDataForUpload.FilePath = filepath;
                fileDataForUpload.FileName = Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(filepath));
            }
            return Files;
        }

        /// <summary>
        /// 5s及安全类型的小项转化成要上传到服务器的小项
        /// </summary>
        /// <returns></returns>
        private ItemDataForUpload FiveSItemToUploadItem(MItemNormal cell)
        {
            ItemDataForUpload item = new ItemDataForUpload();
            item.ID = cell.ID;
            item.GroupId = cell.ParentId;
            item.Remark = cell.remarks.content;
            item.Files = GetFileDataForUpload(cell.remarks.ImagePathList);
            if (cell.bIsEvaluationOfTour)
            {
                item.Score = GlobalValue.PASS;
            }
            else
            {
                item.Score = GlobalValue.NO_PASS;
            }
            if (string.IsNullOrEmpty(item.ID))
            {
                Debug.WriteLine("&&&&&&&&&&&&&&FiveSItemToUploadItem&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            }
            return item;
        }

        #endregion

        /// <summary>
        /// 保存当前特约店的所有列表
        /// </summary>
        public void SaveCurrentSoteForm()
        {
            //保存特约店评价列表
            try
            {
                //评价数据
                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.EVALUATION_SOURCE, DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId]);
                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.EVALUATION_SOURCE_MENU, DMUnivesalEvaluate.INSTANCE.ListUniversalMenu);

                if (ListSignature != null)
                {
                    SerialHelp.SerialObject(DirectoryHelper.INSTANCE.SIGNATURE_DATA, ListSignature);
                }

                if (ListSignature != null && ListSignature.Count(n => n == null) == ListSignature.Count()) return;
                MessageBox.Show("保存本地成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("DMPreview + \n" + ex.Message);
            }
        }

        public void LoadCurrentSoteForm()
        {
            try
            {

                DMUnivesalEvaluate.INSTANCE.ListUniversalMenu =
                  (ObservableCollection<MEvaluateMenu>)
                      SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.EVALUATION_SOURCE_MENU);

                //评价数据
                var offlineEvaData = (ObservableCollection<M_BaseUnivesalsSource>)
                        SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.EVALUATION_SOURCE);

                DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId] =
                    offlineEvaData;

                //签名
                MSignature[] _listSignature =
                    (MSignature[])SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.SIGNATURE_DATA);
                if (_listSignature != null)
                {
                    ListSignature = _listSignature;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DMPreview + LoadCurrentSoteForm" + ex.Message);
            }
        }
    }
}