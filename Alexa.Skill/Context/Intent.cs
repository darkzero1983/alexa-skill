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
    
    public partial class Intent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Intent()
        {
            this.IntentCalls = new HashSet<IntentCall>();
            this.IntentMessages = new HashSet<IntentMessage>();
        }
    
        public int Id { get; set; }
        public int SkillId { get; set; }
        public string Name { get; set; }
        public bool ShouldEndSession { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntentCall> IntentCalls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntentMessage> IntentMessages { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
