using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MyDVLD_Data
{
    public class clsPersonData
    {

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
             string LastName, bool Gender, string Email, string Address, string Phone, int CountryID,
             DateTime DateOfBirth, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO People VALUES(
                             @NationalNo,
                             @FirstName,
                             @SecondName,
                             @ThirdName,
                             @LastName,
                             @DateOfBirth,
                             @Gender,
                             @Address,
                             @Phone,
                             @Email,
                             @CountryID,
                             @ImagePath);
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
                //if (string.IsNullOrEmpty(ThirdName))
                //    command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
                //else
                //    command.Parameters.AddWithValue("@ThirdName", ThirdName);

                if (string.IsNullOrEmpty(ImagePath))
                    command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

            SqlDataReader reader = null;

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    PersonID = InsertedID;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNewPerson: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, bool Gender, string Email, string Address, string Phone, int CountryID,
            DateTime DateOfBirth, string ImagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE People SET
                             NationalNo = @NationalNo,
                             FirstName=  @FirstName,
                             SecondName=  @SecondName,
                             ThirdName=  @ThirdName,
                             LastName=  @LastName,
                             Gender=  @Gender,
                             Email=  @Email,
                             Address=  @Address,
                             Phone=  @Phone,
                             CountryID=  @CountryID,
                             DateOfBirth=  @DateOfBirth,
                             ImagePath =  @ImagePath
                             WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            {

                if (string.IsNullOrEmpty(ThirdName))
                    command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);

                if (string.IsNullOrEmpty(ImagePath))
                    command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdatePerson: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool GetPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref bool Gender, ref string Email, ref string Address, ref string Phone,
            ref int CountryID, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (bool)reader["Gender"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();
                    CountryID = (int)reader["CountryID"];
                    ImagePath = reader["ImagePath"].ToString();

                    {

                        if (reader["ThirdName"] == DBNull.Value)
                            ThirdName = "";
                        else
                            ThirdName = reader["ThirdName"].ToString();

                        if (reader["ImagePath"] == DBNull.Value)
                            ImagePath = "";
                        else
                            ImagePath = reader["ImagePath"].ToString();
                    }

                    IsFound = true;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetPersonByID: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
            return IsFound;
        }

        public static bool GetPersonByNationalNo(ref int PersonID, string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref bool Gender, ref string Email, ref string Address, ref string Phone,
            ref int CountryID, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (bool)reader["Gender"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();
                    CountryID = (int)reader["CountryID"];
                    ImagePath = reader["ImagePath"].ToString();

                    {

                        if (reader["ThirdName"] == DBNull.Value)
                            ThirdName = "";
                        else
                            ThirdName = reader["ThirdName"].ToString();

                        if (reader["ImagePath"] == DBNull.Value)
                            ImagePath = "";
                        else
                            ImagePath = reader["ImagePath"].ToString();
                    }

                    IsFound = true;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetPersonByNationalNo: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
            return IsFound;
        }
        public static bool DeletePersonByID(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "DELETE People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeletePersonByID: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }


        public static bool IsExist(string NationalNo)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT NationalNo FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            SqlDataReader reader = null;

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("IsExistByNationalNo: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
            return IsFound;
        }

        public static bool IsExist(int PersonID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT NationalNo FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            SqlDataReader reader = null;

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("IsExistByNationalNo: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
            return IsFound;
        }
        public static DataTable GetAllPersonInfo()
        {
            DataTable dt = new DataTable(); SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,
                          CASE WHEN Gender = 1 THEN 'Male' WHEN Gender = 0 THEN 'Female' ELSE 'None' END AS Gender,DateOfBirth,
                          (Select Countries.CountryName from Countries 
                          Where People.CountryID=Countries.CountryID) As Nationalty,Phone,Email
                          from People;"; 
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
