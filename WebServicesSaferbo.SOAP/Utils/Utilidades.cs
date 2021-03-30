using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace WebServicesSaferbo.SOAP.Utils
{
    public class Utilidades
    {
        public  string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public void modificarWebConfigRemision(long codRemision)
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach(XmlElement element in XmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach(XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "Codigo_remision")
                        {
                            node.Attributes[1].Value = codRemision.ToString();
                        }
                    }
                }
            }
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}