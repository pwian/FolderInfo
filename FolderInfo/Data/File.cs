using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolderInfo.Data
{
    [Table("File")]
    public partial class File
    {
        [Key]
        [Required]
        public string FileName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DataCreate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DataModified { get; set; }
    }
}
