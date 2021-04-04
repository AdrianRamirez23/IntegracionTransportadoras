using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServicesSaferbo.SOAP.Models
{
    public class RemitenteDestinario
    {
        public string TipoTransportadora { get; set; }
        public string nitr { get; set; }
        public string nombrer { get; set; }
        public string telr { get; set; }
        public string dirr { get; set; } 
        public string idciudadespacho { get; set; }
        public string idciudadestino { get; set; }
        public List<Unidades> Unidades { get; set; }
        public bool _contraentrega { get; set; }
        public decimal unidades { get; set; }
        public decimal valordec { get; set; }
        public string diceContener { get; set; }
        public string  obs_1 { get; set; }
        public string  obs_2 { get; set; }
        public string  obs_3 { get; set; }
        public string nitd { get; set; }
        public string nombred { get; set; }
        public string teld { get; set; }
        public string dird { get; set; }
        public string valorrecaudar { get; set; }
        public int cantidadCajas { get; set; }

    }
    public class Unidades
    {
        public int _largo { get; set; }
        public int _ancho { get; set; }
        public int _alto { get; set; }
        public int _pesovol { get; set; }
    }
}