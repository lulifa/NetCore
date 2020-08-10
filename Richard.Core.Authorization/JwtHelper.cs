using Microsoft.IdentityModel.Tokens;
using Richard.Core.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Richard.Core.Authorization
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <returns></returns>
        public static string IssueJwt(TokenModelJwt tokenModelJwt)
        {
            string issuer = AppSettingHelper.GetSection("Audience", "Issuer");
            string audience = AppSettingHelper.GetSection("Audience", "Audience");
            string secret = SystemHelper.Audience_Secret;

            //****************************************************************************************
            //JWT的构成
            //第一部分我们称它为头部（header),第二部分我们称其为载荷（payload, 类似于飞机上承载的物品)，第三部分是签证（signature).
            //header:jwt的头部承载两部分信息：声明类型，这里是jwt声明加密的算法 通常直接使用 HMAC SHA256 
            //       完整的头部就像下面这样的JSON：{'typ':'JWT','alg':'HS256'} 然后将头部进行base64加密（该加密是可以对称解密的),构成了第一部分.
            //playload:这些有效信息包含三个部分:标准中注册的声明、公共的声明、私有的声明
            //1、标准中注册的声明如下:
            //iss: jwt签发者
            //aud: 接收jwt的一方
            //sub: jwt所面向的用户
            //exp: jwt的过期时间，这个过期时间必须要大于签发时间
            //nbf: 定义在什么时间之前，该jwt都是不可用的.
            //iat: jwt的签发时间
            //jti: jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
            //2、公共的声明 ：
            //公共的声明可以添加任何的信息，一般添加用户的相关信息或其他业务需要的必要信息.
            //但不建议添加敏感信息，因为该部分可以直接base64解码，可以看到里面的信息
            //3、签证：
            //jwt的第三部分是一个签证信息，这个签证信息由三部分组成：header(base64后的).payload(base64后的).secret
            //这个部分需要base64加密后的header和base64加密后的payload使用.连接组成的字符串，然后通过header中声明的加密方式进行加盐secret组合加密，然后就构成了jwt的第三部分。
            //将这三部分用.连接成一个完整的字符串,构成了最终的jwt。注意：secret是保存在服务器端的，jwt的签发生成也是在服务器端的，
            //secret就是用来进行jwt的签发和jwt的验证，所以，它就是你服务端的私钥，在任何场景都不应该流露出去。一旦客户端得知这个secret, 那就意味着客户端是可以自我签发jwt了。
            //*****************************************************************************************
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, tokenModelJwt.UserId));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iss, issuer));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            //claims.Add(new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()));
            claims.AddRange(tokenModelJwt.Role.Split(',').Select(item => new Claim(ClaimTypes.Role, item)));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer, //签发者
                claims: claims, //签名数据
                signingCredentials: credentials //签名
                );
            string encodeJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeJwt;
        }

        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtStr);
            TokenModelJwt tokenModelJwt = new TokenModelJwt();
            tokenModelJwt.UserId = jwtToken.Id;
            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object role);
            tokenModelJwt.Role = role as string;
            return tokenModelJwt;
        }
    }
}