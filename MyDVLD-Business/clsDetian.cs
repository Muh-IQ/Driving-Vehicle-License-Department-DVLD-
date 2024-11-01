using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsDetian
    {
        private enum _enMode { Update, AddNew }
        private _enMode _Mode;
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo{ get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo { get; set; }
        public int ReleaseApplicationID { get; set; }


        private clsDetian(int detainID, int licenseID, DateTime DetainDate, decimal fineFees, int createdByUserID,
            bool isReleased, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            _Mode = _enMode.Update;
            DetainID = detainID;
            LicenseID = licenseID;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
            this.DetainDate= DetainDate;
        }

        public clsDetian()
        {
            _Mode = _enMode.AddNew;
        }

        public static clsDetian Find(int detainID)
        {
            int licenseID = -1, createdByUserID = -1, releasedByUserID = -1, releaseApplicationID = -1;
            decimal fineFees = -1;
            bool isReleased = false; DateTime releaseDate = DateTime.Now, DetainDate = DateTime.Now;
            bool Result =clsDetainData.GetInfoDetainedLicense( detainID, ref licenseID, ref DetainDate,
                ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate,
                ref releasedByUserID, ref releaseApplicationID);
            if (Result)
            {
                return new clsDetian(detainID, licenseID, DetainDate, fineFees, createdByUserID, isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            return null;
        }
        public static clsDetian FindByLicenseID(int licenseID)
        {
            int detainID = -1, createdByUserID = -1, releasedByUserID = -1, releaseApplicationID = -1;
            decimal fineFees = -1;
            bool isReleased = false; DateTime releaseDate = DateTime.Now, DetainDate = DateTime.Now;
            bool Result = clsDetainData.GetInfoDetainedLicenseByLicenseID(ref detainID,  licenseID, ref DetainDate,
                ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate,
                ref releasedByUserID, ref releaseApplicationID);
            if (Result)
            {
                return new clsDetian(detainID, licenseID, DetainDate, fineFees, createdByUserID, isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            return null;
        }

        private bool _AddNew()
        {
            return (this.DetainID = clsDetainData.AddNewDetainedLicense(LicenseID, FineFees, CreatedByUserID)) > - 1;
        }
        private bool _Update()
        {
            return clsDetainData.UpdateDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID);
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

        public static DataTable GetAllDetainedLicense()
        {
            return clsDetainData.GetAllDetainedLicense();
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainData.IsLicenseDetained(LicenseID);
        }
        public bool IsLicenseDetained()
        {
            return clsDetainData.IsLicenseDetained(this.LicenseID);
        }

        public  bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
