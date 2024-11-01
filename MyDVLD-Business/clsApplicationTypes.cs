using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsApplicationTypes
    {
        private enum _enMode { Update=1}
        private _enMode _Mode;
        public int applicationTypeID { get; set; }
        public string ApplicationTypeTitle {  get; set; }
        public decimal ApplicationFees { get; set; }
        private clsApplicationTypes(int applicationTypeID, string applicationTypeTitle, decimal applicationFees)
        {
            _Mode = _enMode.Update;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;
            this.applicationTypeID=applicationTypeID;
        }
        public  static  DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypesList();
        }
        private bool _UpdateApplicationTypes()
        {
            return clsApplicationTypesData.UpdateApplicationTypes(applicationTypeID, ApplicationTypeTitle, ApplicationFees);
        }
        public static clsApplicationTypes Find( int applicationTypeID)
        {
            string ApplicationTypeTitle = "";  decimal ApplicationFees = 0;
           if( clsApplicationTypesData.GetInfoApplicationTypes(applicationTypeID, ref ApplicationTypeTitle,ref ApplicationFees))
            {
                return new clsApplicationTypes(applicationTypeID,ApplicationTypeTitle, ApplicationFees);
            }
            return null;
        }
        public bool Save()
        {
            switch(_Mode) 
            {
                case _enMode.Update:
                    return _UpdateApplicationTypes();
            }
            return false;
        }

    }
}
