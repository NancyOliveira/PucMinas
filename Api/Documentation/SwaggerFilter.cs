using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Documentation
{
    public class SwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var hiddenRoutes = swaggerDoc.Paths
                .Where(x => !x.Key.ToLower().EndsWith("/odontopuc"))
                .Where(x => !x.Key.ToLower().EndsWith("/costumer"))
                .Where(x => !x.Key.ToLower().EndsWith("/consult"))
                .Where(x => !x.Key.ToLower().EndsWith("/service"))
                .ToList();
            hiddenRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}