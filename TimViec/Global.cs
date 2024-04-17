namespace TimViec
{
    public class Global
    {
        private static string _id_job = "";
        private static string _email = "";

        public static string id_job
        {
            get { return _id_job; }
            set { _id_job = value; }
        }
        public static string email
        {
            get { return _email; }
            set { _email = value; }
        }

    }
}
