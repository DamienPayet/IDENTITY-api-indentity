using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Di2P1G3.Authentication.SharedKernel
{
    public class Token
    {
        private string Header { get; }
        private string Payload { get; }
        private string Sign { get; }
        public string _Token { get; }

        private X509Certificate2 _certificate;



        /// <summary>
        ///     Constructeur de la classe
        /// </summary>
        /// <param name="payload">Information sur l'utilisateur + validité du token </param>
        public Token(string payload, X509Certificate2 certificate)
        {
            _certificate = certificate;
            Header = StringToBase64UrlEncode("{\"typ\":\"JWT\"}");
            Payload = StringToBase64UrlEncode(payload);
            Sign = generateSignature();
            _Token = Header + "." + Payload + "." + Sign;
        }
        public Token(X509Certificate2 certificate, string token)
        {
            _certificate = certificate;
          
            Sign = generateSignature();
            _Token = token;
        }

        /// <summary>
        ///     Générer la signature du token
        /// </summary>
        /// <returns>La signature</returns>
        private string generateSignature()
        {
            byte[] body = Encoding.ASCII.GetBytes(Header + "." + Payload);
            RSA publicKey = _certificate.GetRSAPublicKey();
            byte[] signature = publicKey.Encrypt(body, RSAEncryptionPadding.Pkcs1);
            return ByteToBase64UrlEncode(signature);
        }
        /// <summary>
        ///     Vérifier l'intégrité du token
        /// </summary>
        /// <param name="token">Token à vérifier</param>
        /// <returns>true = token valide / false = token invalide</returns>
        public bool VerifySignatureToken(string token)
        {
            var splitedToken = token.Split(".");
            var header = splitedToken[0] ;
            var payload = splitedToken[1];
            
            byte[] signature = Convert.FromBase64String(ReplaceBadCharacters(splitedToken[2]));
            if (_certificate.HasPrivateKey)
            {
                try
                {
                    var privateKey = _certificate.GetRSAPrivateKey();
                    byte[] decryptedSignture = privateKey.Decrypt(signature, RSAEncryptionPadding.Pkcs1);
                    return string.Equals(header+"."+payload, Encoding.UTF8.GetString(decryptedSignture));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            return false;
        }
        public bool VerifyExpirationToken(string token)
		{
            try
			{
                var payload = Encoding.UTF8.GetString(Convert.FromBase64String(token.Split('.')[1]));
                var pl = JsonConvert.DeserializeObject<Payload>(payload);
                var currentTime = DateTime.Now;
                if(currentTime.Ticks < pl.ExDate)
                {
                    return true;
				}
			}
			catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return false;

        }
        /// <summary>
        ///     Remplace les caractères pour l'encodage base64
        /// </summary>
        /// <param name="input">chaine à modifier</param>
        /// <returns>chaine corrigé</returns>
        private string ReplaceBadCharacters(string input)
        {
            string token = input.Replace('_', '/').Replace('-', '+');
            switch (token.Length % 4)
            {
                case 2:
                    token += "==";
                    break;
                case 3:
                    token += "=";
                    break;
            }

            return token;
        }
        /// <summary>
        ///     Remplace les caractères pour l'encodage base64URL
        /// </summary>
        /// <param name="input">chaine à modifier</param>
        /// <returns>chaine corrigé</returns>
        private string StringToBase64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
        /// <summary>
        ///     Remplace les caractères pour l'encodage base64URL
        /// </summary>
        /// <param name="input">tableau de byte à modifier </param>
        /// <returns>chaine corrigé</returns>
        private string ByteToBase64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
    public class Payload
    {
        public Guid UserId { get; }
        public long CrDate { get; }
        public long ExDate { get; }
        public TokenType Type { get; }
        [JsonConstructor]
        public Payload(Guid UserId, long CrDate , long ExDate ,  TokenType Type) {
            this.UserId = UserId;
            this.CrDate = CrDate;
            this.ExDate = ExDate;
            this.Type = Type;
        }
        public Payload(TokenType type)
        {
            CrDate = DateTime.Now.Ticks;
            switch (type)
            {
                case (TokenType.Access):
                    ExDate = DateTime.Now.AddMinutes(60).Ticks;
                    break;
                case (TokenType.Bearer):
                    ExDate = DateTime.Now.AddMinutes(30).Ticks;
                    break;
                case (TokenType.Refresh):
                    ExDate = DateTime.Now.AddMinutes(62).Ticks;
                    break;
            }
            Type = type;
        }
        public Payload(Guid userid, TokenType type)
        {
            UserId = userid;
            CrDate = DateTime.Now.Ticks;
            switch (type)
            {
                case (TokenType.Access):
                    ExDate = DateTime.Now.AddMinutes(60).Ticks;
                    break;
                case (TokenType.Bearer):
                    ExDate = DateTime.Now.AddMinutes(1).Ticks;
                    break;
                case (TokenType.Refresh):
                    ExDate = DateTime.Now.AddMinutes(62).Ticks;
                    break;
            }
            Type = type;
        }
    }
    public enum TokenType
    {
        Bearer,
        Access,
        Refresh
    }
}
