using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace StudentManagement.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("password")]
        public string confirmpassword { get; set; }

        public string chucvu { get; set; }

        public virtual Sinhvien sinhvien { get; set; }
        public virtual Giangvien giangvien { get; set; }
    }
}