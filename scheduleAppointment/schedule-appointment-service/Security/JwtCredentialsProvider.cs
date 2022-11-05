using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;


namespace schedule_appointment_service.Security
{
    public class JwtCredentialsProvider
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public JwtCredentialsProvider()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        public JwtCredentialsProvider(string path)
        {
            var cypher = File.ReadAllText(path);
            Key = PrivateKeyFromPem(cypher);
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        private RsaSecurityKey PrivateKeyFromPem(string keyPairPem)
        {
            var pemReader = new PemReader(new StringReader(keyPairPem));
            var keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
            var privateKeyParameters = (RsaPrivateCrtKeyParameters)keyPair.Private;
            var rsaParameters = DotNetUtilities.ToRSAParameters(privateKeyParameters);
            return new RsaSecurityKey(rsaParameters);
        }
    }
}