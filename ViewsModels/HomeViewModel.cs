using tp_lab_4.Models;

namespace tp_lab_4.ViewsModels
{
    public class HomeViewModel
    {
        public IEnumerable<TicketDetalle> TicketDt1 { get; set; }
        public int TicketId1 {  get; set; }
        public int EstadosId1 { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
