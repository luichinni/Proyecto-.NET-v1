namespace Aseguradora.Aplicacion;
public interface IRepoVehiculo
{
    public void AgregarVehiculo(Vehiculo V);
    public void ModificarVehiculo(Vehiculo V);
    public void EliminarVehiculo(Vehiculo V);
    public List<Vehiculo> ListarVehiculos();
}