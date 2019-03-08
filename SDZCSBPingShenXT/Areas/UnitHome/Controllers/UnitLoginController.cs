using SDZCBootstrap.Common;
using SDZCSBPingShenXT.Models;
using System.Data.Entity;
using SDZCSBPingShenXT.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDZCSBPingShenXT.Areas.UnitHome.Controllers
{
    public class UnitLoginController : Controller
    {
        // GET: UnitHome/UnitLogin
        Models.SDZCSBPingShenXTEntities myModels = new Models.SDZCSBPingShenXTEntities();

        public object mySDZCSBXTEntities { get; private set; }

        /// <summary>
        /// 主界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PingShenSY()
        {
            return View();
        }
        /// <summary>
        /// 单位信息维护页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DanWeiXinXiWH()
        {
            return View();
        }
        /// <summary>
        /// 审核人员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ShenHeRenYuanGL()
        {
            return View();
        }
        /// <summary>
        /// 工作人员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult GongZuoRenYuanGL()
        {
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult XiuGaiMM()
        {
            return View();
        }
        /// <summary>
        /// 单位申报系列
        /// </summary>
        /// <returns></returns>
        public ActionResult DanWeiShenBaoXiLie()
        {
            return View();
        }

        #region 单位信息维护
        /// <summary>
        /// 注册修改绑定数据
        /// </summary>
        /// <param name="UnitRegisterID"></param>
        /// <returns></returns>
        public ActionResult SelectUnitRegister()
        {
            var listUnitRegister = (from tbUnitRegister in myModels.S_UnitRegister
                                    join tbUnitNature in myModels.S_UnitNature on tbUnitRegister.UnitNatureID equals tbUnitNature.UnitNatureID
                                    join tbSubjection in myModels.S_Subjection on tbUnitRegister.SubjectionID equals tbSubjection.SubjectionID
                                    orderby tbUnitRegister.UnitRegisterID descending
                                    select new
                                    {
                                        UnitRegisterID = tbUnitRegister.UnitRegisterID,
                                        StatutoryCall = tbUnitRegister.StatutoryCall,
                                        UseDynamicOrder = tbUnitRegister.UseDynamicOrder,
                                        DynamicOrderCode = tbUnitRegister.DynamicOrderCode,
                                        TissueOrganization = tbUnitRegister.TissueOrganization,
                                        TissueOrganizationCode = tbUnitRegister.TissueOrganizationCode,
                                        Administrative = tbUnitRegister.Administrative,
                                        CorrespondenceSite = tbUnitRegister.CorrespondenceSite,
                                        Contact = tbUnitRegister.Contact,
                                        ContactPhone = tbUnitRegister.ContactPhone,
                                        MailCode = tbUnitRegister.MailCode,
                                        UnitNature = tbUnitNature.UnitNature,
                                        Subjection = tbSubjection.Subjection,
                                    }).ToList();
            return Json(listUnitRegister, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 注册修改保存
        /// </summary>
        /// <returns></returns>
        public ActionResult ZhuCeXiuGaiBaoCun(S_UnitRegister SUnitRegister)
        {
            string strMsg = "fali";
            var pdSUnitRegister = (from tbUnitRegister in myModels.S_UnitRegister
                                   where
                                        tbUnitRegister.UnitRegisterID == SUnitRegister.UnitRegisterID
                                   select tbUnitRegister).Single();
            if (pdSUnitRegister != null)
            {
                pdSUnitRegister.Administrative = SUnitRegister.Administrative;
                pdSUnitRegister.CorrespondenceSite = SUnitRegister.CorrespondenceSite;
                pdSUnitRegister.Contact = SUnitRegister.Contact;
                pdSUnitRegister.ContactPhone = SUnitRegister.ContactPhone;
                pdSUnitRegister.MailCode = SUnitRegister.MailCode;
                myModels.Entry(pdSUnitRegister).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 审核人员管理
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSex()
        {
            var listSex = (from tbSex in myModels.S_Sex
                           select new SelectVo
                           {
                               id = tbSex.SexID,
                               text = tbSex.Sex
                           }).ToList();
            listSex = Tools.SetSelectJson(listSex);
            return Json(listSex, JsonRequestBehavior.AllowGet);
        }
   
        /// <summary>
        /// 用户状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectUserState()
        {
            var listUserState = (from tbUserState in myModels.S_UserState
                                 select new SelectVo
                                 {
                                     id = tbUserState.UserStateID,
                                     text = tbUserState.UserState
                                 }).ToList();
            listUserState = Tools.SetSelectJson(listUserState);
            return Json(listUserState, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 用户列表新增
        /// </summary>
        /// <param name="CheckName"></param>
        /// <param name="CheckUserName"></param>
        /// <param name="SexID"></param>
        /// <param name="MaillSite"></param>
        /// <param name="PhoneCode"></param>
        /// <param name="RegisterTime"></param>
        /// <param name="FinallyEnterTime"></param>
        /// <param name="UserStateID"></param>
        /// <returns></returns>
        public ActionResult InsertCheckPersonnel(string CheckName, string CheckUserName, int SexID, string MaillSite, string PhoneCode, DateTime RegisterTime, DateTime FinallyEnterTime, int UserStateID)
        {
            string strMsg = "fail";
            try
            {
                int oldCheckPersonnel = (from tbCheckPersonnel in myModels.B_CheckPersonnel
                                         where
                                             tbCheckPersonnel.CheckName == CheckName
                                            && tbCheckPersonnel.CheckUserName == CheckUserName
                                            && tbCheckPersonnel.SexID == SexID
                                            && tbCheckPersonnel.MaillSite == MaillSite
                                            && tbCheckPersonnel.PhoneCode == PhoneCode
                                            && tbCheckPersonnel.RegisterTime == RegisterTime
                                            && tbCheckPersonnel.FinallyEnterTime == FinallyEnterTime
                                            && tbCheckPersonnel.UserStateID == UserStateID
                                         select tbCheckPersonnel).Count();
                if (oldCheckPersonnel == 0)
                {
                    B_CheckPersonnel myCheckPersonnel = new B_CheckPersonnel();
                    myCheckPersonnel.CheckName = CheckName;
                    myCheckPersonnel.CheckUserName = CheckUserName;
                    myCheckPersonnel.SexID = SexID;
                    myCheckPersonnel.MaillSite = MaillSite;
                    myCheckPersonnel.PhoneCode = PhoneCode;
                    myCheckPersonnel.RegisterTime = RegisterTime;
                    myCheckPersonnel.FinallyEnterTime = FinallyEnterTime;
                    myCheckPersonnel.UserStateID = UserStateID;
                    myModels.B_CheckPersonnel.Add(myCheckPersonnel);
                    if (myModels.SaveChanges () > 0)
                    {
                        strMsg = "success";
                    }
                }          
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectCheckPersonnel(BsgridPage bsgridPage,int sexId, string checkName, string checkUserName)
        {
            try
            {
                List<CheckVo> linqCheck = (from tbCheck in myModels.B_CheckPersonnel
                                             join tbUserState in myModels.S_UserState on tbCheck.UserStateID equals tbUserState.UserStateID
                                             join tbSex in myModels.S_Sex on tbCheck.SexID equals tbSex.SexID
                                             select new CheckVo
                                             {
                                                 SexID = tbSex.SexID,
                                                 CheckPersonnelID = tbCheck.CheckPersonnelID,
                                                 CheckName = tbCheck.CheckName,
                                                 CheckUserName = tbCheck.CheckUserName,
                                                 Sex = tbSex.Sex,
                                                 MaillSite = tbCheck.MaillSite,
                                                 PhoneCode = tbCheck.PhoneCode,
                                                 registerTimer = tbCheck.RegisterTime.ToString(),
                                                 finallyEnterTimer = tbCheck.FinallyEnterTime.ToString(),
                                                 UserState = tbUserState.UserState,
                                             }).ToList();

                if (!string.IsNullOrEmpty(checkName))
                {
                    linqCheck = linqCheck.Where(s => s.CheckName.Contains(checkName.Trim())).ToList();
                }
                if (!string.IsNullOrEmpty(checkUserName))
                {
                    linqCheck = linqCheck.Where(s => s.CheckUserName.Contains(checkUserName.Trim())).ToList();
                }
                if (sexId > 0)
                {
                    linqCheck = linqCheck.Where(s => s.SexID == sexId).ToList();
                }              
                List<CheckVo> listFile = linqCheck.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<CheckVo> bsgrid = new Bsgrid<CheckVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = linqCheck.Count();
                bsgrid.data = linqCheck;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 修改绑定数据
        /// </summary>
        /// <param name="CheckPersonnelID"></param>
        /// <returns></returns>
        public ActionResult SelectCheckPer(int CheckPersonnelID)
        {
            var listCheckPer = (from tbCheckPer in myModels.B_CheckPersonnel
                                where tbCheckPer.CheckPersonnelID == CheckPersonnelID
                                select new
                                {
                                    CheckPersonnelID = tbCheckPer.CheckPersonnelID,
                                    CheckName = tbCheckPer.CheckName,
                                    CheckUserName = tbCheckPer.CheckUserName,
                                    SexID = tbCheckPer.SexID,
                                    MaillSite = tbCheckPer.MaillSite,
                                    PhoneCode = tbCheckPer.PhoneCode,
                                    registerTimer = tbCheckPer.RegisterTime.ToString(),
                                    finallyEnterTimer = tbCheckPer.FinallyEnterTime.ToString(),
                                    UserStateID = tbCheckPer.UserStateID 
                                }).Single();
            return Json(listCheckPer, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 用户列表修改保存
        /// </summary>
        /// <param name="BCheckPersonnel"></param>
        /// <returns></returns>
        public ActionResult UpdataCheckPer(B_CheckPersonnel BCheckPersonnel)
        {
            string strMsg = "fali";
            var pdACheckPersonnel = (from tbCheckPer in myModels.B_CheckPersonnel
                                 where
                                        tbCheckPer.CheckPersonnelID == BCheckPersonnel.CheckPersonnelID
                                 select tbCheckPer).Single();
            if (pdACheckPersonnel != null)
            {
                pdACheckPersonnel.CheckName = BCheckPersonnel.CheckName;
                pdACheckPersonnel.CheckUserName = BCheckPersonnel.CheckUserName;
                pdACheckPersonnel.SexID = BCheckPersonnel.SexID;
                pdACheckPersonnel.MaillSite = BCheckPersonnel.MaillSite;
                pdACheckPersonnel.PhoneCode = BCheckPersonnel.PhoneCode;
                pdACheckPersonnel.RegisterTime = BCheckPersonnel.RegisterTime;
                pdACheckPersonnel.FinallyEnterTime = BCheckPersonnel.FinallyEnterTime;
                pdACheckPersonnel.UserStateID = BCheckPersonnel.UserStateID;
                myModels.Entry(pdACheckPersonnel).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 用户列表删除数据
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCheckper(int CheckPersonnelID)
        {
            string strMsg = "fail";
            try
            {
                B_CheckPersonnel dbCheckPer = (from tbCheckPer in myModels.B_CheckPersonnel
                                               where tbCheckPer.CheckPersonnelID == CheckPersonnelID
                                               select tbCheckPer).Single();
                myModels.B_CheckPersonnel.Remove(dbCheckPer);
                myModels.SaveChanges();
                strMsg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 角色新增
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="RoleName"></param>
        /// <param name="RoleDescribe"></param>
        /// <returns></returns>
        public ActionResult InsertRole(string RoleCode, string RoleName, string RoleDescribe)
        {
            string strMsg = "fali";
            try
            {
                int oldRole = (from tbRole in myModels.B_Role
                                       where
                                            tbRole.RoleCode == RoleCode
                                            && tbRole.RoleName == RoleName
                                            && tbRole.RoleDescribe == RoleDescribe
                               select tbRole).Count();
                if (oldRole == 0)
                {
                    B_Role myRole = new B_Role();
                    myRole.RoleCode = RoleCode;
                    myRole.RoleName = RoleName;
                    myRole.RoleDescribe = RoleDescribe;
                    myModels.B_Role.Add(myRole);
                    if (myModels.SaveChanges() > 0)
                    {
                        strMsg = "success";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectRole(BsgridPage bsgridPage)
        {
            try
            {
                List<RoleVo> linqRole = (from tbRole in myModels.B_Role
                                           select new RoleVo
                                           {
                                               RoleID = tbRole.RoleID,
                                               RoleCode = tbRole.RoleCode,
                                               RoleName = tbRole.RoleName,
                                               RoleDescribe = tbRole.RoleDescribe,
                                           }).ToList();
                int intTotalRow = linqRole.Count();
                List<RoleVo> listFile = linqRole.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<RoleVo> bsgrid = new Bsgrid<RoleVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqRole;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 角色删除
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public ActionResult DeleteRole(int RoleID)
        {
            string strMsg = "fail";
            try
            {
                B_Role dbRole = (from tbRole in myModels.B_Role
                                     where tbRole.RoleID == RoleID
                                     select tbRole).Single();
                myModels.B_Role.Remove(dbRole);
                myModels.SaveChanges();
                strMsg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询最后一条数据的用户名
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckName()
        {
            string strRoleName = "";
            var listRoleName = (from tbName in myModels.B_CheckPersonnel
                              orderby tbName.CheckPersonnelID,
                                       tbName.CheckName
                                select tbName).ToList();
            if (listRoleName.Count > 0)
            {
                int count = listRoleName.Count;
                B_CheckPersonnel modelUR = listRoleName[count - 1];//获取表的最后一条数据
                strRoleName = modelUR.CheckName.ToString();//获取最后一个用户名
            }
            return Json(strRoleName, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 工作人员管理
        /// <summary>
        /// 工作人员列表新增
        /// </summary>
        /// <param name="SexID"></param>
        /// <param name="UserStateID"></param>
        /// <param name="Workname"></param>
        /// <param name="WorkLoginName"></param>
        /// <param name="Password"></param>
        /// <param name="MaillSite"></param>
        /// <param name="PhoneCode"></param>
        /// <param name="RegisterTime"></param>
        /// <param name="FinallyEnterTime"></param>
        /// <returns></returns>
        public ActionResult InsertWorkPersonnel(int SexID, int UserStateID, string Workname, string WorkLoginName, string Password, string MaillSite, string PhoneCode, DateTime RegisterTime, DateTime FinallyEnterTime)
        {
            string strMsg = "fail";
            try
            {
                int oldWorkPersonnel = (from tbWorkPersonnel in myModels.B_WorkPersonnel
                                         where
                                             tbWorkPersonnel.SexID == SexID
                                            && tbWorkPersonnel.UserStateID == UserStateID
                                            && tbWorkPersonnel.Workname == Workname
                                            && tbWorkPersonnel.WorkLoginName == WorkLoginName
                                            && tbWorkPersonnel.Password == Password
                                            && tbWorkPersonnel.MaillSite == MaillSite
                                            && tbWorkPersonnel.PhoneCode == PhoneCode
                                            && tbWorkPersonnel.RegisterTime == RegisterTime
                                            && tbWorkPersonnel.FinallyEnterTime == FinallyEnterTime
                                        select tbWorkPersonnel).Count();
                if (oldWorkPersonnel == 0)
                {
                    B_WorkPersonnel myWorkPersonnel = new B_WorkPersonnel();
                    myWorkPersonnel.SexID = SexID;
                    myWorkPersonnel.UserStateID = UserStateID;
                    myWorkPersonnel.Workname = Workname;
                    myWorkPersonnel.WorkLoginName = WorkLoginName;
                    myWorkPersonnel.Password = Password;
                    myWorkPersonnel.MaillSite = MaillSite;
                    myWorkPersonnel.PhoneCode = PhoneCode;
                    myWorkPersonnel.RegisterTime = RegisterTime;
                    myWorkPersonnel.FinallyEnterTime = FinallyEnterTime;
                    myModels.B_WorkPersonnel.Add(myWorkPersonnel);
                    if (myModels.SaveChanges() > 0)
                    {
                        strMsg = "success";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询工作人员列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectWorkPersonnel(BsgridPage bsgridPage)
        {
            try
            {
                List<WorkVo> linqWork = (from tbWork in myModels.B_WorkPersonnel
                                         join tbUserState in myModels.S_UserState on tbWork.UserStateID equals tbUserState.UserStateID
                                           join tbSex in myModels.S_Sex on tbWork.SexID equals tbSex.SexID
                                           select new WorkVo
                                           {
                                               WorkPersonnelID = tbWork.WorkPersonnelID,
                                               Workname = tbWork.Workname,
                                               WorkLoginName = tbWork.WorkLoginName,
                                               Sex = tbSex.Sex,
                                               Password = tbWork.Password,
                                               MaillSite = tbWork.MaillSite,
                                               PhoneCode = tbWork.PhoneCode,
                                               registerTimer = tbWork.RegisterTime.ToString(),
                                               finallyEnterTimer = tbWork.FinallyEnterTime.ToString(),
                                               UserState = tbUserState.UserState,
                                           }).ToList();
                int intTotalRow = linqWork.Count();
                List<WorkVo> listFile = linqWork.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<WorkVo> bsgrid = new Bsgrid<WorkVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqWork;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 工作人员修改数据绑定
        /// </summary>
        /// <param name="WorkPersonnelID"></param>
        /// <returns></returns>
        public ActionResult SelectWorkPer(int WorkPersonnelID)
        {
            var listWorkPer = (from tbWorkPer in myModels.B_WorkPersonnel
                               where tbWorkPer.WorkPersonnelID == WorkPersonnelID
                               select new
                                {
                                   WorkPersonnelID = tbWorkPer.WorkPersonnelID,
                                   Workname = tbWorkPer.Workname,
                                   WorkLoginName = tbWorkPer.WorkLoginName,
                                   SexID = tbWorkPer.SexID,
                                   Password = tbWorkPer.Password,
                                   MaillSite = tbWorkPer.MaillSite,
                                   PhoneCode = tbWorkPer.PhoneCode,
                                   registerTimer = tbWorkPer.RegisterTime.ToString(),
                                   finallyEnterTimer = tbWorkPer.FinallyEnterTime.ToString(),
                                   UserStateID = tbWorkPer.UserStateID
                                }).Single();
            return Json(listWorkPer, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 工作人员列表修改
        /// </summary>
        /// <param name="WorkPer"></param>
        /// <returns></returns>
        public ActionResult UpdataWorkPersonnel(B_WorkPersonnel WorkPer)
        {
            string strMsg = "fali";
            var pdAWorkPersonnel = (from tbWorkPer in myModels.B_WorkPersonnel
                                     where
                                            tbWorkPer.WorkPersonnelID == WorkPer.WorkPersonnelID
                                    select tbWorkPer).Single();
            if (pdAWorkPersonnel != null)
            {
                pdAWorkPersonnel.Workname = WorkPer.Workname;
                pdAWorkPersonnel.WorkLoginName = WorkPer.WorkLoginName;
                pdAWorkPersonnel.Password = WorkPer.Password;
                pdAWorkPersonnel.SexID = WorkPer.SexID;
                pdAWorkPersonnel.MaillSite = WorkPer.MaillSite;
                pdAWorkPersonnel.PhoneCode = WorkPer.PhoneCode;
                pdAWorkPersonnel.RegisterTime = WorkPer.RegisterTime;
                pdAWorkPersonnel.FinallyEnterTime = WorkPer.FinallyEnterTime;
                pdAWorkPersonnel.UserStateID = WorkPer.UserStateID;
                myModels.Entry(pdAWorkPersonnel).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 工作人员列表删除
        /// </summary>
        /// <param name="WorkPersonnelID"></param>
        /// <returns></returns>
        public ActionResult DeleteWorkPersonnel(int WorkPersonnelID)
        {
            string strMsg = "fail";
            try
            {
                B_WorkPersonnel dbWorkPer = (from tbWorkPer in myModels.B_WorkPersonnel
                                               where tbWorkPer.WorkPersonnelID == WorkPersonnelID
                                               select tbWorkPer).Single();
                myModels.B_WorkPersonnel.Remove(dbWorkPer);
                myModels.SaveChanges();
                strMsg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///重置密码
        /// </summary>
        /// <param name="UnitRegisterID"></param>
        /// <returns></returns>
        public ActionResult UpdataWork(int  WorkPersonnelID)
        {
            B_WorkPersonnel WorkPersonnel = (from tbWorkPer in myModels.B_WorkPersonnel
                                             where
                                                    tbWorkPer.WorkPersonnelID == WorkPersonnelID
                                             select tbWorkPer).Single();
            WorkPersonnel.Password = "66666666";
            myModels.Entry(WorkPersonnel).State = System.Data.Entity.EntityState.Modified;
            if (myModels.SaveChanges() > 0)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("fail", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///全部重置密码
        /// </summary>
        /// <param name="WorkPersonnelID"></param>
        /// <returns></returns>
        public ActionResult UpdataWorkPer()
        {
            int WorkPersonnelID;
            var WorkPersonnel = (from tbWorkPer in myModels.B_WorkPersonnel
                                 select new
                                 {
                                     WorkPersonnelID = tbWorkPer.WorkPersonnelID
                                 }).ToList();
            foreach (var item in WorkPersonnel)
            {
                WorkPersonnelID = Convert.ToInt32(item.WorkPersonnelID);
                B_WorkPersonnel WorkPersonnels = (from tbWorkPer in myModels.B_WorkPersonnel
                                                  where
                                                      tbWorkPer.WorkPersonnelID == WorkPersonnelID
                                                  select tbWorkPer).Single();
                WorkPersonnels.Password = "66666666";
                myModels.Entry(WorkPersonnels).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();
            }  
                            
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 导出工作人员
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportToExcel()
        {
            var linqWork = from tbWork in myModels.B_WorkPersonnel
                           join tbUserState in myModels.S_UserState on tbWork.UserStateID equals tbUserState.UserStateID
                           join tbSex in myModels.S_Sex on tbWork.SexID equals tbSex.SexID
                           select new WorkVo
                           {
                               UserState = tbUserState.UserState,
                               Sex = tbSex.Sex,
                               Workname = tbWork.Workname,
                               WorkLoginName = tbWork.WorkLoginName,
                               MaillSite = tbWork.MaillSite,
                               PhoneCode = tbWork.PhoneCode,
                               registerTimer = tbWork.RegisterTime.ToString(),
                               finallyEnterTimer = tbWork.FinallyEnterTime.ToString()
                           };
            //查询数据
            List<WorkVo> listExaminee = linqWork.ToList();
            //创建Excel对象 工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook excelBook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //创建Excel工作表 Sheet
            NPOI.SS.UserModel.ISheet sheet1 = excelBook.CreateSheet("工作人员信息");
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);//给sheet1添加第一行的头部标题
            row1.CreateCell(0).SetCellValue("姓名");//0
            row1.CreateCell(1).SetCellValue("登录名");//1
            row1.CreateCell(2).SetCellValue("性别");//2
            row1.CreateCell(3).SetCellValue("邮址");//3
            row1.CreateCell(4).SetCellValue("手机");//4
            row1.CreateCell(5).SetCellValue("注册时间");//5
            row1.CreateCell(6).SetCellValue("最后登录时间");//6
            row1.CreateCell(7).SetCellValue("用户状态");//7

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < listExaminee.Count; i++)
            {
                //创建行
                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                //姓名
                rowTemp.CreateCell(0).SetCellValue(listExaminee[i].Workname);
                //登录名
                rowTemp.CreateCell(1).SetCellValue(listExaminee[i].WorkLoginName);
                //性别
                rowTemp.CreateCell(2).SetCellValue(listExaminee[i].Sex);
                //邮址
                rowTemp.CreateCell(3).SetCellValue(listExaminee[i].MaillSite);
                //手机
                rowTemp.CreateCell(4).SetCellValue(listExaminee[i].PhoneCode);
                //注册时间
                rowTemp.CreateCell(5).SetCellValue(listExaminee[i].registerTimer);
                //最后登录时间
                rowTemp.CreateCell(6).SetCellValue(listExaminee[i].finallyEnterTimer);
                //用户状态
                rowTemp.CreateCell(7).SetCellValue(listExaminee[i].UserState);
            }
            //输出的文件名称
            string fileName = "工作人员信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";

            //把Excel转化为文件流，输出
            //创建文件流
            System.IO.MemoryStream bookStream = new System.IO.MemoryStream();
            //将工作薄写入文件流
            excelBook.Write(bookStream);
            //输出之前调用Seek（偏移量，游标位置）方法：获取文件流的长度
            bookStream.Seek(0, System.IO.SeekOrigin.Begin);
            //Stream对象,文件类型,文件名称
            return File(bookStream, "application/vnd.ms-excel", fileName);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 绑定旧密码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectUnitMiMa()
        {
            string strRegisterCode = "";
            var listRegisterCode = (from tbUnit in myModels.S_UnitRegister
                                    orderby tbUnit.UnitRegisterID,
                                       tbUnit.RegisterCode
                              select tbUnit).ToList();
            if (listRegisterCode.Count > 0)
            {
                int count = listRegisterCode.Count;
                S_UnitRegister modelUR = listRegisterCode[count - 1];//获取表的最后一条数据
                strRegisterCode = modelUR.RegisterCode.ToString();//获取最后一个密码
            }
            return Json(strRegisterCode, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UnitRegisters"></param>
        /// <returns></returns>
        public ActionResult UpdataUnitMM(S_UnitRegister UnitRegisters)
        {
            string strMsg = "fali";
            var pdSUnit = (from tbSUnit in myModels.S_UnitRegister               
                           select tbSUnit).ToList();

            S_UnitRegister UnitRegister = pdSUnit[pdSUnit.Count - 1];

            var pdSUnits = (from tbSUnit in myModels.S_UnitRegister
                                    where
                                           tbSUnit.UnitRegisterID == UnitRegister.UnitRegisterID
                                    select tbSUnit).Single();
            if (pdSUnits != null)
            {
                pdSUnits.RegisterCode = UnitRegisters.RegisterCode;
                pdSUnits.AffirmCode = UnitRegisters.AffirmCode;
                myModels.Entry(pdSUnits).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 单位申报系列

        /// <summary>
        /// 申报系列
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSBXiLie()
        {
            List<SelectVo> listSBXiLie = (from tbSBXiLie in myModels.S_DeclareSeries
                                          select new SelectVo
                                          {
                                              id = tbSBXiLie.DeclareSeriesID,
                                              text = tbSBXiLie.DeclareSeries
                                          }).ToList();
            listSBXiLie = Tools.SetSelectJson(listSBXiLie);
            return Json(listSBXiLie, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 系列等级
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSBDengJi()
        {
            List<SelectVo> listSBDengJi = (from tbSBDengJi in myModels.S_DeclareGrade
                                           select new SelectVo
                                           {
                                               id = tbSBDengJi.DeclareGradeID,
                                               text = tbSBDengJi.DeclareGrade
                                           }).ToList();
            listSBDengJi = Tools.SetSelectJson(listSBDengJi);
            return Json(listSBDengJi, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 年度系列
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSBNianDu()
        {
            List<SelectVo> listSBNianDu = (from tbSBNianDu in myModels.S_DeclareYear
                                           select new SelectVo
                                           {
                                               id = tbSBNianDu.DeclareYearID,
                                               text = tbSBNianDu.DeclareYear
                                           }).ToList();
            listSBNianDu = Tools.SetSelectJson(listSBNianDu);
            return Json(listSBNianDu, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 开启关闭申报系列表新增
        /// </summary>
        /// <param name="DeclareSeriesID"></param>
        /// <param name="DeclareGradeID"></param>
        /// <param name="DeclareYearID"></param>
        /// <param name="Explain"></param>
        /// <returns></returns>
        public ActionResult InsertUnitDeclare(int DeclareSeriesID, int DeclareGradeID, int DeclareYearID, string Explain)
        {
            string strMsg = "fail";
            try
            {
                int oldUnitDeclare = (from tbUnitDeclare in myModels.B_UnitDeclare
                                        where
                                            tbUnitDeclare.DeclareSeriesID == DeclareSeriesID
                                           && tbUnitDeclare.DeclareGradeID == DeclareGradeID
                                           && tbUnitDeclare.Explain == Explain
                                           && tbUnitDeclare.DeclareYearID == DeclareYearID
                                      select tbUnitDeclare).Count();
                if (oldUnitDeclare == 0)
                {
                    B_UnitDeclare myUnitDeclare = new B_UnitDeclare();
                    myUnitDeclare.DeclareSeriesID = DeclareSeriesID;
                    myUnitDeclare.DeclareGradeID = DeclareGradeID;
                    myUnitDeclare.Explain = Explain;
                    myUnitDeclare.DeclareYearID = DeclareYearID;
                    myModels.B_UnitDeclare.Add(myUnitDeclare);
                    if (myModels.SaveChanges() > 0)
                    {
                        strMsg = "success";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询开启关闭申报系列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectUnitDeclare(BsgridPage bsgridPage)
        {
            try
            {
                List<UnitDeclareVo> linqUnitDeclare = (from tbUnitDeclare in myModels.B_UnitDeclare
                                          join tbDeclareSeries in myModels.S_DeclareSeries on tbUnitDeclare.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                                          join tbDeclareGrade in myModels.S_DeclareGrade on tbUnitDeclare.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                                          join tbDeclareYear in myModels.S_DeclareYear on tbUnitDeclare.DeclareYearID equals tbDeclareYear.DeclareYearID
                                          select new UnitDeclareVo
                                          {
                                              UnitDeclareID = tbUnitDeclare.UnitDeclareID,
                                              DeclareSeries = tbDeclareSeries.DeclareSeries,
                                              DeclareGrade = tbDeclareGrade.DeclareGrade,
                                              Explain = tbUnitDeclare.Explain,
                                              DeclareYear = tbDeclareYear.DeclareYear,
                                              ToVoidNo = tbUnitDeclare.ToVoidNo

                                          }).ToList();
                int intTotalRow = linqUnitDeclare.Count();
                List<UnitDeclareVo> listFile = linqUnitDeclare.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<UnitDeclareVo> bsgrid = new Bsgrid<UnitDeclareVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqUnitDeclare;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 修改状态、开启、关闭
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateUnitDeclareState(int unitDeclareId,bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                var unitDeclare = (from tbUnitDeclare in myModels.B_UnitDeclare
                                             where tbUnitDeclare.UnitDeclareID == unitDeclareId
                                             select tbUnitDeclare).Single();
                unitDeclare.ToVoidNo = state;
                myModels.Entry(unitDeclare).State = EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    returnJson.State = true;
                    returnJson.Text = "修改成功";
                }
                else
                {
                    returnJson.State = false;
                    returnJson.Text = "修改失败";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                returnJson.State = false;
                returnJson.Text = "数据异常";
            }
            return Json(returnJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="unitDeclareId"></param>
        /// <returns></returns>
        public ActionResult DeleteUnitDeclareState(int unitDeclareId)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                B_UnitDeclare dbunitDeclare = (from tbUnitDeclare in myModels.B_UnitDeclare
                                             where tbUnitDeclare.UnitDeclareID == unitDeclareId
                                             select tbUnitDeclare).Single();
                myModels.B_UnitDeclare.Remove(dbunitDeclare);
                if (myModels.SaveChanges() > 0)
                {
                    returnJson.State = true;
                    returnJson.Text = "删除成功";
                }
                else
                {
                    returnJson.State = false;
                    returnJson.Text = "删除失败";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                returnJson.State = false;
                returnJson.Text = "数据异常";
            }
            return Json(returnJson, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}