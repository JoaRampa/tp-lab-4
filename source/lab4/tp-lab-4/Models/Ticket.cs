namespace tp_lab_4.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int afiliadoId { get; set; }
        public Afiliado afiliados { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public bool descripcion { get; set; }
    }
}
