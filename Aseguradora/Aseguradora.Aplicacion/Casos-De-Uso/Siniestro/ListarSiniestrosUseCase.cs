namespace Aseguradora.Aplicacion;
public class ListarSiniestrosUseCase
{
    IRepoSiniestro _repo;
    public ListarSiniestrosUseCase(IRepoSiniestro repo)
    {
        _repo = repo;
    }
    public List<Siniestro> Ejecutar()
    {
        return _repo.ListarSiniestros();
    }
}