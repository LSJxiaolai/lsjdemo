using SDZCBootstrap.Common;
using SDZCSBPingShenXT.Models;
using SDZCSBPingShenXT.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDZCSBPingShenXT.Areas.UnitHome.Controllers
{
    public class AuditController : Controller
    {
        // GET: UnitHome/Audit
        Models.SDZCSBPingShenXTEntities myModels = new Models.SDZCSBPingShenXTEntities();
        /// <summary>
        /// 审核上报
        /// </summary>
        /// <returns></returns>
        public ActionResult ShenHeShangBao()
        {
            return View();
        }
        /// <summary>
        /// 审核通知
        /// </summary>
        /// <returns></returns>
        public ActionResult ShenHeToZhi()
        {
            return View();
        }
        #region 查询
        /// <summary>
        /// 查询基本信息列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectJiBenXinXi(BsgridPage bsgridPage,string name,string placeUnit,int declareQualificationId,int declareModeId,
                                                                    int declareGradeId,int declareSeriesId,int declareStatusId)
        {
            try
            {
                List<BasicVo> linqBasic = (from tbBasic in myModels.B_Basic
                                           join tbSex in myModels.S_Sex on tbBasic.SexID equals tbSex.SexID
                                           join tbEducation in myModels.S_Education on tbBasic.EducationID equals tbEducation.EducationID
                                           join tbDeclareGrade in myModels.S_DeclareGrade on tbBasic.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                                           join tbDeclareSeries in myModels.S_DeclareSeries on tbBasic.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                                           join tbDeclareQualification in myModels.S_DeclareQualification on tbBasic.DeclareQualificationID equals tbDeclareQualification.DeclareQualificationID
                                           join tbDeclareMode in myModels.S_DeclareMode on tbBasic.DeclareModeID equals tbDeclareMode.DeclareModeID
                                           join tbDeclareStatus in myModels.S_DeclareStatus on tbBasic.DeclareStatusID equals tbDeclareStatus.DeclareStatusID
                                           select new BasicVo
                                          {
                                              BasicID = tbBasic.BasicID,
                                              DeclareQualificationID = tbDeclareQualification.DeclareQualificationID,
                                              DeclareModeID = tbDeclareMode.DeclareModeID,
                                              DeclareGradeID = tbDeclareGrade.DeclareGradeID,
                                              DeclareSeriesID = tbDeclareSeries.DeclareSeriesID,
                                              DeclareStatusID = tbDeclareStatus.DeclareStatusID,
                                              PlaceUnit = tbBasic.PlaceUnit,
                                              Name = tbBasic.Name,
                                              Sex = tbSex.Sex,
                                              birthdayer = tbBasic.Birthday.ToString(),
                                              Major = tbBasic.Major,
                                              Education = tbEducation.Education,                                            
                                              DeclareGrade = tbDeclareGrade.DeclareGrade,
                                              DeclareSeries = tbDeclareSeries.DeclareSeries,
                                              DeclareQualification = tbDeclareQualification.DeclareQualification,
                                              formTimer = tbBasic.FormTime.ToString(),
                                              DeclareMode = tbDeclareMode.DeclareMode,
                                              ContactPhone = tbBasic.ContactPhone,
                                              DeclareStatus = tbDeclareStatus.DeclareStatus
                                           }).ToList();
                if (!string.IsNullOrEmpty(name))
                {
                    linqBasic = linqBasic.Where(s => s.Name.Contains(name.Trim())).ToList();
                }
                if (!string.IsNullOrEmpty(placeUnit))
                {
                    linqBasic = linqBasic.Where(s => s.PlaceUnit.Contains(placeUnit.Trim())).ToList();
                }
                if (declareQualificationId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.DeclareQualificationID == declareQualificationId).ToList();
                }
                if (declareModeId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.DeclareModeID == declareModeId).ToList();
                }
                if (declareGradeId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.DeclareGradeID == declareGradeId).ToList();
                }
                if (declareSeriesId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.DeclareSeriesID == declareSeriesId).ToList();
                }
                if (declareStatusId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.DeclareStatusID == declareStatusId).ToList();
                }
                int intTotalRow = linqBasic.Count();
                List<BasicVo> listFile = linqBasic.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<BasicVo> bsgrid = new Bsgrid<BasicVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqBasic;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSex()
        {
            List<SelectVo> listSex = (from tbSex in myModels.S_Sex
                                      select new SelectVo
                                      {
                                          id = tbSex.SexID,
                                          text = tbSex.Sex
                                      }).ToList();
            listSex = Tools.SetSelectJson(listSex);
            return Json(listSex, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 专业
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectMajor()
        {
            List<SelectVo> listMajor = (from tbMajor in myModels.S_Major
                                        select new SelectVo
                                        {
                                            id = tbMajor.MajorID,
                                            text = tbMajor.Major
                                        }).ToList();
            listMajor = Tools.SetSelectJson(listMajor);
            return Json(listMajor, JsonRequestBehavior.AllowGet);
        }
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
        /// 申报资格
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectDeclareQualification()
        {
            List<SelectVo> listDeclareQualification = (from tbDeclareQualification in myModels.S_DeclareQualification
                                           select new SelectVo
                                           {
                                               id = tbDeclareQualification.DeclareQualificationID,
                                               text = tbDeclareQualification.DeclareQualification
                                           }).ToList();
            listDeclareQualification = Tools.SetSelectJson(listDeclareQualification);
            return Json(listDeclareQualification, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报方式
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectDeclareMode()
        {
            List<SelectVo> listDeclareMode = (from tbDeclareMode in myModels.S_DeclareMode
                                                       select new SelectVo
                                                       {
                                                           id = tbDeclareMode.DeclareModeID,
                                                           text = tbDeclareMode.DeclareMode
                                                       }).ToList();
            listDeclareMode = Tools.SetSelectJson(listDeclareMode);
            return Json(listDeclareMode, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报类别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCategory()
        {
            List<SelectVo> listCategory = (from tbCategory in myModels.S_Category
                                           select new SelectVo
                                           {
                                               id = tbCategory.CategoryID,
                                               text = tbCategory.Category
                                           }).ToList();
            listCategory = Tools.SetSelectJson(listCategory);
            return Json(listCategory, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectDeclareStatus()
        {
            List<SelectVo> listDeclareStatus = (from tbDeclareStatus in myModels.S_DeclareStatus
                                           select new SelectVo
                                           {
                                               id = tbDeclareStatus.DeclareStatusID,
                                               text = tbDeclareStatus.DeclareStatus
                                           }).ToList();
            listDeclareStatus = Tools.SetSelectJson(listDeclareStatus);
            return Json(listDeclareStatus, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 排序方式
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSortlevel()
        {
            List<SelectVo> listSortlevel = (from tbSortlevel in myModels.S_Sortlevel
                                                select new SelectVo
                                                {
                                                    id = tbSortlevel.SortlevelID,
                                                    text = tbSortlevel.Sortlevel
                                                }).ToList();
            listSortlevel = Tools.SetSelectJson(listSortlevel);
            return Json(listSortlevel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 学历
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectEducation()
        {
            List<SelectVo> listEducation = (from tbEducation in myModels.S_Education
                                            select new SelectVo
                                            {
                                                id = tbEducation.EducationID,
                                                text = tbEducation.Education
                                            }).ToList();
            listEducation = Tools.SetSelectJson(listEducation);
            return Json(listEducation, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 专业职称
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectMajorsPost()
        {
            List<SelectVo> listMajorsPost = (from tbMajorsPost in myModels.S_MajorsPost
                                            select new SelectVo
                                            {
                                                id = tbMajorsPost.MajorsPostID,
                                                text = tbMajorsPost.MajorsPost
                                            }).ToList();
            listMajorsPost = Tools.SetSelectJson(listMajorsPost);
            return Json(listMajorsPost, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 外语级别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectForeignLanguageRank()
        {
            List<SelectVo> listForeignLanguageRank = (from tbForeignLanguageRank in myModels.S_ForeignLanguageRank
                                            select new SelectVo
                                            {
                                                id = tbForeignLanguageRank.ForeignLanguageRankID,
                                                text = tbForeignLanguageRank.ForeignLanguageRank
                                            }).ToList();
            listForeignLanguageRank = Tools.SetSelectJson(listForeignLanguageRank);
            return Json(listForeignLanguageRank, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 考试情况
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectExamCase()
        {
            List<SelectVo> listExamCase = (from tbExamCase in myModels.S_ExamCase
                                                      select new SelectVo
                                                      {
                                                          id = tbExamCase.ExamCaseID,
                                                          text = tbExamCase.ExamCase
                                                      }).ToList();
            listExamCase = Tools.SetSelectJson(listExamCase);
            return Json(listExamCase, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 考核结果
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectExamineResult()
        {
            List<SelectVo> listExamineResult = (from tbExamineResult in myModels.S_ExamineResult
                                           select new SelectVo
                                           {
                                               id = tbExamineResult.ExamineResultID,
                                               text = tbExamineResult.ExamineResult
                                           }).ToList();
            listExamineResult = Tools.SetSelectJson(listExamineResult);
            return Json(listExamineResult, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报年度
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectDeclareYear()
        {
            List<SelectVo> listDeclareYear = (from tbDeclareYear in myModels.S_DeclareYear
                                                select new SelectVo
                                                {
                                                    id = tbDeclareYear.DeclareYearID,
                                                    text = tbDeclareYear.DeclareYear
                                                }).ToList();
            listDeclareYear = Tools.SetSelectJson(listDeclareYear);
            return Json(listDeclareYear, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 编号
        /// <summary>
        /// 查询编号基本列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectBianHao(BsgridPage bsgridPage,int educationId, int declareModeId, int majorsPostId)
        {
            try
            {
                List<BasicVo> linqBasic = (from tbBasic in myModels.B_Basic
                                           join tbEducation in myModels.S_Education on tbBasic.EducationID equals tbEducation.EducationID
                                           join tbDeclareMode in myModels.S_DeclareMode on tbBasic.DeclareModeID equals tbDeclareMode.DeclareModeID
                                           join tbMajorsPost in myModels.S_MajorsPost on tbBasic.MajorsPostID equals tbMajorsPost.MajorsPostID
                                           select new BasicVo
                                           {
                                               BasicID = tbBasic.BasicID,
                                               EducationID = tbEducation.EducationID,
                                               DeclareModeID = tbDeclareMode.DeclareModeID,
                                               MajorsPostID = tbMajorsPost.MajorsPostID,
                                               Name = tbBasic.Name,
                                               PlaceUnit = tbBasic.PlaceUnit,                                               
                                               Major = tbBasic.Major,
                                               Education = tbEducation.Education,
                                               DeclareMode = tbDeclareMode.DeclareMode,
                                               MajorsPost = tbMajorsPost.MajorsPost,
                                               joinWorkTimer = tbBasic.JoinWorkTime.ToString()
                                           }).ToList();
                if (educationId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.EducationID == educationId).ToList();
                }
                if (declareModeId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.DeclareModeID == declareModeId).ToList();
                }
                if (majorsPostId > 0)
                {
                    linqBasic = linqBasic.Where(s => s.MajorsPostID == majorsPostId).ToList();
                }
                int intTotalRow = linqBasic.Count();
                List<BasicVo> listFile = linqBasic.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<BasicVo> bsgrid = new Bsgrid<BasicVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqBasic;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 审核
        /// <summary>
        /// 成果奖励表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectAchievement(BsgridPage bsgridPage)
        {
            try
            {
                List<ChengGuoVo> linqItem = (from tbItem in myModels.B_Achievement
                                             orderby tbItem.AchievementID descending
                                             join tbAchievementGrade in myModels.S_AchievementGrade on tbItem.AchievementGradeID equals tbAchievementGrade.AchievementGradeID
                                             select new ChengGuoVo
                                             {
                                                 AchievementID = tbItem.AchievementID,
                                                 Item = tbItem.Item,
                                                 Timeser = tbItem.Timers.ToString(),
                                                 SituateNextrs = tbItem.SituateNextrs,
                                                 AchievementGrade = tbAchievementGrade.AchievementGrade,
                                                 RatifySection = tbItem.RatifySection
                                             }).ToList();
                int intTotalRow = linqItem.Count();
                List<ChengGuoVo> listFile = linqItem.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                //List<ChengGuoVo> modelUR = linqItem(0);
                Bsgrid<ChengGuoVo> bsgrid = new Bsgrid<ChengGuoVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqItem;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 论文作品表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectProduction(BsgridPage bsgridPage)
        {
            try
            {
                List<LunWenVo> linqProduct = (from tbProduct in myModels.B_Production
                                              orderby tbProduct.ProductionID descending
                                              select new LunWenVo
                                              {
                                                  ProductionID = tbProduct.ProductionID,
                                                  Timeser = tbProduct.Timery.ToString(),
                                                  Title = tbProduct.Title,
                                                  SituateNextry = tbProduct.SituateNextry,
                                                  PeriodicalPublishing = tbProduct.PeriodicalPublishing
                                              }).ToList();
                int intTotalRow = linqProduct.Count();
                List<LunWenVo> listFile = linqProduct.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<LunWenVo> bsgrid = new Bsgrid<LunWenVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = intTotalRow;
                bsgrid.data = linqProduct;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 审核上报信息查询绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBasicxtdy()
        {
            var listBasicxtdy = (from tbBasicxtdy in myModels.B_Basic
                                 join tbDeclareGrade in myModels.S_DeclareGrade on tbBasicxtdy.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                                 join tbDeclareSeries in myModels.S_DeclareSeries on tbBasicxtdy.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                                 join tbDeclareQualification in myModels.S_DeclareQualification on tbBasicxtdy.DeclareQualificationID equals tbDeclareQualification.DeclareQualificationID
                                 join tbSex in myModels.S_Sex on tbBasicxtdy.SexID equals tbSex.SexID
                                 join tbEducation in myModels.S_Education on tbBasicxtdy.EducationID equals tbEducation.EducationID
                                 join tbMajor in myModels.S_Major on tbBasicxtdy.MajorID equals tbMajor.MajorID
                                 join tbExpr1 in myModels.S_Education on tbBasicxtdy.JudgeEducationID equals tbExpr1.EducationID
                                 join tbExpr2 in myModels.S_Major on tbBasicxtdy.MajorID1 equals tbExpr2.MajorID
                                 join tbMajorsPost in myModels.S_MajorsPost on tbBasicxtdy.MajorsPostID equals tbMajorsPost.MajorsPostID
                                 join tbForeignLanguageRank in myModels.S_ForeignLanguageRank on tbBasicxtdy.ForeignLanguageRankID equals tbForeignLanguageRank.ForeignLanguageRankID
                                 join tbDeclareMode in myModels.S_DeclareMode on tbBasicxtdy.DeclareModeID equals tbDeclareMode.DeclareModeID
                                 join tbExamCase in myModels.S_ExamCase on tbBasicxtdy.ExamCaseID equals tbExamCase.ExamCaseID
                                 join tbExamResult in myModels.S_ExamineResult on tbBasicxtdy.ExamineResultID equals tbExamResult.ExamineResultID
                                 join tbDeclareYear in myModels.S_DeclareYear on tbBasicxtdy.DeclareYearID equals tbDeclareYear.DeclareYearID
                                 join tbExamineResult in myModels.S_ExamineResult on tbBasicxtdy.ExamineResultID1 equals tbExamineResult.ExamineResultID
                                 join tbExpr4 in myModels.S_DeclareYear on tbBasicxtdy.DeclareYearID1 equals tbExpr4.DeclareYearID
                                 join tbExpr5 in myModels.S_ExamineResult on tbBasicxtdy.ExamineResultID2 equals tbExpr5.ExamineResultID
                                 join tbExpr6 in myModels.S_DeclareYear on tbBasicxtdy.DeclareYearID2 equals tbExpr6.DeclareYearID
                                 join tbExpr7 in myModels.S_ExamineResult on tbBasicxtdy.ExamineResultID3 equals tbExpr7.ExamineResultID
                                 join tbExpr8 in myModels.S_ExamineResult on tbBasicxtdy.ExamineResultID equals tbExpr8.ExamineResultID
                                 orderby tbBasicxtdy.BasicID descending
                                 select new
                                 {
                                     DeclareGradeID = tbDeclareGrade.DeclareGradeID,
                                     DeclareSeriesID = tbDeclareSeries.DeclareSeriesID,
                                     DeclareQualificationID = tbDeclareQualification.DeclareQualificationID,
                                     formTimer = tbBasicxtdy.FormTime.ToString(),
                                     Name = tbBasicxtdy.Name,
                                     SexID = tbSex.SexID,
                                     birthdayer = tbBasicxtdy.Birthday.ToString(),
                                     PlaceUnit = tbBasicxtdy.PlaceUnit,
                                     EducationID = tbEducation.EducationID,
                                     graduateTimer = tbBasicxtdy.GraduateTime.ToString(),
                                     GraduateSchool = tbBasicxtdy.GraduateSchool,
                                     MajorID = tbMajor.MajorID,
                                     EducationID2 = tbEducation.EducationID,
                                     graduateTime1er = tbBasicxtdy.GraduateTime1.ToString(),
                                     GraduateSchool1 = tbBasicxtdy.GraduateSchool1,
                                     MajorID1 = tbExpr2.MajorID,
                                     MajorsPostID = tbMajorsPost.MajorsPostID,
                                     gainedQualificationTimer = tbBasicxtdy.GainedQualificationTime.ToString(),
                                     engageTimer = tbBasicxtdy.EngageTime.ToString(),
                                     EngageYear = tbBasicxtdy.EngageYear,
                                     Major = tbBasicxtdy.Major,
                                     MajorYear = tbBasicxtdy.MajorYear,
                                     takeOfficeTimer = tbBasicxtdy.TakeOffice.ToString(),
                                     TakeOfficeTime = tbBasicxtdy.TakeOfficeTime,
                                     joinWorkTimer = tbBasicxtdy.JoinWorkTime.ToString(),
                                     ForeignLanguage = tbBasicxtdy.ForeignLanguage,
                                     ForeignLanguageRankID = tbForeignLanguageRank.ForeignLanguageRankID,
                                     ForeignLanguageResult = tbBasicxtdy.ForeignLanguageResult,
                                     DeclareModeID = tbDeclareMode.DeclareModeID,
                                     QuantityScore = tbBasicxtdy.QuantityScore,
                                     ExamCaseID = tbExamCase.ExamCaseID,
                                     ExamResult = tbBasicxtdy.ExamResult,
                                     
                                     DeclareYearID = tbBasicxtdy.DeclareYearID,
                                     ExamineResultID1 = tbBasicxtdy.ExamineResultID1,
                                     DeclareYearID1 = tbBasicxtdy.DeclareYearID1,
                                     ExamineResultID2 = tbBasicxtdy.ExamineResultID2,
                                     DeclareYearID2 = tbBasicxtdy.DeclareYearID2,
                                     ExamineResultID3 = tbBasicxtdy.ExamineResultID3,
                                     DeclareYearID3 = tbBasicxtdy.DeclareYearID3,
                                     ExamineResultID4 = tbBasicxtdy.ExamineResultID4,
                                     DeclareYearID4 = tbBasicxtdy.DeclareYearID4,
                                     ExamineResultID5 = tbBasicxtdy.ExamineResultID5,
                                     ExamineResultID = tbBasicxtdy.ExamineResultID,

                                 }).ToList();//基本信息
            var listBasisxtdy = (from tbBasisxtdy in myModels.B_Basis
                                 orderby tbBasisxtdy.BasisID descending
                                 select new
                                 {
                                     BasisID = tbBasisxtdy.BasisID,
                                     RecordUnit = tbBasisxtdy.RecordUnit,
                                 }).ToList();//基础信息
            var listExperiencext = (from tbExperiencext in myModels.B_Experience
                                    orderby tbExperiencext.ExperienceID descending
                                    select new
                                    {
                                        ExperienceID = tbExperiencext.ExperienceID,
                                        Experience = tbExperiencext.Experience,
                                    }).ToList();//工作经历
            var listWorkResultxt = (from tbWorkResultxt in myModels.B_WorkResult
                                    orderby tbWorkResultxt.WorkResultID descending
                                    select new
                                    {
                                        WorkResultID = tbWorkResultxt.WorkResultID,
                                        WorkResult = tbWorkResultxt.WorkResult,
                                    }).ToList();//工作成绩
            return Json(new { listBasicxtdy, listBasisxtdy, listExperiencext, listWorkResultxt}, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 审核上报保存
        /// </summary>
        /// <param name="EducationCondition"></param>
        /// <param name="LanguageCondition"></param>
        /// <param name="ComputerCondition"></param>
        /// <param name="SpecialCondition"></param>
        /// <param name="ProductionFake"></param>
        /// <param name="AchievementFake"></param>
        /// <param name="CertificateFake"></param>
        /// <param name="Other"></param>
        /// <param name="OtherCause"></param>
        /// <param name="AuditPass"></param>
        /// <returns></returns>
        public ActionResult InsertAuditAppear(string EducationCondition,string LanguageCondition, string ComputerCondition, string SpecialCondition, 
                string ProductionFake, string AchievementFake, string CertificateFake,string Other,string OtherCause, string AuditPass)
        {
            int Msg = 0;
            try
            {
                //如果撤销这段代码，“审核意见”那里就不能输入相同的数据，好比如数据库已经有一个数据（暂无），你审核的时候就不能再输入暂无，否则会审核失败
                //int oldAuditAppear = (from tbAuditAppear in myModels.B_AuditAppear
                //                      where
                //                     tbAuditAppear.EducationCondition == EducationCondition
                //                     && tbAuditAppear.LanguageCondition == LanguageCondition
                //                     && tbAuditAppear.LanguageCondition == ComputerCondition
                //                     && tbAuditAppear.LanguageCondition == SpecialCondition
                //                     && tbAuditAppear.LanguageCondition == ProductionFake
                //                     && tbAuditAppear.LanguageCondition == AchievementFake
                //                     && tbAuditAppear.LanguageCondition == CertificateFake
                //                     && tbAuditAppear.LanguageCondition == Other
                //                     && tbAuditAppear.OtherCause == OtherCause
                //                     && tbAuditAppear.AuditPass == AuditPass
                //                      select tbAuditAppear).Count();
                //if (oldAuditAppear == 0)
                //{
                    B_AuditAppear myAuditAppear = new B_AuditAppear();
                    myAuditAppear.EducationCondition = EducationCondition;
                    myAuditAppear.LanguageCondition = LanguageCondition;
                    myAuditAppear.ComputerCondition = ComputerCondition;
                    myAuditAppear.SpecialCondition = SpecialCondition;
                    myAuditAppear.ProductionFake = ProductionFake;
                    myAuditAppear.AchievementFake = AchievementFake;
                    myAuditAppear.CertificateFake = CertificateFake;
                    myAuditAppear.Other = Other;
                    myAuditAppear.OtherCause = OtherCause;
                    myAuditAppear.AuditPass = AuditPass;
                    myModels.B_AuditAppear.Add(myAuditAppear);
                    myModels.SaveChanges();
                    //查询出整个表的信息
                    List<B_AuditAppear> list = (from tbAuditAppear in myModels.B_AuditAppear
                                                select tbAuditAppear).ToList();
                    string Str = list[list.Count - 1].AuditPass.Trim();//查询出最新一行的数据 
                    if (Str == "审核通过")
                    {
                        Msg = 3;
                    }
                    else if (Str == "审核未通过")
                    {
                        Msg = 1;
                    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="BasicID"></param>
        /// <param name="ZhuangTaiID"></param>
        /// <returns></returns>
        public ActionResult UpdateZhuangTi(int basicID, int ZhuangTaiID)
        {
            if (basicID != 0 && ZhuangTaiID != 0)
            {
                List<B_Basic> list = (from tbBasic in myModels.B_Basic
                                      where tbBasic.BasicID == basicID
                                      select tbBasic).ToList();
                list[0].DeclareStatusID = ZhuangTaiID;
                myModels.Entry(list[0]).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 导出花名册
        /// <summary>
        /// 导出花名册
        /// </summary>
        /// <param name="declareGradeId"></param>
        /// <param name="declareSeriesId"></param>
        /// <param name="declareStatusId"></param>
        /// <returns></returns>
        public ActionResult ExportToExcel(int declareGradeId, int declareSeriesId, int declareStatusId)
        {
            var linqBasic = (from tbBasic in myModels.B_Basic
                             join tbSex in myModels.S_Sex on tbBasic.SexID equals tbSex.SexID
                             join tbEducation in myModels.S_Education on tbBasic.EducationID equals tbEducation.EducationID
                             join tbDeclareGrade in myModels.S_DeclareGrade on tbBasic.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                             join tbDeclareSeries in myModels.S_DeclareSeries on tbBasic.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                             join tbDeclareQualification in myModels.S_DeclareQualification on tbBasic.DeclareQualificationID equals tbDeclareQualification.DeclareQualificationID
                             join tbDeclareMode in myModels.S_DeclareMode on tbBasic.DeclareModeID equals tbDeclareMode.DeclareModeID
                             join tbDeclareStatus in myModels.S_DeclareStatus on tbBasic.DeclareStatusID equals tbDeclareStatus.DeclareStatusID
                             select new BasicVo
                             {
                                 BasicID = tbBasic.BasicID,
                                 DeclareGradeID = tbDeclareGrade.DeclareGradeID,
                                 DeclareSeriesID = tbDeclareSeries.DeclareSeriesID,
                                 DeclareStatusID = tbDeclareStatus.DeclareStatusID,
                                 PlaceUnit = tbBasic.PlaceUnit,
                                 Name = tbBasic.Name,
                                 Sex = tbSex.Sex,
                                 birthdayer = tbBasic.Birthday.ToString(),
                                 Major = tbBasic.Major,
                                 Education = tbEducation.Education,
                                 DeclareGrade = tbDeclareGrade.DeclareGrade,
                                 DeclareSeries = tbDeclareSeries.DeclareSeries,
                                 DeclareQualification = tbDeclareQualification.DeclareQualification,
                                 formTimer = tbBasic.FormTime.ToString(),
                                 DeclareMode = tbDeclareMode.DeclareMode,
                                 ContactPhone = tbBasic.ContactPhone,
                                 DeclareStatus = tbDeclareStatus.DeclareStatus
                             }).ToList();
            if (declareGradeId > 0)
            {
                linqBasic = linqBasic.Where(s => s.DeclareGradeID == declareGradeId).ToList();
            }
            if (declareSeriesId > 0)
            {
                linqBasic = linqBasic.Where(s => s.DeclareSeriesID == declareSeriesId).ToList();
            }
            if (declareStatusId > 0)
            {
                linqBasic = linqBasic.Where(s => s.DeclareStatusID == declareStatusId).ToList();
            }
            //查询数据
            List<BasicVo> listExaminee = linqBasic.ToList();
            //创建Excel对象 工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook excelBook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //创建Excel工作表 Sheet
            NPOI.SS.UserModel.ISheet sheet1 = excelBook.CreateSheet("基本信息花名册");
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);//给sheet1添加第一行的头部标题
            row1.CreateCell(0).SetCellValue("呈报单位");//0
            row1.CreateCell(1).SetCellValue("姓名");//1
            row1.CreateCell(2).SetCellValue("性别");//2
            row1.CreateCell(3).SetCellValue("出生年月");//3
            row1.CreateCell(4).SetCellValue("现所从事专业");//4
            row1.CreateCell(5).SetCellValue("学历");//5
            row1.CreateCell(6).SetCellValue("等级名称");//6
            row1.CreateCell(7).SetCellValue("专业名称");//7
            row1.CreateCell(8).SetCellValue("资格名称");//8
            row1.CreateCell(9).SetCellValue("报送时间");//9
            row1.CreateCell(10).SetCellValue("申报方式");//10
            row1.CreateCell(11).SetCellValue("联系电话");//11
            row1.CreateCell(12).SetCellValue("申报状态");//12

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < listExaminee.Count; i++)
            {
                //创建行
                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                //呈报单位
                rowTemp.CreateCell(0).SetCellValue(listExaminee[i].PlaceUnit);
                //姓名
                rowTemp.CreateCell(1).SetCellValue(listExaminee[i].Name);
                //性别
                rowTemp.CreateCell(2).SetCellValue(listExaminee[i].Sex);
                //出生年月
                rowTemp.CreateCell(3).SetCellValue(listExaminee[i].birthdayer);
                //现所从事专业
                rowTemp.CreateCell(4).SetCellValue(listExaminee[i].Major);
                //学历
                rowTemp.CreateCell(5).SetCellValue(listExaminee[i].Education);
                //申报等级
                rowTemp.CreateCell(6).SetCellValue(listExaminee[i].DeclareGrade);
                //专业名称
                rowTemp.CreateCell(7).SetCellValue(listExaminee[i].DeclareSeries);
                //资格名称
                rowTemp.CreateCell(8).SetCellValue(listExaminee[i].DeclareQualification);
                //报送时间
                rowTemp.CreateCell(9).SetCellValue(listExaminee[i].formTimer);
                //申报方式
                rowTemp.CreateCell(10).SetCellValue(listExaminee[i].DeclareMode);
                //联系电话
                rowTemp.CreateCell(11).SetCellValue(listExaminee[i].ContactPhone);
                //申报状态
                rowTemp.CreateCell(12).SetCellValue(listExaminee[i].DeclareStatus);
            }
            //输出的文件名称
            string fileName = "基本信息花名册" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";

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

        #region 导出一览表
        /// <summary>
        /// 导出一览表
        /// </summary>
        /// <param name="BasicID"></param>
        /// <returns></returns>
        public ActionResult ExportToExcels(string[] BasicID)
        {

            int BasicIDs = Convert.ToInt32(BasicID[0]);
            var linqBasic = (from tbBasic in myModels.B_Basic
                             join tbSex in myModels.S_Sex on tbBasic.SexID equals tbSex.SexID
                             join tbEducation in myModels.S_Education on tbBasic.EducationID equals tbEducation.EducationID
                             join tbDeclareGrade in myModels.S_DeclareGrade on tbBasic.DeclareGradeID equals tbDeclareGrade.DeclareGradeID
                             join tbDeclareSeries in myModels.S_DeclareSeries on tbBasic.DeclareSeriesID equals tbDeclareSeries.DeclareSeriesID
                             join tbDeclareQualification in myModels.S_DeclareQualification on tbBasic.DeclareQualificationID equals tbDeclareQualification.DeclareQualificationID
                             join tbDeclareMode in myModels.S_DeclareMode on tbBasic.DeclareModeID equals tbDeclareMode.DeclareModeID
                             join tbDeclareStatus in myModels.S_DeclareStatus on tbBasic.DeclareStatusID equals tbDeclareStatus.DeclareStatusID
                             where tbBasic.BasicID == BasicIDs
                             select new BasicVo
                             {
                                 BasicID = tbBasic.BasicID,
                                 DeclareGradeID = tbDeclareGrade.DeclareGradeID,
                                 DeclareSeriesID = tbDeclareSeries.DeclareSeriesID,
                                 DeclareStatusID = tbDeclareStatus.DeclareStatusID,
                                 PlaceUnit = tbBasic.PlaceUnit,
                                 Name = tbBasic.Name,
                                 Sex = tbSex.Sex,
                                 birthdayer = tbBasic.Birthday.ToString(),
                                 Major = tbBasic.Major,
                                 Education = tbEducation.Education,
                                 DeclareGrade = tbDeclareGrade.DeclareGrade,
                                 DeclareSeries = tbDeclareSeries.DeclareSeries,
                                 DeclareQualification = tbDeclareQualification.DeclareQualification,
                                 formTimer = tbBasic.FormTime.ToString(),
                                 DeclareMode = tbDeclareMode.DeclareMode,
                                 ContactPhone = tbBasic.ContactPhone,
                                 DeclareStatus = tbDeclareStatus.DeclareStatus
                             }).ToList();
            //查询数据
            List<BasicVo> listExaminee = linqBasic.ToList();
            //创建Excel对象 工作簿
            NPOI.HSSF.UserModel.HSSFWorkbook excelBook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //创建Excel工作表 Sheet
            NPOI.SS.UserModel.ISheet sheet1 = excelBook.CreateSheet("基本信息花名册");
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);//给sheet1添加第一行的头部标题
            row1.CreateCell(0).SetCellValue("呈报单位");//0
            row1.CreateCell(1).SetCellValue("姓名");//1
            row1.CreateCell(2).SetCellValue("性别");//2
            row1.CreateCell(3).SetCellValue("出生年月");//3
            row1.CreateCell(4).SetCellValue("现所从事专业");//4
            row1.CreateCell(5).SetCellValue("学历");//5
            row1.CreateCell(6).SetCellValue("等级名称");//6
            row1.CreateCell(7).SetCellValue("专业名称");//7
            row1.CreateCell(8).SetCellValue("资格名称");//8
            row1.CreateCell(9).SetCellValue("报送时间");//9
            row1.CreateCell(10).SetCellValue("申报方式");//10
            row1.CreateCell(11).SetCellValue("联系电话");//11
            row1.CreateCell(12).SetCellValue("申报状态");//12

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < listExaminee.Count; i++)
            {
                //创建行
                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);

                //呈报单位
                rowTemp.CreateCell(0).SetCellValue(listExaminee[i].PlaceUnit);
                //姓名
                rowTemp.CreateCell(1).SetCellValue(listExaminee[i].Name);
                //性别
                rowTemp.CreateCell(2).SetCellValue(listExaminee[i].Sex);
                //出生年月
                rowTemp.CreateCell(3).SetCellValue(listExaminee[i].birthdayer);
                //现所从事专业
                rowTemp.CreateCell(4).SetCellValue(listExaminee[i].Major);
                //学历
                rowTemp.CreateCell(5).SetCellValue(listExaminee[i].Education);
                //申报等级
                rowTemp.CreateCell(6).SetCellValue(listExaminee[i].DeclareGrade);
                //专业名称
                rowTemp.CreateCell(7).SetCellValue(listExaminee[i].DeclareSeries);
                //资格名称
                rowTemp.CreateCell(8).SetCellValue(listExaminee[i].DeclareQualification);
                //报送时间
                rowTemp.CreateCell(9).SetCellValue(listExaminee[i].formTimer);
                //申报方式
                rowTemp.CreateCell(10).SetCellValue(listExaminee[i].DeclareMode);
                //联系电话
                rowTemp.CreateCell(11).SetCellValue(listExaminee[i].ContactPhone);
                //申报状态
                rowTemp.CreateCell(12).SetCellValue(listExaminee[i].DeclareStatus);
            }
            //输出的文件名称
            string fileName = "基本信息花名册" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";

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

        #region 上报&&全部上报
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="BasicID"></param>
        /// <param name="StatusZhuangTaiID"></param>
        /// <returns></returns>
        public ActionResult XiuGaiZhuangTi(string [] BasicID)
        {
            if (BasicID != null)
            {
                for (int i = 0; i < BasicID.Length; i++)
                {
                    int BasicIDs = Convert.ToInt32(BasicID[i]);
                    var  list = (from tbBasic in myModels.B_Basic
                                          where tbBasic.BasicID == BasicIDs
                                          select tbBasic).Single();
                    list.DeclareStatusID = 5;
                    myModels.Entry(list).State = System.Data.Entity.EntityState.Modified;
                    myModels.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }            
        }
        /// <summary>
        /// 判断当状态为"待审核"时
        /// </summary>
        /// <param name="BasicID"></param>
        /// <returns></returns>
        public ActionResult DeclareStatus(int BasicID)
        {
            var list = (from tbBasic in myModels.B_Basic
                        where tbBasic.BasicID == BasicID
                        select new {
                            DeclareStatusID =tbBasic.DeclareStatusID
                        }).Single();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 发送邮件
        public ActionResult SendMails(string EmailPostbox, string Emails, string EmailContent, string Email, string EmailName, string EmailPassword)
        {
            string senderServerIp = "smtp.qq.com";  //使用163代理邮箱服务器（也可使用qq的代理邮箱服务器，但需要与具体邮箱对应）
            string toMailAddress = Emails;          //要发送对象的邮箱
            string fromMailAddress = Email;         //你的邮箱
            string subjectInfo = EmailPostbox;      //主题（标题）
            string bodyInfo = "<p>" + EmailContent + "</p>";    //以HTML格式发送的邮件（内容）
            string mailUsername = EmailName;        //登录邮箱的用户名
            string mailPassword = EmailPassword;    //对应的登录邮箱的第三方密码（你的邮箱不论是163邮箱还是qq邮箱，都需要自行开通STMP服务）
            string mailPort = "25";                 //发送邮箱的端口号

            //创建发送邮箱的对象
            Email myEmail = new Email(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, true, false);

            //添加附件
            //Eemail.AddAttachments(attachPath);

            if (myEmail.Send())
            {
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}