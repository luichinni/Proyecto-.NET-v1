namespace Aseguradora.Aplicacion;

public class Titular : Persona
{
    public override int ID {get; set;}
    public override string DNI {get; set;}
    public override string Apellido {get; set;}
    public override string Nombre {get; set;}
    public string? Direccion {get; set;}
    public override string? Telefono {get; set;}
    public string? Email {get; set;}
    
    public Titular(string dni, string apellido, string nombre){
        DNI = dni;
        Apellido = apellido;
        Nombre = nombre;
    }

    public override string ToString()
    {
        string str = $"{ID}: DNI:{DNI} Apellido:{Apellido} Nombre:{Nombre}";
        if(Direccion != null){
            str += $" Direccion:{Direccion}";
        }
        if(Telefono != null){
            str += $" Telefono:{Telefono}";
        }
        if(Email != null){
            str += $" Email:{Email}";
        }
        return str;
    }
}