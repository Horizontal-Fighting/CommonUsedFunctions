using EntityFrameworkExample.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkExample.DAL
{
    public class Common
    {
        public static T_Employee ConvertEmpToDbFormat(Employee emp)
        {
            try
            { 
                T_Employee empRes = new T_Employee();
                empRes.Id = emp.Id;
                empRes.Name = emp.Name;
                empRes.Address = emp.Address;
                empRes.Email = emp.Email;
                empRes.Salary = emp.Salary;
                return empRes;
            }
            catch
            {
                return null;
            }
            
        }

        public static Employee ConvertEmpToEntityFormat(T_Employee t_emp)
        {
            try
            {
                Employee empRes = new Employee();
                empRes.Id = t_emp.Id;
                empRes.Name = t_emp.Name;
                empRes.Address = t_emp.Address;
                empRes.Email = t_emp.Email;
                empRes.Salary = t_emp.Salary;
                return empRes;
            }
            catch
            {
                return null;
            }

        }
    }
}
