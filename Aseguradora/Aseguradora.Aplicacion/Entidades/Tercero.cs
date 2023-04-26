namespace Aseguradora.Aplicacion;

public class Tercero : Persona
{
    public override int ID { get; set; }
    public override int DNI { get; set; }
    public override string Apellido { get; set; }
    public override string Nombre { get; set; }
    public string Aseguradora { get; set; }
    public override string? Telefono { get; set; }
    public int Siniestro { get; set; }

    public Tercero()
    {
        // ARMAR PARA PROXIMA ENTREGA
    }
}
