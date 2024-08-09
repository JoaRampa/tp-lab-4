namespace tp_lab_4.Models
{
    public class Afiliado
    {
        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int DNI { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string? foto { get; set; }
    }
}
