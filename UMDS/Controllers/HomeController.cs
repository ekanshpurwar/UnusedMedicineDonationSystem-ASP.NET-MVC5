using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMDS_DALlib; // for Dal lib
using UMDS.Models; // for business layer
using System.Web.Security; // for authentication

namespace UMDS.Controllers
{
    
    public class HomeController : Controller
    {
        [HandleError]
        public ActionResult Index()
        {
                      
            return View();
        }
        public ActionResult Login()
        {
            //int x = 100;
            //int y = 0;
            //int res = x / y;

            var userType = Request.QueryString.Get("userType");
            ViewData.Add("userType", userType);
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password,string userType,string ReturnUrl)
        {
            if (userType == "Donor")
            {
                BusinessLayer bll = new BusinessLayer();
                var donorId = bll.GetAllDonors().Where(o => o.Email == Email && o.Password == Password).Select(o => o.Id).SingleOrDefault();
                var donorName = bll.GetAllDonors().Where(o => o.Id == donorId).Select(o => o.Name).SingleOrDefault();
                if (donorId != 0)
                {
                    Session.Add("donorId", donorId);
                    Session.Add("donorName", donorName);
                    FormsAuthentication.SetAuthCookie(Email, false);
                    string[] urlParams = ReturnUrl.Split('/');

                    string controller = urlParams[1];
                    string action = urlParams[2].Split('?')[0];
                     
                    return RedirectToAction(action, controller);
                  
                }
                else
                {
                    ViewData.Add("userType", userType);

                    ViewData.Add("status", "Invalid Emailid or Password");
                    return View();
                }
            }
            else if(userType == "Admin")
            {
                if (Email == "admin@gmail.com" && Password == "123")
                {
                    FormsAuthentication.SetAuthCookie(Email, false);
                    string[] urlParams = ReturnUrl.Split('/');

                    string controller = urlParams[1];
                    string action = urlParams[2].Split('?')[0];

                    return RedirectToAction(action, controller);

                }
                else
                {
                    ViewData.Add("userType", userType);

                    ViewData.Add("status", "Invalid Emailid or Password");
                    return View();
                }
                    
            }
            else
            {
                BusinessLayer bll = new BusinessLayer();
                var ngoId = bll.GetAllNGOes().Where(o => o.Email == Email && o.Password == Password).Select(o => o.Id).SingleOrDefault();
                if (ngoId != 0)
                {
                    Session.Add("ngoId", ngoId);
                    FormsAuthentication.SetAuthCookie(Email, false);
                    string[] urlParams = ReturnUrl.Split('/');

                    string controller = urlParams[1];
                    string action = urlParams[2].Split('?')[0];

                    return RedirectToAction(action, controller);
                    
                }
                else
                {
                    ViewData.Add("userType", userType);

                    ViewData.Add("status", "Invalid Emailid or Password");
                    return View();
                }
               
            }
                        
        }
      
    
        [Authorize]

        public ActionResult DonorDashboard()
        {
            FormsAuthentication.SignOut();
            return View();
        }
        public ActionResult DonorLogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult DonateMedicines()
        {
            // assigning pre values to model and passing it to view
            Medicine m = new Medicine();
            //assigning value to session
            m.Donor_Id = (int)Session["donorId"];
            m.ExpiryDate = null;
            m.DonationDate = DateTime.Now.ToShortDateString();
            return View(m);
        }

        [HttpPost]
        public ActionResult DonateMedicines(Medicine m)
        {
            BusinessLayer bll = new BusinessLayer();
            if(m.Quantity==0)
            {
                TempData.Add("errQuantity","Quantity cannot be 0");
                return RedirectToAction("DonateMedicines");
            }
            else
            {
                bll.CreateMedicine(m);
                return RedirectToAction("DonationApproval", m);
            }
            
        }

        public ActionResult DonationApproval(Medicine m)
        {
            return View(m);
        }
        
        public ActionResult MyDonations()
        {
            var donorId = Convert.ToInt32(Session["donorId"]);
            BusinessLayer bll = new BusinessLayer();
            var lstMedicines=bll.GetAllMedicines().Where(o=>o.Donor_Id==donorId).ToList();
            return View(lstMedicines);
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult GetAllNGOes()
        {
            BusinessLayer bll = new BusinessLayer();
            var lstNGOes = bll.GetAllNGOes().Select(o=>new { o.Name,o.Mobile,o.City,o.State,o.Pin,o.Email}).ToList();
            return Json(lstNGOes, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult DonorSignUp()
        {

            return View();
        }
        
        [HttpPost]
        public ActionResult DonorSignUp(Donor d)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.CreateDonor(d);
            return RedirectToAction("DonorDashboard", new { userType = "Donor" });
        }
       
        
        [Authorize]
        public ActionResult ngoDashboard()
        {
            FormsAuthentication.SignOut();
            return View();
        }
        public ActionResult NGOLogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult RequestMedicine()
        {
            BusinessLayer bll = new BusinessLayer();
            var lstMedicines = bll.GetAllMedicines();
            return View(lstMedicines);
        }

        public ActionResult AddToCart(int id)
        {
            BusinessLayer bll = new BusinessLayer();

            var medicineName = bll.GetAllMedicines().Where(o => o.Id == id).Select(o=>o.Name).SingleOrDefault();
            Request r = new Request();
            r.Medicine_Id = id;
            r.MedicineName = medicineName.ToString();
            r.Quantity = 1;
            r.NGO_Id = (int)Session["ngoId"];
            var maxInsertedId=bll.CreateRequest(r);

            
            FinalRequest f = new FinalRequest();
            f.Id = maxInsertedId;
            f.Medicine_Id = id;
            f.MedicineName = medicineName.ToString();
            f.Quantity = 1;
            f.NGO_Id = (int)Session["ngoId"];
            bll.CreateFinalRequest(f);


            var qty=bll.GetAllMedicines().Where(o => o.Id == id).Select(o => o.Quantity).SingleOrDefault();
            if(qty==0)
            {
                //var medId= bll.GetAllMedicines().Where(o => o.Id == id).Select(o => o.Id).SingleOrDefault();
                //bll.DeleteMedicine(medId);
                Session.Add("errMsg","Out of Stock");
                return RedirectToAction("RequestMedicine");
            }
            else
            {
                bll.EditMedicineQuantityById(id);
                return RedirectToAction("RequestMedicine");
            }

        } 
        public ActionResult Checkout()
        {
            BusinessLayer bll = new BusinessLayer();
            // cart is list of request
            var cart = bll.GetAllRequests().Where(o => o.NGO_Id == (int)Session["ngoId"]).ToList();
            if(cart.Count==0)
            {
                ViewData.Add("errMsg", "Your Bag is Empty");
            }
            return View(cart);
        }

        public ActionResult NGOSubmitRequest()
        {
            var ngoId = (int)Session["ngoId"];
            BusinessLayer bll = new BusinessLayer();
            // remove that nogoid data from request table
            var lstRequests = bll.GetAllRequests();
            if(lstRequests.Count!=0)
            {
                bll.DeleteAllRequest(ngoId);

                return View();

            }
            else
            {
                return RedirectToAction("Checkout");
            }
        }
        public ActionResult DeleteFromCart(int id)
        {
            BusinessLayer bll = new BusinessLayer();
            

            //update quantity in medicine table using medicine id from request table
            var medicineId=bll.GetAllRequests().Where(o => o.Id == id).Select(o => o.Medicine_Id).SingleOrDefault();
            bll.AddMedicineQuantityById(medicineId);

            bll.DeleteRequest(id);
            bll.DeleteFinalRequest(id);
            return RedirectToAction("Checkout");
        }

        [HttpGet]
        public ActionResult NGOSignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NGOSignUp(NGO n)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.CreateNGO(n);
            return RedirectToAction("ngoDashboard", new { userType = "NGO" });
        }
        public ActionResult Admin()
        {

            return View();
        }

        [Authorize]
        public ActionResult AdminDashboard()
        {
            FormsAuthentication.SignOut();

            return View();
        }
        
        public ActionResult AdminLogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
       
        public ActionResult ManageDonors()
        {
            BusinessLayer bll = new BusinessLayer();
            var lstDonors = bll.GetAllDonors();
            return View(lstDonors);
        }
        
        public ActionResult ManageNGOes()
        {
            BusinessLayer bll = new BusinessLayer();
            var lstNGOes = bll.GetAllNGOes();
            return View(lstNGOes);
        }
        public ActionResult ManageRequest()
        {
            BusinessLayer bll = new BusinessLayer();
            var lstRequest = bll.GetAllFinalRequests();
            return View(lstRequest);
        }
        
        public ActionResult DeleteFinalRequest(int id)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.DeleteFinalRequest(id);
            return RedirectToAction("ManageRequest");
        }


        public ActionResult ManageMedicine()
        {
            BusinessLayer bll = new BusinessLayer();
            var lstMedicines = bll.GetAllMedicines();
            return View(lstMedicines);
        }
        public ActionResult EditDonor(int id)
        {
            BusinessLayer bll = new BusinessLayer();
            var donor=bll.GetDonorById(id);
            Donor d = new Donor();
            d.Id = id;
            d.Name = donor.Name;
            d.Age = donor.Age;
            d.Aadhar = donor.Aadhar;
            d.Mobile = donor.Mobile;
            d.City = donor.City;
            d.State = donor.State;
            d.Pin = donor.Pin;
            d.Email = donor.Email;
            d.Password = donor.Password;
            return View(d);
        }
        [HttpPost]
        public ActionResult EditDonor(Donor d)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.EditDonor(d);
            return RedirectToAction("ManageDonors");
        }

        public ActionResult DeleteDonor(int id)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.DeleteDonor(id);
            return RedirectToAction("ManageDonors");
        }

        public ActionResult EditNGO(int id)
        {
            NGO n = new NGO();
            BusinessLayer bll = new BusinessLayer();
            var ngo = bll.GetNGOById(id);
            n.Id = id;
            n.Name = ngo.Name;
            n.Mobile = ngo.Mobile;
            n.City = ngo.City;
            n.State = ngo.State;
            n.Pin = ngo.Pin;
            n.Email = ngo.Email;
            n.Password = ngo.Password;
            return View(n);
            
        }
        [HttpPost]
        public ActionResult EditNGO(NGO n)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.EditNGO(n);
            return RedirectToAction("ManageNGOes");
        }

        public ActionResult DeleteNGO(int id)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.DeleteNGO(id);
            return RedirectToAction("ManageNGOes");
        }

        public ActionResult EditMedicine(int id)
        {
            Medicine m = new Medicine();
            BusinessLayer bll = new BusinessLayer();
            var medi = bll.GetMedicineById(id);
            m.Id = id;
            m.Name = medi.Name;
            m.ExpiryDate = medi.ExpiryDate;
            m.PharmaCompany = medi.PharmaCompany;
            m.Quantity = medi.Quantity;
            m.Donor_Id = medi.Donor_Id;

            return View(m);
           
        }
        [HttpPost]
        public ActionResult EditMedicine(Medicine m)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.EditMedicine(m);
            return RedirectToAction("ManageMedicine");
        }
        public ActionResult DeleteMedicine(int id)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.DeleteMedicine(id);
            return RedirectToAction("ManageMedicine");
        }

       

      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Enquiry e)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.CreateEnquiry(e);
            return RedirectToAction("Contact");
            
        }

        public ActionResult ManageEnquiries()
        {
            BusinessLayer bll = new BusinessLayer();
            var lstEnq = bll.GetAllEnquirys();
            return View(lstEnq);
        }
        public ActionResult DeleteEnquiry(int id)
        {
            BusinessLayer bll = new BusinessLayer();
            bll.DeleteEnquiry(id);
            return RedirectToAction("ManageEnquiries");
        }
    }
}