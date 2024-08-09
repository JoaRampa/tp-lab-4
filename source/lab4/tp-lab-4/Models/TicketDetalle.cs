namespace tp_lab_4.Models
{
    public class TicketDetalle
    {
        public int Id { get; set; }
        public Ticket tickets { get; set; }
        public string descripcionPedido { get; set; }
        public bool estados { get; set; }
        public DateTime fechaEstado { get; set; }
    }
}
