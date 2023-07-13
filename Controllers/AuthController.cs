using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using e_catalog_backend.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace e_catalog_backend.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }
    [HttpPost]
    [Route("authenticate")]
    public async Task<ActionResult> Authenticate(string email,string password)
    {
        var user = await _mediator.Send(new ValidateUserQuery(email, password));
        if (user == null) return Unauthorized();
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Authentication:Issuer"]!,
            _configuration["Authentication:Audience"]!,
            new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            },
            expires: DateTime.Now.AddHours(6),
            signingCredentials: credentials);
        
        var TokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(TokenToReturn);
    }
}