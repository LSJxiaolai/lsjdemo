using SDZCSBPingShenXT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDZCSBPingShenXT.Vo
{
    public class WorkVo: B_WorkPersonnel
    {
        public string Sex;
        public string UserState;
        public string registerTimer;
        public string finallyEnterTimer;
        public string RegisterTimer
        {
            get
            {
                return registerTimer;
            }
            set
            {
                try
                {
                    registerTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    registerTimer = value;
                }
            }
        }
        public string FinallyEnterTimer
        {
            get
            {
                return finallyEnterTimer;
            }
            set
            {
                try
                {
                    finallyEnterTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    finallyEnterTimer = value;
                }
            }
        }
    }
}