using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Task2.Models;
using System.Configuration;
using System.Data;

namespace Task2.Controllers
{
    public class AccountsController : Controller
    {
        string conn;

        public AccountsController()
        {

            var dbconfig = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();
            conn = dbconfig["ConnectionStrings:constr"];
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_login1", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailId", obj.EmailID);
                        cmd.Parameters.AddWithValue("@Password", obj.Password);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            HttpContext.Session.SetString("Uname", dr["Name"].ToString());
                            HttpContext.Session.SetString("LoginTime", System.DateTime.Now.ToShortTimeString());
                            return RedirectToAction("Home", "Accounts");
                        }


                        else
                        {
                            ViewBag.error = "EmailID or Password is not correct";
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "something went wrong");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Regrister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Regrister(RegristerModel obj)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_insert1", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                        cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                        cmd.Parameters.AddWithValue("@Password", obj.Password);
                        cmd.Parameters.AddWithValue("@Dob", obj.Dob);
                        cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                        cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
                        cmd.Parameters.AddWithValue("@Dept", obj.Dept);
                        cmd.Parameters.AddWithValue("@Role", obj.Role);
                        cmd.Parameters.AddWithValue("@Fee", obj.Fee);
                        cmd.Parameters.AddWithValue("@Status", obj.Status);
                        cmd.Parameters.AddWithValue("@Qualification", obj.Qualification);
                        int x = cmd.ExecuteNonQuery();
                        if (x > 0)
                        {
                            return RedirectToAction("Login", "Accounts");
                        }
                        else
                        {
                            ModelState.AddModelError("", "something went wrong");
                            return View();
                        }
                    }


                }
                else
                {

                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            finally
            {

            }


            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Displaydata()
        {
            List<DisplayModel> obj = getalldata();
            return View(obj);


        }

        public List<DisplayModel> getalldata()
        {
            List<DisplayModel> display = new List<DisplayModel>();
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_getalldata1", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    display.Add(
                    new DisplayModel
                    {
                       ID = Convert.ToInt32(dr["ID"].ToString()), 
                       FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        EmailID = dr["EmailID"].ToString(),
                        Password = dr["Password"].ToString(),
                        Dob =Convert.ToDateTime(dr["Dob"].ToString()),
                        Gender = dr["Gender"].ToString(),
                        ContactNumber = dr["ContactNumber"].ToString(),
                        Dept = dr["Dept"].ToString(),
                        Role = dr["Role"].ToString(),
                        Fee = Convert.ToDecimal(dr["Fee"].ToString()),
                       Status = dr["Status"].ToString(),
                       Qualification = dr["Qualification"].ToString()

                    }
                              );



                }
            }
            return display;
        }


    }
}
