using EntityFrameworkExample.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EntityFrameworkExample.DAL
{
    public class EmployeeDAL
    {
        EmployeeDataBaseEntities dataContext;
        //IMPLEMENT CRUD
        public void AddEmployee(Employee emp)
        {
            using (dataContext=new EmployeeDataBaseEntities())
            {
                var toBeAddedEmp = Common.ConvertEmpToDbFormat(emp);
                dataContext.T_Employee.Add(toBeAddedEmp);
                dataContext.SaveChanges();
            }
        }

        //查询、编辑、保存
        public void Edit1Employee(Employee emp)
        {
            using (dataContext = new EmployeeDataBaseEntities())
            {
                var model = dataContext.T_Employee.FirstOrDefault(r => r.Id == emp.Id);
                if (model!=null)
                {
                    model.Name = emp.Name;
                    model.Address = emp.Address;
                    model.Email = emp.Email;
                    model.Salary = emp.Salary;
                    dataContext.SaveChanges();
                }
            }
        }

        //直接编辑，不查询
        public void Edit2Employee(Employee emp)
        {
            using (dataContext = new EmployeeDataBaseEntities())
            {
                T_Employee empInDb = Common.ConvertEmpToDbFormat(emp);

                //获取代理对象类的状态为Detaceh
                System.Data.Entity.Infrastructure.DbEntityEntry entry = dataContext.Entry(empInDb);
                //1、将代理类的状态修改成 Unchanged 2、将代理类中的需要更新的字段的IsModified修改成true
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("Name").IsModified = true;
                entry.Property("Address").IsModified = true;
                entry.Property("Email").IsModified = true;
                entry.Property("Salary").IsModified = true;

                //解决对一个或多个实体验证失败的方法：关闭EF的实体合法性检查
                dataContext.Configuration.ValidateOnSaveEnabled = false;

                //保存
                dataContext.SaveChanges();
            }
         }


        //Entity Framework Delete
        public void DeleteEmployee(int empId)
        {
            using (dataContext = new EmployeeDataBaseEntities())
            {
                var list = dataContext.T_Employee.Where(r => r.Id == empId);
                dataContext.T_Employee.RemoveRange(list);
                dataContext.SaveChanges();
            }
        }

        //Execute SQL Command
        public void ExecuteSQLCommand(string sqlStr, SqlParameter[] p)
        {
            using (dataContext = new EmployeeDataBaseEntities())
            {
                
                dataContext.Database.ExecuteSqlCommand(sqlStr, p);
            }
        }

        //查
        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> results = new List<Employee> ();
            using (dataContext = new EmployeeDataBaseEntities())
            {
                string conStr=dataContext.Database.Connection.ConnectionString;
                IQueryable<T_Employee> query = dataContext.T_Employee.Select(r=>r);
                foreach (var tmp in query)
                {
                    results.Add(Common.ConvertEmpToEntityFormat(tmp));
                }
            }
            return results;
         }


        //速查；AsNoTracking
        public IEnumerable<Employee> GetEmployeesAsNoTracking()
        {
            List<Employee> results = new List<Employee>();
            using (dataContext = new EmployeeDataBaseEntities())
            {
                // //使用AsNoTracking()可以提高查询效率，不用在DbContext中进行缓存
                IQueryable<T_Employee> query = dataContext
                    .T_Employee
                    .AsNoTracking()
                    .Select(r => r);
                foreach (var tmp in query)
                {
                    results.Add(Common.ConvertEmpToEntityFormat(tmp));
                }
            }
            return results;
        }
    }
}
