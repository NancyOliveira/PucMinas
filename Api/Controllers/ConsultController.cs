using Api.Documentation.Swagger.Consult;
using Application.Command.Consult;
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
    [Route("v1/consult")]
    [Produces("application/json")]
    public class ConsultController : Controller
    {
        private readonly IMediator _mediator;

        public ConsultController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [SwaggerOperation("Recebe o CPF do usuário, data da consulta e o serverico que deseja marcar a consulta")]
        [SwaggerRequestExample(typeof(ConsultRequestExample), typeof(ConsultRequestExample))]
        [SwaggerResponse(200, "Requisição processada com sucesso.", typeof(ConsultRequestExample))]
        [SwaggerResponseExample(200, typeof(ConsultRequestExample))]
        [SwaggerResponse(401, "Token informado é inválido", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(409, "O cliente já possui uma consulta no horário informado", typeof(void))]
        [SwaggerResponseExample(409, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpPost]
        public async Task<IActionResult> PostAsync(ConsultCommand consultCommand)
        {
            try
            {
                return Ok(await this._mediator.Send(consultCommand));
            }
            catch (DuplicateConsultException ex)
            {
                return StatusCode(409);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [SwaggerOperation("Busca todas as consultas que já foram ou estão agendadas")]
        [SwaggerRequestExample(typeof(ConsultRequestExample), typeof(ConsultRequestExample))]
        [SwaggerResponse(200, "Requisição processada com sucesso.", typeof(GetConsultResponseExample))]
        [SwaggerResponseExample(200, typeof(GetConsultResponseExample))]
        [SwaggerResponse(401, "Token informado é inválido", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(404, "O cliente não possui nenhum agendamento", typeof(void))]
        [SwaggerResponseExample(404, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpGet("consult/{document}")]
        public async Task<IActionResult> GetAsync(string document)
        {
            try
            {
                return Ok(await this._mediator.Send(new GetConsultCommand() { Document = document }));
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