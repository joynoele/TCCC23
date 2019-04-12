namespace TCCC23.Library
{
    public class MongoRepo
    {
        public static string Log => "logs";
        public static string Demo => "troubleshootdata";

        public static string MongoConnectionString()
        {
            // get usernme and password from somewhere else for non-local instances
            return @"mongodb://localhost/TCCC23";
        }
    }
}
