using SDZCBootstrap.Common;
using SDZCSBPingShenXT.Models;
using SDZCSBPingShenXT.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDZCSBPingShenXT.Areas.UnitHome.Controllers
{
    public class SetWayController : Controller
    {
        // GET: UnitHome/SetWay
        Models.SDZCSBPingShenXTEntities myModels = new Models.SDZCSBPingShenXTEntities();
        /// <summary>
        /// 申报路径申请
        /// </summary>
        /// <returns></returns>
        public ActionResult ShenBaoLuJing()
        {
            return View();
        }
        /// <summary>
        /// 代理路径申请
        /// </summary>
        /// <returns></returns>
        public ActionResult DaiLiLuJing()
        {
            return View();
        }
        /// <summary>
        /// 申报路径审核
        /// </summary>
        /// <returns></returns>
        public ActionResult LuJingShenHe()
        {
            return View();
        }
        /// <summary>
        /// 代理路径审核
        /// </summary>
        /// <returns></returns>
        public ActionResult DaiLiLuJingSH()
        {
            return View();
        }

        #region 申报（代理）路径申请

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
        /// 确认状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectQueRenZT()
        {
            List<SelectVo> listQueRenZT = (from tbQueRenZT in myModels.S_CommittedState
                                           select new SelectVo
                                           {
                                               id = tbQueRenZT.CommittedStateID,
                                               text = tbQueRenZT.CommittedState
                                           }).ToList();
            listQueRenZT = Tools.SetSelectJson(listQueRenZT);
            return Json(listQueRenZT, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 申报路径申请新增
        /// </summary>
        /// <param name="DeclareSeriesID"></param>
        /// <param name="DeclareGradeID"></param>
        /// <param name="CommittedStateID"></param>
        /// <param name="ControlUnit"></param>
        /// <returns></returns>
        public ActionResult InsertDeclarePath(int DeclareSeriesID, int DeclareGradeID, int CommittedStateID, string ControlUnit)
        {
            string strMsg = "fail";
            try
            {
                int oldDeclarePath = (from tbDeclarePath in myModels.B_DeclarePath
                                         where
                                             tbDeclarePath.DeclareSeriesID == DeclareSeriesID
                                            && tbDeclarePath.DeclareGradeID == DeclareGradeID
                                            && tbDeclarePath.CommittedStateID == CommittedStateID
                                            && tbDeclarePath.ControlUnit == ControlUnit
                                      select tbDeclarePath).Count();
                if (oldDeclarePath == 0)
                {
                    B_DeclarePath myDeclarePath = new B_DeclarePath();
                    myDeclarePath.DeclareSeriesID = DeclareSeriesID;
                    myDeclarePath.DeclareGradeID = DeclareGradeID;
                    myDeclarePath.CommittedStateID = CommittedStateID;
                    myDeclarePath.ControlUnit = ControlUnit;
                    myModels.B_DeclarePath.Add(myDeclarePath);
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
        /// 查询申报路径列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="declareSeriesId"></param>
        /// <param name="declareGradeId"></param>
        /// <param name="committedStateId"></param>
        /// <param name="ControlUnit"></param>
        /// <returns></returns>
        public ActionResult SelectDeclarePath(BsgridPage bsgridPage, int declareSeriesId, int declareGradeId, int committedStateId, string controlUnit)
        {
            try
            {
                List<DeclarePathVo> linqDeclarePath = (from tbDeclarePath in myModels.B_DeclarePath
                                            join tbDeclareSeries in myModels.S_DeclareSeries on tbDeclarePath.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                                            join tbDeclareGrade in myModels.S_DeclareGrade on tbDeclarePath.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                                                 join tbCommittedState in myModels.S_CommittedState on tbDeclarePath.CommittedStateID equals tbCommittedState.CommittedStateID
                                                 select new DeclarePathVo
                                                 {

                                                     DeclarePathID = tbDeclarePath.DeclarePathID,
                                                     DeclareSeriesID = tbDeclareSeries.DeclareSeriesID,
                                                     DeclareGradeID = tbDeclareGrade.DeclareGradeID,
                                                     CommittedStateID = tbCommittedState.CommittedStateID,
                                                     DeclareSeries = tbDeclareSeries.DeclareSeries,
                                                     DeclareGrade = tbDeclareGrade.DeclareGrade,
                                                     CommittedState = tbCommittedState.CommittedState.Trim(),
                                                     ControlUnit = tbDeclarePath.ControlUnit,

                                                 }).ToList();

                if (!string.IsNullOrEmpty(controlUnit))
                {
                    linqDeclarePath = linqDeclarePath.Where(s => s.ControlUnit.Contains(controlUnit.Trim())).ToList();
                }
                if (declareSeriesId > 0)
                {
                    linqDeclarePath = linqDeclarePath.Where(s => s.DeclareSeriesID == declareSeriesId).ToList();
                }
                if (declareGradeId > 0)
                {
                    linqDeclarePath = linqDeclarePath.Where(s => s.DeclareGradeID == declareGradeId).ToList();
                }
                if (committedStateId > 0)
                {
                    linqDeclarePath = linqDeclarePath.Where(s => s.CommittedStateID == committedStateId).ToList();
                }
                int intTotalRow = linqDeclarePath.Count();
                List<DeclarePathVo> listFile = linqDeclarePath.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<DeclarePathVo> bsgrid = new Bsgrid<DeclarePathVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqDeclarePath;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 修改绑定
        /// </summary>
        /// <param name="DeclarePathID"></param>
        /// <returns></returns>
        public ActionResult SelectDPath(int DeclarePathID)
        {
            var listDeclarePath = (from tbDeclarePath in myModels.B_DeclarePath
                                where tbDeclarePath.DeclarePathID == DeclarePathID
                                select new
                                {
                                    DeclarePathID = tbDeclarePath.DeclarePathID,
                                    DeclareSeriesID = tbDeclarePath.DeclareSeriesID,
                                    DeclareGradeID = tbDeclarePath.DeclareGradeID,
                                    CommittedStateID = tbDeclarePath.CommittedStateID,
                                    ControlUnit = tbDeclarePath.ControlUnit
                                }).Single();
            return Json(listDeclarePath, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报路径申请修改
        /// </summary>
        /// <param name="BDeclarePath"></param>
        /// <returns></returns>
        public ActionResult UpdataDeclarePath(B_DeclarePath BDeclarePath)
        {
            string strMsg = "fali";
            var pdDeclarePath = (from tbDeclarePath in myModels.B_DeclarePath
                                 where
                                            tbDeclarePath.DeclarePathID == BDeclarePath.DeclarePathID
                                 select tbDeclarePath).Single();
            if (pdDeclarePath != null)
            {
                pdDeclarePath.DeclareSeriesID = BDeclarePath.DeclareSeriesID;
                pdDeclarePath.DeclareGradeID = BDeclarePath.DeclareGradeID;
                pdDeclarePath.ControlUnit = BDeclarePath.ControlUnit;
                pdDeclarePath.CommittedStateID = BDeclarePath.CommittedStateID;
                myModels.Entry(pdDeclarePath).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 申报申请路径删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteDeclarePath(int DeclarePathID)
        {
            string strMsg = "fail";
            try
            {
                B_DeclarePath dbDeclarePath = (from tbDeclarePath in myModels.B_DeclarePath
                                               where tbDeclarePath.DeclarePathID == DeclarePathID
                                               select tbDeclarePath).Single();
                myModels.B_DeclarePath.Remove(dbDeclarePath);
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
        /// 批量设置（查询系列等级列表）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="declareSeries"></param>
        /// <param name="declareGradeId"></param>
        /// <returns></returns>
        public ActionResult SelectDeclare(BsgridPage bsgridPage, string declareSeries, int declareGradeId)
        {
            try
            {
                List<DeclarePathVo> linqDeclarePath = (from tbDeclarePath in myModels.B_DeclarePath
                                                       join tbDeclareSeries in myModels.S_DeclareSeries on tbDeclarePath.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                                                       join tbDeclareGrade in myModels.S_DeclareGrade on tbDeclarePath.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                                                       join tbCommittedState in myModels.S_CommittedState on tbDeclarePath.CommittedStateID equals tbCommittedState.CommittedStateID
                                                       select new DeclarePathVo
                                                       {

                                                           DeclarePathID = tbDeclarePath.DeclarePathID,
                                                           DeclareGradeID = tbDeclareGrade.DeclareGradeID,
                                                           DeclareSeries = tbDeclareSeries.DeclareSeries.ToString(),
                                                           DeclareGrade = tbDeclareGrade.DeclareGrade,
                                                       }).ToList();

                if (!string.IsNullOrEmpty(declareSeries))
                {
                    linqDeclarePath = linqDeclarePath.Where(s => s.DeclareSeries.Contains(declareSeries.Trim())).ToList();
                }
                if (declareGradeId > 0)
                {
                    linqDeclarePath = linqDeclarePath.Where(s => s.DeclareGradeID == declareGradeId).ToList();
                }
                int intTotalRow = linqDeclarePath.Count();
                List<DeclarePathVo> listFile = linqDeclarePath.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<DeclarePathVo> bsgrid = new Bsgrid<DeclarePathVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqDeclarePath;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 批量设置（增加申报路径）
        /// </summary>
        /// <param name="DeclarePathID"></param>
        /// <returns></returns>
        public ActionResult GoXuanXianShiQB(int DeclarePathID)
        {

            var linqDeclarePath = (from tbDeclarePath in myModels.B_DeclarePath
                                   join tbDeclareSeries in myModels.S_DeclareSeries on tbDeclarePath.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                                   join tbDeclareGrade in myModels.S_DeclareGrade on tbDeclarePath.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                                   join tbCommittedState in myModels.S_CommittedState on tbDeclarePath.CommittedStateID equals tbCommittedState.CommittedStateID
                                   where tbDeclarePath.DeclarePathID == DeclarePathID
                                   select new
                                   {
                                       DeclarePathID = tbDeclarePath.DeclarePathID,
                                       DeclareGradeID = tbDeclareGrade.DeclareGradeID,
                                       DeclareSeries = tbDeclareSeries.DeclareSeries.ToString(),
                                       DeclareGrade = tbDeclareGrade.DeclareGrade,
                                       ControlUnit = tbDeclarePath.ControlUnit,
                                   }).Single();
            return Json(linqDeclarePath, JsonRequestBehavior.AllowGet);


        }

        /// <summary>
        /// 增加申报路径保存
        /// </summary>
        /// <param name="DeclareSeriesID"></param>
        /// <param name="DeclareGradeID"></param>
        /// <param name="ControlUnit"></param>
        /// <returns></returns>
        public ActionResult InsertLuJing(string SeriesGrade, string ControlUnit)
        {
            string strMsg = "fail";
            try
            {
                int oldSeriesGrade = (from tbSeriesGrade in myModels.S_SeriesGrade
                                      where
                                          tbSeriesGrade.SeriesGrade == SeriesGrade
                                         && tbSeriesGrade.ControlUnit == ControlUnit
                                      select tbSeriesGrade).Count();
                if (oldSeriesGrade == 0)
                {
                    S_SeriesGrade mySeriesGrade = new S_SeriesGrade();
                    mySeriesGrade.SeriesGrade = SeriesGrade;
                    mySeriesGrade.ControlUnit = ControlUnit;
                    myModels.S_SeriesGrade.Add(mySeriesGrade);
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
        #endregion

        #region 申报（代理）路径审核
        /// <summary>
        /// 审核保存
        /// </summary>
        /// <param name="AuditOpinion"></param>
        /// <param name="AuditPass"></param>
        /// <returns></returns>
        public ActionResult InsertAudit(string AuditOpinion, string AuditPass)
        {
            int Msg = 0;
            try
            {
                //这段代码是判断表里是否有相同的数据，如果有则新增失败
                //int oldAudit = (from tbAudit in myModels.B_Audit
                //               where
                //                    tbAudit.AuditOpinion == AuditOpinion
                //                    && tbAudit.AuditPass == AuditPass
                //               select tbAudit).Count();
                //if (oldAudit == 0)
                //{
                    B_Audit myAudit = new B_Audit();
                    myAudit.AuditOpinion = AuditOpinion;
                    myAudit.AuditPass = AuditPass;
                    myModels.B_Audit.Add(myAudit);
                    myModels.SaveChanges();
                    //查询出整个表的信息
                    List<B_Audit> list = (from tbenter in myModels.B_Audit
                                          select tbenter).ToList();
                    string Str = list[list.Count - 1].AuditPass.Trim();//查询出最新一行的数据 
                    if (Str == "审核通过")
                    {
                        Msg = 1;
                    }
                    else if (Str == "审核未通过")
                    {
                        Msg = 2;
                    }

                }
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改申报状态
        /// </summary>
        /// <param name="declarePathID"></param>
        /// <param name="ZhuangTaiID"></param>
        /// <returns></returns>
        public ActionResult UpdateZhuangTi(int declarePathID, int ZhuangTaiID)
        {
            if (declarePathID!=0&& ZhuangTaiID!=0)
            {
                List<B_DeclarePath> list = (from tbDeclarePath in myModels.B_DeclarePath
                                            where tbDeclarePath.DeclarePathID == declarePathID
                                            select tbDeclarePath).ToList();
                list[0].CommittedStateID = ZhuangTaiID;
                myModels.Entry(list[0]).State= EntityState.Modified;
                myModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}