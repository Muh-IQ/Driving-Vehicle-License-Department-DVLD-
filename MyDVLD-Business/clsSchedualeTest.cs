using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsSchedualeTest
    {
        private enum _enMode { Update,AddNew};
        private _enMode _Mode;
        public int L_D_L_App_ID {  get; set; }
        public string ClassName { get; set; }
        public string FullName { get; set; }
        public decimal TestTypeFee {  get; set; }
        public int Trial {  get; set; }





        public int TestTypeID { get; set; }
        public string TestTypeDescription { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int TestAppointmetID { get; set; }
        public clsSchedualeTest()
        {
            _Mode = _enMode.AddNew;
        }
        private clsSchedualeTest(int l_D_L_App_ID, string className, string fullName,
            decimal testTypeFee, int trial)
        {
            L_D_L_App_ID = l_D_L_App_ID;
            ClassName = className;
            FullName = fullName;
            TestTypeFee = testTypeFee;
            Trial = trial;
            _Mode = _enMode.Update;
        }

        public static clsSchedualeTest Find(int l_D_L_App_ID, int TestTypeID)
        {
            string ClassName = "", FullName = "";
            decimal TestTypeFee = 0;
            int Trial = 0;
            
            bool Result = clsSchedualeTestData.GetInfoApplicationData(l_D_L_App_ID,
                ref ClassName, ref FullName);
          
            if (Result)
            {      //Get Trials
                //Result = clsSchedualeTestData.TotalTrialsPerTest(TestTypeID, ref Trial, l_D_L_App_ID);
            }
            if (Result)
            {
                return new clsSchedualeTest(l_D_L_App_ID, ClassName, FullName, TestTypeFee, Trial);
            }
          
            return null;

        }
        /// <summary>
        /// ///////////////
        /// </summary>
        /// <returns></returns>
        private bool _AddNewTestAppointment()
        {
            return (this.TestAppointmetID = clsSchedualeTestData.AddNewTestAppointment(TestTypeID, L_D_L_App_ID, AppointmentDate, TestTypeFee, CreatedByUserID, IsLocked)) > -1;
        }
        public bool Save()
        {
            switch(_Mode) 
            {
                case _enMode.Update: return true;
                case _enMode.AddNew:
                    if (_AddNewTestAppointment())
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
