//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PLAN
    {
        public int ID_Plan { get; set; }
        public string Station { get; set; }
        public Nullable<int> DayPlan { get; set; }
        public Nullable<int> MonthPlan { get; set; }
        public string SHIFT { get; set; }
    }
}
