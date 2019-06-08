using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Servcom.Service.API.Controllers;
using Servcom.SGA.Domain.Core.Interfaces;
using Servcom.SGA.Domain.Core.Notifications;
using Servcom.SGA.Domain.Usuarios.Commands;
using Servcom.SGA.Domain.Usuarios.Repository;
using Servcom.SGA.Infra.CrossCutting.Identity.Authorization;
using Servcom.SGA.Infra.CrossCutting.Identity.Models;
using Servcom.SGA.Infra.CrossCutting.Identity.ViewModels;
using Servcom.SGA.Service.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Servcom.SGA.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediatorHandler _mediator;
        private readonly TokenDescriptor _tokenDescriptor;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UserController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                TokenDescriptor tokenDescriptor,
                INotificationHandler<DomainNotification> notifications,
                IUser user,
                IMediatorHandler mediator,
                IMapper mapper,
                IUsuarioRepository usuarioRepository) : base(notifications, user, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
            _tokenDescriptor = tokenDescriptor;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("lista-usuarios")]
        public IEnumerable<UsuarioViewModel> Get()
        {
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(_usuarioRepository.ObterUsuarios());
        }

        [HttpGet]
        [Authorize(Policy = "PodeGravar")]
        [Route("obter-usuario/{id:guid}")]
        public UsuarioViewModel ObterUsuario(Guid id)
        {
            

            return _mapper.Map<UsuarioViewModel>(_usuarioRepository.ObterPorId(id));
        }

        [HttpPut]
        [Authorize(Policy = "PodeGravar")]
        [Route("editar-usuario")]
        public async Task<IActionResult> EditarUsuario([FromBody] UpdateUserViewModel model)
        {
            var editarCommand = new EditarUsuarioCommand(model.Id, model.Sigla,model.Nome,model.Setor);
            await _mediator.EnviarComando(editarCommand);
                      
            return Response(editarCommand);

        }


        [HttpDelete]
        [Authorize(Policy = "PodeGravar")]
        [Route("excluir-usuario/{id:guid}")]
        public async Task<IActionResult> ExcluirUsuario(Guid id)
        {
            var excluirCommand = new ExcluirUsuarioCommand(id);
            await _mediator.EnviarComando(excluirCommand);

            if (OperacaoValida())
            {
                var idstring = id.ToString();
                var user = await _userManager.FindByIdAsync(idstring);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return Response(user);
                }
                NotificarErro(result.ToString(), "Falha ao realizar o login");
            }
            return Response(excluirCommand);

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login-usuario")]
        public async Task<IActionResult> Post([FromBody] LoginViewModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Sigla, model.Senha, false, true);

            if (result.Succeeded)
            {
                //TODO: log UserController
               // _logger.LogInformation(1, "Usuario logado com sucesso");
                var response = await GerarTokenUsuario(model);
                return Response(response);
            }

            NotificarErro(result.ToString(), "Falha ao realizar o login");
            return Response(model);
        }

        [HttpPost]
        [Authorize(Policy = "PodeGravar")]
        [Route("novo-usuario")]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel model)
        {
        
            var user = new ApplicationUser { UserName = model.Sigla };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Atendimentos", "Ler"));
                await _userManager.AddClaimAsync(user, new Claim("Atendimentos", "Gravar"));
                
                var registroCommand = new RegistrarUsuarioCommand(Guid.Parse(user.Id),model.Sigla, model.Nome,model.Setor);
                await _mediator.EnviarComando(registroCommand);

                if (!OperacaoValida())
                {
                    await _userManager.DeleteAsync(user);
                    return Response(model);
                }

                //_logger.LogInformation(1, "Usuario criado com sucesso!");
                return Response(model);
            }
            AdicionarErrosIdentity(result);
            return Response(model);
        }

        private async Task<object> GerarTokenUsuario(LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Sigla);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);
            var userLogado = _usuarioRepository.ObterPorId(Guid.Parse(user.Id));

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = userLogado.Id,
                    nome = userLogado.Nome,
                    sigla= userLogado.Sigla,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }
    }
}
