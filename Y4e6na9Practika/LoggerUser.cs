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
    
    public partial class LoggerUser
    {
        public int LoggerUser_ID { get; set; }
        public Nullable<int> Users_ID { get; set; }
        public string AddressIP { get; set; }
        public Nullable<System.DateTime> TimeOut { get; set; }
        public string Nameuser { get; set; }
        public string Discription { get; set; }
    
        public virtual Users Users { get; set; }
    }
}