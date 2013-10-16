using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;

namespace API_IOB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Cursos";

            request = WebRequest.Create("http://api.iobconcursos.com");
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = "nome=SEUNOME&key=SUAKEY";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            /////////////////////////////////////////////////////////////////////////
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            ViewBag.api = responseFromServer;

            // XML
            string lista = "";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer); //Carregando o arquivo

            //Pegando elemento pelo nome da TAG
            XmlNodeList xnList = xmlDoc.GetElementsByTagName("curso");

            //Usando foreach para imprimir na tela
            foreach (XmlNode xn in xnList)
            {
                // string sNome = xn["nome"].InnerText;
                // string sEmail = xn["email"].InnerText;

                lista = lista + "<li><a href='" + xn["url_compra"].InnerText + "'>" + xn["titulo_curso"].InnerText + "</a></li>";
            }

            ViewBag.api = lista;

            return View();
        }

        public WebRequest request { get; set; }
    }
}
