using EstiloMestre.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Attributes;

public class OwnerByBarbershopAttribute() : TypeFilterAttribute(typeof(OwnerByBarbershopFilter)) { }
