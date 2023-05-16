using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace TemperatureService
{
    public static class TemperatureConversion
    {
        public static double CelsiusToFahrenheit(string temperature)
        {
            string resultString = null;

            HttpWebRequest request = CreateSoapRequest();
            string soapStr = @"<?xml version=""1.0"" encoding=""utf-8""?>
                               <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                   <soap:Body>
                                       <CelsiusToFahrenheit xmlns=""https://www.w3schools.com/xml/"">
                                           <Celsius>{0}</Celsius>
                                       </CelsiusToFahrenheit>
                                   </soap:Body>
                               </soap:Envelope>";

            soapStr = string.Format(soapStr, temperature);

            using (Stream stream = request.GetRequestStream())
            {
                using StreamWriter writer = new(stream);
                writer.Write(soapStr);
            }

            try
            {
                using WebResponse response = request.GetResponse();
                using StreamReader reader = new(response.GetResponseStream());
                resultString = reader.ReadToEnd();

                XDocument xml = XDocument.Parse(resultString);
                var soapResponse = xml.Descendants()
                                        .Where(x => x.Name.LocalName == "CelsiusToFahrenheitResponse")
                                        .Select(x => x.Value).SingleOrDefault();

                return Convert.ToDouble(soapResponse);
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        public static double FahrenheitToCelsius(string temperature)
        {
            string resultString = null;

            HttpWebRequest request = CreateSoapRequest();
            string soapStr = @"<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                                   <soap12:Body>
                                       <FahrenheitToCelsius xmlns=""https://www.w3schools.com/xml/"">
                                           <Fahrenheit>{0}</Fahrenheit>
                                       </FahrenheitToCelsius>
                                   </soap12:Body>
                               </soap12:Envelope>";

            soapStr = string.Format(soapStr, temperature);

            using (Stream stream = request.GetRequestStream())
            {
                using StreamWriter writer = new(stream);
                writer.Write(soapStr);
            }

            try
            {
                using WebResponse response = request.GetResponse();
                using StreamReader reader = new(response.GetResponseStream());
                resultString = reader.ReadToEnd();

                XDocument xml = XDocument.Parse(resultString);
                var soapResponse = xml.Descendants()
                                        .Where(x => x.Name.LocalName == "FahrenheitToCelsiusResponse")
                                        .Select(x => x.Value).SingleOrDefault();

                return Convert.ToDouble(soapResponse);
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static HttpWebRequest CreateSoapRequest()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Tls13;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.w3schools.com/xml/tempconvert.asmx");

            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "Post";

            return request;
        }
    }
}
