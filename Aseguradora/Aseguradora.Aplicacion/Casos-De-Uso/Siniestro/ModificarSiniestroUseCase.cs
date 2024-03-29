namespace Aseguradora.Aplicacion;
public class ModificarSiniestroUseCase
{
    IRepoSiniestro _repo;
    public ModificarSiniestroUseCase(IRepoSiniestro repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Siniestro S)
    {
        _repo.ModificarSiniestro(S);
    }
}