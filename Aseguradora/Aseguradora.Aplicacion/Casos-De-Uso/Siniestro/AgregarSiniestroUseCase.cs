namespace Aseguradora.Aplicacion;
class AgregarSiniestroUseCase
{
    IRepoSiniestro _repo;
    public AgregarSiniestroUseCase(IRepoSiniestro repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Siniestro S)
    {
        _repo.AgregarSiniestro(S);
    }
}