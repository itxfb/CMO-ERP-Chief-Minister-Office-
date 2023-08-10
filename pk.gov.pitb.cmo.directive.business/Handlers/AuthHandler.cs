using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Exceptions;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Account;
using Microsoft.AspNetCore.Http;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class AuthHandler : BaseHandler
    {
      
        public AuthHandler(IUnitOfWork unitOfWork, IMapper mapper,ClaimsPrincipal claimsPrincipal) : base(unitOfWork, claimsPrincipal,mapper)
        {

        }
        public async Task<AppUserModel> Login(LoginVm vm)
        {


            AppUser? appUser = await _unitOfWork.GetRepositoryAsync<AppUser>().GetOne(a=> a.Username== vm.Username && a.Password==vm.Password && !a.IsDeleted);
            if (appUser != null)
            {
                var user = _unitOfWork.GetRepositoryAsync<AppUser>().GetAllQueryable().Where(a=>a.Username== vm.Username && !a.IsDeleted)
                            .Include(u => u.UserRoles.Where(ur => ur.IsActive && !ur.IsDeleted))
                                .ThenInclude(p => p.Role)
                                .ThenInclude(p => p.RolePermissions)
                                .ThenInclude(a => a.Feature)
                                .ThenInclude(a=>a.Application)
                            .Include(u => u.UserFeaturePermissions.Where(up => up.IsActive && !up.IsDeleted))
                                .ThenInclude(a=>a.Feature)
                                .ThenInclude(a => a.Application)
                            .FirstOrDefault(a => !a.IsDeleted && a.UserRoles.Any(ur=>!ur.IsDeleted));

                return _mapper.Map<AppUserModel>(user);


            }

            throw new AuthenticationException("Invalid username or password.");




        }

        public static LoginResponseVm GenerateToken(AppUserModel userModel)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userModel.Id.ToString()),
                new Claim("UserName", userModel.Username.ToString())

            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes("contact@sami.pk[cmodirectiveV1]"));

            var token = new JwtSecurityToken(
                issuer: "PITB",// _configuration["JWT: ValidIssuer"],
                audience: "PUBLIC",//_configuration["JWT: ValidAudience"],
                                   //expires: DateTime.Now.AddHours(24),
                expires: DateTime.Now.AddDays(100),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            
            

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };



            var r = (new LoginResponseVm()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                DisplayName = userModel.FullName,
                Id = userModel.Id,
                Username = userModel.Username,
                UserPermissions = JsonSerializer.Serialize(userModel.UserFeaturePermissions, options),
                UserRolesPermissions = JsonSerializer.Serialize(userModel.UserRoles,options)
            });

            return r;

        }
    }
}
