using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class StudentManagementController : Controller
    {
        private DB_Entities _db = new DB_Entities();

        // GET: StudentManagement
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }

            User user = Session["user"] as User;
            if (user.chucvu == "sinhvien")
            {
                return RedirectToAction("StudentDash");
            }
            if (user.chucvu == "giangvien")
            {
                return RedirectToAction("TeacherDash");
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                User check = _db.Users.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    if (_user.chucvu == "sinhvien")
                    {
                        Sinhvien _sinhvien = new Sinhvien();
                        _user.sinhvien = _sinhvien;
                    }

                    if (_user.chucvu == "giangvien")
                    {
                        Giangvien _giangvien = new Giangvien();
                        _user.giangvien = _giangvien;
                    }

                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(_user.email, "Email đã tồn tại!");
                    return View("Register", _user);
                }
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = _db.Users.Where(s => s.email.Equals(email) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["user"] = data.FirstOrDefault(); // lưu user vào session
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("email", "Email hoặc mật khẩu không hợp lệ!");
                    ModelState.AddModelError("password", "Email hoặc mật khẩu không hợp lệ!");
                    return RedirectToAction("Login");
                }
            }

            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Students()
        {
            List<Sinhvien> sinhviens = _db.Sinhviens.ToList();
            ViewData["sinhviens"] = sinhviens;

            return View();
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(HttpPostedFileBase avatar, FormCollection formData)
        {
            Sinhvien sinhvien = new Sinhvien();
            User user = new User();

            sinhvien.mssv = formData["mssv"];
            sinhvien.lop = formData["lop"];
            sinhvien.ngaysinh = formData["ngaysinh"];
            sinhvien.phone = formData["phone"];
            sinhvien.khoa = formData["khoa"];
            sinhvien.gioitinh = formData["gioitinh"];
            sinhvien.tongiao = formData["tongiao"];
            sinhvien.nganh = formData["nganh"];

            user.name = formData["name"];
            user.email = formData["email"];
            user.password = GetMD5(formData["password"]);
            user.chucvu = "sinhvien";
            user.confirmpassword = GetMD5(formData["password"]);


            if (avatar != null && avatar.ContentLength > 0)
            {
                // Lấy tên và đường dẫn của file
                var random = Guid.NewGuid();
                var _extension = Path.GetExtension(avatar.FileName);
                string newFileName = random + _extension;
                string _FileName = Path.GetFileName(newFileName);
                string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                avatar.SaveAs(_path);

                sinhvien.avatar = _FileName;
            }
            // Thêm user vào cơ sở dữ liệu
            User userInserted = _db.Users.Add(user);
            _db.SaveChanges();

            // Gán user mới được thêm vào sinhvien.user
            sinhvien.user = userInserted;

            // Thêm sinhvien vào cơ sở dữ liệu
            _db.Sinhviens.Add(sinhvien);
            _db.SaveChanges();

            return View();
        }
        public ActionResult EditStudent(string id)
        {
            int sinhvienId = Convert.ToInt32(id);
            Sinhvien sinhvien = _db.Sinhviens.Where(s => s.id == sinhvienId).FirstOrDefault();
            ViewData["sinhvien"] = sinhvien;
            return View();
        }
        [HttpPost]
        public ActionResult EditStudent(string id, HttpPostedFileBase avatar, FormCollection formData)
        {
            int sinhvienId = int.Parse(id);
            Sinhvien sinhvien = _db.Sinhviens.FirstOrDefault(s => s.id == sinhvienId);

            sinhvien.mssv = formData["mssv"];
            sinhvien.lop = formData["lop"];
            sinhvien.ngaysinh = formData["ngaysinh"];
            sinhvien.phone = formData["phone"];
            sinhvien.khoa = formData["khoa"];
            sinhvien.gioitinh = formData["gioitinh"];
            sinhvien.tongiao = formData["tongiao"];
            sinhvien.nganh = formData["nganh"];

            sinhvien.user.name = formData["name"];
            sinhvien.user.email = formData["email"];
            sinhvien.user.giangvien = new Giangvien();


            if (avatar != null && avatar.ContentLength > 0)
            {
                // Lấy tên và đường dẫn của file
                var random = Guid.NewGuid();
                var _extension = Path.GetExtension(avatar.FileName);
                string newFileName = random + _extension;
                string _FileName = Path.GetFileName(newFileName);
                string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                avatar.SaveAs(_path);

                sinhvien.avatar = _FileName;
            }
            // Thêm user vào cơ sở dữ liệu
            _db.SaveChanges();

            return RedirectToAction("Students");
        }

        public ActionResult ViewStudent()
        {
            return View();
        }
        public ActionResult DelStudent(string id)
        {
            int sinhvienId = Convert.ToInt32(id);
            Sinhvien sinhvien = _db.Sinhviens.Where(s => s.id == sinhvienId).FirstOrDefault();
            User user = sinhvien.user;
            _db.Sinhviens.Remove(sinhvien);
            _db.Users.Remove(user);
            // Thêm user vào cơ sở dữ liệu
            _db.SaveChanges();
            return RedirectToAction("Students");
        }

        public ActionResult Teachers()
        {
            List<Giangvien> giangviens = _db.Giangviens.ToList();
            ViewData["giangviens"] = giangviens;
            return View();
        }
        public ActionResult AddTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTeacher(HttpPostedFileBase avatar, FormCollection formData)
        {
            Giangvien giangvien = new Giangvien();
            User user = new User();

            giangvien.msgv = formData["msgv"];
            giangvien.lop = formData["lop"];
            giangvien.ngaysinh = formData["ngaysinh"];
            giangvien.phone = formData["phone"];
            giangvien.khoa = formData["khoa"];
            giangvien.gioitinh = formData["gioitinh"];
            giangvien.tongiao = formData["tongiao"];
            giangvien.nganh = formData["nganh"];

            user.name = formData["name"];
            user.email = formData["email"];
            user.password = GetMD5(formData["password"]);
            user.chucvu = "giangvien";
            user.confirmpassword = GetMD5(formData["password"]);


            if (avatar != null && avatar.ContentLength > 0)
            {
                // Lấy tên và đường dẫn của file
                var random = Guid.NewGuid();
                var _extension = Path.GetExtension(avatar.FileName);
                string newFileName = random + _extension;
                string _FileName = Path.GetFileName(newFileName);
                string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                avatar.SaveAs(_path);

                giangvien.avatar = _FileName;
            }
            // Thêm user vào cơ sở dữ liệu
            User userInserted = _db.Users.Add(user);
            _db.SaveChanges();

            // Gán user mới được thêm vào sinhvien.user
            giangvien.user = userInserted;

            // Thêm sinhvien vào cơ sở dữ liệu
            _db.Giangviens.Add(giangvien);
            _db.SaveChanges();

            return View();
        }
        public ActionResult ViewTeacher()
        {
            return View();
        }
        public ActionResult EditTeacher(string id)
        {
            int giangvienId = Convert.ToInt32(id);
            Giangvien giangvien = _db.Giangviens.Where(s => s.id == giangvienId).FirstOrDefault();
            ViewData["giangvien"] = giangvien;
            return View();
        }
        public ActionResult DelTeacher(string id)
        {
            int giangvienId = Convert.ToInt32(id);
            Giangvien giangvien = _db.Giangviens.Where(s => s.id == giangvienId).FirstOrDefault();
            User user = giangvien.user;
            _db.Giangviens.Remove(giangvien);
            _db.Users.Remove(user);
            // Thêm user vào cơ sở dữ liệu
            _db.SaveChanges();
            return RedirectToAction("Teachers");
        }
        /*
                [HttpPost]
                public ActionResult EditTeacher(string id, HttpPostedFileBase avatar, FormCollection formData)
                {
                    int giangvienId = int.Parse(id);
                    Giangvien giangvien = _db.Giangviens.FirstOrDefault(s => s.id == giangvienId);

                    giangvien.msgv = formData["msgv"];
                    giangvien.lop = formData["lop"];
                    giangvien.ngaysinh = formData["ngaysinh"];
                    giangvien.phone = formData["phone"];
                    giangvien.khoa = formData["khoa"];
                    giangvien.gioitinh = formData["gioitinh"];
                    giangvien.tongiao = formData["tongiao"];
                    giangvien.nganh = formData["nganh"];

                    giangvien.user.name = formData["name"];
                    giangvien.user.email = formData["email"];
                    giangvien.user.sinhvien = new Sinhvien();

                    if (avatar != null && avatar.ContentLength > 0)
                    {
                        // Lấy tên và đường dẫn của file
                        var random = Guid.NewGuid();
                        var _extension = Path.GetExtension(avatar.FileName);
                        string newFileName = random + _extension;
                        string _FileName = Path.GetFileName(newFileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                        avatar.SaveAs(_path);

                        giangvien.avatar = _FileName;
                    }
                    // Thêm user vào cơ sở dữ liệu
                    _db.SaveChanges();

                    return RedirectToAction("Teachers");
                }*/

        public ActionResult Class()
        {
            return View();
        }
        public ActionResult TeacherDash()
        {
            return View();
        }
        public ActionResult StudentDash()
        {
            return View();
        }
    }
}