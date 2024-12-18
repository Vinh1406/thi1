using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("HocSinh")]
    public partial class HocSinh
    {
        [Key]
        [StringLength(10)]
        [DisplayName("Số báo danh")]
        public string sbd { get; set; }

        [StringLength(50)]
        public string hoten { get; set; }

        [StringLength(50)]
        public string anhduthi { get; set; }

        public int? malop { get; set; }

        public double? diemthi { get; set; }

        public virtual LopHoc LopHoc { get; set; }
    }
}