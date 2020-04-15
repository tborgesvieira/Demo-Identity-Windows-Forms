namespace WindowsFormsApp.Model
{
    public class Usuario : Entity
    {
        public string Nomeusuario { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
