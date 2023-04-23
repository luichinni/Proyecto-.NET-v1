namespace Aseguradora.Aplicacion;
class ModificarVehiculoUseCase
{
    IRepoVehiculo _repo;
    public ModificarVehiculoUseCase(IRepoVehiculo repo)
    {
        _repo = repo;
    }
    public void Ejecutar(Vehiculo V)
    {
        _repo.ModificarVehiculo(V);
    }
}