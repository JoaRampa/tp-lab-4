using System.Collections.Generic;
using tp_lab_4.Models;

namespace tp_lab_4.ViewsModels
{
    public class TicketViewModel
    {
        public IEnumerable<Ticket> Tickets1 { get; set; }
        public int? AfiliadoId { get; set; }
        public string Observacion1 { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
