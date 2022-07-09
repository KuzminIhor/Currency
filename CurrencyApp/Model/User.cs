namespace CurrencyApp.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsBankUser { get; set; } = false;
        public Bank Bank { get; set; }
    }
}
