using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StuddGokApi.Middlewear;

public class UserIdentifier : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        
        if(!context.Request.Headers.ContainsKey("Authorization"))
        {
            await next(context);
            return;
        }
        string? s = context.Request.Headers.Authorization;
        if(s != null)
        {
            string jwtString = s.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtString) as JwtSecurityToken;
            if (jsonToken != null)
            {
                IEnumerable<Claim> gokstademail = jsonToken.Claims.Where(x => x.Type == "gokstademail");
                //IEnumerable<Claim> firstname = jsonToken.Claims.Where(x => x.Type == "firstname");
                //IEnumerable<Claim> lastname = jsonToken.Claims.Where(x => x.Type == "lastname");
                IEnumerable<Claim> id = jsonToken.Claims.Where(x => x.Type == "id");
                IEnumerable<Claim> role = jsonToken.Claims.Where(x => x.Type == "role");    //.FirstOrDefault().Value;
                if (gokstademail.Any())
                {
                    context.Items["GokstadEmail"] = gokstademail.First().Value;
                }
                if (id.Any())
                {
                    context.Items["UserId"] = Convert.ToInt32(id.First().Value);
                }
                if (role.Any())
                {
                    context.Items["Role"] = role.First().Value;
                }
            }
        }
        await next(context);
    }
}
