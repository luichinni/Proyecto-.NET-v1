namespace Aseguradora.Aplicacion;
class EliminarPolizaUseCase
{
    IRepoPoliza _repo;
    public EliminarPolizaUseCase(IRepoPoliza repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Poliza P)
    {
        _repo.EliminarPoliza(P);
    }
}