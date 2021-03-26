using Newtonsoft.Json;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using WebServicesSaferbo.SOAP.Models;
using WebServicesSaferbo.SOAP.WebServicesCoordinadora;

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
            Respuesta res = new Respuesta();
            switch (remdes.TipoTransportadora)
            {
                case "Saferbo":
                    string url = "https://app.saferbo.com/webservices/ws.generar.guias.directo.php?destino=" + remdes.idciudadestino + "&origen=" + remdes.idciudadespacho + "&ds_peso=" + remdes.dskilos + "&ds_valoracion=" + remdes.dsvalordec + "&ds_largo=" + remdes.ds_largo + "&ds_ancho=" + remdes.ds_ancho + "&ds_alto=" + remdes.ds_alto + "&ds_pesovol=" + remdes.ds_pesovol + "&ds_contraentrega=" + remdes.ds_contraentrega + "&tipoacceso=" + remdes.tipoacceso + "&tipodatos=" + remdes.tipodatos + "&ds_cantidad=" + remdes.dsunidad + "&dscodigocliente=" + remdes.dscodigocliente + "&idnegociacion=" + remdes.idnegociacion + "&idtipoenvio=" + remdes.idtipoenvio + "&idtipoliquidacion=" + remdes.idtipoliquidacion + "&idtarifaxtrayecto=" + remdes.idtarifaxtrayecto + "&dsnitr=" + remdes.dsnitr + "&dsnombrer=" + remdes.dsnombrer + "=&dstelr=" + remdes.dstelr + "&dsdirr=" + remdes.dsdirr + "&dsunidad=" + remdes.dsunidad + "&dskilos=" + remdes.dskilos + "&dsvalordec=" + remdes.dsvalordec + "&dsobs_1=" + remdes.dsobs_1 + "&dsobs_2=" + remdes.dsobs_2 + "&dsobs_3=" + remdes.dsobs_3 + "&dscodigodestinatario=" + remdes.dscodigodestinatario + "&dsnitd=" + remdes.dsnitd + "&dsnombred=" + remdes.dsnombred + "&dsteld=" + remdes.dsteld + "&dsdird=" + remdes.dsdird + "&dsvalorrecaudar=" + remdes.dsvalorrecaudar + "&arunidades=" + remdes.arunidades + "&idconsec=" + remdes.idconsec + "&solocotizar=" + remdes.solocotizar + "&generarguia=" + remdes.generarguia + "";
                    var json = new WebClient().DownloadString(url);
                    string[] response = json.Split('|');
                    string[] rem = response[0].Split('¿');

                    res.guia = rem[2];
                    res.ULR_Rotulo = response[4] + response[1];
                    break;
                case "Servientrega":
                    WebServicesServientrega.GeneracionGuias proGuias = new WebServicesServientrega.GeneracionGuias();

                    WebServicesServientrega.CargueMasivoExternoDTO[] cargueExterno = new WebServicesServientrega.CargueMasivoExternoDTO[1];

                    int CantEnvios = 1;
                    cargueExterno[0] = new WebServicesServientrega.CargueMasivoExternoDTO();
                    cargueExterno[0].objEnvios = new WebServicesServientrega.EnviosExterno[CantEnvios];

                    WebServicesServientrega.AuthHeader objAuth = new WebServicesServientrega.AuthHeader();

                    objAuth.Id_CodFacturacion = "SER408";
                    objAuth.login = "Luis1937";
                    objAuth.Nombre_Cargue = "CARGA INTREGRACIÓN BATA";
                    objAuth.pwd = "MZR0zNqnI/KplFlYXiFk7m8/G/Iqxb3O";

                    proGuias.AuthHeaderValue = objAuth;

                    cargueExterno[0].objEnvios[0] = new WebServicesServientrega.EnviosExterno();

                    WebServicesServientrega.EnviosExterno arrEnvios = new WebServicesServientrega.EnviosExterno();
                    arrEnvios.Nom_Contacto = remdes.dsnombred;
                    arrEnvios.Des_Ciudad = remdes.idciudadestino;
                    arrEnvios.Des_DepartamentoDestino = "";
                    arrEnvios.Num_Piezas = Convert.ToInt32(remdes.dsunidad);
                    arrEnvios.Des_TipoGuia = 2;
                    arrEnvios.Ide_Producto = 2;
                    arrEnvios.Des_CiudadOrigen = remdes.idciudadespacho;
                    arrEnvios.Des_DepartamentoDestino = "CALDAS";
                    arrEnvios.Des_Direccion = remdes.dsdird;
                    arrEnvios.Des_MedioTransporte = 1;
                    arrEnvios.Des_TipoDuracionTrayecto = 1;
                    arrEnvios.Des_Telefono = remdes.dsteld;
                    arrEnvios.Des_DiceContener = "CALZADO";
                    arrEnvios.Des_FormaPago = 2;
                    arrEnvios.Num_ValorDeclaradoTotal = remdes.dsvalordec;
                    arrEnvios.Des_CorreoElectronico = "";
                    arrEnvios.Est_CanalMayorista = false;
                    arrEnvios.Nom_UnidadEmpaque = "GENERICA";
                    arrEnvios.Num_Alto = remdes.ds_alto;
                    arrEnvios.Num_Ancho = remdes.ds_ancho;
                    arrEnvios.Num_Largo = remdes.ds_largo;
                    arrEnvios.Num_Largo = remdes.ds_largo;
                    arrEnvios.Num_PesoTotal = remdes.dskilos;
                    arrEnvios.Num_Factura = remdes.dsobs_1;
                    arrEnvios.Gen_Sobreporte = false;
                    arrEnvios.Gen_Cajaporte = false;
                    arrEnvios.Des_VlrCampoPersonalizado1 = remdes.dsobs_2;
                    arrEnvios.Des_UnidadLongitud = "cm";
                    arrEnvios.Des_UnidadPeso = "kg";
                    arrEnvios.Num_Guia = 0;
                    arrEnvios.Doc_Relacionado = remdes.dsnitd;
                    arrEnvios.Ide_Num_Referencia_Dest = remdes.dsteld;
                    cargueExterno[0].objEnvios[0] = arrEnvios;

                    string[] respuesta = new string[CantEnvios];
                    byte[] bytereport = new byte[CantEnvios];

                    if (proGuias.CargueMasivoExterno(ref cargueExterno, ref respuesta))
                    {
                        int sFormatoImpresionGuia = cargueExterno[0].objEnvios[0].Des_TipoGuia;
                        proGuias.GenerarGuiaSticker(decimal.Parse(respuesta[0].ToString()), decimal.Parse(respuesta[respuesta.Length - 1].ToString()), objAuth.Id_CodFacturacion, sFormatoImpresionGuia, cargueExterno[0].objEnvios[0].Id_ArchivoCargar, false, ref bytereport);
                    }

                    res.guia = respuesta[0];

                    string sfile = "d:\\Guias\\reportSticket_" + respuesta[0] + ".pdf";
                    File.WriteAllBytes(sfile, bytereport);

                    res.ULR_Rotulo = sfile;


                    break;
                case "Coordinadora":

                    int cantidadEnvio = 1;
                    WebServicesCoordinadora.JRpcServerSoapManagerService webService = new  JRpcServerSoapManagerService();

                    WebServicesCoordinadora.Agw_typeGenerarGuiaIn objEnvio = new Agw_typeGenerarGuiaIn();

                    objEnvio.codigo_remision = "";
                    string fechahoy = DateTime.Now.ToString("d");
                    string[] fechaDiv = fechahoy.Split('/');
                    objEnvio.id_cliente = 26539;
                    objEnvio.id_remitente =0 ;
                    objEnvio.nit_remitente=remdes.dsnitr;
                    objEnvio.nombre_remitente=remdes.dsnombrer;
                    objEnvio.direccion_remitente = remdes.dsdirr;
                    objEnvio.telefono_remitente = remdes.dstelr;
                    objEnvio.ciudad_remitente = remdes.idciudadespacho;
                    objEnvio.nit_destinatario = remdes.dsnitd;
                    objEnvio.div_destinatario = "";
                    objEnvio.nombre_destinatario = remdes.dsnombred;
                    objEnvio.direccion_destinatario = remdes.dsdird;
                    objEnvio.ciudad_destinatario = remdes.idciudadestino;
                    objEnvio.telefono_destinatario = remdes.dsteld;
                    objEnvio.valor_declarado = Convert.ToSingle(remdes.dsvalordec);
                    objEnvio.codigo_cuenta = 1;
                    objEnvio.codigo_producto = 0;
                    objEnvio.nivel_servicio = 1;
                    objEnvio.linea = "";
                    objEnvio.contenido = remdes.dsobs_1;
                    objEnvio.referencia = remdes.dsobs_2;
                    objEnvio.observaciones = "PRUEBA";
                    objEnvio.estado = "IMPRESO";

                    WebServicesCoordinadora.Agw_typeGuiaDetalle[] items = new Agw_typeGuiaDetalle[cantidadEnvio];
                    WebServicesCoordinadora.Agw_typeGuiaDetalle item = new Agw_typeGuiaDetalle();

                    item.ubl = 0;
                    item.alto = remdes.ds_alto;
                    item.ancho = remdes.ds_ancho;
                    item.largo = remdes.ds_largo;
                    item.peso = Convert.ToSingle(remdes.dskilos);
                    item.unidades = Convert.ToInt32(remdes.dsunidad);
                    item.referencia = remdes.dsobs_1;
                    item.nombre_empaque = "CAJA";

                    items[0] = item;

                    objEnvio.detalle = items;

                    objEnvio.cuenta_contable = "";
                    objEnvio.centro_costos = "";
                    objEnvio.recaudos = new Agw_typeGuiaDetalleRecaudo[cantidadEnvio];
                    objEnvio.margen_izquierdo =2;
                    objEnvio.margen_superior = 2;
                    objEnvio.usuario_vmi = "";
                    objEnvio.formato_impresion = "";
                    objEnvio.atributo1_nombre = "";
                    objEnvio.atributo1_valor = "";
                    objEnvio.notificaciones = new Agw_typeNotificaciones[cantidadEnvio];

                    WebServicesCoordinadora.Agw_typeAtributosRetorno atributo = new Agw_typeAtributosRetorno();

                    atributo.nit = "";
                    atributo.div = "";
                    atributo.nombre = "";
                    atributo.direccion = "";
                    atributo.codigo_ciudad = "";
                    atributo.telefono = "";

                    objEnvio.atributos_retorno = atributo;

                    objEnvio.nro_doc_radicados = "";
                    objEnvio.nro_sobre = "";
                    objEnvio.codigo_vendedor = 0;
                    objEnvio.usuario = "manisol.ws680";
                    objEnvio.clave = "64ac62020a0e515edff284c9fca6ef5039011b917998661fb6ae30c6b8b6d02c";


                    Agw_typeGenerarGuiaOut result = webService.Guias_generarGuia(objEnvio);


                    res.guia = result.codigo_remision;
                    res.ULR_Rotulo = result.url_terceros;

                    break;
            }
            return res;

           
        }
    }
}
