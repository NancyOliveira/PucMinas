using Api.Documentation.Swagger.Customer;
using Application.Command.Customer;
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
    [Route("v1/costumer")]
    [Produces("application/json")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [SwaggerOperation("Recebe o cadastro de um novo cliente")]
        [SwaggerRequestExample(typeof(CostumerRequestExample), typeof(CostumerRequestExample))]
        [SwaggerResponse(200, "Requisição processada com sucesso", typeof(CostumerRequestExample))]
        [SwaggerResponseExample(200, typeof(CostumerRequestExample))]
        [SwaggerResponse(401, "Token informado é inválido", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(409, "O documento informado já existe", typeof(void))]
        [SwaggerResponseExample(409, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpPost("customer")]
        public async Task<IActionResult> PostAsync(CustomerCommand customerCommand)
        {
            try
            {
                return Ok(await this._mediator.Send(customerCommand));
            }
            catch (DuplicateDocumentException ex)
            {
                return StatusCode(409);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [SwaggerOperation("Busca o cliente de acordo com CPF informado")]
        [SwaggerRequestExample(typeof(string), typeof(string))]
        [SwaggerResponse(200, "Requisição processada com sucesso.", typeof(CostumerRequestExample))]
        [SwaggerResponseExample(200, typeof(CostumerRequestExample))]
        [SwaggerResponse(401, "Token informado é inválido", typeof(void))]
        [SwaggerResponseExample(401, typeof(void))]
        [SwaggerResponse(404, "O documento informado não foi encontrado", typeof(void))]
        [SwaggerResponseExample(404, typeof(void))]
        [SwaggerResponse(500, "Erro durante a requisição", typeof(void))]
        [SwaggerResponseExample(500, typeof(void))]
        [HttpGet("customer/{document}")]
        public async Task<IActionResult> GetAsync(string document)
        {
            try
            {
                return Ok(await this._mediator.Send(new GetDocumentCommand() { Document = document }));
            }
            catch (DocumentNotFoundException ex)
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
