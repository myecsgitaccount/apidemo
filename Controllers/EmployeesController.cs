using APIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIDemo.Controllers
{
    public class EmployeesController : ApiController
    {

        //public IEnumerable<Employee> Get()
        //{
        //    using (ApplicationDbContext dbContext = new ApplicationDbContext())
        //    {
        //        return dbContext.Employees.ToList();
        //    }
        //}

        //public Employee Get(int id)
        //{
        //    using (ApplicationDbContext dbContext = new ApplicationDbContext())
        //    {
        //        return dbContext.Employees.FirstOrDefault(e => e.Id == id);
        //    }
        //}

        public HttpResponseMessage Get(string gender = "All")
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            dbContext.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            dbContext.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            dbContext.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "Value for gender must be Male, Female or All. " + gender + " is invalid.");
                }
            }
        }


        [HttpGet]
        public HttpResponseMessage LoadAllEmployees()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var Employees = dbContext.Employees.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, Employees);
            }
        }

        [HttpGet]
        public HttpResponseMessage LoadEmployeeByID(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with ID " + id.ToString() + "not found");
                }
            }
        }


        [HttpGet]
        public HttpResponseMessage LoadAllMaleEmployees()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var Employees = dbContext.Employees.Where(x => x.Gender == "Male").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, Employees);
            }
        }


        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    dbContext.Employees.Add(employee);
                    dbContext.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        employee.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        //{
        //    try
        //    {
        //        using (ApplicationDbContext dbContext = new ApplicationDbContext())
        //        {
        //            var entity = dbContext.Employees.FirstOrDefault(e => e.Id == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //                    "Employee with Id " + id.ToString() + " not found to update");
        //            }
        //            else
        //            {
        //                entity.FirstName = employee.FirstName;
        //                entity.LastName = employee.LastName;
        //                entity.Gender = employee.Gender;
        //                entity.Salary = employee.Salary;
        //                dbContext.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        // update from uri

        //public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee employee)
        //{
        //    try
        //    {
        //        using (ApplicationDbContext dbContext = new ApplicationDbContext())
        //        {
        //            var entity = dbContext.Employees.FirstOrDefault(e => e.Id == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //                    "Employee with Id " + id.ToString() + " not found to update");
        //            }
        //            else
        //            {
        //                entity.FirstName = employee.FirstName;
        //                entity.LastName = employee.LastName;
        //                entity.Gender = employee.Gender;
        //                entity.Salary = employee.Salary;
        //                dbContext.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        // from body


        //public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee employee)
        //{
        //    try
        //    {
        //        using (ApplicationDbContext dbContext = new ApplicationDbContext())
        //        {
        //            var entity = dbContext.Employees.FirstOrDefault(e => e.ID == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
        //                    "Employee with Id " + id.ToString() + " not found to update");
        //            }
        //            else
        //            {
        //                entity.FirstName = employee.FirstName;
        //                entity.LastName = employee.LastName;
        //                entity.Gender = employee.Gender;
        //                entity.Salary = employee.Salary;
        //                dbContext.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var entity = dbContext.Employees.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.Employees.Remove(entity);
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
