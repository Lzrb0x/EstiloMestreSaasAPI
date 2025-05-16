using System.Globalization;

namespace EstiloMestre.API.Middlewares;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureinfo = new CultureInfo("en");

        if (!string.IsNullOrEmpty(requestCulture) && supportedCultures.Exists(c => c.Name.Equals(requestCulture)))
        {
            cultureinfo = new CultureInfo(requestCulture);
        }

        CultureInfo.CurrentCulture = cultureinfo;
        CultureInfo.CurrentUICulture = cultureinfo;

        await _next(context);
    }
}

// Essa classe é um middleware que define a cultura atual do contexto HTTP com base no cabeçalho Accept-Language da solicitação.
// se a cultura enviada no header não for suportada, a padrão será "en".