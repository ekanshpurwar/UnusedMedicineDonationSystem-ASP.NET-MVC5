using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMDS_DALlib; // accessing DAL layer

namespace UMDS.Models
{
    public class BusinessLayer
    {
        public List<Donor> GetAllDonors()
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<Donor> lstDonors = new List<Donor>();
            lstDonors = dal.GetAllDonors();
            return lstDonors;
        }
        public Donor GetDonorById(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            var donor = dal.GetDonorById(id);
            return donor;
        }
        public List<NGO> GetAllNGOes()
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<NGO> lstNGOes = new List<NGO>();
            lstNGOes = dal.GetAllNGOes();
            return lstNGOes;
        }

        public NGO GetNGOById(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            var ngo = dal.GetNGOById(id);
            return ngo;
        }
        public Medicine GetMedicineById(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            var medi = dal.GetMedicineById(id);
            return medi;
        }
        public List<Medicine> GetAllMedicines()
        {

            DataAccessLayer dal = new DataAccessLayer();

            List<Medicine> lstMedicines = new List<Medicine>();
            lstMedicines = dal.GetAllMedicines();
            return lstMedicines;

        }
        public List<Request> GetAllRequests()
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<Request> lstRequests = new List<Request>();
            lstRequests = dal.GetAllRequests();
            return lstRequests;
        }
        public List<FinalRequest> GetAllFinalRequests()
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<FinalRequest> lstRequests = new List<FinalRequest>();
            lstRequests = dal.GetAllFinalRequests();
            return lstRequests;
        }


        public void CreateDonor(Donor d)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.CreateDonor(d);
        }

        public void CreateNGO(NGO n)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.CreateNGO(n);
        }

        public void CreateMedicine(Medicine m)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.CreateMedicine(m);
        }
        public int CreateRequest(Request r)
        {
            DataAccessLayer dal = new DataAccessLayer();
            return dal.CreateRequest(r);
            
        }
        public void CreateFinalRequest(FinalRequest r)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.CreateFinalRequest(r);
        }
        public void EditDonor(Donor d)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.EditDonor(d);
        }
        public void DeleteDonor(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.DeleteDonor(id);
        }

        public void EditNGO(NGO n)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.EditNGO(n);
        }
        public void DeleteNGO(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.DeleteNGO(id);
        }

        public void EditMedicine(Medicine d)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.EditMedicine(d);
        }
        public void DeleteMedicine(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.DeleteMedicine(id);
        }
        public void DeleteRequest(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.DeleteRequest(id);
        }
        public void DeleteFinalRequest(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.DeleteFinalRequest(id);
        }
        public void DeleteAllRequest(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.DeleteAllRequest(id);
        }
        public void EditMedicineQuantityById(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.EditMedicineQuantityById(id);
        }

        public void AddMedicineQuantityById(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.AddMedicineQuantityById(id);
        }

        public void CreateEnquiry(Enquiry n)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.CreateEnquiry(n);
        }
        public List<Enquiry> GetAllEnquirys()
        {
            DataAccessLayer dal = new DataAccessLayer();
            List<Enquiry> lstEnqs = new List<Enquiry>();
            lstEnqs = dal.GetAllEnquirys();
            return lstEnqs;
        }
        public void DeleteEnquiry(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.DeleteEnquiry(id);
        }
    }
}
