using System;
using System.Web.Http.Filters;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;

namespace WebApplication1.Attribute
{
    public class JwtAuthActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            if (request.Headers.Authorization == null)
            {
                throw new Exception("Lost Token");
            }
            string jwt = request.Headers.Authorization.ToString();
            Console.Out.WriteLine("jwt: " + jwt);
            string secret = "shark";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var json = decoder.Decode(jwt, secret, verify: true);
                Console.WriteLine(json);
                base.OnActionExecuting(actionContext);
            }
            catch (TokenExpiredException)
            {
                throw new Exception("Token Expired");
            }
            catch (SignatureVerificationException)
            {
                throw new Exception("Token Verify Failed");
            }
            base.OnActionExecuting(actionContext);
        }
    }
}