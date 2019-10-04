using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class CurdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (DBClass obj = new DBClass("Usp_SetEmployee", CommandType.StoredProcedure))
                {
                    obj.AddParameters("ID", employee.ID);
                    obj.AddParameters("Name", employee.Name);
                    obj.AddParameters("Department", employee.Department);
                    obj.AddParameters("Gender", employee.Gender);
                    obj.AddParameters("City", employee.City);
                    obj.ExecuteNonQuery();
                }
                return RedirectToAction("List");
            }
            return View(employee);
        }
        public IActionResult List()
        {
            using (DBClass obj = new DBClass("Usp_GetEmployee", CommandType.StoredProcedure))
            {
                DataTable dt = obj.ReturnDataTable();
                List<Employee> EmployeeList = new List<Employee>();
                EmployeeList = (from DataRow dr in dt.Rows
                                select new Employee()
                                {
                                    ID = Convert.ToInt32(dr["ID"]),
                                    Name = dr["Name"].ToString(),
                                    Department = dr["Department"].ToString(),
                                    Gender = dr["Gender"].ToString(),
                                    City = dr["City"].ToString()
                                }).ToList();
                return View(EmployeeList);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (DBClass obj = new DBClass("Usp_GetEmployee", CommandType.StoredProcedure))
            {
                obj.AddParameters("ID", id.ToString());
                DataTable dt = obj.ReturnDataTable();
                Employee EmployeeList = new Employee();
                EmployeeList = (from DataRow dr in dt.Rows
                                select new Employee()
                                {
                                    ID = Convert.ToInt32(dr["ID"]),
                                    Name = dr["Name"].ToString(),
                                    Department = dr["Department"].ToString(),
                                    Gender = dr["Gender"].ToString(),
                                    City = dr["City"].ToString()
                                }).FirstOrDefault();
                if (EmployeeList == null)
                {
                    return NotFound();
                }
                return View(EmployeeList);
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (DBClass obj = new DBClass("Usp_SetEmployee", CommandType.StoredProcedure))
                {
                    obj.AddParameters("ID", employee.ID);
                    obj.AddParameters("Name", employee.Name);
                    obj.AddParameters("Department", employee.Department);
                    obj.AddParameters("Gender", employee.Gender);
                    obj.AddParameters("City", employee.City);
                    obj.ExecuteNonQuery();
                }
                return RedirectToAction("List");
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (DBClass obj = new DBClass("Usp_GetEmployee", CommandType.StoredProcedure))
            {
                obj.AddParameters("ID", id.ToString());
                DataTable dt = obj.ReturnDataTable();
                Employee EmployeeList = new Employee();
                EmployeeList = (from DataRow dr in dt.Rows
                                select new Employee()
                                {
                                    ID = Convert.ToInt32(dr["ID"]),
                                    Name = dr["Name"].ToString(),
                                    Department = dr["Department"].ToString(),
                                    Gender = dr["Gender"].ToString(),
                                    City = dr["City"].ToString()
                                }).FirstOrDefault();
                if (EmployeeList == null)
                {
                    return NotFound();
                }
                return View(EmployeeList);
            }
        }

    }
}