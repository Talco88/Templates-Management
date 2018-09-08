using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCoreBusiness.Database
{
    public class DbTempImp : IDataBase
    {
        private static DbTempImp m_instance = null;
        private static readonly object m_padlock = new object();
        private static readonly string m_serverIp = "192.116.98.61";
        private static readonly string m_port = "1433";
        private static readonly string m_dataBaseName = "TemplateCore";
        private static readonly string m_userId = "orho";
        private static readonly string m_password = "Aa123456";
        private StringBuilder m_connetionString = null;
        private SqlConnection m_connection = null;
        private readonly string[] m_UserColumns = { "FirstName", "LastName", "AccessToken", "Email", "Id" };
        private readonly string[] m_TemplateColumns = { "id", "TemplateText", "TemplateUser" };

        DbTempImp()
        {
        }

        public static DbTempImp GetInstance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_padlock)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new DbTempImp();
                        }
                    }
                }
                return m_instance;
            }
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

        public string DeleteTemplate(int userId, int templateId)
        {
            return null;
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
                string column = createStringFromArrayOfObjects(columnList, false);
                string values = createStringFromArrayOfObjects(valuesList, true);

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

        private string createStringFromArrayOfObjects(object[] array, bool isValue)
        {
            StringBuilder values = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                object item = array[i];
                if (item.GetType().Equals(typeof(string)) && isValue)
                {
                    item = "'" + item + "'";
                }
                if (i < array.Length - 1)
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
                    m_connetionString.AppendFormat("Server ={0},{1}; Database={2} ;User ID = {3}; Password = {4}", m_serverIp,
                        m_port, m_dataBaseName, m_userId, m_password);
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
