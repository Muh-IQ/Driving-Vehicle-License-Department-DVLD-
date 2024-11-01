using MyDVLD_Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Business
{
    public class clsCountry
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }


        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static clsCountry Find(int CountryID)
        {
            string countryName = "";

            if (clsCountryData.GetCountryByID(CountryID, ref countryName))
                return new clsCountry(CountryID, countryName);
            else
                return null;
        }
        public static DataTable ListCounties()
        {
            return clsCountryData.GetListCountries();
        }
    }
}
