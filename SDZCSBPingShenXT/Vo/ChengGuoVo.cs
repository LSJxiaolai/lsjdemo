using SDZCSBPingShenXT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDZCSBPingShenXT.Vo
{
    public class ChengGuoVo : B_Achievement
    {
        public string AchievementGrade;
        private string timeser;

        public string Timeser
        {
            get {
                return timeser;
            }
            set
            {
                try
                {
                    timeser = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    timeser = value;
                }
            }
        } 
    }
}