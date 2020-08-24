using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolderInfo.Data
{
    [Table("File")]
    public partial class File
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(850)]
        [Column(TypeName = "nvarchar")]
        public string FileName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DataCreate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DataModified { get; set; }
    }
}
