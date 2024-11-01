using MyDVLD_Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsApplication
    {
        public enum enMode { Update, AddNew }
        public enMode Mode;
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum _enApplicationStatus { New = 1 , Cancelled = 2 , Complated = 3}
        public int ApplicationID {  get; set; }
        public int ApplicantPersonID { get; set; }
        public string ApplicantName
        {
            get 
            {
                return clsPerson.FindByPersonID(ApplicantPersonID).FullName;
            }
        }
        public clsPerson PersonInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationTypeInfo { get; private set; }
        public _enApplicationStatus ApplicationStatus { get; set; }
        public string ApplicationStatusText
        {
            get
            {
                switch(ApplicationStatus) 
                {
                    case _enApplicationStatus.New:
                        return "New";
                    case _enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case _enApplicationStatus.Complated:
                        return "Complated";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo { get; set; }
        public clsApplication()
        {
            Mode = enMode.AddNew;
            ApplicationTypeID = 1;
            ApplicationDate= DateTime.Now;
            LastStatusDate= DateTime.Now;

        }

        public clsApplication( int applicationID, int applicantPersonID, DateTime applicationDate,
            int applicationTypeID, _enApplicationStatus applicationStatus, DateTime lastStatusDate, 
            decimal paidFees, int createdByUserID)
        {
            Mode = enMode.Update;
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            PersonInfo = clsPerson.FindByPersonID(applicantPersonID);
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeInfo = clsApplicationTypes.Find(applicationTypeID);
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            UserInfo = clsUser.FindByUserID(createdByUserID);
        }

        public static clsApplication FindBaseApplication(int applicationID)
        {
            int  applicantPersonID = -1, applicationTypeID = -1, createdByUserID = -1;
            DateTime applicationDate = DateTime.Now, lastStatusDate = DateTime.Now;
            int applicationStatus = -1; decimal paidFees = -1;
            bool Result = clsApplicationData.GetInfoApplication(applicationID,ref applicantPersonID,
                ref applicationDate,ref applicationTypeID, ref applicationStatus, 
                ref lastStatusDate, ref paidFees, ref createdByUserID);
            if (Result)
            {
                return new clsApplication(applicationID,  applicantPersonID,
                 applicationDate,  applicationTypeID,  (_enApplicationStatus)applicationStatus,
                 lastStatusDate,  paidFees, createdByUserID);
            }
            else { return null; }
        }
        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(ApplicationID, ApplicantPersonID,
                ApplicationDate, ApplicationTypeID,(int) ApplicationStatus, LastStatusDate,
                PaidFees, CreatedByUserID);
        }
        private bool _AddNewApplication()
        {
            return (this.ApplicationID = clsApplicationData.AddNewApplication(ApplicantPersonID,
                ApplicationDate, ApplicationTypeID, (int)ApplicationStatus, LastStatusDate,
                PaidFees, CreatedByUserID) ) > 0;
            
        }
        public bool Save()
        {
            switch( Mode ) 
            {
                case enMode.Update:
                    return _UpdateApplication();
                case enMode.AddNew:
                    if( _AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
            }
            return false;
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, (int)_enApplicationStatus.Cancelled);
        }
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, (int)_enApplicationStatus.Complated);
        }
        public static int GetActiveApplicationIDForLicenseClass
            (int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID, clsApplication._enApplicationStatus applicationStatus)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, (int)ApplicationTypeID, LicenseClassID,(int)applicationStatus);
        }
        public static bool IsCompleteApplicationExistForLicenseClass(int ApplicantPersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
             return clsApplication.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, ApplicationTypeID, LicenseClassID, clsApplication._enApplicationStatus.Complated) != -1;
        }


        // بحث عن طلب غير مكتمل من نفس الرخصة 
        //public static bool IsExistApplicationNewLocal(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        //{
        //    return clsApplicationData.IsExistApplicationNewLocal(ApplicantPersonID, ApplicationTypeID, LicenseClassID);
        //}
    }
    
}
