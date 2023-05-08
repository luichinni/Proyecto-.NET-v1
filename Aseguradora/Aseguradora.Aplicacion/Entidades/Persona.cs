namespace Aseguradora.Aplicacion;

public abstract class Persona
{
    public int ID { get; set; } = -1;
    public int DNI {get;set;}
    public string Apellido { get; set; }
    public string Nombre { get; set; }
    public string? Telefono { get; set; }
    public Persona(int dni, string apellido, string nombre){
        DNI = dni;
        Apellido = apellido;
        Nombre = nombre;
    }
    public override string ToString()
    {
        return $"{ID}: DNI:{DNI} Apellido:{Apellido} Nombre:{Nombre}";
    }
}