using Newtonsoft.Json;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
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

                    int kilos = 0;

                    foreach (Unidades unidad in remdes.Unidades)
                    {
                        kilos = kilos + unidad._pesovol;
                    }

                    string url = ConfigurationManager.AppSettings["URL_Saferbo"] + remdes.idciudadestino + "&origen=" + remdes.idciudadespacho + "&ds_peso=" + kilos + "&ds_valoracion=" + remdes.valordec + "&ds_largo=" + 1 + "&ds_ancho=" + 1 + "&ds_alto=" + 1 + "&ds_pesovol=" + 0 + "&ds_contraentrega=" + remdes._contraentrega + "&tipoacceso=" + 1 + "&tipodatos=" + 2 + "&ds_cantidad=" + remdes.cantidadCajas + "&dscodigocliente=" + ConfigurationManager.AppSettings["dscodigocliente"] + " & idnegociacion=" + 1 + "&idtipoenvio=" + 1 + "&idtipoliquidacion=" + 1 + "&idtarifaxtrayecto=" + remdes.idtarifaxtrayecto + "&dsnitr=" + remdes.nitr + "&dsnombrer=" + remdes.nombrer + "=&dstelr=" + remdes.telr + "&dsdirr=" + remdes.dirr + "&dsunidad=" + remdes.unidades + "&dskilos=" + kilos + "&dsvalordec=" + remdes.valordec + "&dsobs_1=" + remdes.obs_1 + "&dsobs_2=" + remdes.obs_2 + "&dsobs_3=" + remdes.obs_3 + "&dscodigodestinatario=" + 0 + "&dsnitd=" + remdes.nitd + "&dsnombred=" + remdes.nombred + "&dsteld=" + remdes.teld + "&dsdird=" + remdes.dird + "&dsvalorrecaudar=" + remdes.valorrecaudar + "&arunidades=" + 0 + "&idconsec=" + 0 + "&solocotizar=0&generarguia=1";
                    var json = new WebClient().DownloadString(url);
                    string[] response = json.Split('|');
                    string[] rem = response[0].Split('¿');

                    res.guia = rem[2];
                    res.ULR_Rotulo = response[4] + response[1];
                    break;
                case "Servientrega":
                    WebServicesServientrega.GeneracionGuias proGuias = new WebServicesServientrega.GeneracionGuias();

                    WebServicesServientrega.CargueMasivoExternoDTO[] cargueExterno = new WebServicesServientrega.CargueMasivoExternoDTO[1];

                    int CantEnvios = remdes.cantidadCajas;
                    cargueExterno[0] = new WebServicesServientrega.CargueMasivoExternoDTO();
                    cargueExterno[0].objEnvios = new WebServicesServientrega.EnviosExterno[CantEnvios];

                    WebServicesServientrega.AuthHeader objAuth = new WebServicesServientrega.AuthHeader();

                    objAuth.Id_CodFacturacion = ConfigurationManager.AppSettings["Id_CodFacturacion"];
                    objAuth.login = ConfigurationManager.AppSettings["login"];
                    objAuth.Nombre_Cargue = ConfigurationManager.AppSettings["Nombre_Cargue"];
                    objAuth.pwd = ConfigurationManager.AppSettings["pwd"];

                    proGuias.AuthHeaderValue = objAuth;

                    for (int i = 0; i < CantEnvios; i++)
                    {
                        cargueExterno[0].objEnvios[0] = new WebServicesServientrega.EnviosExterno();

                        WebServicesServientrega.EnviosExterno arrEnvios = new WebServicesServientrega.EnviosExterno();
                        arrEnvios.Nom_Contacto = remdes.nombred;
                        arrEnvios.Des_Ciudad = remdes.idciudadestino;
                        arrEnvios.Des_DepartamentoDestino = "";
                        arrEnvios.Num_Piezas = Convert.ToInt32(remdes.unidades);
                        arrEnvios.Des_TipoGuia = 2;
                        arrEnvios.Ide_Producto = 2;
                        arrEnvios.Des_CiudadOrigen = remdes.idciudadespacho;
                        arrEnvios.Des_DepartamentoDestino = ConfigurationManager.AppSettings["deptarmentoDestino"];
                        arrEnvios.Des_Direccion = remdes.dird;
                        arrEnvios.Des_MedioTransporte = 1;
                        arrEnvios.Des_TipoDuracionTrayecto = 1;
                        arrEnvios.Des_Telefono = remdes.teld;
                        arrEnvios.Des_DiceContener = remdes.diceContener;

                        if (remdes._contraentrega)
                        {
                            arrEnvios.Des_FormaPago = 4;
                        }
                        else
                        {
                            arrEnvios.Des_FormaPago = 2;
                        }

                        arrEnvios.Num_ValorDeclaradoTotal = remdes.valordec;
                        arrEnvios.Des_CorreoElectronico = "";
                        arrEnvios.Est_CanalMayorista = false;
                        arrEnvios.Nom_UnidadEmpaque = ConfigurationManager.AppSettings["TipoEmpaqueServientrega"];
                        arrEnvios.Num_Alto = remdes.Unidades[i]._alto;
                        arrEnvios.Num_Ancho = remdes.Unidades[i]._ancho; ;
                        arrEnvios.Num_Largo = remdes.Unidades[i]._largo;
                        arrEnvios.Num_PesoTotal = remdes.Unidades[i]._pesovol;
                        arrEnvios.Num_Factura = remdes.obs_1;
                        arrEnvios.Gen_Sobreporte = false;
                        arrEnvios.Gen_Cajaporte = false;
                        arrEnvios.Des_VlrCampoPersonalizado1 = remdes.obs_2;
                        arrEnvios.Des_UnidadLongitud = "cm";
                        arrEnvios.Des_UnidadPeso = "kg";
                        arrEnvios.Num_Guia = 0;
                        arrEnvios.Doc_Relacionado = remdes.nitd;
                        arrEnvios.Ide_Num_Referencia_Dest = remdes.teld;
                        arrEnvios.Num_Recaudo = remdes.valorrecaudar;
                        cargueExterno[0].objEnvios[0] = arrEnvios;
                    }



                    string[] respuesta = new string[CantEnvios];
                    byte[] bytereport = new byte[CantEnvios];

                    if (proGuias.CargueMasivoExterno(ref cargueExterno, ref respuesta))
                    {
                        int sFormatoImpresionGuia = cargueExterno[0].objEnvios[0].Des_TipoGuia;
                        proGuias.GenerarGuiaSticker(decimal.Parse(respuesta[0].ToString()), decimal.Parse(respuesta[respuesta.Length - 1].ToString()), objAuth.Id_CodFacturacion, sFormatoImpresionGuia, cargueExterno[0].objEnvios[0].Id_ArchivoCargar, false, ref bytereport);
                    }

                    res.guia = respuesta[0];

                    string sfile = ConfigurationManager.AppSettings["rutaGuiaPDF"] + respuesta[0] + ".pdf";
                    File.WriteAllBytes(sfile, bytereport);

                    res.ULR_Rotulo = sfile;


                    break;
                case "Coordinadora":

                    int cantidadEnvio = remdes.cantidadCajas;
                    WebServicesCoordinadora.JRpcServerSoapManagerService webService = new JRpcServerSoapManagerService();

                    WebServicesCoordinadora.Agw_typeGenerarGuiaIn objEnvio = new Agw_typeGenerarGuiaIn();

                    objEnvio.codigo_remision = "";
                    string fechahoy = DateTime.Now.ToString("d");
                    string[] fechaDiv = fechahoy.Split('/');
                    objEnvio.id_cliente = Convert.ToInt32(ConfigurationManager.AppSettings["Id_Cliente"]);
                    objEnvio.id_remitente = 0;
                    objEnvio.nit_remitente = remdes.nitr;
                    objEnvio.nombre_remitente = remdes.nombrer;
                    objEnvio.direccion_remitente = remdes.dirr;
                    objEnvio.telefono_remitente = remdes.telr;
                    objEnvio.ciudad_remitente = remdes.idciudadespacho;
                    objEnvio.nit_destinatario = remdes.nitd;
                    objEnvio.div_destinatario = "";
                    objEnvio.nombre_destinatario = remdes.nombred;
                    objEnvio.direccion_destinatario = remdes.dird;
                    objEnvio.ciudad_destinatario = remdes.idciudadestino;
                    objEnvio.telefono_destinatario = remdes.teld;
                    objEnvio.valor_declarado = Convert.ToSingle(remdes.valordec);
                    objEnvio.codigo_cuenta = 1;
                    objEnvio.codigo_producto = 0;
                    objEnvio.nivel_servicio = 1;
                    objEnvio.linea = "";
                    objEnvio.contenido = remdes.obs_1;
                    objEnvio.referencia = remdes.obs_2;
                    objEnvio.observaciones = remdes.diceContener;
                    objEnvio.estado = "IMPRESO";

                    WebServicesCoordinadora.Agw_typeGuiaDetalle[] items = new Agw_typeGuiaDetalle[cantidadEnvio];
                    WebServicesCoordinadora.Agw_typeGuiaDetalle item = new Agw_typeGuiaDetalle();

                    foreach(Unidades unidad in remdes.Unidades)
                    {
                        item.ubl = 0;
                        item.alto = unidad._alto;
                        item.ancho = unidad._ancho;
                        item.largo = unidad._largo;
                        item.peso = Convert.ToSingle(unidad._pesovol);
                        item.unidades = Convert.ToInt32(remdes.unidades);
                        item.referencia = remdes.obs_1;
                    }


                   
                    item.nombre_empaque = ConfigurationManager.AppSettings["TipoEmpaqueCoordinadora"];

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
                    objEnvio.usuario = ConfigurationManager.AppSettings["usuario"];
                    objEnvio.clave = new Utils.Utilidades().GetSHA256(ConfigurationManager.AppSettings["clave"]);


                    Agw_typeGenerarGuiaOut result = webService.Guias_generarGuia(objEnvio);


                    res.guia = result.codigo_remision;
                    res.ULR_Rotulo = result.url_terceros;

                    break;
            }
            return res;

           
        }
    }
}
