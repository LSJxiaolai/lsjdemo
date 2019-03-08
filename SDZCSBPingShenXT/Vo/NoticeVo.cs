using SDZCSBPingShenXT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDZCSBPingShenXT.Vo
{
    public class NoticeVo : B_Notice
    {
        public string NoticeType;
        public string PromulgatorUnit;
        public string releaseTimeStrTimer;
        public string effectiveTimer;
        public string ReleaseTimeStrTimer
        {
            get
            {
                return releaseTimeStrTimer;
            }
            set
            {
                try
                {
                    releaseTimeStrTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    releaseTimeStrTimer = value;
                }
            }
        }
        public string EffectiveTimer
        {
            get
            {
                return effectiveTimer;
            }
            set
            {
                try
                {
                    effectiveTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    effectiveTimer = value;
                }
            }
        }
    }
}