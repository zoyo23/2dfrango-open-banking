using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OpenBankingMock.Domain.Models.Entities
{
    public class Usuario
    {
        // TODO: Obviamente o código de geração e validação do token não devem estar aqui
        private static string KeyCodeJWT = "wTc$%+6dsgxvtVPba&44M6=%AfuQwT=2VXQza7JeKwX#J7AvAd52pFYtkRNNF$UWaCdH-few%b4h@2NvE%xeA657VJsv7^ht3j!9y!%R_B&NL?+$VC#wK3Nra&%6RZ$#KSSW5CCtSfCHV!_=g^-u+m3X-e*$St#@jcg=uaRC#tYt-tK5Rv7x7BPC?x4VNJ*A9RU5DfZSMwe^9$euV4TyTHf-VH3&!CZZrNgDyqzDtTw33Rf#QUXTGUBUM&btEVhY";
        private static string KeyAccessTokenJWT = "7Ex-Q9bp&-rjRrNQKkXmuB%mCC5QU&4P_8M_M3vQ&A&FQM$=#sFh5VKqPNFy7_3?9d$Xyrr3Z#n9zSfQ@@AdHc4%AXK-#gm-QFHsNMyrJpznRDWQfThd_=k2Bas^R22tndJVRR-RLWhGMGJWqy+9pgFesrty#qpRDd46^-D7@$KA&AufQAC*sU#LsHcQv8D?f=wU?AzVr@NJ4cCDbB4F5?5zEt@cAN6zc&Yk!-@dX$4m8%jq*JUF8g9NHjcEWzu4";
        public static string Issuer { get; private set; } = "https://2dfrango.com";
        public static string Audience { get; private set; } = "https://2dfrango.com";


        public string Cpf { get; set; }
        public string Nome { get; set; }

        internal string GerarCodigoAutorizacao(string clientId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = CriarJWTHendlerParaCodigo(clientId, tokenHandler, KeyCodeJWT);
            return tokenHandler.WriteToken(token);
        }

        internal static string ValidarCodigoAutorizacao(string clientId, string code)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(
                code,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(KeyCodeJWT))
                },
                out SecurityToken validatedToken);
            var token = tokenHandler.ReadJwtToken(code);
            var claims = token.Payload.Claims;
            return claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid)?.Value;
        }

        internal string GerarAccessToken(string clientId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = CriarJWTHendlerParaCodigo(clientId, tokenHandler, KeyAccessTokenJWT);
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CriarJWTHendlerParaCodigo(string clientId, JwtSecurityTokenHandler tokenHandler, string signingKey)
        {
            return tokenHandler.CreateJwtSecurityToken(
                            issuer: Issuer,
                            audience: Audience,
                            subject: CriarClaims(clientId),
                            notBefore: DateTime.UtcNow,
                            expires: DateTime.UtcNow.AddDays(1),
                            signingCredentials:
                            new SigningCredentials(
                                new SymmetricSecurityKey(
                                    Encoding.Default.GetBytes(signingKey)),
                                    SecurityAlgorithms.HmacSha256Signature));
        }

        private ClaimsIdentity CriarClaims(string clientId)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, Cpf));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, clientId));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userData)));

            return claimsIdentity;
        }
    }
}
