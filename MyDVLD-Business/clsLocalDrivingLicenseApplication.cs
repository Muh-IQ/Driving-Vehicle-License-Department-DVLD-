using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyDVLD_Business
{
    public class clsLocalDrivingLicenseApplication :clsApplication
    {
        private enum _enMode { Update, AddNew }
        private _enMode _Mode; 
        public int L_D_L_ApplacitionID { get; set; }
        public string PersonFullName
        { 
            get
            {
                return base.PersonInfo.FullName;
            }
        }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public clsLocalDrivingLicenseApplication() 
        {
            LicenseClassID = -1;
            L_D_L_ApplacitionID = -1;
            _Mode = _enMode.AddNew;
        }

        public clsLocalDrivingLicenseApplication(int L_D_L_ApplacitionID,
            int applicationID, int licenseClassID, int applicantPersonID, DateTime applicationDate,
            int applicationTypeID, _enApplicationStatus applicationStatus, DateTime lastStatusDate,
            decimal paidFees, int createdByUserID)
        {
            this.L_D_L_ApplacitionID = L_D_L_ApplacitionID;
            this.LicenseClassID = licenseClassID;
            this.ApplicationID = applicationID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.UserInfo = clsUser.FindByUserID(createdByUserID);
            this.ApplicantPersonID= applicantPersonID;
            this.PersonInfo = clsPerson.FindByPersonID(applicantPersonID);
            LicenseClassInfo = clsLicenseClass.Find(licenseClassID);
            _Mode = _enMode.Update;
        }
        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int L_D_L_ApplacitionID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            bool result = clsLocalDrivingLicenseApplicationsData.GetInfoLocalDrivingLicenseApplicationByL_D_L_ApplacitionID
                ( L_D_L_ApplacitionID, ref ApplicationID, ref LicenseClassID);
            if (result)
            {
                clsApplication application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(L_D_L_ApplacitionID, ApplicationID, LicenseClassID, application.ApplicantPersonID,application.ApplicationDate,application.ApplicationTypeID,application.ApplicationStatus,application.LastStatusDate,application.PaidFees,application.CreatedByUserID);
            }
            else { return null; }
        }
        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int L_D_L_ApplacitionID = -1, LicenseClassID = -1;
            bool result = clsLocalDrivingLicenseApplicationsData.GetInfoLocalDrivingLicenseApplicationByApplicationID
                (ref L_D_L_ApplacitionID,  ApplicationID, ref LicenseClassID);
            if (result)
            {
                clsApplication application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(L_D_L_ApplacitionID, ApplicationID, LicenseClassID, application.ApplicantPersonID, application.ApplicationDate, application.ApplicationTypeID, application.ApplicationStatus, application.LastStatusDate, application.PaidFees, application.CreatedByUserID);
            }
            else { return null; }
        }
        private bool _AddNew_L_D_License()
        {
            return (this.L_D_L_ApplacitionID 
                = clsLocalDrivingLicenseApplicationsData.
                AddNewLocalDrivingLicenseApplications(ApplicationID, LicenseClassID)) > 0;
        }
        private bool _Update_L_D_License()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication(L_D_L_ApplacitionID, ApplicationID, LicenseClassID);
        }
        public bool Save()
        {
            base.Mode = (clsApplication.enMode)_Mode;
            if (!base.Save())
            {
                return false;
            }

            switch (_Mode)
            {
                case _enMode.Update:
                    return _Update_L_D_License();
                case _enMode.AddNew:
                    if (_AddNew_L_D_License())
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    return false;
            }
            return false;
        }
        public static DataTable GetAllLocalApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalApplication();
        }

        public  bool Delete()
        {
            bool IsDeletedL_D_L_App = false;
            bool IsDeletedApp = false;
            IsDeletedL_D_L_App = clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(L_D_L_ApplacitionID);
            if (IsDeletedL_D_L_App)
            {
                IsDeletedApp = base.Delete();
            }
            return IsDeletedApp;
        }

        public static int TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest((int)TestTypeID, LocalDrivingLicenseApplicationID);
        }
        public  int TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest((int)TestTypeID, this.L_D_L_ApplacitionID);
        }

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesAttendTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public  bool DoesAttendTestType( clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesAttendTestType(this.L_D_L_ApplacitionID, (int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public  bool IsThereAnActiveScheduledTest( clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(L_D_L_ApplacitionID, (int)TestTypeID);
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesPassTestType(LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }
        public  bool DoesPassTestType( clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesPassTestType(L_D_L_ApplacitionID, (int)TestTypeID);
        }

        public clsTests GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTests.FindLastTestPerLocalDrivingAppIDAndTestTypeID(L_D_L_ApplacitionID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTests.GetPassedTestCount(L_D_L_ApplacitionID);
        }
        public bool PassedAllTest()
        {
            return clsTests.PassedAllTest(L_D_L_ApplacitionID);
        }

        public bool IsLicenseIssued()
        {
            return GetActiveLicenseID() > -1;
        }
        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
        public int? IssueLicenseForFirstTime(string Notes,int UserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindByPersonID(ApplicantPersonID);
            if (Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = ApplicantPersonID;
                Driver.CreatedByUserID = UserID;
                Driver.CreatedDate = DateTime.Now;
                if (Driver.Save())
                {
                    DriverID=Driver.DriverID ;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            clsLicense license = new clsLicense();
            license.DriverID = DriverID;
            license.ApplicationID = this.ApplicationID;
            license.LicenseClassID = this.LicenseClassID;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            license.Notes = Notes;
            license.PaidFees = this.PaidFees;
            license.IsActive=true;
            license.IssueReason = clsLicense.enIssueReason.FirstTime;
            license.CreatedByUserID= UserID;
            if (license.Save())
            {
                this.SetComplete();
            }

            return license.LicenseID;
        }
    }
}
