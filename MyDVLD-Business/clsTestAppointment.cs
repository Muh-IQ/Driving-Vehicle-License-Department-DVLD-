using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsTestAppointment
    {
        private enum _enMode { AddNew, Update }
        private _enMode _Mode;
        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID {  get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestAppInfo { get; set; }
        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }
        public clsTestAppointment() 
        {
            _Mode = _enMode.AddNew;
        }

        private clsTestAppointment(int testAppointmentID, clsTestTypes.enTestType testTypeID,
            int localDrivingLicenseApplicationID, DateTime appointmentDate,
            decimal paidFees, int createdByUserID, bool isLocked, int RetakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);

            _Mode = _enMode.Update;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int RetakeTestApplicationID = -1;
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1 , CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now; decimal PaidFees = -1; bool IsLocked=false;
            if(clsTestAppointmentData.GetInfoTestAppointment(TestAppointmentID,
                ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate,
                ref PaidFees, ref CreatedByUserID, ref IsLocked,ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID,
                 (clsTestTypes.enTestType)TestTypeID,  LocalDrivingLicenseApplicationID,  AppointmentDate,
                PaidFees,  CreatedByUserID,  IsLocked, RetakeTestApplicationID);
            }
            return null;
        }

        public static clsTestAppointment GetLastTestAppointment(int TestAppointmentID,clsTestTypes.enTestType TestTypeID)
        {
            int RetakeTestApplicationID = -1;
            int  LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now; decimal PaidFees = -1; bool IsLocked = false;
            if (clsTestAppointmentData.GetLastTestAppointment( LocalDrivingLicenseApplicationID, (int)TestTypeID, ref TestAppointmentID,
                 ref AppointmentDate,
                ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID,
                 (clsTestTypes.enTestType)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            return null;
        }
        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(TestAppointmentID, (int)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
        }
        private bool _AddNewTestAppointment()
        {
            return (this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)) > 0;
        }
        public bool Save()
        {
            switch(_Mode) 
            {
                case _enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        _Mode= _enMode.Update;
                        return true;
                    }
                    return false;
                case _enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }
        public static DataTable GetApplicationTestAppointmentsPerTestType(clsTestTypes.enTestType TestTypeID,int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType((int)TestTypeID, LocalDrivingLicenseApplicationID);
        }
        public  DataTable GetApplicationTestAppointmentsPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType((int)TestTypeID, this.LocalDrivingLicenseApplicationID);
        }
        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
    }
}
