using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Models;
using TemplateCoreBusiness.Properties;

namespace TemplateCoreBusiness.Database
{
    public class DbTempImp : IDataBase
    {
        private StringBuilder m_connetionString = null;
        private SqlConnection m_connection = null;
        private readonly string[] m_UserColumns = {"FirstName", "LastName", "Password", "Email", "CreationTime"};

        private readonly string[] m_TemplateColumns =
            {"TemplateText", "TemplateUser", "TemplateHead", "Rate", "Comments"};

        internal DbTempImp()
        {
        }

        public string CreateNewTemplate(object[] templateValues)
        {
            openConnection();
            string retVal = insertRowToTable(ListOfTables.Templates, m_TemplateColumns, templateValues);
            closeConnection();
            return retVal;
        }

        public void CreateNewUser(UserEntity userEntity)
        {
            openConnection();
            object[] userValues = getUserValues(userEntity);
            string retVal = insertRowToTable(ListOfTables.UserInformation, m_UserColumns, userValues);
            if (String.IsNullOrEmpty(retVal) == false)
            {
                throw new Exception(retVal);
            }
            closeConnection();
        }

        public UserEntity GetUser(string iEmail)
        {
            openConnection();
            UserEntity retVal = getUserInformation(iEmail);
            closeConnection();
            return retVal;
        }

        public void SearchTemplate()
        {
            throw new NotImplementedException();
        }

        public string DeleteTemplate(int templateId)
        {
            openConnection();
            string retVal = deleteFromDB(ListOfTables.Templates, templateId);
            closeConnection();
            return retVal;
        }

        private object[] getUserValues(UserEntity userEntity)
        {
            List<object> userList = new List<object>();
            userList.Add(userEntity.FirstName);
            userList.Add(userEntity.LastName);
            userList.Add(userEntity.Email);
            userList.Add(userEntity.Password);
            userList.Add(userEntity.CreationTime);
            return userList.ToArray();
        }

        private string deleteFromDB(string nameOftable, int templateId)
        {
            string retVal = null;
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage = $"DELETE from [TemplateCore].[dbo].[{nameOftable}] WHERE Id = {templateId}";
                    command.CommandText = insertMessage.ToString();
                    try
                    {
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected == 1)
                        {
                            retVal = "Delete succeeded";
                        }
                        else
                        {
                            retVal = "Delete failed";
                        }
                    }
                    catch (Exception ex)
                    {
                        retVal = "Delete failed " + ex.Message;
                    }
                }
            }
            else
            {
                retVal = "There is no connection there for can not delete the template";
            }

            return retVal;
        }

        private UserEntity getUserInformation(string iEmail)
        {
            
            if (m_connection != null)
            {
                
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT * from [TemplateCore].[dbo].[{ListOfTables.UserInformation}] WHERE Email = '{iEmail}'";
                    command.CommandText = insertMessage.ToString();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        UserEntity retVal = new UserEntity();
                        retVal.FirstName = ((SqlString)reader.GetSqlValue(0)).ToString();
                        retVal.LastName = ((SqlString)reader.GetSqlValue(1)).ToString();
                        retVal.Password = ((SqlString)reader.GetSqlValue(2)).ToString();
                        retVal.Email = ((SqlString)reader.GetSqlValue(3)).ToString();
                        retVal.CreationTime = ((SqlDateTime)reader.GetSqlValue(4)).Value;
                        return retVal;
                    }
                    else
                    {
                        throw new Exception("There was no result with email: " + iEmail);
                    }
                }
            }
            else
            {
                throw new Exception("There is no connection, there for can not return user information");
            }
        }

        private string insertRowToTable(string nameOftable, string[] columnList, object[] valuesList)
        {
            string retVal = "";
            if (m_connection != null)
            {
                int countOfFields = calculateNumberOfFileds(columnList.Length, valuesList.Length);
                string column = createStringFromArrayOfObjects(columnList, false, countOfFields);
                string values = createStringFromArrayOfObjects(valuesList, true, countOfFields);
                string insertMessage = $"INSERT into [TemplateCore].[dbo].[{nameOftable}] ({column}) VALUES ({values})";
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = m_connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insertMessage.ToString();
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected != 1)
                        {
                            retVal = "The insert to DB falied";
                        }
                    }
                }
                catch (Exception e)
                {
                    retVal = "The insert to DB falied";
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                retVal = "There is no connection to DB therefore did not insert the row";
            }

            return retVal;
        }

        private int calculateNumberOfFileds(int lengh1, int length2)
        {
            int retVal = lengh1;
            if (length2 < lengh1)
            {
                retVal = length2;
            }

            return retVal;
        }

        private string createStringFromArrayOfObjects(object[] array, bool isValue, int count)
        {
            StringBuilder values = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                object item = array[i];
                if (item != null && isValue)
                {
                    if (item.GetType().Equals(typeof(string)) || item.GetType().Equals(typeof(DateTime)))
                    {
                        item = "'" + item + "'";
                    }
                    
                }

                if (i < count - 1)
                {
                    values.Append(item).Append(", ");
                }
                else
                {
                    values.Append(item);
                }
            }

            return values.ToString();
        }

        private void openConnection()
        {
            try
            {
                createSqlConnection();
                m_connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Failed to open connection to db\n" + e.Message);
            }
        }

        private void closeConnection()
        {
            try
            {
                if (m_connection != null)
                {
                    m_connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Failed to close connection to db\n" + e.Message);
            }
        }

        private void createSqlConnection()
        {
            if (m_connetionString == null && m_connection == null)
            {
                try
                {
                    string m_connetionString =
                        $"Server ={Settings.Default.SERVER_IP}, {Settings.Default.PORT}; Database={Settings.Default.DATA_BASE_NAME} ;User ID = {Settings.Default.USER_ID}; Password = {Settings.Default.PASSWORD}";
                    m_connection = new SqlConnection(m_connetionString);
                }
                catch (Exception e)
                {
                    m_connetionString = null;
                    m_connection = null;
                    throw new Exception("Failed to create sql connection\n" + e.Message);
                }
            }
        }
    }
}