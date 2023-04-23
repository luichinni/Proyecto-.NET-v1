namespace Aseguradora.Aplicacion;
class EliminarVehiculoUseCase
{
    IRepoVehiculo _repo;
    public EliminarVehiculoUseCase(IRepoVehiculo repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Vehiculo V)
    {
        _repo.EliminarVehiculo(V);
    }
}