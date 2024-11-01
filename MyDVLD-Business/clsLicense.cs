using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsLicense
    {
        private enum _enMode { Update, AddNew };
        private _enMode _Mode;


        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementforDamaged = 3, Replacementforlost = 4 }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo;
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        //save datatime from data access
        public DateTime IssueDate { get;  set; }
        public DateTime ExpirationDate { get;  set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }
        public string GetIssueReasonText
        {
            get
            {
                return GetIssueReason(this.IssueReason);
            }
        }
        public bool IsDetained {
            get
            {
                return clsDetian.IsLicenseDetained(this.LicenseID);///Implementation Detaind
            }
        }
        public clsDetian DetianInfo { get; set; }

        private clsLicense(int licenseID, int applicationID, int driverID,
            int licenseClassID, DateTime issueDate, DateTime expirationDate,
            string notes, decimal paidFees, bool isActive, enIssueReason issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClassID = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            _Mode = _enMode.Update;
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.ApplicationInfo = clsApplication.FindBaseApplication(applicationID);
            this.LicenseClassInfo = clsLicenseClass.Find(licenseClassID);
            this.DetianInfo = clsDetian.FindByLicenseID(licenseID);
        }

        public clsLicense() 
        {
            _Mode = _enMode.AddNew;
            this.IssueReason = enIssueReason.FirstTime;
        }
        private bool _AddNew()
        {
            return (this.LicenseID = clsLicenseData.AddNewLicense(ApplicationID, DriverID, LicenseClassID, Notes, PaidFees, IsActive, (int)IssueReason, CreatedByUserID)) > -1;
        }
        private bool _Update()
        {
            return clsLicenseData.UpdateLicense(LicenseID,ApplicationID, DriverID, LicenseClassID,IssueDate,ExpirationDate, Notes, PaidFees, IsActive, (int)enIssueReason.FirstTime, CreatedByUserID);
        }
        public bool Save()
        {
            switch (_Mode) 
            {
                case _enMode.AddNew:
                    if (_AddNew()) 
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    return false;
                case _enMode.Update:
                    return _Update();
            }
            return false;
        }


        public static clsLicense Find(int LicenseID)
        {
            int applicationID = -1, driverID = -1, licenseClassID = -1, issueReason = -1, 
                createdByUserID = -1;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;
            string notes = ""; decimal paidFees = 1; bool IsActive=false;
            bool Result = clsLicenseData.GetInfoLicenseByID(LicenseID, ref applicationID, ref driverID,
                ref licenseClassID, ref issueDate, ref expirationDate, ref notes,
                ref paidFees, ref IsActive, ref issueReason, ref createdByUserID);
            if (Result) 
            {
                return new clsLicense(LicenseID, applicationID, driverID,
                 licenseClassID, issueDate, expirationDate, notes,
                paidFees, IsActive, (enIssueReason)issueReason, createdByUserID);     
            }
            return null;
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return  clsLicenseData.GetDriverLicenses(DriverID);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }
        public static bool IsExistActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) > -1 ;
        }

        private static string GetIssueReason(enIssueReason issueReason)
        {
            switch (issueReason) 
            {
                case enIssueReason.FirstTime:
                    return "FirstTime";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.ReplacementforDamaged:
                    return "ReplacementforDamaged";
                case enIssueReason.Replacementforlost:
                    return "Replacementforlost";
                default:
                    return "None";
            }
        }
        public Boolean IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }
        public Boolean DeactivetCurrentLicense()
        {
            return clsLicenseData.DeactiveLicense(LicenseID);
        }

        public clsLicense RenewLicense(string Note,int UserID)
        {
            clsApplication application = new clsApplication();
            {               
                application.ApplicantPersonID = ApplicationInfo.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now;
                application.LastStatusDate = DateTime.Now;
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
                application.ApplicationStatus = clsApplication._enApplicationStatus.Complated;
                application.PaidFees = clsApplication.FindBaseApplication((int)clsApplication.enApplicationType.RenewDrivingLicense).PaidFees;
                application.CreatedByUserID = UserID;
                if (!application.Save())
                {
                    return null;
                }
            }
            clsLicense NewLicense = new clsLicense();
            {
               
                NewLicense.ApplicationID = application.ApplicationID;
                NewLicense.DriverID=this.DriverID;
                NewLicense.LicenseClassID= this.LicenseClassID;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
                NewLicense.Notes = Note;
                NewLicense.PaidFees= this.PaidFees;
                NewLicense.IsActive = true;
                NewLicense.IssueReason = enIssueReason.Renew;
                NewLicense.CreatedByUserID= UserID;
                if (!NewLicense.Save())
                    return null;
            }
            DeactivetCurrentLicense();
            return NewLicense;
        }
        public clsLicense ReplaceLicense(enIssueReason issueReason, int UserID)
        {
            clsApplication application = new clsApplication();
            {
                application.ApplicantPersonID = ApplicationInfo.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now;
                application.LastStatusDate = DateTime.Now;
                application.ApplicationTypeID = (int)((issueReason == enIssueReason.Replacementforlost)?
                    clsApplication.enApplicationType.ReplaceLostDrivingLicense:
                    clsApplication.enApplicationType.ReplaceDamagedDrivingLicense);
                application.ApplicationStatus = clsApplication._enApplicationStatus.Complated;
                application.PaidFees = clsApplication.FindBaseApplication(application.ApplicationTypeID).PaidFees;
                application.CreatedByUserID = UserID;
                if (!application.Save())
                {
                    return null;
                }
            }
            clsLicense NewLicense = new clsLicense();
            {

                NewLicense.ApplicationID = application.ApplicationID;
                NewLicense.DriverID = this.DriverID;
                NewLicense.LicenseClassID = this.LicenseClassID;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
                NewLicense.Notes = this.Notes;
                NewLicense.PaidFees = this.PaidFees;
                NewLicense.IsActive = true;
                NewLicense.IssueReason = issueReason;
                NewLicense.CreatedByUserID = UserID;
                if (!NewLicense.Save())
                    return null;
            }
            DeactivetCurrentLicense();
            return NewLicense;
        }

        public int? Detain(decimal FineFees, int CreatedByUserID)
        {
            clsDetian  detian = new clsDetian();
            detian.LicenseID = this.LicenseID;
            detian.FineFees = FineFees;
            detian.CreatedByUserID= CreatedByUserID;
            if (!detian.Save())
            {
                return null;
            }

            return detian.DetainID;
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int? ApplicationID)
        {
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            application.ApplicationStatus = clsApplication._enApplicationStatus.Complated;
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            application.CreatedByUserID = ReleasedByUserID;
            if (!application.Save())
            {
                ApplicationID = null;
                return false;
            }
            ApplicationID= application.ApplicationID;

            return DetianInfo.ReleaseDetainedLicense(ReleasedByUserID, application.ApplicationID);

        }
    }
}
