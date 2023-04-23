namespace Aseguradora.Aplicacion;
public class EliminarTitularUseCase
{
    IRepoTitular _repo;
    public EliminarTitularUseCase(IRepoTitular repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Titular T)
    {
        _repo.EliminarTitular(T);
    }
}