namespace EstiloMestre.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}