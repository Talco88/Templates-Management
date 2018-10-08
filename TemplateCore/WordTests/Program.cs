using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
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
                //CreateAdminUsers();
                //CreateTopics();
                //string jsonValue = "{\"Template\": \" this is a $firstName $lastName \n $FriendFirstName $FriendLastName\", \"numberOfChanges\": 4}";
                //AppEngineBuilder.GetAppEngine().CreateNewTemplate(jsonValue, "יום הולדת", "orho@gmail.com", "ברכות");
                //string jsonValue = "{\"Template\": \" this is a $firstName $lastName \n $FriendFirstName $FriendLastName\", \"numberOfChanges\": 4}";
                //AppEngineBuilder.GetAppEngine().CreateNewTemplate(jsonValue, "יום הולדת", "orho@gmail.com", "ברכות");
                /*
                AppEngineBuilder.GetAppEngine().CreateNewTemplate("טל", "שלטים", "talCo@gmail.com", "כללי");
                
                //Console.WriteLine();
                //Console.WriteLine(UserEngineBuilder.GetUserEngine().RegisterNewUser("Guy", "tvil", "0542021405", "guy@gmail.com"));
                */

                //SELECT
                //UserEntity userInformation = UserEngineBuilder.GetUserEngine().LogInUser("chen@gmail.com", "03021991");
                //Console.WriteLine();
                /*
                List<string> templateNames = AppEngineBuilder.GetAppEngine().GetTemplateFromSearch("שלט",false, "orchen@gmail.com");
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

                //DELETE
                /*
                Console.WriteLine(AppEngineBuilder.GetAppEngine().DeleteTemplate("כללי", "שלט חוצות", "chen@gmail.com"));
                Console.WriteLine();
                
                Console.WriteLine(AppEngineBuilder.GetAppEngine().DeleteTopic("ברכות", "בר מצווה"));
                Console.WriteLine();
                

                Console.WriteLine(DataBaseFactory.GetDbInstance().DeleteAllTable("Topic"));
                Console.WriteLine();
                */

                //Update
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().GenerateHTMLTemplateWithValues(CreateTemplateFormation()));
                //Console.WriteLine();

                //TemplateFormation templateFormation = AppEngineBuilder.GetAppEngine().GetTemplate("ברכות", "יום הולדת");
                //Console.WriteLine();
                /*
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().RateTamplate("כללי", "טלוויזיה", 2));
                //Console.WriteLine();

                //Console.WriteLine(AppEngineBuilder.GetAppEngine().AddCommentToTemplate("כללי", "טלוויזיה", "chen@gmail.com", "ממליצה בחום"));
                //Console.WriteLine();
                
                Console.WriteLine(AppEngineBuilder.GetAppEngine().AddCommentToTemplate("כללי", "טלוויזיה", "orho@gmail.com", "אכן template מעולה"));
                Console.WriteLine();
                
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().MarkTemplateAsFavorite("כללי", "טלוויזיה", "orho@gmail.com"));
                //Console.WriteLine();

                Console.WriteLine(AppEngineBuilder.GetAppEngine().RemoveMarkTemplateAsFavorite("כללי", "טלוויזיה", "orho@gmail.com"));
                Console.WriteLine();
                 
                Console.WriteLine(AppEngineBuilder.GetAppEngine().SetSharedTemplate("כללי", "שלט חוצות", "chen@gmail.com", true));
                Console.WriteLine();
                */

                //Office
                //Console.WriteLine(AppEngineBuilder.GetAppEngine().OpenTemplateInWord("shani", "<p  align=\"right\" style=\"font-size:20px; color:green;\">אני <b>ושני </b>גדולים.</p><p align=\"left\" style=\"font-size:16px; color:blueViolet;\"><b>This text</b> is <b>bold or.</b></p><p align=\"left\" style=\"font-size:12px; color:blue; \">YESSSSSSS\nYOOOOOOOOOHOOOOOOOO</p>"));
                //Console.WriteLine();
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
                .RegisterNewUser("Or", "Horovitz", "orho@gmail.com", "25011991", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Tal", "cohen", "talCo@gmail.com", "1234", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Shani", "Somech", "Shani@gmail.com", "1245", true));
            Console.WriteLine(UserEngineBuilder.GetUserEngine()
                .RegisterNewUser("Nati", "Lehrer", "Nati@gmail.com", "1246", true));
        }

        private static TemplateFormation CreateTemplateFormation()
        {
            TemplateFormation retVal = new TemplateFormation();
            retVal.CategoryName = "ברכות";
            retVal.HeaderName = "יום הולדת";
            retVal.Values = createWebDataContainerList();

            return retVal;
        }

        private static List<WebDataContainer> createWebDataContainerList()
        {
            List<WebDataContainer> retVal = new List<WebDataContainer>();
            WebDataContainer webDataContainer = new WebDataContainer();
            webDataContainer.Name = "firstName";
            webDataContainer.Value = "or";
            retVal.Add(webDataContainer);

            WebDataContainer webDataContainer1 = new WebDataContainer();
            webDataContainer1.Name = "lastName";
            webDataContainer1.Value = "horovitz";
            retVal.Add(webDataContainer1);

            WebDataContainer webDataContainer2 = new WebDataContainer();
            webDataContainer2.Name = "FriendFirstName";
            webDataContainer2.Value = "tal";
            retVal.Add(webDataContainer2);

            WebDataContainer webDataContainer3 = new WebDataContainer();
            webDataContainer3.Name = "FriendLastName";
            webDataContainer3.Value = "cohen";
            retVal.Add(webDataContainer3);

            return retVal;
        }
    }
}