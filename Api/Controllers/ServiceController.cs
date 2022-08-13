using Api.Documentation.Swagger.Service;
using Application.Command.Service;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("v1/service")]
    [Produces("application/json")]
    public class ServiceController : Controller
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [SwaggerOperation("Busca todos os serviços ativos")]
        [SwaggerResponse(200, "Requisição processada com sucesso", typeof(ServiceResponseExemple))]
        [SwaggerResponseExample(200, typeof(ServiceResponseExemple))]
        [SwaggerResponse(401, "Token informado é inválido", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(404, "Não encontramos nenhum servico ativo", typeof(void))]
        [SwaggerResponseExample(404, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpGet("service")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await this._mediator.Send(new GetServiceCommand()));
            }
            catch (ServiceNotFoundException ex)
            {
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [SwaggerOperation("Busca todos os horários disponíveis para o serviço informado")]
        [SwaggerResponse(200, "Requisição processada com sucesso", typeof(ServiceResponseExemple))]
        [SwaggerResponseExample(200, typeof(ServiceResponseExemple))]
        [SwaggerResponse(401, "Token informado é inválido", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(404, "Não encontramos nenhum horário disponível para os próximos 10 dias", typeof(void))]
        [SwaggerResponseExample(404, typeof(void))]
        [SwaggerResponse(406, "O serviço informado não foi encontrado", typeof(void))]
        [SwaggerResponseExample(406, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpGet("service/{serviceID}")]
        public async Task<IActionResult> AvailableTimesAsync(int serviceID)
        {
            try
            {
                return Ok(await this._mediator.Send(new GetAvailableTimesCommand() { ServiceID = serviceID }));
            }
            catch (ServiceNotFoundException ex)
            {
                return StatusCode(406);
            }
            catch (ConsultNotFoundException ex)
            {
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}