using SDZCSBPingShenXT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDZCSBPingShenXT.Vo
{
    public class BasicVo: B_Basic
    {
        public string DeclareGrade { get; set; }
        public string DeclareSeries { get; set; }
        public string DeclareQualification { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public string Education2 { get; set; }
        public string Major2 { get; set; }
        public string MajorsPost { get; set; }
        public string ForeignLanguageRank { get; set; }
        public string DeclareMode { get; set; }
        public string Majors { get; set; }


        public string ExamCase { get; set; }
        public string ExamineResult { get; set; }
        public string DeclareYear { get; set; }
        public string ExamineResult2 { get; set; }
        public string DeclareYear2 { get; set; }
        public string ExamineResult3 { get; set; }
        public string DeclareYear3 { get; set; }
        public string ExamineResult4 { get; set; }
        public string ExamineResult5 { get; set; }
        public string RecordUnit { get; set; }
        public string Timers { get; set; }
        public string Item { get; set; }
        public string AchievementGrade { get; set; }
        public string SituateNextrs { get; set; }
        public string RatifySection { get; set; }
        public string Accessoryrs { get; set; }
        public string Experience { get; set; }
        public string WorkResult { get; set; }
        public string Timery { get; set; }
        public string Title { get; set; }
        public string SituateNextry { get; set; }
        public string PeriodicalPublishing { get; set; }
        public string Accessoryry { get; set; }

        public string DeclareStatus { get; set; }
        public string birthdayer;
        public string formTimer;
        public string joinWorkTimer;
        public string graduateTimer;
        public string graduateTime1er;
        public string gainedQualificationTimer;
        public string engageTimer;
        public string takeOfficeTimer;
        public string Birthdayer
        {
            get
            {
                return birthdayer;
            }
            set
            {
                try
                {
                    birthdayer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    birthdayer = value;
                }
            }
        }
        public string FormTimer
        {
            get
            {
                return formTimer;
            }
            set
            {
                try
                {
                    formTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    formTimer = value;
                }
            }
        }
        public string JoinWorkTimer
        {
            get
            {
                return joinWorkTimer;
            }
            set
            {
                try
                {
                    joinWorkTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    joinWorkTimer = value;
                }
            }
        }
        public string GraduateTimer
        {
            get
            {
                return graduateTimer;
            }
            set
            {
                try
                {
                    graduateTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    graduateTimer = value;
                }
            }
        }
        public string GraduateTime1er
        {
            get
            {
                return graduateTime1er;
            }
            set
            {
                try
                {
                    graduateTime1er = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    graduateTime1er = value;
                }
            }
        }
        public string GainedQualificationTimer
        {
            get
            {
                return gainedQualificationTimer;
            }
            set
            {
                try
                {
                    gainedQualificationTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    gainedQualificationTimer = value;
                }
            }
        }
        public string EngageTimer
        {
            get
            {
                return engageTimer;
            }
            set
            {
                try
                {
                    engageTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    engageTimer = value;
                }
            }
        }
        public string TakeOfficeTimer
        {
            get
            {
                return takeOfficeTimer;
            }
            set
            {
                try
                {
                    takeOfficeTimer = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    takeOfficeTimer = value;
                }
            }
        }
    }
}