using EstiloMestre.Domain.DTOs;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Repositories.BarbershopService;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.DataAccess.Repositories;

public class BarbershopServiceRepository(EstiloMestreDbContext dbContext) : IBarbershopServiceRepository
{

}
