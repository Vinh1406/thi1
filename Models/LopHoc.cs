using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("LopHoc")]
    public partial class LopHoc
    {
        public LopHoc()
        {
            HocSinhs = new HashSet<HocSinh>();
        }

        [Key]
        public int malop { get; set; }

        [StringLength(25)]
        public string tenlop { get; set; }

        public virtual ICollection<HocSinh> HocSinhs { get; set; }
    }
}