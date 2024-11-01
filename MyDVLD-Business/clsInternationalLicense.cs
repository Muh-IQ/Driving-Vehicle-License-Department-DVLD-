using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsInternationalLicense:clsApplication
    {
        private enum _enMode { Update, AddNew }
        private _enMode _Mode;
        public int InternationalLicenseID { get; set; }
        
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
 

        public clsInternationalLicense() 
        {
            IssueDate = DateTime.Now;
            IsActive = true;
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            this.ApplicationStatus = clsApplication._enApplicationStatus.Complated;
            _Mode = _enMode.AddNew;
        }

        private clsInternationalLicense(int applicationID, int applicantPersonID, DateTime applicationDate,
            int applicationTypeID, _enApplicationStatus applicationStatus, DateTime lastStatusDate,
            decimal paidFees, int createdByUserID,
            int internationalLicenseID,
            int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, 
            DateTime expirationDate, bool isActive)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            InternationalLicenseID = internationalLicenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            DriverInfo = clsDriver.FindByDriverID(DriverID);
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;
            _Mode = _enMode.Update;
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID=-1, DriverID=-1, IssuedUsingLocalLicenseID=-1, CreatedByUserID=-1;
            bool IsActive = false;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool Result = clsInternationalLicenseData.GetInfoInternationalLicense(InternationalLicenseID,
                ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate,
                ref ExpirationDate, ref IsActive, ref CreatedByUserID);
            if (Result)
            {
                clsApplication application = clsApplication.FindBaseApplication(ApplicationID);
                if (application == null)
                {
                    return null;
                }
                return new clsInternationalLicense(application.ApplicationID,application.ApplicantPersonID
                    ,application.ApplicationDate,application.ApplicationTypeID,
                    application.ApplicationStatus,application.LastStatusDate,application.PaidFees
                    ,application.CreatedByUserID  ,InternationalLicenseID, DriverID,
                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive);
            }
            return null;
        }
        private bool _AddNewInternationl()
        {
            return (this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IsActive, this.CreatedByUserID)) > -1;
        }
        private bool _UpdateInternationl()
        {
           return clsInternationalLicenseData.UpdateInternationalLicense(InternationalLicenseID,
                this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive,
                this.CreatedByUserID);
        }
        public bool Save()
        {
            base.Mode =(clsApplication.enMode)_Mode;
            if(!base.Save()) 
            {
                return false;
            }

            switch (_Mode)
            {
                case _enMode.Update:
                    return _UpdateInternationl();
                case _enMode.AddNew:
                    if (_AddNewInternationl())
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    return false;
            }
            return false;
        }



        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicenseData.GetAllInternationalLicense();
        }






        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        public static DataTable GetDriverInernationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInernationalLicenses(DriverID);
        }
    }
}
