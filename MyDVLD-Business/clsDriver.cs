using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsDriver
    {
        private enum _enMode { Update, AddNew };
        private _enMode _Mode;
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public int DriverID { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsPerson PersonInfo;
        public clsDriver() 
        {
            _Mode = _enMode.AddNew;
        }

        private clsDriver(int personID, int createdByUserID, int driverID, DateTime CreatedDate)
        {
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            DriverID = driverID;
            this.CreatedDate = CreatedDate;
            PersonInfo = clsPerson.FindByPersonID(personID);
            _Mode = _enMode.Update;
        }

        public static clsDriver FindByDriverID (int driverID)
        {
            int PersonID = 0, CreatedByUserID = 0;  DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetInfoDriverByDriverID(driverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(PersonID, CreatedByUserID, driverID, CreatedDate);
            }
            return null;
        }
        public static clsDriver FindByPersonID(int PersonID)
        {
            int driverID = 0, CreatedByUserID = 0; DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetInfoDriverByPersonID(ref driverID,  PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(PersonID, CreatedByUserID, driverID, CreatedDate);
            }
            return null;
        }
        private bool _AddNew()
        {
            return (this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID)) > -1;
        }
        private bool _Update()
        {
            return clsDriverData.UpdateDriver(this.DriverID,this.PersonID, this.CreatedByUserID,this.CreatedDate);
        }
        public bool Save()
        {
            switch(_Mode) 
            {
                case _enMode.Update:
                    return _Update();
                case _enMode.AddNew:
                    if(_AddNew()) 
                    {
                        _Mode = _enMode.Update;
                        return true;
                    }
                    return false;
            }
            return false;
        }


        public static DataTable GetLicenses(int driverID)
        {
            return clsLicense.GetDriverLicenses(driverID);
        }
        public static DataTable GetAllDriver()
        {
            return clsDriverData.GetAllDriver();
        }
        public static DataTable GetDriverInernationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverInernationalLicenses(DriverID);
        }
    }
}
