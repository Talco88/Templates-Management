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
        private readonly string[] m_UserColumns;
        private readonly string[] m_TemplateColumns;
        private readonly string[] m_TopicColumns;

        internal DbTempImp()
        {
            m_UserColumns = getColumnsFromString(Settings.Default.USER_COLUMN);
            m_TemplateColumns = getColumnsFromString(Settings.Default.TEMPLATE_COLUMN);
            m_TopicColumns = getColumnsFromString(Settings.Default.TOPIC_COLUMN);
        }

        public string CreateNewTemplate(TemplateEntity templateEntity)
        {
            openConnection();
            object[] templateValues = getTemplateValues(templateEntity);
            string retVal = insertRowToTable(ListOfTables.Templates, m_TemplateColumns, templateValues);
            closeConnection();
            if (string.IsNullOrEmpty(retVal) == false)
            {
                throw new Exception(retVal);
            }

            return retVal;
        }

        public void CreateNewUser(UserEntity userEntity)
        {
            openConnection();
            object[] userValues = getUserValues(userEntity);
            string retVal = insertRowToTable(ListOfTables.UserInformation, m_UserColumns, userValues);
            closeConnection();
            if (string.IsNullOrEmpty(retVal) == false)
            {
                throw new Exception(retVal);
            }
        }

        public UserEntity GetUser(string iEmail)
        {
            openConnection();
            UserEntity retVal = getUserInformation(iEmail);
            closeConnection();
            return retVal;
        }

        public List<string> SearchTemplate(string iSearchKey)
        {
            openConnection();
            List<string> retVal = getSearchedTemplates(iSearchKey);
            closeConnection();
            return retVal;
        }

        public string DeleteTemplate(string templateName)
        {
            openConnection();
            string retVal = deleteFromDB(ListOfTables.Templates, m_TemplateColumns[1], templateName);
            closeConnection();
            return retVal;
        }

        public string CreateNewTopic(TopicEntity topicEntity)
        {
            openConnection();
            object[] templateValues = getTopicValues(topicEntity);
            string retVal = insertRowToTable(ListOfTables.Topic, m_TopicColumns, templateValues);
            closeConnection();
            if (string.IsNullOrEmpty(retVal) == false)
            {
                throw new Exception(retVal);
            }

            return retVal;
        }

        public string UpdateHeaderInTopic(TopicEntity topicEntity, string i_newHeaderName)
        {
            openConnection();
            string retVal = updateTopicHeader(topicEntity, i_newHeaderName);
            closeConnection();
            if (string.IsNullOrEmpty(retVal) == false)
            {
                throw new Exception(retVal);
            }

            return retVal;
            
        }

        public string DeleteTopic(TopicEntity topicEntity)
        {
            openConnection();
            string retVal = deleteTopicFromDB(topicEntity);
            closeConnection();
            return retVal;
        }

        public List<string> getTopicsInCategory(string i_categoryName)
        {
            openConnection();
            List<string> retVal = getTopicsInCategoryFromDB(i_categoryName);
            closeConnection();
            return retVal;
        }

        public List<TopicEntity> getAllTopics()
        {
            openConnection();
            List<TopicEntity> retVal = getAllTopicsFromDB();
            closeConnection();
            return retVal;
        }

        private List<string> getTopicsInCategoryFromDB(string i_categoryName)
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT distinct {m_TopicColumns[1]} from [TemplateCore].[dbo].[{ListOfTables.Topic}] WHERE {m_TopicColumns[0]} = '{i_categoryName}'";
                    command.CommandText = insertMessage;
                    SqlDataReader reader = command.ExecuteReader();
                    List<string> retVal = new List<string>();
                    while (reader.Read())
                    {
                        retVal.Add(reader.GetValue(0).ToString());
                    }

                    return retVal;
                }
            }
            else
            {
                throw new Exception("There is no connection, there for can not return topics");
            }
        }

        private List<TopicEntity> getAllTopicsFromDB()
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT distinct * from [TemplateCore].[dbo].[{ListOfTables.Topic}]";
                    command.CommandText = insertMessage;
                    SqlDataReader reader = command.ExecuteReader();
                    List<TopicEntity> retVal = new List<TopicEntity>();
                    while (reader.Read())
                    {
                        retVal.Add(new TopicEntity(reader.GetValue(0).ToString(), reader.GetValue(1).ToString()));
                    }

                    return retVal;
                }
            }
            else
            {
                throw new Exception("There is no connection, there for can not return topics");
            }
        }

        private List<string> getSearchedTemplates(string iSearchKey)
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT distinct {m_TemplateColumns[1]} from [TemplateCore].[dbo].[{ListOfTables.Templates}] WHERE HeadName like '%{iSearchKey}%'";
                    command.CommandText = insertMessage;
                    SqlDataReader reader = command.ExecuteReader();
                    List<string> retVal = new List<string>();
                    while (reader.Read())
                    {
                        retVal.Add(reader.GetValue(0).ToString());
                    }

                    return retVal;
                }
            }
            else
            {
                throw new Exception("There is no connection, there for can not return search template information");
            }
        }

        private object[] getUserValues(UserEntity userEntity)
        {
            List<object> userList = new List<object>();
            userList.Add(userEntity.FirstName);
            userList.Add(userEntity.LastName);
            userList.Add(userEntity.Email);
            userList.Add(userEntity.Password);
            userList.Add(userEntity.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return userList.ToArray();
        }

        private string[] getColumnsFromString(string columnList)
        {
            return columnList.Split(',');
        }

        private object[] getTemplateValues(TemplateEntity templateEntity)
        {
            List<object> templateList = new List<object>();
            templateList.Add(templateEntity.TemplateJsonRow);
            templateList.Add(templateEntity.HeadName);
            templateList.Add(templateEntity.UserIdentity);
            templateList.Add(templateEntity.Rate);
            templateList.Add(templateEntity.Comments);
            templateList.Add(templateEntity.RateCounter);
            templateList.Add(templateEntity.Category);
            return templateList.ToArray();
        }

        private object[] getTopicValues(TopicEntity topicEntity)
        {
            List<object> topicList = new List<object>();
            topicList.Add(topicEntity.Category);
            topicList.Add(topicEntity.Header);
            return topicList.ToArray();
        }

        private string deleteTopicFromDB(TopicEntity topicEntity)
        {
            string retVal;
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"DELETE from [TemplateCore].[dbo].[{ListOfTables.Topic}] WHERE {m_TopicColumns[0]} = '{topicEntity.Category}' and {m_TopicColumns[1]} = '{topicEntity.Header}'";
                    command.CommandText = insertMessage;
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
                retVal = "There is no connection there for can not delete the topic";
            }

            return retVal;
        }

        private string deleteFromDB(string nameOftable, string nameOfColumn, string id)
        {
            string retVal = null;
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"DELETE from [TemplateCore].[dbo].[{nameOftable}] WHERE {nameOfColumn} = {id}";
                    command.CommandText = insertMessage;
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
                        $"SELECT distinct * from [TemplateCore].[dbo].[{ListOfTables.UserInformation}] WHERE Email = '{iEmail}'";
                    command.CommandText = insertMessage;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        UserEntity retVal = new UserEntity();
                        retVal.FirstName = ((SqlString) reader.GetSqlValue(0)).ToString();
                        retVal.LastName = ((SqlString) reader.GetSqlValue(1)).ToString();
                        retVal.Password = ((SqlString) reader.GetSqlValue(2)).ToString();
                        retVal.Email = ((SqlString) reader.GetSqlValue(3)).ToString();
                        retVal.CreationTime = ((SqlDateTime) reader.GetSqlValue(4)).Value;
                        return retVal;
                    }
                    else
                    {
                        throw new Exception($"There was no result with email: {iEmail}");
                    }
                }
            }
            else
            {
                throw new Exception("There is no connection, there for can not return user information");
            }
        }

        private string updateTopicHeader(TopicEntity topicEntity, string i_newHeaderName)
        {
            string retVal = "";
            if (m_connection != null)
            {
                try
                {
                    string insertMessage = $"UPDATE [TemplateCore].[dbo].[{ListOfTables.Topic}] SET {m_TopicColumns[1]} = '{i_newHeaderName}' WHERE {m_TopicColumns[0]} = '{topicEntity.Category}' and {m_TopicColumns[1]} = '{topicEntity.Header}'";
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = m_connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insertMessage;
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected != 1)
                        {
                            retVal = "The update to DB falied";
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"The update to DB falied: {e.Message}");
                }
            }
            else
            {
                retVal = "There is no connection to DB therefore did not update the topic";
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
                string insertMessage = $"INSERT into [TemplateCore].[dbo].[{nameOftable}] ({column}) VALUES ({values})";
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = m_connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insertMessage;
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected != 1)
                        {
                            retVal = "The insert to DB falied";
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"The insert to DB falied, Exeption: {e.Message}");
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
                throw new Exception($"Failed to open connection to db, Exception: {e.Message}");
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
                throw new Exception($"Failed to close connection to db Exception: {e.Message}");
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
                    throw new Exception($"Failed to create sql connection Exception: {e.Message}");
                }
            }
        }
    }
}