using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServicesSaferbo.SOAP.Models
{
    public class RemitenteDestinario
    {
        public string dscodigocliente { get; set; }
        public string dsnitr { get; set; }
        public string dsnombrer { get; set; }
        public string dstelr { get; set; }
        public string dsdirr { get; set; }
        public int idtipoenvio { get; set; }
        public int idtipoliquidacion { get; set; }
        public int idnegociacion { get; set; }
        public string idciudadespacho { get; set; }
        public string idciudadestino { get; set; }
        public int idtarifaxtrayecto { get; set; }
        public int ds_largo { get; set; }
        public int ds_ancho { get; set; }
        public int ds_alto { get; set; }
        public int ds_pesovol { get; set; }
        public int ds_contraentrega { get; set; }
        public int tipoacceso { get; set; }
        public int tipodatos { get; set; }
        public decimal dsunidad { get; set; }
        public decimal dskilos { get; set; }
        public decimal dsvalordec { get; set; }
        public string  dsobs_1 { get; set; }
        public string  dsobs_2 { get; set; }
        public string  dsobs_3 { get; set; }
        public string dscodigodestinatario { get; set; }
        public string dsnitd { get; set; }
        public string dsnombred { get; set; }
        public string dsteld { get; set; }
        public string dsdird { get; set; }
        public string dsvalorrecaudar { get; set; }
        public string arunidades { get; set; }
        public string idconsec { get; set; }
        public string solocotizar { get; set; }
        public string generarguia { get; set; }

    }
}