using EntityFrameworkExample.DAL;
using EntityFrameworkExample.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EntityFrameworkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeDAL employeeDAL = new EmployeeDAL();
            Stopwatch sw;

            //Insert
            for (int i = 0; i < 1000; i++)
            {
                Employee empTmp1 = new Employee();
                empTmp1.Id = 1;
                empTmp1.Name = i.ToString();
                empTmp1.Address = i.ToString();
                empTmp1.Salary = i;
                empTmp1.Email = i.ToString();
                employeeDAL.AddEmployee(empTmp1);
            }


            //查
            sw = new Stopwatch();
            sw.Start();
            List<Employee> employees1 = new List<Employee>();
            employees1 = employeeDAL.GetEmployees().ToList();
            foreach (var tmpEmp in employees1)
            {
                Console.WriteLine(tmpEmp.ToString());
                Console.WriteLine("--------------------");
            }
            sw.Stop();
            Console.WriteLine("普通查询耗时(毫秒):" + sw.ElapsedMilliseconds);
            Console.WriteLine("-------------------------");


            //速查，使用AsNoTracking()可以提高查询效率，不用在DbContext中进行缓存
            sw = new Stopwatch();
            sw.Start();
            List<Employee> employees2 = new List<Employee>();
            employees2 = employeeDAL.GetEmployeesAsNoTracking().ToList();
            foreach (var tmpEmp in employees2)
            {
                Console.WriteLine(tmpEmp.ToString());
                Console.WriteLine("***************");
            }
            sw.Stop();
            Console.WriteLine("AsNoTracking快查耗时(毫秒):" + sw.ElapsedMilliseconds);


            Employee empTmp = new Employee();
            empTmp.Id = 1;
            empTmp.Name = "Alexandra";
            empTmp.Address = "333w hempstead ave";
            empTmp.Salary = 7999m;
            empTmp.Email = "fdsaf@gmail.com";

            //改1
            sw = new Stopwatch();
            sw.Start();
            employeeDAL.Edit1Employee(empTmp);
            sw.Stop();
            Console.WriteLine("EDIT1(毫秒):" + sw.ElapsedMilliseconds);

            //改2
            sw = new Stopwatch();
            sw.Start();
            employeeDAL.Edit2Employee(empTmp);
            sw.Stop();
            Console.WriteLine("EDIT2(毫秒):" + sw.ElapsedMilliseconds);

            //删
            sw = new Stopwatch();
            sw.Start();
            employeeDAL.DeleteEmployee(1);
            sw.Stop();
            Console.WriteLine("delete(毫秒):" + sw.ElapsedMilliseconds);

            //执行sql语句
            sw = new Stopwatch();
            sw.Start();
            string sql = "update T_Employee set Salary=Salary+1000 where Salary<@Salary";
            SqlParameter[] param = new SqlParameter[] {
                             new SqlParameter("@Salary",8000)
                      };
            employeeDAL.ExecuteSQLCommand(sql, param);
            sw.Stop();
            Console.WriteLine("执行SQL语句(毫秒):" + sw.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}
