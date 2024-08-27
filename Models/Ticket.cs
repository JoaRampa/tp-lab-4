namespace tp_lab_4.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int AfiliadoId { get; set; }
        public Afiliado? Afiliados { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Observacion { get; set; }
    }
}
