//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDZCSBPingShenXT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class B_CheckPersonnel
    {
        public int CheckPersonnelID { get; set; }
        public Nullable<int> UserStateID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string CheckName { get; set; }
        public string CheckUserName { get; set; }
        public Nullable<int> SexID { get; set; }
        public string MaillSite { get; set; }
        public string PhoneCode { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public Nullable<System.DateTime> FinallyEnterTime { get; set; }
    }
}