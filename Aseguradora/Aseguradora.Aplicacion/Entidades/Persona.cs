namespace Aseguradora.Aplicacion;

public abstract class Persona
{
    public abstract int ID { get; set; }
    public abstract int DNI {get;set;}
    public abstract string Apellido { get; set; }
    public abstract string Nombre { get; set; }
    public abstract string? Telefono { get; set; }
}
