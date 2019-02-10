namespace WebApi.DataModel
{
    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Login) && string.IsNullOrEmpty(Password));
        }
    }
}
