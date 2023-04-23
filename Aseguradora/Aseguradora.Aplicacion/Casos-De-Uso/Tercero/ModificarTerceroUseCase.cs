namespace Aseguradora.Aplicacion;
class ModificarTerceroUseCase
{
    IRepoTercero _repo;
    public ModificarTerceroUseCase(IRepoTercero repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Tercero T)
    {
        _repo.ModificarTercero(T);
    }
}