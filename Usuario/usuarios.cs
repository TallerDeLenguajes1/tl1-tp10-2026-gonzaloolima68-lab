namespace EspacioUsuario
{
    public class Address
    {
        public String street { get; set; } = string.Empty;
        public String suite { get; set; } = string.Empty;
        public String city { get; set; } = string.Empty;
        public String zipcode { get; set; } = string.Empty;
    }

    public class Usuarios
    {
        public int Id{get; set;}

        public string Name{get;set;}=string.Empty;
        public string Username{get;set;}=string.Empty;
        public string Email{get;set;}=string.Empty;
        public Address address{get;set;}=new Address();
    }
}