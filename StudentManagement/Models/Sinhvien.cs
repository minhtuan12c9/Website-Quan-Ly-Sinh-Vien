using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Sinhvien
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string mssv { get; set; }
        public string lop { get; set; }
        public string ngaysinh { get; set; }
        public string phone { get; set; }
        public string khoa { get; set; }
        public string gioitinh { get; set; }
        public string tongiao { get; set; }
        public string nganh { get; set; }
        public string avatar { get; set; }

        public virtual User user { get; set; }

    }
}