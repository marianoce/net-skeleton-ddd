using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Contracts.Identity;
using Application.Models.Identity;
using Application.Constants;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.Email);

            if (user == null)
                throw new Exception("Password incorrecto");
            
            var resultado = await _signInManager.PasswordSignInAsync(user.UserName, authRequest.Password, false, lockoutOnFailure: false);
            
            if (!resultado.Succeeded)
                throw new Exception("Password incorrecto");

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(await GenerateToken(user)),
                Email = user.Email,
                Username = user.UserName
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                throw new Exception("El username ya existe");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("El email ya existe");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellido = request.Apellidos,
                UserName = request.Username,
                EmailConfirmed = false
            };

            var resultado = await _userManager.CreateAsync(user, request.Password);

            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");
                return new RegistrationResponse
                {
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(await GenerateToken(user)),
                    UserId = user.Id,
                    Username = user.UserName
                };
            }

            throw new Exception($"{resultado.Errors}");
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach(var role in userRoles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signInCredentials
            );

            return jwtSecurityToken;
        }
    }
}