using CrystalDecisions.CrystalReports.Engine;
using SDZCBootstrap.Common;
using SDZCSBPingShenXT.Models;
using SDZCSBPingShenXT.Vo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;


namespace SDZCSBPingShenXT.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        Models.SDZCSBPingShenXTEntities myModels = new Models.SDZCSBPingShenXTEntities();
        /// <summary>
        /// 前台登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginT()
        {
            return View();
        }
        /// <summary>
        /// 后台登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginUnit()
        {
            return View();
        }

        /// <summary>
        /// 申报年度列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ShenBaoNianDuLB()
        {
            return View();
        }

        /// <summary>
        /// 用户注册    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserRegister()
        {
            return View();
        }
        /// <summary>
        /// 个人申报    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Declare()
        {
            return View();
        }
        /// <summary>
        /// 个人申报页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Declares()
        {
            return View();
        }
        /// <summary>
        /// 单位注册    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UnitRegister()
        {
            return View();
        }
        /// <summary>
        /// 业务指南    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult YeWuZhiNan()
        {
            return View();
        }
        /// <summary>
        /// 通知公告    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ToZhiGG()
        {
            return View();
        }
        /// <summary>
        /// 任职资格条件  页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RenZhiZiGeTJ()
        {
            return View();
        }
        /// <summary>
        /// 相关政策    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult XiangGuanZhengCe()
        {
            return View();
        }
        /// <summary>
        /// 常见问题    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangJianWenTi()
        {
            return View();
        }
        /// <summary>
        /// 工作流程介绍  页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GongZuoLiuChengJS()
        {
            return View();
        }
        /// <summary>
        /// 联系我们    页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LianXiWoMen()
        {
            return View();
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ValideCode()
        {
            string strVildeCode = ValidCodeUtils.GetRandomCode(5);
            Session["vildeCode"] = strVildeCode;//放入session
            byte[] vildeImage = ValidCodeUtils.CreateImage(strVildeCode);
            return File(vildeImage, @"image/jpeg");
        }

        #region 下拉框
        /// <summary>
        /// 下拉性别
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
        /// 下拉密码问题
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectMiMaWT()
        {
            List<SelectVo> listMiMaWT = (from tbMiMaWT in myModels.S_CodeAnswer
                                      select new SelectVo
                                      {
                                          id = tbMiMaWT.AnswerID,
                                          text = tbMiMaWT.Answer
                                      }).ToList();
            listMiMaWT = Tools.SetSelectJson(listMiMaWT);
            return Json(listMiMaWT, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 现专业等级
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectZYDengJi()
        {
            List<SelectVo> listZYDengJi = (from tbZYDengJi in myModels.S_MajorsGrade
                                         select new SelectVo
                                         {
                                             id = tbZYDengJi.MajorsGradeID,
                                             text = tbZYDengJi.MajorsGrade
                                         }).ToList();
            listZYDengJi = Tools.SetSelectJson(listZYDengJi);
            return Json(listZYDengJi, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 现专业系列
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectZYXiLie()
        {
            List<SelectVo> listZYXiLie = (from tbZYXiLie in myModels.S_MajorsSeries
                                           select new SelectVo
                                           {
                                               id = tbZYXiLie.MajorsSeriesID,
                                               text = tbZYXiLie.MajorsSeries
                                           }).ToList();
            listZYXiLie = Tools.SetSelectJson(listZYXiLie);
            return Json(listZYXiLie, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 下拉专业职称
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectZYZhiCheng()
        {
            List<SelectVo> listZYZhiCheng = (from tbZYZhiCheng in myModels.S_MajorsPost
                                          select new SelectVo
                                          {
                                              id = tbZYZhiCheng.MajorsPostID,
                                              text = tbZYZhiCheng.MajorsPost
                                          }).ToList();
            listZYZhiCheng = Tools.SetSelectJson(listZYZhiCheng);
            return Json(listZYZhiCheng, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报年度
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
        /// 拟申报等级
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
        /// 拟申报系列
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
        /// 拟申报职称
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSBZhiCheng()
        {
            List<SelectVo> listSBZhiCheng = (from tbSBZhiCheng in myModels.S_DeclarePost
                                             select new SelectVo
                                             {
                                                 id = tbSBZhiCheng.DeclarePostID,
                                                 text = tbSBZhiCheng.DeclarePost
                                             }).ToList();
            listSBZhiCheng = Tools.SetSelectJson(listSBZhiCheng);
            return Json(listSBZhiCheng, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报资格
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSBZiGe()
        {
            List<SelectVo> listSBZiGe = (from tbSBZiGe in myModels.S_DeclareQualification
                                             select new SelectVo
                                             {
                                                 id = tbSBZiGe.DeclareQualificationID,
                                                 text = tbSBZiGe.DeclareQualification
                                             }).ToList();
            listSBZiGe = Tools.SetSelectJson(listSBZiGe);
            return Json(listSBZiGe, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 成果等级
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCGDengJi()
        {
            List<SelectVo> listCGDengJi = (from tbCGDengJi in myModels.S_AchievementGrade
                                         select new SelectVo
                                         {
                                             id = tbCGDengJi.AchievementGradeID,
                                             text = tbCGDengJi.AchievementGrade
                                         }).ToList();
            listCGDengJi = Tools.SetSelectJson(listCGDengJi);
            return Json(listCGDengJi, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 单位性质
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectUnitXZ()
        {
            List<SelectVo> listUnitXZ = (from tbUnitXZ in myModels.S_UnitNature
                                             select new SelectVo
                                             {
                                                 id = tbUnitXZ.UnitNatureID,
                                                 text = tbUnitXZ.UnitNature
                                             }).ToList();
            listUnitXZ = Tools.SetSelectJson(listUnitXZ);
            return Json(listUnitXZ, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 单位所属行业
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSuoShuHY()
        {
            List<SelectVo> listSuoShuHY = (from tbSuoShuHY in myModels.S_BelongIndustry
                                         select new SelectVo
                                         {
                                             id = tbSuoShuHY.BelongIndustryID,
                                             text = tbSuoShuHY.BelongIndustry
                                         }).ToList();
            listSuoShuHY = Tools.SetSelectJson(listSuoShuHY);
            return Json(listSuoShuHY, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报人属于
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSBShuYu()
        {
            List<SelectVo> listSBShuYu = (from tbSBShuYu in myModels.S_DeclareBelong
                                           select new SelectVo
                                           {
                                               id = tbSBShuYu.DeclareBelongID,
                                               text = tbSBShuYu.DeclareBelong
                                           }).ToList();
            listSBShuYu = Tools.SetSelectJson(listSBShuYu);
            return Json(listSBShuYu, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 隶属关系
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectLiShuGX()
        {
            List<SelectVo> listLiShuGX = (from tbLiShuGX in myModels.S_Subjection
                                           select new SelectVo
                                           {
                                               id = tbLiShuGX.SubjectionID,
                                               text = tbLiShuGX.Subjection
                                           }).ToList();
            listLiShuGX = Tools.SetSelectJson(listLiShuGX);
            return Json(listLiShuGX, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 学历
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectXueLi()
        {
            List<SelectVo> listXueLi = (from tbXueLi in myModels.S_Education
                                        select new SelectVo
                                          {
                                              id = tbXueLi.EducationID,
                                              text = tbXueLi.Education
                                          }).ToList();
            listXueLi = Tools.SetSelectJson(listXueLi);
            return Json(listXueLi, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 专业
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectZhuanYe()
        {
            List<SelectVo> listZhuanYe = (from tbZhuanYe in myModels.S_Major
                                        select new SelectVo
                                        {
                                            id = tbZhuanYe.MajorID,
                                            text = tbZhuanYe.Major
                                        }).ToList();
            listZhuanYe = Tools.SetSelectJson(listZhuanYe);
            return Json(listZhuanYe, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 外语级别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectWaiYuJiBie()
        {
            List<SelectVo> lisWaiYuJiBie = (from tbWaiYuJiBie in myModels.S_ForeignLanguageRank
                                          select new SelectVo
                                          {
                                              id = tbWaiYuJiBie.ForeignLanguageRankID,
                                              text = tbWaiYuJiBie.ForeignLanguageRank
                                          }).ToList();
            lisWaiYuJiBie = Tools.SetSelectJson(lisWaiYuJiBie);
            return Json(lisWaiYuJiBie, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 考试情况
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectKaoShiQK()
        {
            List<SelectVo> listKaoShiQK = (from tbKaoShiQK in myModels.S_ExamCase
                                           select new SelectVo
                                           {
                                               id = tbKaoShiQK.ExamCaseID,
                                               text = tbKaoShiQK.ExamCase
                                           }).ToList();
            listKaoShiQK = Tools.SetSelectJson(listKaoShiQK);
            return Json(listKaoShiQK, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 考核结果
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectKaoHeJieGuo()
        {
            List<SelectVo> listKaoHeJieGuo = (from tbKaoHeJieGuo in myModels.S_ExamineResult
                                           select new SelectVo
                                           {
                                               id = tbKaoHeJieGuo.ExamineResultID,
                                               text = tbKaoHeJieGuo.ExamineResult
                                           }).ToList();
            listKaoHeJieGuo = Tools.SetSelectJson(listKaoHeJieGuo);
            return Json(listKaoHeJieGuo, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 任职以来各年度
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectNianDu()
        {
            List<SelectVo> listNianDu = (from tbNianDu in myModels.S_DeclareYear
                                         select new SelectVo
                                              {
                                                  id = tbNianDu.DeclareYearID,
                                                  text = tbNianDu.DeclareYear
                                         }).ToList();
            listNianDu = Tools.SetSelectJson(listNianDu);
            return Json(listNianDu, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 申报方式
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSBFangShi()
        {
            List<SelectVo> listSBFangShi = (from tbSBFangShi in myModels.S_DeclareMode
                                         select new SelectVo
                                         {
                                             id = tbSBFangShi.DeclareModeID,
                                             text = tbSBFangShi.DeclareMode
                                         }).ToList();
            listSBFangShi = Tools.SetSelectJson(listSBFangShi);
            return Json(listSBFangShi, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 用户登录  职称申报
        /// <summary>
        /// 注册新增用户
        /// </summary>
        /// <param name="SUserRegister"></param>
        /// <param name="fileStudentImage"></param>
        /// <returns></returns>
        /// 
        public ActionResult InsertUser(S_UserRegister SUserRegister)
        {
            string strMsg = "fali";
            try
            {
                int oldStudentRows = (from tbUserRegister in myModels.S_UserRegister
                                      where tbUserRegister.Name == tbUserRegister.Name
                                            || tbUserRegister.RegisterCode == tbUserRegister.RegisterCode
                                      select tbUserRegister).Count();
                //提取登录密码进行加密，加密后保存入数据库
                string strRegisterCodes = AESEncryptHelper.Encrypt(SUserRegister.RegisterCode);
                SUserRegister.RegisterCode = strRegisterCodes;

                if (oldStudentRows >= 0)
                {
                    myModels.S_UserRegister.Add(SUserRegister);
                    myModels.SaveChanges();
                    strMsg = "success";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 正则邮箱验证
        /// </summary>
        /// <param name="Email">邮箱</param>
        /// <returns></returns>
        public ActionResult RegularMailbox(string Email)
        {
            var msgs = false;
            bool bola = Regex.IsMatch(Email, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
            if (bola == true)
            {
                msgs = true;
            }

            return Json(msgs, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 个人用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin(S_UserRegister suser)
        {
            string strMsg = "fail";
            string strUsername = Request.Form["Username"];//用户名
            string strRegisterCode = Request.Form["RegisterCode"];//密码
            string strvalidCode = Request.Form["validCode"];//验证码
            //获取 session中的验证码
            string strSessionValidCode = "";
            if (Session["vildeCode"] !=null)
            {
                strSessionValidCode = Session["vildeCode"].ToString();

                if (strSessionValidCode.Equals(strvalidCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    //根据 Username 查询用户
                    var dbUser = (from tbUser in myModels.S_UserRegister
                                    where tbUser.Username == strUsername.Trim()
                                    select new
                                    {
                                        tbUser.UserRegisterID,
                                        tbUser.Username,
                                        tbUser.Name,
                                        tbUser.RegisterCode,
                                        tbUser.AffirmCode
                                    }).ToList();
                    //根据确认密码加密与登录密码匹配否
                    string strPass = AESEncryptHelper.Encrypt(dbUser[0].AffirmCode);
                    
                    if (dbUser.Count > 0 )//判断是否拥有用户
                    {
                        //根据 dbRegisterCode 查询用户
                        var dbRegisterCode = (from tbUser in myModels.S_UserRegister
                                                where tbUser.RegisterCode == strPass.Trim()
                                                select new
                                                {
                                                    tbUser.UserRegisterID,
                                                    tbUser.Username,
                                                    tbUser.Name,
                                                    tbUser.RegisterCode,
                                                }).ToList();
                        
                        if (dbRegisterCode.Count > 0)
                        {
                            strMsg = "login";//登录成功
                        }
                        else
                        {
                            strMsg = "passwordErro";//密码错误
                        }
                    }
                    else
                    {
                        strMsg = "NotUser";//没有该用户
                    }
                }
                else
                {
                    strMsg = "vildeCodeErro！";//验证码错误
                }
            }           
            else
            {
                strMsg = "NotVildeCode";//图片里面没有验证码的时候，就显示NotVildeCode
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);           
        }
        /// <summary>
        /// 查询成果奖励一览表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectAchievements(BsgridPage bsgridPage)
        {
            try
            {
                List<ChengGuoVo> linqItem = (from tbItem in myModels.B_Achievement
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
        /// 查询论文作品一览表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectProduction(BsgridPage bsgridPage)
        {
            try
            {
                List<LunWenVo> linqProduct = (from tbProduct in myModels.B_Production
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
        /// 保存可申报年度列表
        /// </summary>
        /// <returns></returns>
        ///
        public ActionResult BCShenBaoNianDu(int MajorsGradeID,int MajorsSeriesID,int MajorsPostID,int DeclareYearID,int DeclareGradeID,int DeclareSeriesID,int DeclarePostID)
        {         
            string strMsg = "fali";
            try
            {
                int oldStudentRows = (from tbBDeclare in myModels.B_Declare
                                      where 
                                            tbBDeclare.MajorsGradeID == MajorsGradeID
                                            && tbBDeclare.MajorsPostID == MajorsPostID
                                            && tbBDeclare.MajorsSeriesID == MajorsSeriesID
                                            && tbBDeclare.DeclareGradeID == DeclareGradeID
                                            && tbBDeclare.DeclarePostID == DeclarePostID
                                            && tbBDeclare.DeclareSeriesID == DeclareSeriesID
                                            && tbBDeclare.DeclareYearID == DeclareYearID
                                      select tbBDeclare).Count();
                if (oldStudentRows == 0)
                {
                    B_Declare myDeclare = new B_Declare();
                    myDeclare.MajorsGradeID = MajorsGradeID;
                    myDeclare.MajorsPostID = MajorsPostID;
                    myDeclare.MajorsSeriesID = MajorsSeriesID;
                    myDeclare.DeclareGradeID = DeclareGradeID;
                    myDeclare.DeclarePostID = DeclarePostID;
                    myDeclare.DeclareSeriesID = DeclareSeriesID;
                    myDeclare.DeclareYearID = DeclareYearID;
                    myModels.B_Declare.Add(myDeclare);
                    if (myModels.SaveChanges()>0)
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
        /// 查询最后一条数据的身份证号
        /// </summary>
        /// <returns></returns>
        public ActionResult JCShenFenZhengHao()
        {
            string strCode = "";
            var listShenFenZhengHao = (from tbShenFenZhengHao in myModels.S_UserRegister
                                       orderby tbShenFenZhengHao.UserRegisterID,//根据ID，倒叙orderby
                                                tbShenFenZhengHao.IdentityCode
                                       select tbShenFenZhengHao).ToList();
            if (listShenFenZhengHao.Count > 0)
            {
                int count = listShenFenZhengHao.Count;
                S_UserRegister modelUR = listShenFenZhengHao[count -1];//获取表的最后一条数据
                strCode = modelUR.IdentityCode.ToString();//获取最后一个身份证号
            }
            return Json(strCode, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询最后一条数据的邮箱码
        /// </summary>
        /// <returns></returns>
        public ActionResult JCYouXiang()
        {
            string strMailbox = "";
            var listMailbox = (from tbMailbox in myModels.S_UserRegister
                                       orderby tbMailbox.UserRegisterID,//根据ID，倒叙orderby
                                              tbMailbox.Mailbox
                                       select tbMailbox).ToList();
            if (listMailbox.Count > 0)
            {
                int count = listMailbox.Count;
                S_UserRegister modelUR = listMailbox[count - 1];//获取表的最后一条数据
                strMailbox = modelUR.Mailbox.ToString();//获取最后一个邮箱码
            }
            return Json(strMailbox, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询S_UserRegister的最后一条数据的姓名
        /// </summary>
        /// <returns></returns>
        public ActionResult JBXingMing()
        {
            string strIdName = "";
            var listIdName = (from tbName in myModels.S_UserRegister
                              orderby tbName.UserRegisterID,
                                       tbName.Name
                              select tbName).ToList();
            if (listIdName.Count > 0)
            {
                int count = listIdName.Count;
                S_UserRegister modelUR = listIdName[count - 1];//获取表的最后一条数据
                strIdName = modelUR.Name.ToString();//获取最后一个姓名
            }
            return Json(strIdName, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 基础信息新增
        /// </summary>
        /// <param name="BBasis"></param>
        /// <param name="fileStudentImage"></param>
        /// <returns></returns>
        ///       
        public ActionResult InsertBasis(B_Basis BBasis)
        {
            
            string strMsg = "fali";
           
                if (BBasis.DeclareBelongID != null)
                {
                    var kdInsertBasis = (from tbInsertBasis in myModels.B_Basis
                                         where tbInsertBasis.BasisID == BBasis.BasisID
                                         select tbInsertBasis).Single();
                    kdInsertBasis.DeclareBelongID = BBasis.DeclareBelongID;
                    kdInsertBasis.StatutoryCall = BBasis.StatutoryCall;
                    kdInsertBasis.SubjectionID = BBasis.SubjectionID;
                    kdInsertBasis.ContactPhone = BBasis.ContactPhone;
                    kdInsertBasis.RecordUnit = BBasis.RecordUnit;
                    myModels.Entry(kdInsertBasis).State = System.Data.Entity.EntityState.Modified;//修改
                    myModels.SaveChanges();
                    strMsg = "success";
                }
                else
                {
                    myModels.B_Basis.Add(BBasis);

                    if (myModels.SaveChanges() > 0)
                    {
                        var listShenFenZhengHao = (from tbShenFenZhengHao in myModels.B_Basis
                                                   orderby tbShenFenZhengHao.IdentityCode//倒叙orderby
                                                   select tbShenFenZhengHao).ToList();
                        if (listShenFenZhengHao.Count > 0)
                        {
                            int count = listShenFenZhengHao.Count;
                            B_Basis modelUR = listShenFenZhengHao[count - 1];//获取表的最后一条数据
                            strMsg = modelUR.BasisID.ToString();//获取最后一个身份证号
                        }
                       
                    }
                }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 基本信息新增
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertBasic(int DeclareGradeID,int DeclareSeriesID, int DeclareQualificationID, DateTime FormTime, 
               DateTime TakeOfficeTime,string Name, int SexID, DateTime Birthday, string PlaceUnit, int EducationID, 
               int JudgeEducationID, int MajorsPostID, DateTime GainedQualificationTime, DateTime EngageTime, string EngageYear, 
               string TakeOffice, string Major, string MajorYear, DateTime JoinWorkTime, string ForeignLanguage, 
               int ForeignLanguageRankID, string ForeignLanguageResult, int ExamCaseID, int ExamineResultID, int ExamineResultID1,
               int ExamineResultID2, int ExamineResultID3, int ExamineResultID4, int ExamineResultID5, int ExamineResultID6, 
               int DeclareYearID, int DeclareYearID1, int DeclareYearID2, int DeclareYearID3, int DeclareYearID4, 
               int DeclareYearID5,int DeclareModeID, string QuantityScore, string ExamResult,
               DateTime GraduateTime, DateTime GraduateTime1, string GraduateSchool, string GraduateSchool1, int MajorID, int MajorID1)
        {
            string strMsg = "fali";
            try
            {
                int InsertBasicXZ = (from tbBasic in myModels.B_Basic
                                    where
                                        tbBasic.DeclareGradeID == DeclareGradeID
                                        && tbBasic.DeclareSeriesID == DeclareSeriesID
                                        && tbBasic.DeclareQualificationID == DeclareQualificationID
                                        && tbBasic.FormTime == FormTime
                                        && tbBasic.Name == Name
                                        && tbBasic.SexID == SexID
                                        && tbBasic.Birthday == Birthday
                                        && tbBasic.PlaceUnit == PlaceUnit
                                        && tbBasic.EducationID == EducationID
                                        && tbBasic.GraduateTime == GraduateTime
                                        && tbBasic.GraduateSchool == GraduateSchool
                                        && tbBasic.MajorID == MajorID
                                        && tbBasic.JudgeEducationID == JudgeEducationID
                                        && tbBasic.GraduateTime1 == GraduateTime1
                                        && tbBasic.GraduateSchool1 == GraduateSchool1
                                        && tbBasic.MajorID1 == MajorID1
                                        && tbBasic.MajorsPostID == MajorsPostID
                                        && tbBasic.GainedQualificationTime == GainedQualificationTime
                                        && tbBasic.EngageTime == EngageTime
                                        && tbBasic.EngageYear == EngageYear
                                        && tbBasic.TakeOffice == TakeOffice
                                        && tbBasic.TakeOfficeTime == TakeOfficeTime
                                        && tbBasic.Major == Major
                                        && tbBasic.MajorYear == MajorYear
                                        && tbBasic.JoinWorkTime == JoinWorkTime
                                        && tbBasic.ForeignLanguage == ForeignLanguage
                                        && tbBasic.ForeignLanguageRankID == ForeignLanguageRankID
                                        && tbBasic.ForeignLanguageResult == ForeignLanguageResult
                                        && tbBasic.ExamCaseID == ExamCaseID
                                        && tbBasic.ExamineResultID == ExamineResultID
                                        && tbBasic.DeclareYearID == DeclareYearID
                                        && tbBasic.DeclareYearID1 == DeclareYearID1
                                        && tbBasic.DeclareYearID2 == DeclareYearID2
                                        && tbBasic.DeclareYearID3 == DeclareYearID3
                                        && tbBasic.DeclareYearID4 == DeclareYearID4
                                        && tbBasic.DeclareYearID5 == DeclareYearID5
                                        && tbBasic.ExamineResultID1 == ExamineResultID1
                                        && tbBasic.ExamineResultID2 == ExamineResultID2
                                        && tbBasic.ExamineResultID3 == ExamineResultID3
                                        && tbBasic.ExamineResultID4 == ExamineResultID4
                                        && tbBasic.ExamineResultID5 == ExamineResultID5
                                        && tbBasic.ExamineResultID6 == ExamineResultID6
                                        && tbBasic.DeclareModeID == DeclareModeID
                                        && tbBasic.QuantityScore == QuantityScore
                                        && tbBasic.ExamResult == ExamResult
                                     select tbBasic).Count();
                if (InsertBasicXZ == 0)
                {
                    B_Basic myBasic = new B_Basic();
                    myBasic.DeclareGradeID = DeclareGradeID;
                    myBasic.DeclareSeriesID = DeclareSeriesID;
                    myBasic.DeclareQualificationID = DeclareQualificationID;
                    myBasic.FormTime = FormTime;
                    myBasic.Name = Name;
                    myBasic.SexID = SexID;
                    myBasic.Birthday = Birthday;
                    myBasic.PlaceUnit = PlaceUnit;
                    myBasic.EducationID = EducationID;
                    myBasic.GraduateTime = GraduateTime;
                    myBasic.GraduateSchool = GraduateSchool;
                    myBasic.MajorID = MajorID;
                    myBasic.JudgeEducationID = JudgeEducationID;
                    myBasic.GraduateTime1 = GraduateTime1;
                    myBasic.GraduateSchool1 = GraduateSchool1;
                    myBasic.MajorID1 = MajorID1;
                    myBasic.MajorsPostID = MajorsPostID;
                    myBasic.GainedQualificationTime = GainedQualificationTime;
                    myBasic.EngageTime = EngageTime;
                    myBasic.EngageYear = EngageYear;
                    myBasic.TakeOffice = TakeOffice;
                    myBasic.TakeOfficeTime = TakeOfficeTime;
                    myBasic.Major = Major;
                    myBasic.MajorYear = MajorYear;
                    myBasic.JoinWorkTime = JoinWorkTime;
                    myBasic.ForeignLanguage = ForeignLanguage;
                    myBasic.ForeignLanguageRankID = ForeignLanguageRankID;
                    myBasic.ForeignLanguageResult = ForeignLanguageResult;
                    myBasic.ExamCaseID = ExamCaseID;
                    myBasic.ExamineResultID = ExamineResultID;
                    myBasic.DeclareYearID = DeclareYearID;
                    myBasic.DeclareYearID1 = DeclareYearID1;
                    myBasic.DeclareYearID2 = DeclareYearID2;
                    myBasic.DeclareYearID3 = DeclareYearID3;
                    myBasic.DeclareYearID4 = DeclareYearID4;
                    myBasic.DeclareYearID5 = DeclareYearID5;
                    myBasic.ExamineResultID1 = ExamineResultID1;
                    myBasic.ExamineResultID2 = ExamineResultID2;
                    myBasic.ExamineResultID3 = ExamineResultID3;
                    myBasic.ExamineResultID4 = ExamineResultID4;
                    myBasic.ExamineResultID5 = ExamineResultID5;
                    myBasic.ExamineResultID6 = ExamineResultID6;
                    myBasic.DeclareModeID = DeclareModeID;
                    myBasic.QuantityScore = QuantityScore;
                    myBasic.ExamResult = ExamResult;
                    myBasic.DeclareGradeID = DeclareGradeID;
                    myModels.B_Basic.Add(myBasic);
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
        /// 保存工作经历
        /// </summary>
        /// <param name="Experience"></param>
        /// <returns></returns>
        public ActionResult InsertExperience(string Experience)
        {
            string strMsg = "fali";
            try
            {
                int oldExperience = (from tbExperience in myModels.B_Experience
                                      where
                                            tbExperience.Experience == Experience
                                      select tbExperience).Count();
                if (oldExperience == 0)
                {
                    B_Experience myExperience = new B_Experience();
                    myExperience.Experience = Experience;
                    myModels.B_Experience.Add(myExperience);
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
        /// 保存工作成绩
        /// </summary>
        /// <param name="Experience"></param>
        /// <returns></returns>
        public ActionResult InsertWorkResult(string WorkResult)
        {
            string strMsg = "fali";
            try
            {
                int oldWorkResult = (from tbWorkResult in myModels.B_WorkResult
                                     where
                                           tbWorkResult.WorkResult == WorkResult
                                     select tbWorkResult).Count();
                if (oldWorkResult == 0)
                {
                    B_WorkResult myWorkResult = new B_WorkResult();
                    myWorkResult.WorkResult = WorkResult;
                    myModels.B_WorkResult.Add(myWorkResult);
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
        /// 成果奖励新增
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Timers"></param>
        /// <param name="MessageDetailID"></param>
        /// <param name="SituateNextrs"></param>
        /// <param name="RatifySection"></param>
        /// <returns></returns>
        public ActionResult InsertAchievement(string Item,DateTime Timers, int AchievementGradeID, string SituateNextrs, string RatifySection)
        {
            string strMsg = "fali";
            try
            {
                int oldAchievement = (from tbAchievement in myModels.B_Achievement
                                      where
                                            tbAchievement.Item == Item
                                            && tbAchievement.Timers == Timers
                                            && tbAchievement.AchievementGradeID == AchievementGradeID
                                            && tbAchievement.SituateNextrs == SituateNextrs
                                            && tbAchievement.RatifySection == RatifySection
                                      select tbAchievement).Count();
                if (oldAchievement == 0)
                {
                    B_Achievement myAchievement = new B_Achievement();
                    myAchievement.Item = Item;
                    myAchievement.Timers = Timers;
                    myAchievement.AchievementGradeID = AchievementGradeID;
                    myAchievement.SituateNextrs = SituateNextrs;
                    myAchievement.RatifySection = RatifySection;
                    myModels.B_Achievement.Add(myAchievement);
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
        /// 论文作品新增
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Timery"></param>
        /// <param name="SituateNextry"></param>
        /// <param name="PeriodicalPublishing"></param>
        /// <returns></returns>
        public ActionResult IntsertProduction(string Title, DateTime Timery, string SituateNextry, string PeriodicalPublishing)
        {
            string strMsg = "fali";
            try
            {
                int oldProduction = (from tbProduction in myModels.B_Production
                                      where
                                            tbProduction.Title == Title
                                            && tbProduction.Timery == Timery
                                            && tbProduction.SituateNextry == SituateNextry
                                            && tbProduction.PeriodicalPublishing == PeriodicalPublishing
                                      select tbProduction).Count();
                if (oldProduction == 0)
                {
                    B_Production myProduction = new B_Production();
                    myProduction.Title = Title;
                    myProduction.Timery = Timery;
                    myProduction.SituateNextry = SituateNextry;
                    myProduction.PeriodicalPublishing = PeriodicalPublishing;
                    myModels.B_Production.Add(myProduction);
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
        /// 修改数据绑定
        /// </summary>
        /// <param name="AchievementID"></param>
        /// <returns></returns>
        public ActionResult SelectAchievement(int AchievementID)
        {
            var listAchievement = (from tbAchievement in myModels.B_Achievement
                                            where tbAchievement.AchievementID == AchievementID
                                            select new 
                                            {
                                                AchievementID = tbAchievement.AchievementID,
                                                AchievementGradeID = tbAchievement.AchievementGradeID,
                                                Item = tbAchievement.Item,
                                                Timeser = tbAchievement.Timers.ToString(),
                                                SituateNextrs = tbAchievement.SituateNextrs,
                                                RatifySection = tbAchievement.RatifySection,
                                            }).Single();
            return Json(listAchievement, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 成果奖励修改
        /// </summary>
        /// <param name="BAchievement"></param>
        /// <returns></returns>
        public ActionResult UpdateAchievement(B_Achievement BAchievement)
        {
            string strMsg = "fali";
                var pdAchievement = (from tbAchievement in myModels.B_Achievement
                                     where
                                            tbAchievement.AchievementID == BAchievement.AchievementID
                                     select tbAchievement).Single();
                if (pdAchievement != null)
                {
                pdAchievement.Item = BAchievement.Item;
                pdAchievement.Timers = BAchievement.Timers;
                pdAchievement.AchievementGradeID = BAchievement.AchievementGradeID;
                pdAchievement.SituateNextrs = BAchievement.SituateNextrs;
                pdAchievement.RatifySection = BAchievement.RatifySection;
                myModels.Entry(pdAchievement).State = System.Data.Entity.EntityState.Modified;
                    if (myModels.SaveChanges() > 0)
                    {
                        strMsg = "success";
                    }
                }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改数据绑定
        /// </summary>
        /// <param name="ProductionID"></param>
        /// <returns></returns>
        public ActionResult SelectProductions (int ProductionID)
        {
            var listProduction = (from tbProduction in myModels.B_Production
                                   where tbProduction.ProductionID == ProductionID
                                   select new
                                   {
                                       ProductionID = tbProduction.ProductionID,
                                       Title = tbProduction.Title,
                                       Timeser = tbProduction.Timery.ToString(),
                                       SituateNextry = tbProduction.SituateNextry,
                                       PeriodicalPublishing = tbProduction.PeriodicalPublishing,
                                   }).Single();
            return Json(listProduction, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 论文作品修改
        /// </summary>
        /// <param name="BProduction"></param>
        /// <returns></returns>
        public ActionResult UpdateProduction(B_Production BProduction)
        {
            string strMsg = "fali";
            var pdProduction = (from tbProduction in myModels.B_Production
                                 where
                                        tbProduction.ProductionID == BProduction.ProductionID
                                 select tbProduction).Single();
            if (pdProduction != null)
            {
                pdProduction.Title = BProduction.Title;
                pdProduction.Timery = BProduction.Timery;
                pdProduction.SituateNextry = BProduction.SituateNextry;
                pdProduction.PeriodicalPublishing = BProduction.PeriodicalPublishing;
                myModels.Entry(pdProduction).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 成果奖励删除
        /// </summary>
        /// <param name="AchievementID"></param>
        /// <returns></returns>
        public ActionResult DeleteAchievement(int AchievementID)
        {
            string strMsg = "fail";
            try
            {
                B_Achievement dbAchievement = (from tbAchievement in myModels.B_Achievement
                                               where tbAchievement.AchievementID == AchievementID
                                               select tbAchievement).Single();
                myModels.B_Achievement.Remove(dbAchievement);
                myModels.SaveChanges();
                strMsg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 论文作品删除
        /// </summary>
        /// <param name="ProductionID"></param>
        /// <returns></returns>
        public ActionResult DeleteProduction(int ProductionID)
        {
            string strMsg = "fail";
            try
            {
                B_Production dbProduction = (from tbProduction in myModels.B_Production
                                              where tbProduction.ProductionID == ProductionID
                                             select tbProduction).Single();
                myModels.B_Production.Remove(dbProduction);
                myModels.SaveChanges();
                strMsg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 水晶报表
        /// <summary>
        /// 一览表打印
        /// </summary>
        /// <returns></returns>
        public ActionResult Basicxtdy (int BasicID)
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
                                           //where tbBasicxtdy.BasicID == BasicID
                                           orderby tbBasicxtdy.BasicID descending
                                           select new
                                           {
                                               BasicID = tbBasicxtdy.BasicID,
                                               DeclareGrade = tbDeclareGrade.DeclareGrade,
                                               DeclareSeries = tbDeclareSeries.DeclareSeries,
                                               DeclareQualification = tbDeclareQualification.DeclareQualification,
                                               FormTime = tbBasicxtdy.FormTime,
                                               Name = tbBasicxtdy.Name,
                                               Sex = tbSex.Sex,
                                               Birthdayer = tbBasicxtdy.Birthday,
                                               PlaceUnit = tbBasicxtdy.PlaceUnit,
                                               Education = tbEducation.Education,
                                               GraduateTime = tbBasicxtdy.GraduateTime,
                                               GraduateSchool = tbBasicxtdy.GraduateSchool,
                                               Major = tbMajor.Major,
                                               Education2 = tbExpr1.Education,
                                               GraduateTime1 = tbBasicxtdy.GraduateTime1,
                                               GraduateSchool1 = tbBasicxtdy.GraduateSchool1,
                                               Major2 = tbExpr2.Major,
                                               MajorsPost = tbMajorsPost.MajorsPost,
                                               GainedQualificationTime = tbBasicxtdy.GainedQualificationTime,
                                               EngageTime = tbBasicxtdy.EngageTime,
                                               EngageYear = tbBasicxtdy.EngageYear,
                                               Majors = tbBasicxtdy.Major,
                                               MajorYear = tbBasicxtdy.MajorYear,
                                               TakeOffice = tbBasicxtdy.TakeOffice,
                                               TakeOfficeTime = tbBasicxtdy.TakeOfficeTime,
                                               JoinWorkTime = tbBasicxtdy.JoinWorkTime,
                                               ForeignLanguage = tbBasicxtdy.ForeignLanguage,
                                               ForeignLanguageRank = tbForeignLanguageRank.ForeignLanguageRank,
                                               ForeignLanguageResult = tbBasicxtdy.ForeignLanguageResult,
                                               DeclareMode = tbDeclareMode.DeclareMode,
                                               QuantityScore = tbBasicxtdy.QuantityScore,
                                               ExamCase = tbExamCase.ExamCase,
                                               ExamineResult = tbExamResult.ExamineResult,
                                               DeclareYear = tbDeclareYear.DeclareYear,
                                               ExamineResult2 = tbExamineResult.ExamineResult,
                                               DeclareYear2 = tbExpr4.DeclareYear,
                                               ExamineResult3 = tbExpr5.ExamineResult,
                                               DeclareYear3 = tbExpr6.DeclareYear,
                                               ExamineResult4 = tbExpr7.ExamineResult,
                                               ExamineResult5 = tbExpr8.ExamineResult,
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
            var listAchievementxt = (from tbAchievementxt in myModels.B_Achievement
                                     orderby tbAchievementxt.AchievementID descending
                                     join tbAchievementGrade in myModels.S_AchievementGrade on tbAchievementxt.AchievementGradeID equals tbAchievementGrade.AchievementGradeID
                                     select new
                                     {
                                         AchievementID = tbAchievementxt.AchievementID,
                                         Timers = tbAchievementxt.Timers,
                                         Item = tbAchievementxt.Item,
                                         AchievementGrade = tbAchievementGrade.AchievementGrade,
                                         SituateNextrs = tbAchievementxt.SituateNextrs,
                                         RatifySection = tbAchievementxt.RatifySection,
                                         Accessoryrs = tbAchievementxt.Accessoryrs,
                                     }).ToList();//成果奖励
            var listProductionxt = (from tbProductionxt in myModels.B_Production
                                    orderby tbProductionxt.ProductionID descending
                                    select new
                                    {
                                        ProductionID = tbProductionxt.ProductionID,
                                        Timery = tbProductionxt.Timery,
                                        Title = tbProductionxt.Title,
                                        SituateNextry = tbProductionxt.SituateNextry,
                                        PeriodicalPublishing = tbProductionxt.PeriodicalPublishing,
                                        Accessoryry = tbProductionxt.Accessoryry,
                                    }).ToList();//论文作品
            ////1、实例数据集                  
            Areas.CRPPrint.DataYiLanBiao dtBasicxtdy = new Areas.CRPPrint.DataYiLanBiao();
            ////实例table
            DataTable dt = new DataTable();
            //给table添加列 
            dt.Columns.Add("DeclareGrade");
            dt.Columns.Add("DeclareSeries");
            dt.Columns.Add("DeclareQualification");
            dt.Columns.Add("FormTime");
            dt.Columns.Add("Name");
            dt.Columns.Add("Sex");
            dt.Columns.Add("Birthday");
            dt.Columns.Add("PlaceUnit");
            dt.Columns.Add("Education");
            dt.Columns.Add("GraduateTime");
            dt.Columns.Add("GraduateSchool");
            dt.Columns.Add("Major");
            dt.Columns.Add("Education2");//Expr1
            dt.Columns.Add("GraduateTime1");
            dt.Columns.Add("GraduateSchool1");
            dt.Columns.Add("Major2");//Expr2
            dt.Columns.Add("MajorsPost");
            dt.Columns.Add("GainedQualificationTime");
            dt.Columns.Add("EngageTime");
            dt.Columns.Add("EngageYear");
            dt.Columns.Add("Majors");//Expr3
            dt.Columns.Add("MajorYear");
            dt.Columns.Add("TakeOffice");
            dt.Columns.Add("TakeOfficeTime");
            dt.Columns.Add("JoinWorkTime");
            dt.Columns.Add("ForeignLanguage");
            dt.Columns.Add("ForeignLanguageRank");
            dt.Columns.Add("ForeignLanguageResult");
            dt.Columns.Add("DeclareMode");
            dt.Columns.Add("QuantityScore");
            dt.Columns.Add("ExamCase");
            dt.Columns.Add("ExamResult");
            dt.Columns.Add("DeclareYear");
            dt.Columns.Add("ExamineResult");
            dt.Columns.Add("DeclareYear2");  //Expr4
            dt.Columns.Add("ExamineResult3");//Expr5
            dt.Columns.Add("DeclareYear3");  //Expr6
            dt.Columns.Add("ExamineResult4");//Expr7
            dt.Columns.Add("ExamineResult5");//Expr8
            dt.Columns.Add("RecordUnit");
            dt.Columns.Add("Experience");
            dt.Columns.Add("WorkResult");
            dt.Columns.Add("Timers");
            dt.Columns.Add("Item");
            dt.Columns.Add("AchievementGrade");
            dt.Columns.Add("SituateNextrs");
            dt.Columns.Add("RatifySection");
            dt.Columns.Add("Accessoryrs");
            dt.Columns.Add("Timery");
            dt.Columns.Add("Title");
            dt.Columns.Add("SituateNextry");
            dt.Columns.Add("PeriodicalPublishing");
            dt.Columns.Add("Accessoryry");
            //便利查询的表格的数据
                DataRow dr = dt.NewRow();
                dr["DeclareGrade"] = listBasicxtdy[0].DeclareGrade;//申报等级
                dr["DeclareSeries"] = listBasicxtdy[0].DeclareSeries;//申报系列
                dr["DeclareQualification"] = listBasicxtdy[0].DeclareQualification;//申报资格
                dr["FormTime"] = listBasicxtdy[0].FormTime;//填表时间
                dr["Name"] = listBasicxtdy[0].Name;//姓名
                dr["Sex"] = listBasicxtdy[0].Sex;//性别
                dr["Birthday"] = listBasicxtdy[0].Birthdayer;//出生年月
                dr["PlaceUnit"] = listBasicxtdy[0].PlaceUnit;//现所在单位
                dr["Education"] = listBasicxtdy[0].Education;//第一学历
                dr["GraduateTime"] = listBasicxtdy[0].GraduateTime;//毕业时间
                dr["GraduateSchool"] = listBasicxtdy[0].GraduateSchool;//毕业院校
                dr["Major"] = listBasicxtdy[0].Major;//专业
                dr["Education2"] = listBasicxtdy[0].Education2;//依据评审学历
                dr["GraduateTime1"] = listBasicxtdy[0].GraduateTime1;//毕业时间
                dr["GraduateSchool1"] = listBasicxtdy[0].GraduateSchool1;//毕业院校
                dr["Major2"] = listBasicxtdy[0].Major2;//专业
                dr["MajorsPost"] = listBasicxtdy[0].MajorsPost;//专业技术职称
                dr["GainedQualificationTime"] = listBasicxtdy[0].GainedQualificationTime;//获得资格时间
                dr["EngageTime"] = listBasicxtdy[0].EngageTime;//聘任时间
                dr["EngageYear"] = listBasicxtdy[0].EngageYear;//聘任年限
                dr["Majors"] = listBasicxtdy[0].Majors;//现所从事专业
                dr["MajorYear"] = listBasicxtdy[0].MajorYear;//从事专业年限
                dr["TakeOffice"] = listBasicxtdy[0].TakeOffice;//现任行政职务
                dr["TakeOfficeTime"] = listBasicxtdy[0].TakeOfficeTime;//任职时间
                dr["JoinWorkTime"] = listBasicxtdy[0].JoinWorkTime;//参加工作时间
                dr["ForeignLanguage"] = listBasicxtdy[0].ForeignLanguage;//外语语种
                dr["ForeignLanguageRank"] = listBasicxtdy[0].ForeignLanguageRank;//外语级别
                dr["ForeignLanguageResult"] = listBasicxtdy[0].ForeignLanguageResult;//外语成绩
                dr["DeclareMode"] = listBasicxtdy[0].DeclareMode;//申报方式
                dr["QuantityScore"] = listBasicxtdy[0].QuantityScore;//量化赋分
                dr["ExamCase"] = listBasicxtdy[0].ExamCase;//全国专业技术人员计算机应用能力考试情况
                dr["ExamResult"] = listBasicxtdy[0].ExamineResult;//考试成绩
                dr["DeclareYear"] = listBasicxtdy[0].DeclareYear;//年度
                dr["ExamineResult"] = listBasicxtdy[0].ExamineResult;//结果
                dr["DeclareYear2"] = listBasicxtdy[0].DeclareYear2;//年度
                dr["ExamineResult3"] = listBasicxtdy[0].ExamineResult3;//结果
                dr["DeclareYear3"] = listBasicxtdy[0].DeclareYear3;//年度
                dr["ExamineResult4"] = listBasicxtdy[0].ExamineResult4;//结果
                dr["ExamineResult5"] = listBasicxtdy[0].ExamineResult5;//任期届满考核结果

                dr["RecordUnit"] = listBasisxtdy[0].RecordUnit;//托管单位

                dr["Timers"] = listAchievementxt[0].Timers;//时间
                dr["Item"] = listAchievementxt[0].Item;//项目名称
                dr["AchievementGrade"] = listAchievementxt[0].AchievementGrade;//成果等级
                dr["SituateNextrs"] = listAchievementxt[0].SituateNextrs;//位次
                dr["RatifySection"] = listAchievementxt[0].RatifySection;//批准机关
                dr["Accessoryrs"] = listAchievementxt[0].Accessoryrs;//附件
                dr["Experience"] = listExperiencext[0].Experience;//工作经历
                dr["WorkResult"] = listWorkResultxt[0].WorkResult;//工作成绩
                dr["Timery"] = listProductionxt[0].Timery;//时间
                dr["Title"] = listProductionxt[0].Title;//题目
                dr["SituateNextry"] = listProductionxt[0].SituateNextry;//位次
                dr["PeriodicalPublishing"] = listProductionxt[0].PeriodicalPublishing;//刊物或出版社
                dr["Accessoryry"] = listProductionxt[0].Accessoryry;//附件
            //给table添加行数据
            dt.Rows.Add(dr);
            
            //2、合并：把查询出来的表格和数据集中的表合并
            dtBasicxtdy.Tables["B_Basicxt"].Merge(dt);
            dtBasicxtdy.Tables["B_Basisxt"].Merge(dt);
            dtBasicxtdy.Tables["B_Experiencext"].Merge(dt);
            dtBasicxtdy.Tables["B_WorkResultxt"].Merge(dt);
            dtBasicxtdy.Tables["B_Achievementxt"].Merge(dt);
            dtBasicxtdy.Tables["B_Productionxt"].Merge(dt);
            //实例化报表
            ReportDocument rd = new ReportDocument();
            //获取报表物理文件地址              
            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CRPPrint\\rpt_YiLanBiaoDaYin.rpt";
            rd.Load(strRptPath);//把报表文件加载到ReportDocument
            rd.SetDataSource(dtBasicxtdy); //设置报表数据源
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat); //把ReportDocument转化为文件流
            return File(stream, "application/pdf"); //返回 文档格式pdf  
        }
        #endregion

        #region 单位
        /// <summary>
        /// 单位注册
        /// </summary>
        /// <param name="UnitIDCide"></param>
        /// <param name="RegisterCode"></param>
        /// <param name="AffirmCode"></param>
        /// <param name="StatutoryCall"></param>
        /// <param name="TissueOrganization"></param>
        /// <param name="TissueOrganizationCode"></param>
        /// <param name="UseDynamicOrder"></param>
        /// <param name="DynamicOrderCode"></param>
        /// <param name="Administrative"></param>
        /// <param name="CorrespondenceSite"></param>
        /// <param name="Contact"></param>
        /// <param name="ContactPhone"></param>
        /// <param name="MailCode"></param>
        /// <param name="UnitNatureID"></param>
        /// <param name="SubjectionID"></param>
        /// <param name="BelongIndustryID"></param>
        /// <returns></returns>
        public ActionResult InsertUnit(string UnitIDCide, string RegisterCode, string AffirmCode, string StatutoryCall, string TissueOrganization,
                            string TissueOrganizationCode, string UseDynamicOrder, string DynamicOrderCode, string Administrative, string CorrespondenceSite,
                            string Contact, string ContactPhone, string MailCode, int UnitNatureID, int SubjectionID, int BelongIndustryID)
        {
            string strMsg = "fali";
            try
            {
                int oldUnitRegister = (from tbUnitRegister in myModels.S_UnitRegister
                                       where
                                            tbUnitRegister.UnitIDCide == UnitIDCide
                                             && tbUnitRegister.RegisterCode == RegisterCode
                                             && tbUnitRegister.AffirmCode == AffirmCode
                                             && tbUnitRegister.StatutoryCall == StatutoryCall
                                             && tbUnitRegister.TissueOrganization == TissueOrganization
                                             && tbUnitRegister.TissueOrganizationCode == TissueOrganizationCode
                                             && tbUnitRegister.UseDynamicOrder == UseDynamicOrder
                                             && tbUnitRegister.DynamicOrderCode == DynamicOrderCode
                                             && tbUnitRegister.Administrative == Administrative
                                             && tbUnitRegister.CorrespondenceSite == CorrespondenceSite
                                             && tbUnitRegister.Contact == Contact
                                             && tbUnitRegister.ContactPhone == ContactPhone
                                             && tbUnitRegister.MailCode == MailCode
                                             && tbUnitRegister.UnitNatureID == UnitNatureID
                                             && tbUnitRegister.SubjectionID == SubjectionID
                                             && tbUnitRegister.BelongIndustryID == BelongIndustryID
                                       select tbUnitRegister).Count();
                //提取登录密码进行加密，加密后保存入数据库
                string strRegisterCodes = AESEncryptHelper.Encrypt(RegisterCode);
                RegisterCode = strRegisterCodes;
                if (oldUnitRegister == 0)
                {
                    S_UnitRegister myUnitRegister = new S_UnitRegister();
                    myUnitRegister.UnitIDCide = UnitIDCide;
                    myUnitRegister.RegisterCode = RegisterCode;
                    myUnitRegister.AffirmCode = AffirmCode;
                    myUnitRegister.StatutoryCall = StatutoryCall;
                    myUnitRegister.TissueOrganization = TissueOrganization;
                    myUnitRegister.TissueOrganizationCode = TissueOrganizationCode;
                    myUnitRegister.UseDynamicOrder = UseDynamicOrder;
                    myUnitRegister.DynamicOrderCode = DynamicOrderCode;
                    myUnitRegister.Administrative = Administrative;
                    myUnitRegister.CorrespondenceSite = CorrespondenceSite;
                    myUnitRegister.Contact = Contact;
                    myUnitRegister.ContactPhone = ContactPhone;
                    myUnitRegister.MailCode = MailCode;
                    myUnitRegister.UnitNatureID = UnitNatureID;
                    myUnitRegister.SubjectionID = SubjectionID;
                    myUnitRegister.BelongIndustryID = BelongIndustryID;
                    myModels.S_UnitRegister.Add(myUnitRegister);
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
        /// 单位登录
        /// </summary>
        /// <param name="sunit"></param>
        /// <returns></returns>
        public ActionResult UnitLogin(S_UnitRegister sunit)
        {
            string strMsg = "fail";
            string strUnitIDCide = sunit.UnitIDCide;//单位ID
            string strRegisterCode = sunit.RegisterCode;//登录密码
            try
            {
                //根据 UnitIDCide 查询用户
                var dbUnit = (from tbUnit in myModels.S_UnitRegister
                              where tbUnit.UnitIDCide == strUnitIDCide.Trim()
                              select new
                              {
                                  tbUnit.UnitRegisterID,
                                  tbUnit.UnitIDCide,
                                  tbUnit.RegisterCode,
                                  tbUnit.DynamicOrderCode,
                                  tbUnit.AffirmCode
                              }).ToList();
                //根据确认密码加密与登录密码匹配否
                string strPass = AESEncryptHelper.Encrypt(dbUnit[0].AffirmCode);
                if (dbUnit.Count > 0)//判断是否拥有用户
                {
                    //根据 dbUnit 查询用户
                    var dbRegisterCode = (from tbUnit in myModels.S_UnitRegister
                                          where tbUnit.RegisterCode == strPass.Trim()
                                          select new
                                          {
                                              tbUnit.UnitRegisterID,
                                              tbUnit.UnitIDCide,
                                              tbUnit.RegisterCode,
                                              tbUnit.DynamicOrderCode,
                                          }).ToList();
                    if (dbRegisterCode.Count > 0)
                    {
                        strMsg = "login";//登录成功
                    }
                    else
                    {
                        strMsg = "passwordErro";//密码错误
                    }
                }
                else
                {
                    strMsg = "NotUser";//没有该用户
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
               
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 单位解锁
        /// </summary>
        /// <param name="sunit"></param>
        /// <returns></returns>
        public ActionResult UnitDeblock(S_UnitRegister sunit)
        {
            string strMsg = "fail";
            string strDynamicOrderCode = sunit.DynamicOrderCode;//动态口令
            try
            {
                var dbunit = (from tbunit in myModels.S_UnitRegister
                             where tbunit.DynamicOrderCode == strDynamicOrderCode.Trim()
                             select new
                             {
                                 tbunit.UnitRegisterID,
                                 tbunit.DynamicOrderCode,
                             }).ToList();
                if (dbunit.Count > 0)//判断动态口令是否存在
                {
                    strMsg = "login";//解锁成功
                }
                else
                {
                    strMsg = "NotDynamicOrderCode";//没有该动态口令
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 倒叙获取动态令牌号
        /// </summary>
        /// <returns></returns>
        public ActionResult DWDongTaiLingPai()
        {
            string strCode = "";
            var listDongTaiLingPai = (from tbDongTaiLingPai in myModels.S_UnitRegister
                                       orderby tbDongTaiLingPai.UnitRegisterID,//根据ID，倒叙orderby
                                                tbDongTaiLingPai.DynamicOrderCode
                                       select tbDongTaiLingPai).ToList();
            if (listDongTaiLingPai.Count > 0)
            {
                int count = listDongTaiLingPai.Count;
                S_UnitRegister modelUR = listDongTaiLingPai[count - 1];//获取表的最后一条数据
                strCode = modelUR.DynamicOrderCode.ToString();//获取最后一个动态令牌号
            }
            return Json(strCode, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 发送短信验证码
        public static string PostUrl = ConfigurationManager.AppSettings["WebReference.Service.PostUrl"];
        public void Page_Load(string phone)
        {
            string account = "C09783861";//用户名是登录用户中心->验证码、通知短信->帐户及签名设置->APIID
            string password = "995c642c8aa6f1e0bf5d88da80778e7f"; //密码是请登录用户中心->验证码、通知短信->帐户及签名设置->APIKEY
            string mobile = phone;
            Random rad = new Random();
            int mobile_code = rad.Next(1000, 10000);
            string content = "您的验证码是：" + mobile_code + " 。请不要把验证码泄露给其他人。";
            Session["mobile_code"] = mobile_code;
            
            Session["mobile_code"] = mobile_code;
            Session["mobile"] = mobile;

            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, account, password, mobile, content));

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(PostUrl);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postData.Length;

            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                //Response.Write(reader.ReadToEnd());

                string res = reader.ReadToEnd();
                int len1 = res.IndexOf("</code>");
                int len2 = res.IndexOf("<code>");
                string code = res.Substring((len2 + 6), (len1 - len2 - 6));
                //Response.Write(code);

                int len3 = res.IndexOf("</msg>");
                int len4 = res.IndexOf("<msg>");
                string msg = res.Substring((len4 + 5), (len3 - len4 - 5));
                Response.Write(msg);

                Response.End();

            }
            else
            {
                //访问失败
            }
        }

        //匹配验证码
        public ActionResult take()
        {
            string mobile_code = Session["mobile_code"].ToString();
            return Json(mobile_code, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}