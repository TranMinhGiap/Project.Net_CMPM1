using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang.Model
{
    public class Employee
    {
        private int employeeid;
        private string employeeuser;
        private string employeepass;
        private string employeename;
        private string gender;
        private string role;
        private string phone;
        private DateTime birthday;
        private string employeeaddress;
        private string employeeemail;
        private string employeeimage;

        public Employee(int employeeid, string employeeuser, string employeepass, string employeename, string gender, string role, string phone, DateTime birthday, string employeeaddress, string employeeemail, string employeeimage)
        {
            this.employeeid = employeeid;
            this.employeeuser = employeeuser;
            this.employeepass = employeepass;
            this.employeename = employeename;
            this.gender = gender;
            this.role = role;
            this.phone = phone;
            this.birthday = birthday;
            this.employeeaddress = employeeaddress;
            this.employeeemail = employeeemail;
            this.employeeimage = employeeimage;
        }
        public Employee() { }
        public int Employeeid { get { return employeeid; } set { employeeid = value; } }
        public string Employeeuser { get { return employeeuser; } set { this.employeeuser = value; } }
        public string Employeepass { get { return employeepass; } set { this.employeepass= value; } }
        public string EmployeeName { get { return employeename; } set { this.employeename = value; } }
        public string Gender { get { return gender; } set { gender = value; } }
        public string Role { get { return role; } set { role = value; } }
        public string EmployeePhone { get { return phone; } set { phone = value; } }
        public DateTime Birthday { get { return birthday; } set { birthday = value; } }
        public string EmployeeAddress { get { return employeeaddress; } set { employeeaddress = value; } }
        public string EmployeeEmail { get { return employeeemail; } set { employeeemail = value; } }
        public string Employeeimage { get { return employeeimage; } set { this.employeeimage= value; } }

    }
}
