using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpenBankingMock.Domain.Models.Entities
{
    public class Usuario
    {
        // TODO: Obviamente esta chave não deve ser hardcoded
        private string Key = "wTc$%+6dsgxvtVPba&44M6=%AfuQwT=2VXQza7JeKwX#J7AvAd52pFYtkRNNF$UWaCdH-few%b4h@2NvE%xeA657VJsv7^ht3j!9y!%R_B&NL?+$VC#wK3Nra&%6RZ$#KSSW5CCtSfCHV!_=g^-u+m3X-e*$St#@jcg=uaRC#tYt-tK5Rv7x7BPC?x4VNJ*A9RU5DfZSMwe^9$euV4TyTHf-VH3&!CZZrNgDyqzDtTw33Rf#QUXTGUBUM&btEVhY";
        public string Cpf { get; set; }
        public string Nome { get; set; }

        internal string GerarTokenAutorizacao(string clientId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateJwtSecurityToken(
                issuer: "https://2dfrango.com",
                audience: "https://2dfrango.com",
                subject: CriarClaims(clientId),
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.Default.GetBytes(Key)),
                        SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity CriarClaims(string clientId)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Cpf));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, Nome));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, clientId));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userData)));

            return claimsIdentity;
        }
    }
}
