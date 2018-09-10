using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCoreBusiness.Word;
using TemplateCoreBusiness.Database;
using TemplateCoreBusiness.Engine;
using TemplateCoreBusiness.Models;

namespace WordTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test.BasicTest();

            try
            {

                //INSERT
                //Console.WriteLine(UserEngineBuilder.GetUserEngine().RegisterNewUser("shani", "vds", "vds44", "lvnfasfa@gmail.com"));
                
                //object[] templateValues = { 65447, "Guy", 126 };
                //Console.WriteLine(DataBaseFactory.GetDbInstance().CreateNewTemplate(templateValues));
                //Console.WriteLine();
                
                //SELECT
                UserEntity userInformation = UserEngineBuilder.GetUserEngine().LogInUser("lvnfasfa@gmail.com", "vds44");
                Console.WriteLine();

                /*
                //DELETE
                Console.WriteLine(DataBaseFactory.GetDbInstance().DeleteTemplate(65447));
                Console.WriteLine();
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}