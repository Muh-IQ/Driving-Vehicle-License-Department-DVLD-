using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDVLD_Data
{
    public class clsTestsData
    {
        public static bool GetInfoTest(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false; SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM Tests 
                            	WHERE TestID = @TestID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = " ";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
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
        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Tests 
                            VALUES
                                   (@TestAppointmentID,
                                   @TestResult,
                                   @Notes,
                                   @CreatedByUserID);

                            UPDATE TestAppointments SET
                                    IsLocked =1
                                    WHERE TestAppointmentID = @TestAppointmentID;
                            SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue(@"Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue(@"Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    TestID = InsertedID;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNew Test: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }

            return TestID;
        }
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE Tests SET
                            TestAppointmentID =@TestAppointmentID,
                          TestResult =@TestResult,
                          Notes =@Notes,
                          CreatedByUserID =@CreatedByUserID	
                        WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue(@"Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue(@"Notes", Notes); command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Update Test: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool GetLastTestByLocalDrivingLicenseApplicationIDAndTestTypeID(int LocalDrivingLicenseApplicationID, int TestTypeID, ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT TOP 1 Tests.TestID, Tests.TestAppointmentID, Tests.TestResult, Tests.Notes, Tests.CreatedByUserID
                            FROM     TestAppointments INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
				  WHERE (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        AND (TestTypeID = @TestTypeID)
				  ORDER BY Tests.TestAppointmentID DESC;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", TestID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsFound = true;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetLastTest : " + ex.Message);
            }
            finally
            {
                reader?.Close();
                connection.Close();
            }
            return IsFound;
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTest = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT COUNT(Tests.TestID) AS PassedTest
                            FROM     TestAppointments INNER JOIN
                  Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID 
				 WHERE (Tests.TestResult = 1)
            AND (TestAppointments .LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) ;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte InsertedID))
                    PassedTest = InsertedID;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddNew Test: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return PassedTest;
        }
        
        /// فانكشن من التصميم الاول غير مستخدمة
        public static bool DidHePassLastTest(int L_D_L_AppID,int TestTypeID)
        {
            bool IsFound = false; SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = @"select * from (

                           SELECT Top 1 Tests.TestResult
                           FROM     LocalDrivingLicenseApplications INNER JOIN
                                             TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                             TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID INNER JOIN
                                             Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                           where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID =@L_D_L_AppID AND TestTypes.TestTypeID=@TestTypeID 
                           order by Tests.TestAppointmentID desc 
				  
				           ) R1
				           where   TestResult=1";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@L_D_L_AppID", L_D_L_AppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
                Debug.WriteLine("IsExist Tests: " + ex.Message);
            }
            finally
            {

                connection.Close();
            }
            return IsFound;
        }
        public static bool LastTestResultIsSuccessful(int TestTypeID,  int LocalDrivingLicenseApplicationID)
        {
            bool TestResult = true; 
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT TOP (1) TestResult
                             FROM     TestAppointments  JOIN
                                               TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID   JOIN
                                               Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
     WHERE  (TestTypes.TestTypeID = @TestTypeID AND LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID)
                             ORDER BY Tests.TestAppointmentID DESC;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            SqlDataReader reader = null;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestResult = (bool)reader["TestResult"];                   
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
            return TestResult;
        }
    }
}
