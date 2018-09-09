using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Properties;

namespace TemplateCoreBusiness.Database
{
    public class DbTempImp : IDataBase
    {
        private StringBuilder m_connetionString = null;
        private SqlConnection m_connection = null;
        private readonly string[] m_UserColumns = { "FirstName", "LastName", "AccessToken", "Email", "Id" };
        private readonly string[] m_TemplateColumns = { "id", "TemplateText", "TemplateUser" , "TemplateHead", "Rate", "Comments" };

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

        public string CreateNewUser(object[] userValues)
        {
            openConnection();
            string retVal = insertRowToTable(ListOfTables.UserInformation, m_UserColumns, userValues);
            closeConnection();
            return retVal;
        }

        public Dictionary<string, object> GetUser(int id)
        {
            openConnection();
            Dictionary<string, object> retVal = getUserInformation(id);
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

        private string deleteFromDB(string nameOftable, int templateId)
        {
            string retVal = null;
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    StringBuilder insertMessage = new StringBuilder();
                    insertMessage.AppendFormat("DELETE from [TemplateCore].[dbo].[{0}] WHERE Id ={1}", nameOftable, templateId);
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

        private Dictionary<string, object> getUserInformation(int id)
        {
            Dictionary<string, object> retVal = null;
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    StringBuilder insertMessage = new StringBuilder();
                    insertMessage.AppendFormat("SELECT * from [TemplateCore].[dbo].[{0}] WHERE Id = {1}",
                        ListOfTables.UserInformation, id);
                    command.CommandText = insertMessage.ToString();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        retVal = new Dictionary<string, object>();
                        retVal.Add("FirstName", reader.GetSqlValue(0));
                        retVal.Add("LastName", reader.GetSqlValue(1));
                        retVal.Add("AccessToken", reader.GetSqlValue(2));
                        retVal.Add("Email", reader.GetSqlValue(3));
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no connection, there for can not return user information");
            }
            
            return retVal;
        }

        private string insertRowToTable(string nameOftable, string[] columnList, object[] valuesList)
        {
            string retVal = "";
            if (m_connection != null)
            {
                int countOfFields = calculateNumberOfFileds(columnList.Length, valuesList.Length);
                string column = createStringFromArrayOfObjects(columnList, false, countOfFields);
                string values = createStringFromArrayOfObjects(valuesList, true, countOfFields);

                StringBuilder insertMessage = new StringBuilder();
                insertMessage.AppendFormat("INSERT into [TemplateCore].[dbo].[{0}] ({1}) VALUES ({2})", nameOftable, column, values);
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = m_connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insertMessage.ToString();
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected == 1)
                        {
                            retVal = "The insert to DB succeeded";
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
                if (item!= null && item.GetType().Equals(typeof(string)) && isValue)
                {
                    item = "'" + item + "'";
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
                Console.WriteLine(e);
                throw;
            }
        }

        private void closeConnection()
        {
            if (m_connection != null)
            {
                m_connection.Close();
            }
        }

        private void createSqlConnection()
        {
            if (m_connetionString == null && m_connection == null)
            {
                try
                {
                    StringBuilder m_connetionString = new StringBuilder();
                    m_connetionString.AppendFormat("Server ={0},{1}; Database={2} ;User ID = {3}; Password = {4}", DataBaseSettings.Default.SERVER_IP,
                        DataBaseSettings.Default.PORT, DataBaseSettings.Default.DATA_BASE_NAME, DataBaseSettings.Default.USER_ID, DataBaseSettings.Default.PASSWORD);
                    m_connection = new SqlConnection(m_connetionString.ToString());
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
