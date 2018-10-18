using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.CIM.Caojin.RPTWebApp.Models
{
    //Model
    [Bind(Exclude = "PersonID")]
    public class PersonViewModel
    {
        [ScaffoldColumn(false)]
        public int PersonID { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }

        [Display(Name = "手机号")]
        [Required(ErrorMessage = "不能为空")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNum { get; set; }

        public bool IsMarried { get; set; }

        public string Email { get; set; }
        public string Home { get; set; }
        public string Height { get; set; }

    }
}