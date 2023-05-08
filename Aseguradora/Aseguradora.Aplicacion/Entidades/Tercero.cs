namespace Aseguradora.Aplicacion;

public class Tercero : Persona
{
    public string Aseguradora { get; set; } = "";
    public int Siniestro { get; set; }

    public Tercero(int dni, string apellido, string nombre):base(dni, apellido, nombre)
    {
        // ARMAR PARA PROXIMA ENTREGA
    }
}