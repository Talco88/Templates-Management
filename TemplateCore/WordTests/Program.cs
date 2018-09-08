using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Word;
using TemplateCoreBusiness.Database;

namespace WordTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test.BasicTest();

            try
            {
                /*
                //INSERT
                object[] userValues = { "or", "horovitz", "2244", "lvvsavasfa@gmail.com", 126 };
                Console.WriteLine(DbTempImp.GetInstance.CreateNewUser(userValues));

                object[] templateValues = { 65445, "horovitz", 126 };
                Console.WriteLine(DbTempImp.GetInstance.CreateNewTemplate(templateValues));
                */
                
                //SELECT
                Dictionary<string, object> userInformation = DbTempImp.GetInstance.GetUser(125);
                Console.WriteLine();
                /*
                //DELETE
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = cnn; // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        "DELETE from [TemplateCore].[dbo].[Templates] WHERE Id = 234";
                    int recordsAffected = command.ExecuteNonQuery();
                    if (recordsAffected == 1)
                    {
                        Console.WriteLine("Delete succeeded");
                    }
                    else
                    {
                        Console.WriteLine("Delete failed");
                    }
                    
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Can not open connection ! ");
            }
        }
    }
}