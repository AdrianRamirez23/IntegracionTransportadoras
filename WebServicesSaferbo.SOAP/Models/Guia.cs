using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServicesSaferbo.SOAP.Models
{
    public class Guia
    {
        public string GuiaID { get; set; }
    }
    public class Solicitud
    {
        public List<Guia> listGuias { get; set; }
        public string FechaRecodiga { get; set; }
        public string HoraRecogida { get; set; }
        public string ReferenciaAdicional { get; set; }
    }
}