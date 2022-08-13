using Api.Documentation.Swagger.User;
using Application.Command.User;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/authentication")]
    [Produces("application/json")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [SwaggerOperation("Recebe o login e senha do usuário para geração de token")]
        [SwaggerRequestExample(typeof(TokenRequestExample), typeof(TokenRequestExample))]
        [SwaggerResponse(200, "Requisição processada com sucesso", typeof(TokenResponseExample))]
        [SwaggerResponseExample(200, typeof(TokenResponseExample))]
        [SwaggerResponse(401, "Usuário ou/e senha inválido(s)", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(400, "Usuário ou/e senha inválido(s)", typeof(void))]
        [SwaggerResponseExample(400, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpPost("user")]
        public async Task<IActionResult> PostAsync(LoginCommand loginCommand)
        {
            try
            {
                return Ok(await this._mediator.Send(loginCommand));
            }
            catch(UnauthorizedException ex)
            {
                return StatusCode(401);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [SwaggerOperation("Atualiza a senha do usuário")]
        [SwaggerRequestExample(typeof(PasswordRequestExample), typeof(PasswordRequestExample))]
        [SwaggerResponse(200, "Requisição processada com sucesso.")]
        [SwaggerResponseExample(200, typeof(void))]
        [SwaggerResponse(401, "Usuário ou/e senha inválido(s)", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(409, "Senha inválida, pois a nova senha é igual a anterior", typeof(void))]
        [SwaggerResponseExample(409, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpPut("user")]
        public async Task<IActionResult> PutAsync(PasswordCommand passwordCommand)
        {
            try
            {
                return Ok(await this._mediator.Send(passwordCommand));
            }
            catch(UnauthorizedException ex)
            {
                return StatusCode(401);
            }
            catch(InvalidPasswordException ex)
            {
                return StatusCode(409);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}