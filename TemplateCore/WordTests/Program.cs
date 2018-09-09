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
                //object[] userValues = { "or", "horovitz", "2244", "lvvsavasfa@gmail.com", 126 };
                //Console.WriteLine(DataBaseFactory.GetDbInstance().CreateNewUser(userValues));

                object[] templateValues = { 65447, "Guy", 126 };
                Console.WriteLine(DataBaseFactory.GetDbInstance().CreateNewTemplate(templateValues));
                Console.WriteLine();
                
                //SELECT
                Dictionary<string, object> userInformation = DataBaseFactory.GetDbInstance().GetUser(125);
                Console.WriteLine();
                */

                //DELETE
                Console.WriteLine(DataBaseFactory.GetDbInstance().DeleteTemplate(65447));
                Console.WriteLine();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}