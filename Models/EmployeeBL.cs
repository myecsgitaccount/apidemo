using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDemo.Models
{
    public class EmployeeBL
    {
        public List<Employeetwo> GetEmployees()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we hardcoded the data
            List<Employeetwo> empList = new List<Employeetwo>();
            for (int i = 0; i < 10; i++)
            {
                if (i > 5)
                {
                    empList.Add(new Employeetwo()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Dept = "IT",
                        Salary = 1000 + i,
                        Gender = "Male"
                    });
                }
                else
                {
                    empList.Add(new Employeetwo()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Dept = "HR",
                        Salary = 1000 + i,
                        Gender = "Female"
                    });
                }
            }
            return empList;
        }
    }

}