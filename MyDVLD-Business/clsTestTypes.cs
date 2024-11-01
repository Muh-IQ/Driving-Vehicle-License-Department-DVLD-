using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsTestTypes
    {
        private enum _enMode { Update , AddNew};
        private _enMode _Mode;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public clsTestTypes.enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestTypes(clsTestTypes.enTestType testTypeID, string testTypeTitle, string testTypeDescription, decimal testTypeFees)
        {
            TestTypeID = clsTestTypes.enTestType.VisionTest;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
            _Mode=_enMode.Update;
        }

        public static DataTable GetAllTestTypes() 
        {
            return clsTestTypesData.GetAllTestTypes();
        }

        static public clsTestTypes Find(clsTestTypes.enTestType testTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestTypeFees = 0;
            if (clsTestTypesData.GetInfoTestType((int)testTypeID,ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestTypes(testTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            return null;
        }
        private bool _UpdateTestType()
        {
           return clsTestTypesData.UpdateTestType((int)TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case _enMode.Update:
                    return _UpdateTestType();
            }
            return false;
        }
    }
}
