//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alexa.Skill.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
        public int Id { get; set; }
        public Nullable<int> WatchlistItemId { get; set; }
        public int NewsCategoryId { get; set; }
        public System.DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
