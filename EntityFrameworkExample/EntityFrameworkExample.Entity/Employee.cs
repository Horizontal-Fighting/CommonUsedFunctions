using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkExample.Entity
{
    public class Employee
    {
        int id;
        string name;
        string address;
        decimal salary;
        string email;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public decimal Salary
        {
            get
            {
                return salary;
            }

            set
            {
                salary = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public override string ToString()
        {
            return "ID:" + this.id + "\r\n" + "Name:" + this.name + "\r\n" + "Salary:" + this.salary + "\r\n" + "Email:" + this.email;
        }
    }
}
