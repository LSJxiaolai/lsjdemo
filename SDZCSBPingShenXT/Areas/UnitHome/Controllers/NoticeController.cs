using SDZCBootstrap.Common;
using SDZCSBPingShenXT.Models;
using SDZCSBPingShenXT.Vo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SDZCSBPingShenXT.Areas.UnitHome.Controllers
{
    public class NoticeController : Controller
    {
        // GET: UnitHome/Notice
        Models.SDZCSBPingShenXTEntities myModels = new Models.SDZCSBPingShenXTEntities();
        
        /// <summary>
        /// 通知公告--管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ToZhiGoGao()
        {
            return View();
        }
        /// <summary>
        /// 详情页面
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public ActionResult NoticeDetailed(int noticeId)
        {
            try
            {
                //根据公告id查询出公告的信息
                ANoticeVo notice = (from tbNotices in myModels.B_Notice

                                     join tbNoticeType in myModels.S_NoticeType on tbNotices.NoticeTypeID
                                     equals tbNoticeType.NoticeTypeID                                     
                                     where tbNotices.NoticeID == noticeId
                                     select new ANoticeVo
                                     {
                                         NoticeName = tbNotices.NoticeName,
                                         ReleaseTimeStr = tbNotices.ReleaseTimeStr,
                                         NoticeContent = tbNotices.NoticeContent,
                                         NoticeTypeName = tbNoticeType.NoticeType,
                                         ReleaseTimeStrr = tbNotices.ReleaseTimeStr.ToString()
                                     }).Single();



                //加载公告内容
                string textFileName = Server.MapPath("~/Document/Notice/Text/") + notice.NoticeContent;
                if (System.IO.File.Exists(textFileName))
                {
                    //文件存在
                    notice.NoticeContent = System.IO.File.ReadAllText(textFileName);
                }
                else
                {
                    //文件不存在
                    notice.NoticeContent = "<p>没有找到公告内容,可能文件已经丢失;请重新编辑发布</p>";
                }

                //加载附件列表
                List<FilesVo> files = (from tbFile in myModels.B_File
                                       join tbNoticeTable in myModels.B_Notice on tbFile.NoticeID equals tbNoticeTable.NoticeID
                                       join tbFileType in myModels.S_FileType on tbFile.FileTypeID equals tbFileType.FileTypeID
                                       where tbFile.NoticeID == noticeId
                                       select new FilesVo
                                       {
                                           FileID = tbFile.FileID,
                                           FileTypeID = tbFile.FileTypeID,
                                           Files = tbFile.Files,
                                           FileTypeName = tbFileType.FileTypeName,
                                           FileName = tbFile.Files
                                       }).ToList();
                ViewBag.isErrro = false;//用来处理异常
                ViewBag.notice = notice;//公告的内容
                ViewBag.files = files;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ViewBag.isErrro = true;
                ViewBag.notice = null;//为了避免出现异常
                ViewBag.files = null;
            }
            return View();
        }

        /// <summary>
        /// 新增公告页面
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertNotice()
        {
            return View();
        }

        /// <summary>
        /// 修改公告页面
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public ActionResult UpdateNotice(int? noticeId)
        {
            if (noticeId != null)
            {
                //try
                //{

                //    //跟登录界面ValideCode那里面的传递ID定义一样
                //    int user = Convert.ToInt32(Session["UserID"].ToString());
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //    //去登录页面定义一个方法，然后第一次运行的时候会直接跳转到登录页面
                //    return Redirect("/Main/Login");

                //}
                //通过ViewData 传送到页面
                ViewData["noticeId"] = noticeId;
                return View();
            }
            else
            {
                return Content("传入的修改参数不正确");
            }
        }

        #region 维护公告
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public ActionResult UpEeditorFile(HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null)
                {
                    //Path 引用了IO流
                    string fileExtension = Path.GetExtension(upload.FileName);

                    //如果是大写，那么就把大写转换成小写
                    fileExtension = fileExtension.ToLower();

                    //判断图片的格式 是否在预设的宽展的名称里  不然的话就会限制他的上传
                    if ("(.gif)|(.jpg)|(.bmp)|(.jpeg)|(.png)".Contains(fileExtension))
                    {
                        // fileName 用来保存到服务端  保证同一张图片上传也不会起冲突  获取当前的时间  Guid唯一的 fileExtension文件的类型
                        string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + fileExtension;
                        //获取物理路径  MapPath返回window相对的路径   ~/Document/Notice/ 符号要写完整，不然保存的路径就有问题了
                        string filePath = Server.MapPath("~/Document/Notice/") + fileName;

                        //保存到物理路径中去了
                        upload.SaveAs(filePath);

                        //获取上传的临时文件表(未保存的)
                        List<string> tempFile = new List<string>();
                        //判断前面临时的session文件，如果不为空，就要把它获取出来，然后在添加保存进去
                        if (Session["tempEditorFile"] != null)
                        {
                            tempFile = Session["tempEditorFile"] as List<string>;
                        }
                        //未保存公告的临时文件
                        tempFile.Add(fileName);
                        //保存到Session 用户的操作，
                        Session["tempEditorFile"] = tempFile;

                        //保存之后确保我们可以访问url的地址
                        string url = "/Document/Notice/" + fileName;

                        var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];
                        // 上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个 tab 选项
                        return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
                    }
                    else
                    {
                        //如果不行，就弹出提示框
                        return Content("<script>alert(\"只能上传图片\");</script>");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Content("<script>alert(\"上传图片失败\");</script>");
        }

        /// <summary>
        /// 清理公告文件
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearNtioce()
        {
            //获取文件上传的相对路径 ，所以需要MapPath("~/Document/Notice/")来获取物理地址
            string strServerPath = Server.MapPath("~/Document/Notice/");

            //获取上传的临时文件表(未保存的)
            List<string> tempFile = new List<string>();
            //判断前面临时的session文件，如果不为空，就要把它获取出来，然后再添加保存进去
            if (Session["tempEditor"] != null)
            {
                tempFile = Session["tempEditor"] as List<string>;
                foreach (string str in tempFile)
                {
                    // 完整的路径
                    string strFilePath = strServerPath + str;
                    //为了避免出现异常，用try catch
                    try
                    {//上传未保存的文件删除了
                        System.IO.File.Delete(strFilePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            //删除上传的附件
            //获取session中的文件表
            List<FilesVo> sessionFiles = new List<FilesVo>();
            if (Session["sessionFiles"] != null)
            {
                sessionFiles = Session["sessionFiles"] as List<FilesVo>;

                foreach (FilesVo file in sessionFiles)
                {
                    //字符串的拼接是经过了Url编码的  所以要进行解码还原回来
                    string strFileName = Server.UrlDecode(file.FileName);

                    string strFilePath = strServerPath + strFileName;
                    try
                    {
                        //删除未上传的文件
                        System.IO.File.Delete(strFilePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }
                }
            }
            //移除session
            Session.Remove("tempEditor");
            Session.Remove("sessionFiles");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 附件文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //file要跟视图那边定义的一样
        public ActionResult UpAttachment(HttpPostedFileBase file)
        {
            //处理数据一般要用try catch来定义一下

            try
            {
                //获取文件类型的扩展名名称
                string fileExtension = Path.GetExtension(file.FileName);
                //不包含文件扩展名的名称 
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);

                //原始文件名称（包括扩展名）
                string oldFileName = file.FileName;

                //文件名称，添加时间字符串，避免文件名称相同 2017-06-02-10-30-10-0000
                fileName = fileName + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + fileExtension;

                //保存文件的路径
                string filePath = Server.MapPath("~/Document/Notice/") + fileName;
                //保存文件
                file.SaveAs(filePath);

                //文件格式 （需要跟数据库对应）
                int fileTypeId = 0;
                string fileTypeName = "";
                //大写转小写
                fileExtension = fileExtension.ToLower();
                if ("(.mp4)|(.avi)|(.flv)|(.rmvb)|(.rm)|(.3gp)|(.mkv)|(.dvd)|(.mpg)|(.mov)".Contains(fileExtension))
                {
                    fileTypeId = 1;//视频
                    fileTypeName = "视频";
                }
                else if ("(.mp3)|(.wav)|(.cd)|(.ogg)|(.ape)|(.au)".Contains(fileExtension))
                {
                    fileTypeId = 2;//音频
                    fileTypeName = "音频";
                }
                else if ("(.txt)|(.text)".Contains(fileExtension))
                {
                    fileTypeId = 3;//文字
                    fileTypeName = "文字";
                }
                else if ("(.doc)|(.docx)|(.xsl)|(.xslx)|(.ppt)|(.pptx)".Contains(fileExtension))
                {
                    fileTypeId = 4;//文档
                    fileTypeName = "文档";
                }
                else if ("(.gif)|(.jpg)|(.bmp)|(.jpeg)|(.png)".Contains(fileExtension))
                {
                    fileTypeId = 5;//图片
                    fileTypeName = "图片";
                }
                else
                {
                    fileTypeId = 7;//其他
                    fileTypeName = "其他";
                }

                //获取session中的文件表
                List<FilesVo> sessionFiles = new List<FilesVo>();
                //把相关的信息存储在临时的文件
                if (Session["sessionFiles"] != null)
                {
                    sessionFiles = Session["sessionFiles"] as List<FilesVo>;
                }
                //存储刚刚上传的信息
                FilesVo filesVo = new FilesVo
                {
                    //设置的type的ID
                    //   FileTypeID = fileTypeId,
                    FileTypeID = fileTypeId,
                    //设置文件类型的名称
                    FileTypeName = fileTypeName,
                    FileName = Server.UrlEncode(fileName),//进行URL编码  
                    Files = "<a href=\"/UnitHome/Notice/DownAttachment?fileName=" + fileName + "\" target=\"_blank\">" + oldFileName + "</a>"
                };//Files 存的是文件下载的路径

                //把对象添加到列表里面去，
                sessionFiles.Add(filesVo);
                //把Session中的列表更新
                Session["sessionFiles"] = sessionFiles;

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json("fail", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <returns></returns>
        public ActionResult DownAttachment(string fileName)
        {
            //拼接路径  MapPath("~/Document/Notice/")上传的是服务器的存放目录的路径
            string strServerPath = Server.MapPath("~/Document/Notice/");

            //拼接文件完整的路径
            string strfilePath = strServerPath + fileName;
            //用来判断、拼接的路径是否存在
            if (System.IO.File.Exists(strfilePath))
            {

                //获取不包含文件扩展名的名称
                string strfileName = Path.GetFileNameWithoutExtension(strfilePath);
                //去掉时间字符串  Substring获取从开始到结束的字符串
                //Substring(0, strfileName.Length-24) 指的是从0开始，然后是整个字符串长度减去最后的24个字符
                strfileName = strfileName.Substring(0, strfileName.Length - 24);
                //还原文件名称 路径
                strfileName = strfileName + Path.GetExtension(strfilePath);
                //返回要下载的文件 File 文件  FileStream 文件流   FileMode.Open 我们只需要打开这个文件  strfileName由我们自己给他下载的指定文件名称
                return File(new FileStream(strfilePath, FileMode.Open), "application/octet-stream", strfileName);
            }
            else
            {
                return Content("您下载的文件不存在");
            }

        }

        /// <summary>
        /// 添加附件      列表查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectSessionFiles(BsgridPage bsgridPage)
        {
            //初始化
            List<FilesVo> sessionFiles = new List<FilesVo>();
            //用来存储返回回去的 listFiles
            List<FilesVo> listFiles = new List<FilesVo>();
            if (Session["sessionFiles"] != null)
            {
                sessionFiles = Session["sessionFiles"] as List<FilesVo>;
            }

            if (sessionFiles != null)//列表不为空 然后获取出来
            {
                int endIndex = bsgridPage.GetEndIndex();//结束的行
                if (endIndex >= sessionFiles.Count)//行号大于列表中行数减一的序号
                {

                    endIndex = sessionFiles.Count - 1;
                }

                for (int i = bsgridPage.GetStartIndex(); i <= endIndex; i++)
                {
                    //  接收查询出来的数据 listFiles
                    //sessionFiles[i]获取当前索引的工作对象添加到listFiles去
                    listFiles.Add(sessionFiles[i]);
                }
            }
            //分页插件的返回 bsgrid
            Bsgrid<FilesVo> bsgrid = new Bsgrid<FilesVo>();
            if (sessionFiles != null)
            {
                bsgrid.totalRows = sessionFiles.Count;
            }
            else
            {
                bsgrid.totalRows = 0;
            }
            bsgrid.success = true;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listFiles;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除附件文件信息
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public ActionResult DeleteAttachment(string FileName)
        {
            try
            {
                //解码 
                FileName = Server.UrlDecode(FileName);

                //删除文件
                string filePath = Server.MapPath("~/Document/Notice/") + FileName;
                System.IO.File.Delete(filePath);

                //获取session中的附件文件表
                List<FilesVo> sessionFiles = new List<FilesVo>();
                if (Session["sessionFiles"] != null)
                {
                    sessionFiles = Session["sessionFiles"] as List<FilesVo>;
                }
                //移除对应的文件名称
                if (sessionFiles != null)
                {
                    foreach (FilesVo sessionFile in sessionFiles)
                    {

                        if (sessionFile.FileName == Server.UrlEncode(FileName))
                        {
                            sessionFiles.Remove(sessionFile);
                            break;
                        }
                    }
                }
                //更新session
                Session["sessionFiles"] = sessionFiles;
                return Json(true, JsonRequestBehavior.AllowGet);//成功
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 公告新增保存
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)] //把验证方法关掉
        public ActionResult NoticeInsert(B_Notice Notice, HttpPostedFileBase noticeCarouseImage, string noticeCarousel)
        {
            try
            {
                //定义list 存放需要保存的复文本框图片的名称（路径）
                List<string> savedImageList = new List<string>();

                if (!string.IsNullOrEmpty(Notice.NoticeName) && !string.IsNullOrEmpty(Notice.NoticeContent))
                {
                    //检查 存放公告内容的 目录是否存在,不存在就创建
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Document/Notice/Text/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Document/Notice/Text/"));
                    }

                    // --->用txt文件来保存公告内容
                    //txt文件名称 也就是扩展名称 
                    string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + ".txt";
                    //txt文件路径  完整的路径
                    string filePath = Server.MapPath("~/Document/Notice/Text/") + fileName;
                    //txt文件内容 公告的内容
                    string textCount = Notice.NoticeContent;

                    //-----获取最终保存的图片文件 正则表达式  System.Text.RegularExpressions
                    //MatchCollection匹配之后返回的多条数据
                    //Matches只会返回第一个匹配值 只有满足正则表达式条件都会返回回来
                    System.Text.RegularExpressions.MatchCollection matchs = System.Text.RegularExpressions.Regex.Matches(textCount, "(?<=/Document/Notice/).+?(?=\".+?/>)");
                    foreach (System.Text.RegularExpressions.Match match in matchs)
                    {
                        savedImageList.Add(match.Value);
                    }

                    //保存文件 txt文件
                    //StreamWriter 文件路径，是否追加，文件编码
                    //TextWriter属于IO流  StreamWriter文件操作流 filePath路径 false 你是追加还是覆盖，追加是true 覆盖就是false 追加就是从最后一个数继续追加下去，覆盖就是直接在原有的数据直接删除覆盖掉 UTF8Encoding(false)) 编码把Tum的扩展去掉
                    TextWriter textWriter = new StreamWriter(filePath, false, new System.Text.UTF8Encoding(false));
                    textWriter.Write(textCount);//把文件内容直接写进去
                    textWriter.Close();//手动把IO流关掉

                    //保存公告信息
                    //Notice.UserID = 1;//用户ID  需要跟上面定义的一样
                    Notice.NoticeContent = fileName;//公告内容 需要跟上面定义的一样
                    Notice.ReleaseTimeStr = DateTime.Now;//发布时间

                    //保存公告数据
                    myModels.B_Notice.Add(Notice);
                    if (myModels.SaveChanges() > 0)
                    {
                        int intNoticeId = Notice.NoticeID;
                        //--->保存附件文件
                        //获取session中的文件表
                        List<FilesVo> sessionFiles = new List<FilesVo>();
                        if (Session["sessionFiles"] != null)
                        {
                            sessionFiles = Session["sessionFiles"] as List<FilesVo>;
                        }
                        //有上传的附件
                        if (sessionFiles != null && sessionFiles.Count > 0)
                        {
                            List<B_File> listfFiles = new List<B_File>();
                            //便利下面的session
                            for (int i = 0; i < sessionFiles.Count; i++)
                            {
                                B_File file = new B_File();
                                file.NoticeID = intNoticeId;
                                file.FileTypeID = sessionFiles[i].FileTypeID;
                                file.Files = sessionFiles[i].Files;
                                listfFiles.Add(file);
                            }
                            //保存
                            //AddRange 保存一个列表，多条数据 add 是一条
                            myModels.B_File.AddRange(listfFiles);
                            myModels.SaveChanges();
                        }

                        //清理上传到复文本框中，后被删除（即未被使用的图片）
                        //获取上传的临时图片文件表(未保存的)
                        List<string> tempFile = new List<string>();
                        if (Session["tempEditorFile"] != null)
                        {
                            //session上传之后，定义一个tempEditorFile
                            tempFile = Session["tempEditorFile"] as List<string>;
                        }
                        if (tempFile != null && tempFile.Count > 0)
                        {
                            //获取路径
                            string dFilePath = Server.MapPath("~/Document/Notice/");
                            //遍历临时文件表
                            //重命名按主Ctrl键+r+r 可直接修改
                            foreach (string img in tempFile)
                            {
                                //当临时文件表中的文件不存在于被保存的文件时,删除该文件,避免文件冗余
                                //
                                if (!savedImageList.Contains(img))
                                {
                                    //拼接要删除的文件
                                    string strdeletePath = dFilePath + img;
                                    try
                                    {
                                        System.IO.File.Delete(strdeletePath);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
                                }
                            }
                        }
                        //移除session
                        Session.Remove("sessionFiles");
                        Session.Remove("tempEditorFile");

                        return Json(true, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="notieId"></param>
        /// <returns></returns>
        public ActionResult NoticeDelete(int notieId)
        {
            try
            {
                B_Notice dbNoticeTable = (from tbNoticeTable in myModels.B_Notice where tbNoticeTable.NoticeID == notieId select tbNoticeTable).Single();
                //先把公告内容的名称拿到
                string strNoticeContentPath = Server.MapPath("~/Document/Notice/Text/") + dbNoticeTable.NoticeContent;

                //进行文件判断是否存在
                if (System.IO.File.Exists(strNoticeContentPath))
                {

                    string strContent = System.IO.File.ReadAllText(strNoticeContentPath);

                    //匹配出文件名称 保存到list oldSavedImageList Matches匹配出图片的名称
                    MatchCollection oldMatchs = Regex.Matches(strContent, "(?<=Document/Notice/).+?(?=\".+?/>)");
                    foreach (Match match in oldMatchs)
                    {
                        string strImgPath = Server.MapPath("~/Document/Notice/") + match.Value;
                        try
                        {
                            System.IO.File.Delete(strImgPath);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    //删除公告内容的文件
                    try
                    {
                        System.IO.File.Delete(strNoticeContentPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                //移除
                myModels.B_Notice.Remove(dbNoticeTable);

                //删除附件
                List<B_File> listFiles = (from tbFile in myModels.B_File where tbFile.NoticeID == notieId select tbFile).ToList();
                //删除附件文件
                foreach (B_File file in listFiles)
                {
                    //附件的文件路径  （链接）
                    string filePath = "~/Document/Notice/" + Regex.Match(file.Files, "(?<=fileName=).+?(?=\")").Value;
                    //从相对地址转换成物理地址
                    filePath = Server.MapPath(filePath);

                    try
                    {
                        //删除文件路径
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                //删除附件的数据库信息
                myModels.B_File.RemoveRange(listFiles);


                myModels.SaveChanges();//保存
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询公告信息 by id （修改）
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public ActionResult SelectNoticeById(int? noticeId)//绑定数据哪里可复制查询方法，然后加多where查询语句
        {
            try
            {
                //根据公告id查询出公告的信息
                ANoticeVo notice = (from tbNotices in myModels.B_Notice
                                    join tbNoticeType in myModels.S_NoticeType on tbNotices.NoticeTypeID
                                    equals tbNoticeType.NoticeTypeID
                                    join tbSPromulgatorUnit in myModels.S_PromulgatorUnit on tbNotices.PromulgatorUnitID
                                    equals tbSPromulgatorUnit.PromulgatorUnitID
                                    where tbNotices.NoticeID == noticeId
                                    select new ANoticeVo
                                    {
                                        NoticeID = tbNotices.NoticeID,
                                        NoticeName = tbNotices.NoticeName,
                                        PromulgatorUnitID = tbSPromulgatorUnit.PromulgatorUnitID,
                                        Effective = tbNotices.Effective,
                                        EffectiveStr = tbNotices.Effective.ToString(),
                                        ReleaseTimeStr = tbNotices.ReleaseTimeStr,
                                        NoticeContent = tbNotices.NoticeContent,
                                        NoticeTypeName = tbNoticeType.NoticeType,
                                        NoticeTypeID = tbNoticeType.NoticeTypeID,
                                        ReleaseTimeStrr = tbNotices.ReleaseTimeStr.ToString()
                                    }).Single();
                //加载公告内容  文件地址名称 notice.NoticeContent;
                // Server.MapPath("~/Document/Notice/Text/")监听物理地址 
                string textFileName = Server.MapPath("~/Document/Notice/Text/") + notice.NoticeContent;
                //
                if (System.IO.File.Exists(textFileName))
                {//文件存在
                    //ReadAllText把文件里面的所有行读取出来
                    notice.NoticeContent = System.IO.File.ReadAllText(textFileName);
                }
                else
                {//文件不存在返回
                    notice.NoticeContent = "<p>没有找到公告内容，可能文件已经丢失，请重新编辑发布</p>";
                }

                //加载附件列表到session
                List<FilesVo> sessionFiles = (from tbFile in myModels.B_File
                                              join tbNoticeTable in myModels.B_Notice on tbFile.NoticeID equals tbNoticeTable.NoticeID
                                              join tbFileType in myModels.S_FileType on tbFile.FileTypeID equals tbFileType.FileTypeID
                                              where tbFile.NoticeID == noticeId
                                              select new FilesVo
                                              {
                                                  FileID = tbFile.FileID,
                                                  FileTypeID = tbFile.FileTypeID,
                                                  Files = tbFile.Files,
                                                  FileTypeName = tbFileType.FileTypeName,
                                                  FileName = tbFile.Files
                                              }).ToList();
                for (int i = 0; i < sessionFiles.Count; i++)
                {
                    //通过正则表达式获取文件名称
                    //Files 需要被匹配的正则表达式
                    string strFileName = System.Text.RegularExpressions.Regex.Match(sessionFiles[i].Files, "(?<=fileName=).+?(?=\" )").Value;
                    sessionFiles[i].FileName = Server.UrlEncode(strFileName);
                }
                List<FilesVo> oldSessionFiles = new List<FilesVo>();
                foreach (FilesVo file in sessionFiles)
                {
                    //用来存储以前的数据
                    oldSessionFiles.Add(file);
                }
                //更新sessionFiles
                Session["sessionFiles"] = sessionFiles;
                //记录原来的附件  
                Session["oldSessionFiles"] = oldSessionFiles;
                return Json(notice, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 修改公告保存
        /// </summary>
        /// <param name="pwNotice"></param>
        /// <param name="noticeCarouseImage"></param>
        /// <param name="noticeCarousel"></param>
        /// <returns></returns>
        [HttpPost]//限定只能通过post方法提交
        [ValidateInput(false)]//关闭验证
        public ActionResult NoticeUpdate(B_Notice pwNotice)
        {
            try
            {

                //保存现在插入的图片
                List<string> savedImageList = new List<string>();
                //保存原始插入图片
                List<string> oldSavedImageList = new List<string>();

                //检查公告标题不为空 
                if (!string.IsNullOrEmpty(pwNotice.NoticeName))
                {//查询原始公告信息
                    B_Notice dbNotice = (from tbNoticeTable in myModels.B_Notice where tbNoticeTable.NoticeID == pwNotice.NoticeID select tbNoticeTable).Single();
                    //检查目录是否存在，不存在就创建
                    if (!Directory.Exists(Server.MapPath("~/Document/Notice/Text/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Document/Notice/Text/"));
                    }
                    //检查目录是否存在，不存在就创建
                    if (!Directory.Exists(Server.MapPath("~/Document/Notice/NoticeCarousel/")))
                    {
                        //CreateDirectory 指定路径创建所有目录跟子目录
                        Directory.CreateDirectory(Server.MapPath("~/Document/Notice/NoticeCarousel/"));
                    }
                    //用txt文件保持公告内容
                    //txt文件名称
                    string fileName;
                    //txt文件路径
                    string filePath;
                    //加载原公告内容 （路径加上内容名称）
                    string oldFilePath = Server.MapPath("~/Document/Notice/Text/") + dbNotice.NoticeContent;
                    //判断原公告内容是否存在
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        //读取原始文本内容 
                        //原来文本存在文件 ReadAllText读取
                        string oldTextContent = System.IO.File.ReadAllText(oldFilePath);
                        //匹配出文件名称 保存到list oldSavedImageList Matches匹配出图片的名称
                        MatchCollection oldMatchs = Regex.Matches(oldTextContent, "(?<=Document/Notice/).+?(?=\".+?/>)");
                        foreach (Match match in oldMatchs)
                        {
                            //原来的文本，需要更新数据
                            oldSavedImageList.Add(match.Value);
                        }
                        //获取文件名称
                        fileName = dbNotice.NoticeContent;
                        //文件路径
                        filePath = oldFilePath;

                    }
                    else
                    {
                        //不存在的文件 就重新创建一个文件名称
                        fileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + Guid.NewGuid() + ".txt";
                        //不存在文件路径  重新创建一个路径
                        filePath = Server.MapPath("~/Document/Notice/Text/") + fileName;
                    }
                    //新修改后提交txt的文件内容
                    string textContent = pwNotice.NoticeContent;
                    //获取最终匹配保存名称到数据中的文件
                    MatchCollection matchs = Regex.Matches(textContent, "(?<=/Document/Notice/).+?(?=\".+?/>)");
                    foreach (Match match in matchs)
                    {// 把保存到的图片加到 savedImageList里面去
                        savedImageList.Add(match.Value);
                    }
                    //检查没有使用的插图，将其删除 遍历原来的插图文件列表oldSavedImageList，如在现在的文件列表savedImageList，则在使用，如不存在，则未使用过的应该移除掉
                    foreach (string str in oldSavedImageList)
                    {//判断是否存在，不存在就删除
                        if (!savedImageList.Contains(str))
                        {
                            //删除文件（先获取地址名称加收文件名）
                            string dfilePath = Server.MapPath("~/Document/Notice/") + str;
                            try
                            {//从物理地址上移除掉
                                System.IO.File.Delete(dfilePath);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }
                    //
                    //获取上传的临时插图文件表（未保存的）
                    List<string> tempFile = new List<string>();
                    if (Session["tempEditorFile"] != null)//tempEditorFile 记录修改的时候上传的图片
                    {
                        tempFile = Session["tempEditorFile"] as List<string>;
                    }
                    if (tempFile != null)
                    {
                        string dFilePath = Server.MapPath("~/Document/Notice/");
                        //遍历临时文件表
                        foreach (string s in tempFile)
                        {
                            //当临时文件表中的文件 不存在 与被保存的文件时，删除该文件，避免文件冗余
                            if (!savedImageList.Contains(s))
                            {
                                string strdeletePath = dFilePath + s;
                                try
                                {
                                    //把复文本框的冗余删除掉
                                    System.IO.File.Delete(strdeletePath);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                        }
                    }

                    //保存文件 公告内容的txt文件
                    //StreamWriter 输出流  UTF8Encoding(false)不要编码扩展 false不是追加就是覆盖
                    TextWriter textWriter = new StreamWriter(filePath, false, new System.Text.UTF8Encoding(false));
                    textWriter.Write(textContent);
                    textWriter.Close();//关闭流

                    //更新公告信息
                    dbNotice.NoticeTypeID = pwNotice.NoticeTypeID;
                    dbNotice.PromulgatorUnitID = pwNotice.PromulgatorUnitID;
                    dbNotice.NoticeName = pwNotice.NoticeName;
                    dbNotice.Effective = pwNotice.Effective;
                    dbNotice.ReleaseTimeStr = pwNotice.ReleaseTimeStr;//发布更新时间
                    dbNotice.NoticeContent = fileName;
                    //保存更新
                    myModels.Entry(dbNotice).State = System.Data.Entity.EntityState.Modified;
                    if (myModels.SaveChanges() > 0)
                    {
                        int intNoticeId = pwNotice.NoticeID;
                        //保存附件文件
                        //获取session中的文件表 
                        List<FilesVo> sessionFiles = new List<FilesVo>();
                        if (Session["sessionFiles"] != null)
                        {
                            sessionFiles = Session["sessionFiles"] as List<FilesVo>;
                        }
                        //获取oldSessionFiles原来的附件文件列表
                        List<FilesVo> oldSessionFiles = new List<FilesVo>();

                        var ss = Session["oldSessionFiles"];

                        if (Session["oldSessionFiles"] != null)
                        {
                            oldSessionFiles = Session["oldSessionFiles"] as List<FilesVo>;
                        }
                        //添加附件
                        List<FilesVo> addFiles = new List<FilesVo>();
                        if (sessionFiles != null)
                        {
                            //sessionFiles新附件列表中的文件不在 oldSessionFiles旧附件列表中的文件就是新增的  sessionFiles这里面有的，而oldSessionFiles没有的，就是新添加的附件 
                            foreach (FilesVo filesVo in sessionFiles)
                            {
                                if (!oldSessionFiles.Contains(filesVo))
                                {
                                    addFiles.Add(filesVo);
                                }
                            }
                        }
                        //移除附件  如果oldSessionFiles有的，而sessionFiles没有的就是被移除掉了
                        List<FilesVo> deleteFiles = new List<FilesVo>();
                        if (oldSessionFiles != null)
                        {
                            foreach (FilesVo filesVo in oldSessionFiles)
                            {//判断是否在sessionFiles中
                                if (!sessionFiles.Contains(filesVo))
                                {
                                    deleteFiles.Add(filesVo);
                                }
                            }
                        }
                        //有新上传的附件
                        if (addFiles.Count > 0)
                        {
                            //添加的时候一定要用PW_File
                            //遍历addFiles
                            List<B_File> listfFiles = new List<B_File>();
                            for (int i = 0; i < addFiles.Count; i++)
                            {
                                B_File file = new B_File();
                                file.NoticeID = intNoticeId;
                                file.FileTypeID = addFiles[i].FileTypeID;
                                file.Files = addFiles[i].Files;
                                listfFiles.Add(file);
                            }
                            //把新添加的附件到保存listfFiles
                            myModels.B_File.AddRange(listfFiles);
                            myModels.SaveChanges();
                        }
                        //有移除的附件
                        if (deleteFiles.Count > 0)
                        {
                            //获取上传的附件的目录（物理路径·）
                            string dFilePath = Server.MapPath("~/Document/Notice/");
                            List<B_File> listfFiles = new List<B_File>();

                            for (int i = 0; i < deleteFiles.Count; i++)
                            {
                                int fileId = deleteFiles[i].FileID;
                                B_File file = (from tbFile in myModels.B_File where tbFile.FileID == fileId select tbFile).Single();
                                listfFiles.Add(file);
                                //删除文件
                                try
                                {
                                    System.IO.File.Delete(dFilePath + deleteFiles[i].FileName);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                            //保存
                            myModels.B_File.RemoveRange(listfFiles);
                            myModels.SaveChanges();//对应的数据库删除
                        }
                        //处理完了之后移除session
                        Session.Remove("tempEditorFile");
                        Session.Remove("sessionFiles");
                        Session.Remove("oldSessionFiles");

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelectNoticer()
        {
            var listNotice = (from tbNotice in myModels.B_Notice
                              orderby tbNotice.ReleaseTimeStr
                              select new ANoticeVo
                              {
                                  NoticeID = tbNotice.NoticeID,
                                  NoticeTypeID = tbNotice.NoticeTypeID,
                                  NoticeName = tbNotice.NoticeName,
                                  ReleaseTimeStr = tbNotice.ReleaseTimeStr,
                                  NoticeContent = tbNotice.NoticeContent,
                                  ReleaseTimeStrr = tbNotice.ReleaseTimeStr.ToString()
                              }).ToList();
            return Json(listNotice, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 数据维护
        /// <summary>
        /// 公告类别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectNoticeType()
        {
            var listNoticeType = (from tbNoticeType in myModels.S_NoticeType
                           select new SelectVo
                           {
                               id = tbNoticeType.NoticeTypeID,
                               text = tbNoticeType.NoticeType
                           }).ToList();
            listNoticeType = Tools.SetSelectJson(listNoticeType);
            return Json(listNoticeType, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 发布单位
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPromulgatorUnit()
        {
            var listPromulgatorUnit = (from tbPromulgatorUnit in myModels.S_PromulgatorUnit
                                       select new SelectVo
                                       {
                                           id = tbPromulgatorUnit.PromulgatorUnitID,
                                           text = tbPromulgatorUnit.PromulgatorUnit
                                       }).ToList();
            listPromulgatorUnit = Tools.SetSelectJson(listPromulgatorUnit);
            return Json(listPromulgatorUnit,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询公告列表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult selectNotice(BsgridPage bsgridPage, int noticeTypeId, string noticeName, string releaseTimeStrTimer)
        {
            try
            {
                List<NoticeVo> linqNotice = (from tbNotice in myModels.B_Notice
                                             join tbTyoe in myModels.S_NoticeType on tbNotice.NoticeTypeID equals tbTyoe.NoticeTypeID
                                             join tbPromulgatorUnit in myModels.S_PromulgatorUnit on tbNotice.PromulgatorUnitID equals tbPromulgatorUnit.PromulgatorUnitID
                                             select new NoticeVo
                                             {
                                                 NoticeID = tbNotice.NoticeID,
                                                 NoticeTypeID = tbTyoe.NoticeTypeID,
                                                 NoticeType = tbTyoe.NoticeType,
                                                 PromulgatorUnit = tbPromulgatorUnit.PromulgatorUnit,
                                                 NoticeName = tbNotice.NoticeName,
                                                 NoticeContent = tbNotice.NoticeContent,
                                                 releaseTimeStrTimer = tbNotice.ReleaseTimeStr.ToString(),
                                                 effectiveTimer = tbNotice.Effective.ToString(),
                                             }).ToList();
                if (noticeTypeId > 0)
                {
                    linqNotice = linqNotice.Where(s => s.NoticeTypeID == noticeTypeId).ToList();
                }
                if (!string.IsNullOrEmpty(noticeName))
                {
                    linqNotice = linqNotice.Where(s => s.NoticeName.Contains(noticeName.Trim())).ToList();
                }
                if (!string.IsNullOrEmpty(releaseTimeStrTimer))
                {
                    linqNotice = linqNotice.Where(s => s.releaseTimeStrTimer.Contains(releaseTimeStrTimer.Trim())).ToList();
                }
                List<NoticeVo> listFile = linqNotice.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页
                Bsgrid<NoticeVo> bsgrid = new Bsgrid<NoticeVo>();
                bsgrid.success = true;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.totalRows = linqNotice.Count();
                bsgrid.data = linqNotice;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 申报指南
        public ActionResult ShenBaoZhiNan()
        {
            return View();
        }
        #endregion
    }
}