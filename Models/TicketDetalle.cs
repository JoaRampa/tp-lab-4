namespace tp_lab_4.Models
{
    public class TicketDetalle
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket? Tickets { get; set; }
        public string DescripcionPedido { get; set; }
        public int EstadosId { get; set; }
        public Estado? Estados { get; set; }
        public DateTime fechaEstado { get; set; }
    }
}
