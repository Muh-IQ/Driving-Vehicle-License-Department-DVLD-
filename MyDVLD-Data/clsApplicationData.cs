using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Data
{
    public class clsApplicationData
    {
        public static int AddNewApplication(int ApplicantPersonID, DateTime
            ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime
            LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Applications 
                            VALUES(@ApplicantPersonID,
                            @ApplicationDate,
                            @ApplicationTypeID,
                            @ApplicationStatus,
                            @LastStatusDate,
                            @PaidFees,
                            @CreatedByUserID);
                            SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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
                Debug.WriteLine("AddNew Application: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }

            return PersonID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus,
            DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE Applications SET
                            ApplicantPersonID =@ApplicantPersonID,
                            ApplicationDate =@ApplicationDate,
                            ApplicationTypeID =@ApplicationTypeID,
                            ApplicationStatus =@ApplicationStatus,
                            LastStatusDate =@LastStatusDate,
                            PaidFees =@PaidFees,
                            CreatedByUserID =@CreatedByUserID	
                            WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Update Application: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool GetInfoApplication(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref int ApplicationStatus, ref DateTime LastStatusDate, ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool IsFound = false; SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM Applications 
                            	WHERE ApplicationID = @ApplicationID;
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (int)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        public static bool IsExistApplication(int ApplicationID)
        {
            bool IsFound = false; SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT ApplicationID From Applications
                                WHERE  ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


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
                Debug.WriteLine("IsExist Application: " + ex.Message);
            }
            finally
            {

                connection.Close();
            }
            return IsFound;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE Applications 
                            	WHERE ApplicationID = @ApplicationID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Delete Application: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        // after edit
        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID,int ApplicationStatus)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT ActiveApplicationID = Applications.ApplicationID
                                FROM     Applications INNER JOIN
                                  LocalDrivingLicenseApplications ON 
				                  Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
				                  WHERE Applications.ApplicantPersonID = @ApplicantPersonID AND 
				                		Applications.ApplicationStatus= @ApplicationStatus AND 
				                		Applications.ApplicationTypeID = @ApplicationTypeID AND 
				                		LocalDrivingLicenseApplications.LicenseClassID =@LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);


            SqlDataReader reader = null;

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    ActiveApplicationID = InsertedID;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNew License: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static bool UpdateStatus(int ApplicationID, int NewStatus)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        //  public static bool IsExistApplicationNewLocal(int ApplicantPersonID, int ApplicationTypeID ,int LicenseClassID)
        //  {
        //      bool IsFound = false; SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
        //      string query = @"SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
        //                       FROM     Applications INNER JOIN
        //            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID

        //WHERE ApplicantPersonID=@ApplicantPersonID AND ApplicationTypeID=@ApplicationTypeID AND LicenseClassID=@LicenseClassID";
        //      SqlCommand command = new SqlCommand(query, connection);
        //      command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
        //      command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
        //      command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

        //      try
        //      {
        //          connection.Open();
        //          object result = command.ExecuteScalar();

        //          if (result != null)
        //          {
        //              IsFound = true;
        //          }

        //      }
        //      catch (Exception ex)
        //      {
        //          Debug.WriteLine("IsExist ApplicationNewLocal: " + ex.Message);
        //      }
        //      finally
        //      {

        //          connection.Close();
        //      }
        //      return IsFound;
        //  }
    }

}
