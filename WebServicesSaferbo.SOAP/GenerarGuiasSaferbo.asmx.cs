using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using WebServicesSaferbo.SOAP.Models;

namespace WebServicesSaferbo.SOAP
{
    /// <summary>
    /// Descripción breve de GenerarGuiasSaferbo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class GenerarGuiasSaferbo : System.Web.Services.WebService
    {

        [WebMethod]
        public Respuesta GenerarGuias(RemitenteDestinario remdes)
        {
            string url = "https://app.saferbo.com/webservices/ws.generar.guias.directo.php?destino=" + remdes.idciudadestino + "&origen=" + remdes.idciudadespacho + "&ds_peso=" + remdes.dskilos + "&ds_valoracion=" + remdes.dsvalordec + "&ds_largo=" + remdes.ds_largo + "&ds_ancho=" + remdes.ds_ancho + "&ds_alto=" + remdes.ds_alto + "&ds_pesovol=" + remdes.ds_pesovol + "&ds_contraentrega=" + remdes.ds_contraentrega + "&tipoacceso=" + remdes.tipoacceso + "&tipodatos=" + remdes.tipodatos + "&ds_cantidad=" + remdes.dsunidad + "&dscodigocliente=" + remdes.dscodigocliente + "&idnegociacion=" + remdes.idnegociacion + "&idtipoenvio=" + remdes.idtipoenvio + "&idtipoliquidacion=" + remdes.idtipoliquidacion + "&idtarifaxtrayecto=" + remdes.idtarifaxtrayecto + "&dsnitr=" + remdes.dsnitr + "&dsnombrer=" + remdes.dsnombrer + "=&dstelr=" + remdes.dstelr + "&dsdirr=" + remdes.dsdirr + "&dsunidad=" + remdes.dsunidad + "&dskilos=" + remdes.dskilos + "&dsvalordec=" + remdes.dsvalordec + "&dsobs_1=" + remdes.dsobs_1 + "&dsobs_2=" + remdes.dsobs_2 + "&dsobs_3=" + remdes.dsobs_3 + "&dscodigodestinatario=" + remdes.dscodigodestinatario + "&dsnitd=" + remdes.dsnitd + "&dsnombred=" + remdes.dsnombred + "&dsteld=" + remdes.dsteld + "&dsdird=" + remdes.dsdird + "&dsvalorrecaudar=" + remdes.dsvalorrecaudar + "&arunidades=" + remdes.arunidades+"&idconsec="+remdes.idconsec+"&solocotizar="+remdes.solocotizar+"&generarguia="+remdes.generarguia+"";
            var json = new WebClient().DownloadString(url);

            string[] response = json.Split('|');
            string[] rem = response[0].Split('¿');


            Respuesta res = new Respuesta();

            res.guia = rem[2];
            res.ULR_Rotulo = response[4] + response[1];

         

            return res;
        }
    }
}
