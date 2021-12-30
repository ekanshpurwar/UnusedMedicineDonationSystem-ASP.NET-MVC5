using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMDS_DALlib
{
    public class DataAccessLayer
    {

        // Get All Entities
        public List<Donor> GetAllDonors()
        {
            var ctx = new UmdsDBEntities();

            List<Donor> lstDonors = new List<Donor>();
            lstDonors = ctx.Donors.ToList();
            return lstDonors;

        }
        public Donor GetDonorById(int id)
        {
            using(var ctx = new UmdsDBEntities())
            {
                var donor = ctx.Donors.Where(o => o.Id == id).SingleOrDefault();
                return donor;
            }
        }
        public List<NGO> GetAllNGOes()
        {
            var ctx = new UmdsDBEntities();

            List<NGO> lstNGOes = new List<NGO>();
            lstNGOes = ctx.NGOes.ToList();
            return lstNGOes;

        }

        public NGO GetNGOById(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var ngo = ctx.NGOes.Where(o => o.Id == id).SingleOrDefault();
                return ngo;
            }
        }

        public Medicine GetMedicineById(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var medi = ctx.Medicines.Where(o => o.Id == id).SingleOrDefault();
                return medi;
            }
        }
        public List<Medicine> GetAllMedicines()
        {
            using (var ctx = new UmdsDBEntities())
            {
                List<Medicine> lstMedicines = new List<Medicine>();
                lstMedicines = ctx.Medicines.ToList();
                return lstMedicines;
            }
        }
        public List<Request> GetAllRequests()
        {
            using (var ctx = new UmdsDBEntities())
            {
                List<Request> lstRequests = new List<Request>();
                lstRequests = ctx.Requests.ToList();
                return lstRequests;
            }
        }

        public List<FinalRequest> GetAllFinalRequests()
        {
            using (var ctx = new UmdsDBEntities())
            {
                List<FinalRequest> lstRequests = new List<FinalRequest>();
                lstRequests = ctx.FinalRequests.ToList();
                return lstRequests;
            }
        }
        public void CreateDonor(Donor d)
        {
            using (var ctx = new UmdsDBEntities())
            {
                ctx.Donors.Add(d);
                ctx.SaveChanges();
            }
        }

        // Create All Entities
        public void CreateNGO(NGO n)
        {
            using (var ctx = new UmdsDBEntities())
            {
                ctx.NGOes.Add(n);
                ctx.SaveChanges();
            }
        }

        public void CreateMedicine(Medicine m)
        {
            using(var ctx = new UmdsDBEntities())
            {
                ctx.Medicines.Add(m);
                ctx.SaveChanges();
            }
        }

        public int CreateRequest(Request r)
        {
            using (var ctx = new UmdsDBEntities())
            {
                ctx.Requests.Add(r);

                ctx.SaveChanges();

                var maxInsertedId=ctx.Requests.Max(o => o.Id);
                return maxInsertedId;
            }
        }
        public void CreateFinalRequest(FinalRequest r)
        {
            using (var ctx = new UmdsDBEntities())
            {
                ctx.FinalRequests.Add(r);
                ctx.SaveChanges();
            }
        }


        // Edit Entities

        public void EditDonor(Donor d)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.Donors.Where(o => o.Id == d.Id).SingleOrDefault();
                if (record != null)
                {
                    record.Name = d.Name;
                    record.Age = d.Age;
                    record.Aadhar = d.Aadhar;
                    record.Mobile = d.Mobile;
                    record.City = d.City;
                    record.State = d.State;
                    record.Pin = d.Pin;
                    record.Email = d.Email;
                    record.Password = d.Password;
                    ctx.SaveChanges();
                }
            }
        }

        public void EditMedicine(Medicine d)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.Medicines.Where(o => o.Id == d.Id).SingleOrDefault();
                if (record != null)
                {
                    record.Name = d.Name;
                    record.ExpiryDate = d.ExpiryDate;
                    record.PharmaCompany = d.PharmaCompany;
                    record.Quantity = d.Quantity;
                    record.Donor_Id = record.Donor_Id;

                    ctx.SaveChanges();
                }
            }
        }
        public void EditMedicineQuantityById(int id)
        {
            var ctx = new UmdsDBEntities();
            var record = ctx.Medicines.Where(o => o.Id == id).SingleOrDefault();
            
            record.Quantity--;
            ctx.SaveChanges();

        }
        public void AddMedicineQuantityById(int id)
        {
            var ctx = new UmdsDBEntities();
            var record = ctx.Medicines.Where(o => o.Id == id).SingleOrDefault();

            record.Quantity++;
            ctx.SaveChanges();
        }
        public void DeleteMedicine(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.Medicines.Where(o => o.Id == id).SingleOrDefault();
                if (record != null)
                {
                    ctx.Medicines.Remove(record);
                    ctx.SaveChanges();
                }

            }
        }
        public void DeleteDonor(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.Donors.Where(o => o.Id == id).SingleOrDefault();
                if (record != null)
                {
                    ctx.Donors.Remove(record);
                    ctx.SaveChanges();
                }
            }
        }

        public void DeleteRequest(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.Requests.Where(o => o.Id == id).SingleOrDefault();
                if (record != null)
                {
                    ctx.Requests.Remove(record);
                    ctx.SaveChanges();
                }
            }
        }

        public void DeleteFinalRequest(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.FinalRequests.Where(o => o.Id == id).SingleOrDefault();
                if (record != null)
                {
                    ctx.FinalRequests.Remove(record);
                    ctx.SaveChanges();
                }
            }
        }
        public void DeleteAllRequest(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var lstRecord = ctx.Requests.Where(o => o.NGO_Id == id).ToList();
                foreach (var record in lstRecord)
                {
                    if (record != null)
                    {
                        ctx.Requests.Remove(record);
                        ctx.SaveChanges();
                    }
                }
                
            }
        }


        public void EditNGO(NGO n)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.NGOes.Where(o => o.Id == n.Id).SingleOrDefault();
                if (record != null)
                {
                    record.Name = n.Name;
                    record.Mobile = n.Mobile;
                    record.City = n.City;
                    record.State = n.State;
                    record.Pin = n.Pin;
                    record.Email = n.Email;
                    record.Password = n.Password;
                    ctx.SaveChanges();
                }
            }
        }

        public void DeleteNGO(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.NGOes.Where(o => o.Id == id).SingleOrDefault();
                if (record != null)
                {
                    ctx.NGOes.Remove(record);
                    ctx.SaveChanges();
                }
            }
        }

        public void CreateEnquiry(Enquiry n)
        {
            using (var ctx = new UmdsDBEntities())
            {
                ctx.Enquiries.Add(n);
                ctx.SaveChanges();
            }
        }
        public List<Enquiry> GetAllEnquirys()
        {
            var ctx = new UmdsDBEntities();

            List<Enquiry> lstEnqs = new List<Enquiry>();
            lstEnqs = ctx.Enquiries.ToList();
            return lstEnqs;

        }
        public void DeleteEnquiry(int id)
        {
            using (var ctx = new UmdsDBEntities())
            {
                var record = ctx.Enquiries.Where(o => o.Id == id).SingleOrDefault();
                if (record != null)
                {
                    ctx.Enquiries.Remove(record);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
