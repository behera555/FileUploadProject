//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileUploadProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileUpload
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public System.DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public string Description { get; set; }
    }
}
