//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Y4e6na9Practika
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application_Submission
    {
        public int Application_Submission_ID { get; set; }
        public Nullable<int> Educational_institution_ID { get; set; }
        public Nullable<System.DateTime> Date_of_submission { get; set; }
        public string Status { get; set; }
        public Nullable<int> Users_ID { get; set; }
    
        public virtual Educational_Institution Educational_Institution { get; set; }
        public virtual Users Users { get; set; }
    }
}
