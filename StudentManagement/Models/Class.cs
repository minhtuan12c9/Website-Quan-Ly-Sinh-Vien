using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Class
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string name { get; set; }
        public string nganh { get; set; }
        public string khoa { get; set; }
        public string soluong { get; set; }
        public string GVCN { get; set; }
    }
}