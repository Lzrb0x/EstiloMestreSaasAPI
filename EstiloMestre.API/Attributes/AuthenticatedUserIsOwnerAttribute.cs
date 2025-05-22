using EstiloMestre.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Attributes;

public class AuthenticatedUserIsOwnerAttribute() : TypeFilterAttribute(typeof(AuthenticatedUserIsOwnerFilter)) { }
