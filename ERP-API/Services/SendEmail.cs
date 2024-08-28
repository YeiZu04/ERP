using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ERP_API.Services
{
    public class SendEmail
    {
        private readonly HttpClient _client;
        private readonly string apiKey;

        // Inyección de dependencias para HttpClient
        public SendEmail(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://api.zerobounce.net/v2/");
            apiKey = "663b6f903a954b77a14e030e3ea1560b";
        }

        public async Task<bool> VerifyEmailExists(string email)
        {
            bool result = false;
            string ipAddress = GetClientIPAddress();
            string status = "";

            string requestUrl = $"validate?api_key={apiKey}&email={email}&ip_address={ipAddress}";

            HttpResponseMessage response = await _client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonResponse);

                status = (string)json["status"];
                if (status == "valid")
                {
                    result = true;
                }
            }

            return result;
        }

        private string GetClientIPAddress()
        {
            var ipAddress = _client.DefaultRequestHeaders.GetValues("X-Forwarded-For").FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = "127.0.0.1"; // Valor predeterminado en caso de que no se pueda recuperar la IP
            }

            return ipAddress;
        }
        
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            bool isEmailValid = await VerifyEmailExists(toEmail);

            if (!isEmailValid)
            {
                // Log o manejar el caso donde el correo no es válido
                throw new Exception("El correo no existe");
                return;
            }

            string fromEmail = "studymateuniversidadnacional@gmail.com";
            string fromName = "Studymate";
            string fromPassword = "llgqlatqqarkvvwh"; // Token de autenticación de la aplicación
            //string fromPassword = "studymate123.";

            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, fromPassword); // Usa el correo y el token
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true, // Si el cuerpo del mensaje contiene HTML
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }

    }
}
