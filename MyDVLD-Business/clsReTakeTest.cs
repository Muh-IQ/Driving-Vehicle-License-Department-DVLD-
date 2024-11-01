using System;
using static System.Net.Mime.MediaTypeNames;

namespace MyDVLD_Business
{
    public class clsReTakeTest
    {
        private enum _enMode { AddNew, Update }
        private _enMode _Mode;
        public int L_D_L_App_ID { get; set; }
        public string ClassName { get; set; }
        public int RetakeTestApplicationID { get; private set; }

        public int CreatedByUserID { get; set; }

       
        private clsApplication _application;
        private clsApplication _NewApplication;
        private bool _Result=false;
        public clsReTakeTest()
        {
            _Mode = _enMode.AddNew;
        }
        private bool _AddNew()
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(L_D_L_App_ID);
            if (localDrivingLicenseApplication != null)
            {
                //_application = clsApplication.FindApplication(localDrivingLicenseApplication.ApplicationID);
                //if (_application != null)
                //{
                //    _NewApplication = new clsApplication();
                //    _NewApplication.ApplicantPersonID = _application.ApplicantPersonID;
                //    _NewApplication.ApplicationDate = DateTime.Now;
                //    _NewApplication.ApplicationTypeID = 2;
                //    _NewApplication.ApplicationStatus = 1;
                //    _NewApplication.LastStatusDate = _application.ApplicationDate;
                //    _NewApplication.PaidFees = clsApplicationTypes.Find(2).ApplicationFees;
                //    _NewApplication.CreatedByUserID = CreatedByUserID;
                //    _Result = _NewApplication.Save();
                //    RetakeTestApplicationID = _NewApplication.ApplicationID;
                //} 
            }
            return _Result;
        }
        public bool Save()
        {
            switch(_Mode) 
            {
                case _enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
}
