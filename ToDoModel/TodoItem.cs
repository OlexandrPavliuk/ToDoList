//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToDoDBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public System.DateTime AddedAt { get; set; }
        public string AddedBy { get; set; }
        public byte WasDone { get; set; }
        public Nullable<System.DateTime> WasDoneAt { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
    }
}
