namespace TemplateCore.Models.DB
{
    public class DataBaseFactory
    {
        public static IDataBase GetDbInstance()
        {
            return new DbTempImp();
        }
    }
}