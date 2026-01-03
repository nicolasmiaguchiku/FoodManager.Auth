using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace FoodManager.Auth.CrossCutting.Extentions
{
    public sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider schemeProvider) : IOpenApiDocumentTransformer
    {
        private readonly IAuthenticationSchemeProvider _schemeProvider = schemeProvider;

        public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var schemes = await _schemeProvider.GetAllSchemesAsync();
            if (!schemes.Any(s => s.Name == "Bearer"))
                return;

            document.Components ??= new OpenApiComponents();

            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

            document.Components.SecuritySchemes["Bearer"] =
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                };

            foreach (var operation in document.Paths.SelectMany(p => p.Value!.Operations!.Values))
            {
                operation.Security ??= [];

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer")] = []
                });
            }
        }
    }
}