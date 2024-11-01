using MyDVLD_Data;
using System;
using System.Data;

namespace MyDVLD_Business
{
    public class clsPerson
    {
        public enum _enMode { AddNew = 0, Update = 1 };
        public _enMode Mode;

        public int PersonID { private set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public bool Gender { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public int CountryID { set; get; }
        public string ImagePath { set; get; }
        public clsCountry CountryInfo;
        public string FullName
        {
            get
            { 
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gender = false;
            this.DateOfBirth = DateTime.Now;
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.CountryID = -1;
            this.ImagePath = "";

            Mode = _enMode.AddNew;
        }

        private clsPerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName,
            string LastName, bool Gender, DateTime DateOfBirth, string Email,
            string Phone, string Address, int CountryID, string ImagePath)
        {
            this.PersonID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.Find(CountryID);
            Mode = _enMode.Update;
        }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                 this.Gender, this.Email, this.Address, this.Phone, this.CountryID, this.DateOfBirth, this.ImagePath);

            return (PersonID > 0);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                   this.LastName, this.Gender, this.Email, this.Address, this.Phone, this.CountryID, this.DateOfBirth, this.ImagePath);
        }

        public static clsPerson FindByPersonID(int PersonID)
        {

            int CountryID = -1;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "",
                Phone = "", Address = "", ImagePath = "";

            bool Gender = false;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPersonData.GetPersonByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref Gender, ref Email, ref Address, ref Phone, ref CountryID, ref DateOfBirth, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Gender, DateOfBirth,
                    Email, Phone, Address, CountryID, ImagePath);
            }
            else
                return null;
        }

        public static clsPerson FindByNationalNo(string NationalNo)
        {

            int CountryID = -1, PersonID = -1;
            string  FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "",
                Phone = "", Address = "", ImagePath = "";

            bool Gender = false;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPersonData.GetPersonByNationalNo(ref PersonID, NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                ref Gender, ref Email, ref Address, ref Phone, ref CountryID, ref DateOfBirth, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Gender, DateOfBirth,
                    Email, Phone, Address, CountryID, ImagePath);
            }
            else
                return null;
        }
        public static bool DeletePersonByID(int PersonID)
        {
            return clsPersonData.DeletePersonByID(PersonID);
        }
        
        public bool Save()
        {
            switch (Mode)
            {
                case _enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {
                            Mode = _enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case _enMode.Update:
                    {
                        return _UpdatePerson();
                    }
            }
            return false;
        }


        public static DataTable GetAllPersons()
        {
            return clsPersonData.GetAllPersonInfo();
        }

        public static bool IsExist(string NationalNo)
        {
            return clsPersonData.IsExist(NationalNo);
        }
        public static bool IsExist(int PersonID)
        {
            return clsPersonData.IsExist(PersonID);
        }

    }
}
