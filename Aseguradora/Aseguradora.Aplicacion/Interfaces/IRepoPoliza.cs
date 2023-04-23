namespace Aseguradora.Aplicacion;
public interface IRepoPoliza
{
    public void AgregarPoliza(Poliza P);
    public void ModificarPoliza(Poliza P);
    public void EliminarPoliza(Poliza P);
    public List<Poliza> ListarPolizas();
}