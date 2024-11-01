using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsTests
    {
        private enum _enMode { Update, AddNew };
        private _enMode _Mode;
        
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { get;private set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser UserInfo {  get;private set; }
        public clsTests() 
        {
            _Mode = _enMode.AddNew;
        }

        
        private clsTests( int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            _Mode = _enMode.Update;
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(this.TestAppointmentID);
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;
            this.UserInfo=clsUser.FindByUserID(this.CreatedByUserID);
        }
        public static clsTests Find(int TestID)
        {
            int  testAppointmentID = -1, createdByUserID = -1;
            string notes = string.Empty; bool testResult = false;
            if (clsTestsData.GetInfoTest(TestID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTests(TestID, testAppointmentID, testResult, notes, createdByUserID);
            }
            return null;
        }

        public static clsTests FindLastTestPerLocalDrivingAppIDAndTestTypeID( int LocalDrivingAppID, clsTestTypes.enTestType TestTypeID)
        {
            int testID = -1 , testAppointmentID =-1 ,  createdByUserID =-1;
            string notes = string.Empty; bool testResult = false;
            if (clsTestsData.GetLastTestByLocalDrivingLicenseApplicationIDAndTestTypeID(LocalDrivingAppID, (int)TestTypeID,
                ref testID,ref testAppointmentID,ref testResult,ref notes,ref createdByUserID))
            {
                return new clsTests (testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            return null;
        }
        private bool _AddNewTest()
        {
            return (this.TestID = clsTestsData.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID)) > -1;   
        }
        private bool _UpdateTest()
        {
            return clsTestsData.UpdateTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }
        public bool Save()
        {
            switch (_Mode) 
            {
                case _enMode.Update:
                    return _UpdateTest();
                case _enMode.AddNew:
                    if (_AddNewTest())
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    return false;
            }
            return false;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTest(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

        /// فانكشن من التصميم الاول غير مستخدمة
        public static bool DidHePassLastTest(int L_D_L_AppID, int TestTypeID)
        {
            return clsTestsData.DidHePassLastTest(L_D_L_AppID, TestTypeID);
        }
        public static bool LastTestResultIsSuccessful(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            return clsTestsData.LastTestResultIsSuccessful(TestTypeID, LocalDrivingLicenseApplicationID);
        }
    }
}
