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
            try
            {
                openConnection();
                return getUserInformation(iEmail);
            }
            finally
            {
                closeConnection();
            }
        }

        public List<string> SearchTemplate(string iSearchKey, bool isAdmin = false, string iUserEmail = "")
        {
            try
            {
                openConnection();
                return getSearchedTemplates(iSearchKey, isAdmin, iUserEmail);
            }
            finally
            {
                closeConnection();
            }
        }

        public string DeleteTemplate(string iCategoryName, string iTemplateName)
        {
            try
            {
                openConnection();
                return deleteTemplateFromDB(iCategoryName, iTemplateName);
            }
            finally
            {
                closeConnection();
            }
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

        public string UpdateHeaderInTopic(TopicEntity topicEntity, string iNewHeaderName)
        {
            openConnection();
            string retVal = updateTopicHeader(topicEntity, iNewHeaderName);
            closeConnection();
            if (string.IsNullOrEmpty(retVal) == false)
            {
                throw new Exception(retVal);
            }

            return retVal;
        }

        public string DeleteTopic(TopicEntity topicEntity)
        {
            try
            {
                openConnection();
                return deleteTopicFromDB(topicEntity);
            }
            finally
            {
                closeConnection();
            }
        }

        public List<string> GetTopicsInCategory(string iCategoryName)
        {
            try
            {
                openConnection();
                return getTopicsInCategoryFromDB(iCategoryName);
            }
            finally
            {
                closeConnection();
            }
        }

        public List<string> GetTopicsNames()
        {
            try
            {
                openConnection();
                return getTopicNamesFromDB();
            }
            finally
            {
                closeConnection();
            }
        }

        public List<TopicEntity> GetAllTopics()
        {
            try
            {
                openConnection();
                return getAllTopicsFromDB();
            }
            finally
            {
                closeConnection();
            }
        }

        public bool IsTopicExistInCategory(string iCategoryName, string iHeaderName)
        {
            try
            {
                openConnection();
                return isCategoryAndHeaderExistsInDb(iCategoryName, iHeaderName);
            }
            finally
            {
                closeConnection();
            }
        }

        public TemplateEntity GetTemplateEntity(string iCategoryName, string iTemplateName)
        {
            try
            {
                openConnection();
                return getTemplateEntityFromDB(iCategoryName, iTemplateName);
            }
            finally
            {
                closeConnection();
            }
        }

        public string UpdateTemplate(TemplateEntity iTemplateEntity)
        {
            try
            {
                openConnection();
                return updateTemplateInDB(iTemplateEntity);
            }
            finally
            {
                closeConnection();
            }
        }

        public string UpdateUser(UserEntity iUserEntity)
        {
            try
            {
                openConnection();
                return updateUserInDB(iUserEntity);
            }
            finally
            {
                closeConnection();
            }
        }

        [Obsolete]
        public string DeleteAllTable(string iTableName)
        {
            try
            {
                openConnection();
                return deleteAllTableFromDB(iTableName);
            }
            finally
            {
                closeConnection();
            }
        }

        private List<string> getTopicsInCategoryFromDB(string iCategoryName)
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT distinct {m_TopicColumns[1]} from [TemplateCore].[dbo].[{ListOfTables.Topic}] WHERE {m_TopicColumns[0]} = '{iCategoryName}'";
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

        private List<string> getTopicNamesFromDB()
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT distinct {m_TopicColumns[0]} from [TemplateCore].[dbo].[{ListOfTables.Topic}]";
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
                throw new Exception("There is no connection, there for can not return topics names");
            }
        }

        private bool isCategoryAndHeaderExistsInDb(string iCategoryName, string iHeaderName)
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT count(*) from [TemplateCore].[dbo].[{ListOfTables.Topic}] WHERE {m_TopicColumns[0]} = '{iCategoryName}' and {m_TopicColumns[1]} = '{iHeaderName}'";
                    command.CommandText = insertMessage;
                    SqlDataReader reader = command.ExecuteReader();
                    bool retVal = false;
                    while (reader.Read())
                    {
                        if ((int) reader.GetValue(0) != 0)
                        {
                            retVal = true;
                        }
                    }

                    return retVal;
                }
            }
            else
            {
                throw new Exception("There is no connection, there for can not return topics names");
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

        private List<string> getSearchedTemplates(string iSearchKey, bool isAdmin, string iUserEmail)
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;

                    string insertMessage = getInsertMessageForSearchTamplate(iSearchKey, isAdmin, iUserEmail);
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

        private string getInsertMessageForSearchTamplate(string iSearchKey, bool isAdmin, string iUserEmail)
        {
            string retVal;
            if (isAdmin)
            {
                retVal = $"SELECT distinct {m_TemplateColumns[1]} from [TemplateCore].[dbo].[{ListOfTables.Templates}] WHERE {m_TemplateColumns[1]} like '%{iSearchKey}%'";
            }
            else
            {
                retVal = $"SELECT distinct {m_TemplateColumns[1]} from [TemplateCore].[dbo].[{ListOfTables.Templates}] WHERE {m_TemplateColumns[1]} like '%{iSearchKey}%' and({m_TemplateColumns[2]} = '{iUserEmail}' or {m_TemplateColumns[7]} = 1)";
            }

            return retVal;
        }

        private object[] getUserValues(UserEntity userEntity)
        {
            List<object> userList = new List<object>();
            userList.Add(userEntity.FirstName);
            userList.Add(userEntity.LastName);
            userList.Add(userEntity.Email);
            userList.Add(userEntity.Password);
            userList.Add(userEntity.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
            userList.Add(userEntity.FavoriteTemplates);
            userList.Add(userEntity.IsAdmin);
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
            templateList.Add(templateEntity.IsShared);
            templateList.Add(templateEntity.RateSum);
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
                        if (recordsAffected > 0)
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

        private string deleteAllTableFromDB(string nameOftable)
        {
            string retVal = null;
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"DELETE from [TemplateCore].[dbo].[{nameOftable}]";
                    command.CommandText = insertMessage;
                    try
                    {
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected > 0)
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

        private string deleteTemplateFromDB(string iCategoryName, string iHeadName)
        {
            string retVal = null;
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"DELETE from [TemplateCore].[dbo].[{ListOfTables.Templates}] WHERE {m_TemplateColumns[6]} = '{iCategoryName}' and {m_TemplateColumns[1]} = '{iHeadName}'";
                    command.CommandText = insertMessage;
                    try
                    {
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected > 0)
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
                        if (recordsAffected > 0)
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

        private TemplateEntity getTemplateEntityFromDB(string iCategoryName, string iTemplateName)
        {
            if (m_connection != null)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = m_connection;
                    command.CommandType = CommandType.Text;
                    string insertMessage =
                        $"SELECT distinct * from [TemplateCore].[dbo].[{ListOfTables.Templates}] WHERE HeadName = '{iTemplateName}' and Category = '{iCategoryName}'";
                    command.CommandText = insertMessage;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return getTemplateFromDB(reader);
                    }
                    else
                    {
                        throw new Exception(
                            $"There was no result with HeadName = '{iTemplateName}' and Category = {iCategoryName}");
                    }
                }
            }
            else
            {
                throw new Exception("There is no connection, there for can not return template information");
            }
        }

        private TemplateEntity getTemplateFromDB(SqlDataReader reader)
        {
            TemplateEntity retVal = new TemplateEntity();
            retVal.TemplateJsonRow = (string) reader.GetValue(0);
            retVal.HeadName = (string) reader.GetValue(1);
            retVal.UserIdentity = (string) reader.GetValue(2);
            retVal.Rate = (int) reader.GetValue(3);
            retVal.Comments = (string) reader.GetValue(4);
            retVal.RateCounter = (int) reader.GetValue(5);
            retVal.Category = (string) reader.GetValue(6);
            retVal.IsShared = (bool) reader.GetValue(7);
            retVal.RateSum = (int) reader.GetValue(8);
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
                        return getUserFromDB(reader);
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

        private UserEntity getUserFromDB(SqlDataReader reader)
        {
            UserEntity retVal = new UserEntity();
            retVal.FirstName = ((SqlString) reader.GetSqlValue(0)).Value;
            retVal.LastName = ((SqlString) reader.GetSqlValue(1)).Value;
            retVal.Password = ((SqlString) reader.GetSqlValue(2)).Value;
            retVal.Email = ((SqlString) reader.GetSqlValue(3)).Value;
            retVal.CreationTime = ((SqlDateTime) reader.GetSqlValue(4)).Value;
            retVal.FavoriteTemplates = ((SqlString) reader.GetSqlValue(5)).Value;
            retVal.IsAdmin = ((SqlBoolean) reader.GetSqlValue(6)).Value;
            return retVal;
        }

        private string updateTemplateInDB(TemplateEntity iTemplateEntity)
        {
            string retVal = "";
            if (m_connection != null)
            {
                try
                {
                    int isShared = iTemplateEntity.IsShared ? 1 : 0;
                    string insertMessage =
                        $"UPDATE [TemplateCore].[dbo].[{ListOfTables.Templates}] SET {m_TemplateColumns[0]} = '{iTemplateEntity.TemplateJsonRow}', {m_TemplateColumns[1]} = '{iTemplateEntity.HeadName}', {m_TemplateColumns[2]} = '{iTemplateEntity.UserIdentity}', {m_TemplateColumns[3]} = '{iTemplateEntity.Rate}', {m_TemplateColumns[4]} = '{iTemplateEntity.Comments}', {m_TemplateColumns[5]} = '{iTemplateEntity.RateCounter}', {m_TemplateColumns[6]} = '{iTemplateEntity.Category}', {m_TemplateColumns[7]} = '{isShared}', {m_TemplateColumns[8]} = '{iTemplateEntity.RateSum}' WHERE {m_TemplateColumns[6]} = '{iTemplateEntity.Category}' and {m_TemplateColumns[1]} = '{iTemplateEntity.HeadName}'";
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = m_connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insertMessage;
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected < 1)
                        {
                            retVal = "The update to DB falied";
                        }
                        else
                        {
                            retVal = "The update to DB succeeded";
                        }
                    }
                }
                catch (Exception e)
                {
                    retVal = $"The update to DB falied: {e.Message}";
                }
            }
            else
            {
                retVal = "There is no connection to DB therefore did not update the topic";
            }

            return retVal;
        }

        private string updateUserInDB(UserEntity iUserEntity)
        {
            string retVal = "";
            if (m_connection != null)
            {
                try
                {
                    int isAdmin = iUserEntity.IsAdmin ? 1 : 0;
                    string creationTime = iUserEntity.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string insertMessage =
                        $"UPDATE [TemplateCore].[dbo].[{ListOfTables.UserInformation}] SET {m_UserColumns[0]} = '{iUserEntity.FirstName}', {m_UserColumns[1]} = '{iUserEntity.LastName}', {m_UserColumns[2]} = '{iUserEntity.Password}', {m_UserColumns[3]} = '{iUserEntity.Email}', {m_UserColumns[4]} = '{creationTime}', {m_UserColumns[5]} = '{iUserEntity.FavoriteTemplates}', {m_UserColumns[6]} = '{isAdmin}' WHERE {m_UserColumns[3]} = '{iUserEntity.Email}'";
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = m_connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insertMessage;
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected < 1)
                        {
                            retVal = "The update to DB falied";
                        }
                        else
                        {
                            retVal = "The update to DB succeeded";
                        }
                    }
                }
                catch (Exception e)
                {
                    retVal = $"The update to DB falied: {e.Message}";
                }
            }
            else
            {
                retVal = "There is no connection to DB therefore did not update the topic";
            }

            return retVal;
        }

        private string updateTopicHeader(TopicEntity topicEntity, string iNewHeaderName)
        {
            string retVal = "";
            if (m_connection != null)
            {
                try
                {
                    string insertMessage =
                        $"UPDATE [TemplateCore].[dbo].[{ListOfTables.Topic}] SET {m_TopicColumns[1]} = '{iNewHeaderName}' WHERE {m_TopicColumns[0]} = '{topicEntity.Category}' and {m_TopicColumns[1]} = '{topicEntity.Header}'";
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = m_connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insertMessage;
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected < 1)
                        {
                            retVal = "The update to DB falied";
                        }
                    }
                }
                catch (Exception e)
                {
                    retVal = $"The update to DB falied: {e.Message}";
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
                        if (recordsAffected < 1)
                        {
                            retVal = "The insert to DB falied";
                        }
                    }
                }
                catch (Exception e)
                {
                    retVal = $"The insert to DB falied, Exeption: {e.Message}";
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
                    else
                    {
                        if (item.GetType().Equals(typeof(bool)))
                        {
                            if ((bool) item)
                            {
                                item = "'1'";
                            }
                            else
                            {
                                item = "'0'";
                            }
                        }
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