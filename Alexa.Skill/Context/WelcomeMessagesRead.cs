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
    
    public partial class WelcomeMessagesRead
    {
        public int Id { get; set; }
        public int WelcomeMessageId { get; set; }
        public string UserId { get; set; }
        public bool ReadSecond { get; set; }
    
        public virtual WelcomeMessage WelcomeMessage { get; set; }
    }
}
