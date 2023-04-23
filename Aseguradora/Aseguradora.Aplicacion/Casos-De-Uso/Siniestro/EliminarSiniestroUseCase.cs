namespace Aseguradora.Aplicacion;
class EliminarSiniestroUseCase
{
    IRepoSiniestro _repo;
    public EliminarSiniestroUseCase(IRepoSiniestro repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Siniestro S)
    {
        _repo.EliminarSiniestro(S);
    }
}