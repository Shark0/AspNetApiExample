using System;
using System.Linq;
using System.Web.Http;
using JWT.Algorithms;
using JWT.Builder;
using WebApplication1.Controllers.Pojo;
using WebApplication1.Controllers.Pojo.User;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : ApiController
    {
        ExampleEntities exampleEntities = new ExampleEntities();
        
        [Route("user/register")]
        [HttpPost]
        public ResponseDTO<object> register([FromBody] RegisterInputDO registerInput)
        {
            USER user = new USER();
            user.NAME = registerInput.name;
            user.ACCOUNT = registerInput.account;
            user.PASSWORD = registerInput.password;
            exampleEntities.USER.Add(user);
            exampleEntities.SaveChanges();
            ResponseDTO<object> responseDto = new ResponseDTO<object>();
            responseDto.code = 1;
            return responseDto;
        }
        
        
        [Route("user/login")]
        [HttpPost]
        public ResponseDTO<UserDO> login([FromBody]LoginInputDO loginInput)
        {
            USER user = exampleEntities.USER.Single(USER => USER.ACCOUNT == loginInput.account);
            ResponseDTO<UserDO> responseDto = new ResponseDTO<UserDO>();
            if (null == user)
            {
                responseDto.code = -1;
                responseDto.message = "使用者不存在";
                return responseDto;
            }

            if (!user.PASSWORD.Equals(loginInput.password))
            {
                responseDto.code = -2;
                responseDto.message = "密碼錯誤";
                return responseDto;
            }

            responseDto.code = 1;
            UserDO userDo = new UserDO();
            userDo.id = user.ID;
            userDo.name = user.NAME;
            string token = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                .WithSecret("shark")
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds())
                .AddClaim("id", user.ID)
                .Encode();
            userDo.jwt = token;
            responseDto.data = userDo;
            return responseDto;
        }
    }
}