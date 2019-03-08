using SDZCSBPingShenXT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDZCSBPingShenXT.Vo
{
    public class ANoticeVo : B_Notice
    {
        /// <summary>
        /// 公告类型ID
        /// </summary>
        public int NoticeTypeIiD { get; set; }
        /// <summary>
        /// 公告类型
        /// </summary>
        public string NoticeTypeName { get; set; }

        public string PromulgatorUnitIiD { get; set; }

        public string PromulgatorUnit { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string ReleaseTimeStrr { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public string EffectiveStr { get; set; }
    }
}