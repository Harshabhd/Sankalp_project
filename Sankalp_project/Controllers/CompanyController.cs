using Microsoft.AspNetCore.Mvc;
using Sankalp_project.DataAccessLayer;
using Sankalp_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sankalp_project.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DAL _dal;
        private readonly IConfiguration _config;
        public CompanyController(DAL dal,IConfiguration config)
        {
                _dal= dal;
            _config= config;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("HD") =="1") 
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login_Model obj)
        {
            var res=_dal.Login_Details.Where(x=>x.UserName==obj.UserName && x.Password == obj.Password && x.ConfirmPassword==obj.ConfirmPassword).FirstOrDefault();
            if(res!=null) 
            {

                var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
                var Payload = new[]
                {
                    new Claim("Id",obj.Id.ToString()),
                    new Claim("Name",obj.UserName)
                };
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"], claims: Payload,
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: credentials
                    );
                var Token = new JwtSecurityTokenHandler().WriteToken(token);
              
                var result = res.Id.ToString();
                HttpContext.Session.SetString ("HD", result);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }
        public IActionResult LogOut()
        {

            if (HttpContext.Session.GetString("HD") == "1")
            {
                TempData["Error Message"] = "<Script>alert('Logout Successfull')</script>";
                HttpContext.Session.Remove("HD");
                
                return RedirectToAction("Login");
            }
            else
                return RedirectToAction("Login");

        }
        public IActionResult CreateCompany()
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public IActionResult GetCompany()
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Company_Details.ToList();
                return View(res);
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }
        [HttpPost]
        public IActionResult CreateCompany(CompanyModel model)
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Company_Details.Where(x => x.CompanyName == model.CompanyName).FirstOrDefault();
                if (res != null)
                {
                    return Json(new { Error = "Company Already Exists" });

                }
                else
                {
                    _dal.Company_Details.Add(model);
                    _dal.SaveChanges();
                    return RedirectToAction("GetCompany");
                }

            }
            else
            {
                return RedirectToAction("Login");
            }
           

           
        }
        public IActionResult CreateEmployee()
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Company_Details.ToList();
                ViewBag.company = res;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Employee_Details.ToList();
                if (res != null)
                {
                    for (int i = 0; i < res.Count; i++)
                    {
                        var obj = Convert.ToInt32(res[i].CompanyName);
                        res[i].CompanyName = _dal.Company_Details.Where(x => x.Id == obj).Select(x => x.CompanyName).FirstOrDefault();


                    }

                }
                return View(res);
            }
            else
            {
                return RedirectToAction("Login");
            }
      
          
            
        }
        [HttpPost]
        public IActionResult CreateEmployee(EmployeeModel model)
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Message = "<script> alert('Select Ok if the filled Information in Correct')</Script>";
                    _dal.Employee_Details.Add(model);
                    _dal.SaveChanges();
                    return RedirectToAction("GetEmployee");
                }
                else
                    return View();
                
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }
      
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Company_Details.Where(x => x.Id == id).FirstOrDefault();
                if (res != null)
                {
                    return View(res);
                }
                else
                    return RedirectToAction("GetCompany");
            }
            else
            {
                return RedirectToAction("Login");
            }
            

        }
        [HttpPost]
        public IActionResult Update(int id ,CompanyModel obj) 
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Company_Details.Where(x => x.Id == id).FirstOrDefault();
                if (res != null)
                {
                    res.CompanyName = obj.CompanyName;
                    res.CompanyAddress = obj.CompanyAddress;
                    res.CompanyNumber = obj.CompanyNumber;
                    res.Email = obj.Email;
                    res.Password = obj.Password;
                    res.ComfirmPassword = obj.ComfirmPassword;
                    _dal.SaveChanges();
                    return RedirectToAction("GetCompany");
                }
                else { return View(); }
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        public IActionResult UpdateEmp(int id)
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Employee_Details.Where(x => x.EmployeeId == id).FirstOrDefault();
                if (res != null)
                {
                   
                        res.CompanyName = _dal.Company_Details.Where(x => x.Id.ToString() == res.CompanyName).Select(x => x.CompanyName).FirstOrDefault();
                        return View(res);
                }
                else
                    return RedirectToAction("GetEmployee");
            }
            else
            {
                return RedirectToAction("Login");
            }
            

        }
        [HttpPost]
        public IActionResult UpdateEmp(int id,EmployeeModel model)
        {
            if (HttpContext.Session.GetString("HD") == "1")
            {
                var res = _dal.Employee_Details.Where(x => x.EmployeeId == id).FirstOrDefault();
                
                if (res != null)
                {
                    res.EmployeeId = model.EmployeeId;
                    res.Name = model.Name;
                    res.CompanyName = _dal.Company_Details.Where(x => x.CompanyName == model.CompanyName).Select(_ => _.Id).FirstOrDefault().ToString();
                    _dal.SaveChanges();
                    return RedirectToAction("GetEmployee");
                }
                else
                    return RedirectToAction("GetEmployee");

            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

    }
}

