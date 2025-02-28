using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DemoProject.Models;

namespace DemoProject.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login([Bind(Include ="Email,Password")] User user)
        {
            using (var context=new UserContext())
            {
                bool IsValidEmployee = context.Users.Any(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password));
                if (IsValidEmployee)
                {
              
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("GetAllEmployees");
 

                }
                ModelState.AddModelError("", "invalid Username or Password");
              
            }
            return View();
        }
        [AllowAnonymous]
        [Route("signup")]
        public ActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                using (var context = new UserContext()) {
              
                    context.Users.Add(user);
                    context.SaveChanges();
                }
              
            }

            return RedirectToAction("Login");
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // Create Employee - GET
        [HttpGet]
        [Route("create")]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        // Create Employee - POST
        [HttpPost]
        [Route("create")]
        public ActionResult CreateEmployee(Employee employee)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    using (var context = new EmployeeContext())
                    {
                        context.Employees.Add(employee);
                        context.SaveChanges();
                    }

                }
            }

            return RedirectToAction("GetAllEmployees");
        }

        // Get All Employees
        [HttpGet]
        [Route("employees")]
        [AllowAnonymous]
        public ActionResult GetAllEmployees()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<Employee> employees;
                using (var context = new EmployeeContext())
                {
                    employees = context.Employees.ToList();
                }
                ViewBag.Employees = employees;

            }
            else
            {
                return RedirectToAction("Login");
            }
                return View();
        }


        // Delete Employee
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var context = new EmployeeContext())
                {
                    var existingEmployee = context.Employees.Find(id);
                    if (existingEmployee != null)
                    {
                        context.Employees.Remove(existingEmployee);
                        context.SaveChanges();
                    }
                }
            }
           

            return RedirectToAction("GetAllEmployees");
        }

        // Edit Employee - GET
        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            Employee emp;
            using (var context = new EmployeeContext())
            {
                emp = context.Employees.Find(id);
            }

            if (emp == null)
            {
                return HttpNotFound();
            }

            return View(emp);
        }

        // Edit Employee - POST
        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EmployeeContext())
                {
                    var existingEmployee = context.Employees.Find(id);
                    if (existingEmployee != null)
                    {
                        existingEmployee.Name = employee.Name;
                        existingEmployee.Department = employee.Department;
                        existingEmployee.Salary = employee.Salary;
                        context.SaveChanges();
                    }
                }
            }



            return RedirectToAction("GetAllEmployees");
        }
    }
}
