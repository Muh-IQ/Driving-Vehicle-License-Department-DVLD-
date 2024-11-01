using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsLicenseClass
    {
        private enum _enMode { Update, AddNew };
        private _enMode _Mode;
        public int LicenseClassID {  get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }
        public clsLicenseClass()
        {
            _Mode = _enMode.AddNew;
        }
        private clsLicenseClass(int licenseClassID, string className, string classDescription,
            byte minimumAllowedAge, byte defaultValidityLength, decimal classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
            _Mode = _enMode.Update;
        }

        public static clsLicenseClass Find(string className)
        {
            int licenseClassID = -1; byte minimumAllowedAge = 0, defaultValidityLength = 0;
            string classDescription = "";
            decimal classFees = -1;
            if (LicensesClassData.GetInfoLicenseClassByClassName(ref licenseClassID, className, 
                ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenseClass(licenseClassID, className, classDescription, 
                    minimumAllowedAge, defaultValidityLength, classFees);
            }
            return null;
        }
        public static clsLicenseClass Find(int licenseClassID)
        {
            ; byte minimumAllowedAge = 0, defaultValidityLength = 0;
            string classDescription = "", className="";
            decimal classFees = -1;
            if (LicensesClassData.GetInfoLicenseClasseByLicenseClassID( licenseClassID, ref className,
                ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenseClass(licenseClassID, className, classDescription,
                    minimumAllowedAge, defaultValidityLength, classFees);
            }
            return null;
        }
        public static DataTable GetAllLicenseClasses()
        {
            return LicensesClassData.GetAllLicenseClasses();
        }
        private bool _Update()
        {
            return LicensesClassData.UpdateLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
        }
        private bool _AddNew()
        {
            return (this.LicenseClassID = LicensesClassData.AddNewLicenseClass(ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees)) > -1;
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case _enMode.Update:
                    return _Update();
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
