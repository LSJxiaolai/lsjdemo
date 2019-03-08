using SDZCSBPingShenXT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDZCSBPingShenXT.Vo
{
    public class FilesVo:B_File
    {
        /// <summary>
        /// 公告名称
        /// </summary>
        public string NoticeName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileTypeName { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
    }
}