namespace DatabaseInserter
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Account(string username, string pw)
        {
            Username = username;
            Password = pw;
        }
        public Account()
        {
        }
    }
}
