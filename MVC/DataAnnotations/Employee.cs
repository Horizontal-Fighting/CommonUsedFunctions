//Created by: Rong Fan
//Email:rong.fan1031@gmail.com
//2016-9-11

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAnnotations.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        //展示在界面的名称, showed on UI
        [DisplayName("Employ Name")]
        [Required(ErrorMessage ="Employee Name is required")]
        [StringLength(35)]//maximum
        public string Name { get; set; }

        [Required(ErrorMessage ="Employee Address is required")]
        [StringLength(300)]
        public string Address { get; set; }

        [Required(ErrorMessage ="salary is required")]
        // Range()是一个双闭区间，就是说，包含3000和100000。
        [Range(3000,100000,ErrorMessage ="Salary must be between 3000 and 1000000")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage ="please enter your email Address")]
        [DataType(DataType.EmailAddress)]
        [DisplayName( "Email Address")]
        [MaxLength(50)]
        [RegularExpression(@"/^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/",ErrorMessage ="Please enter correct email address.")]
        public string Email { get; set; }

    }
}