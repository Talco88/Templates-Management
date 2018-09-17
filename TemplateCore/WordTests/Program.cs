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
                //CreateTopics();
                 
               //AppEngineBuilder.GetAppEngine().CreateNewTemplate("גמגמ", "טלוויזיה", "chen@gmail.com", "כללי");
               //Console.WriteLine();
                /* 
               Console.WriteLine(UserEngineBuilder.GetUserEngine().RegisterNewUser("Chen", "Maimaran", "0542021405", "chen@gmail.com"));
               CreateAdminUsers();
               */

                /*
                //SELECT
                UserEntity userInformation = UserEngineBuilder.GetUserEngine().LogInUser("chen@gmail.com", "03021991");
                Console.WriteLine();
                
                List<string> templateNames = AppEngineBuilder.GetAppEngine().GetTemplateFromSearch("Guy");
                Console.WriteLine();
                
                List<TopicEntity> listEntities = AppEngineBuilder.GetAppEngine().GetAllTopics();
                Console.WriteLine();

                List<string> headersList = AppEngineBuilder.GetAppEngine().GetTopicsInCategory("מסמכים");
                Console.WriteLine();

                List<string> topicsList = AppEngineBuilder.GetAppEngine().GetTopicsNames();
                Console.WriteLine();
                 

                UserEntity userInformation = UserEngineBuilder.GetUserEngine().GetUserData("chen@gmail.com");
                Console.WriteLine();
                */
                /*
                //DELETE
                Console.WriteLine(AppEngineBuilder.GetAppEngine().DeleteTemplate(65447));
                Console.WriteLine();
                
                Console.WriteLine(AppEngineBuilder.GetAppEngine().DeleteTopic("ברכות", "בר מצווה"));
                Console.WriteLine();
                

                Console.WriteLine(DataBaseFactory.GetDbInstance().DeleteAllTable("Topic"));
                Console.WriteLine();
                */

                //Update
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().RateTamplate("כללי", "טלוויזיה", 2));
                //Console.WriteLine();

                Console.WriteLine(AppEngineBuilder.GetAppEngine().AddCommentToTemplate("כללי", "טלוויזיה", "chen@gmail.com", "ממליצה בחום"));
                Console.WriteLine();

                Console.WriteLine(AppEngineBuilder.GetAppEngine().AddCommentToTemplate("כללי", "טלוויזיה", "orho@gmail.com", "אכן template מעולה"));
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CreateTopics()
        {
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("ברכות", "הולדת הבן"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("ברכות", "הולדת הבת"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("ברכות", "יום הולדת"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("ברכות", "שנה טובה"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("ברכות", "בר מצווה"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("ברכות", "בת מצווה"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("מסמכים", "חוזה עבודה"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("מסמכים", "חוזה שכר דירה"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("מסמכים", "תביעה"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("כללי", "מייל לעובדים"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("כללי", "דפוס חוזר"));
            Console.WriteLine(AppEngineBuilder.GetAppEngine().CreateNewTopic("כללי", "דף שער"));
        }

        private static void CreateAdminUsers()
        {
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Or", "Horovitz", "25011991", "orho@gmail.com", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Tal", "cohen", "1234", "talCo@gmail.com", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Shani", "Somech", "1245", "Shani@gmail.com", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Nati", "Lehrer", "1246", "Nati@gmail.com", true));
        }
    }
}