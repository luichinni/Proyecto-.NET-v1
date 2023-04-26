namespace Aseguradora.Aplicacion;

public class Titular : Persona
{
    public override int ID {get; set;} = -1;
    public override int DNI {get; set;}
    public override string Apellido {get; set;}
    public override string Nombre {get; set;}
    public string? Direccion {get; set;}
    public override string? Telefono {get; set;}
    public string? Email {get; set;}
    public List<int> ListaVehiculos {get;set;}
    public Titular(string dni, string apellido, string nombre){
        DNI = dni;
        Apellido = apellido;
        Nombre = nombre;
    }

    public override string ToString()
    {
        // EJ de toString: 
        // 1: DNI:44130300 Apellido:Macias Nombre:Luciano
        string str = $"{ID}: DNI:{DNI} Apellido:{Apellido} Nombre:{Nombre}";
        if(Direccion != null){
            // 1: DNI:44130300 Apellido:Macias Nombre:Luciano Direccion:25 N545
            str += $" Direccion:{Direccion}";
        }
        if(Telefono != null){
            // 1: DNI:44130300 Apellido:Macias Nombre:Luciano Direccion:25 N545 Telefono:221-1231234
            str += $" Telefono:{Telefono}";
        }
        if(Email != null){
            // 1: DNI:44130300 Apellido:Macias Nombre:Luciano Direccion:25 N545 Telefono:221-1231234 Email:lumacias@hotmail.com
            str += $" Email:{Email}";
        }
        if(ListaVehiculos.Count != 0){
            // 1: DNI:44130300 Apellido:Macias Nombre:Luciano Direccion:25 N545 Telefono:221-1231234 Email:lumacias@hotmail.com Vehiculos:
            str+="Vehiculos:";
            foreach(int i in ListaVehiculos){
                // 1: DNI:44130300 Apellido:Macias Nombre:Luciano Direccion:25 N545 Telefono:221-1231234 Email:lumacias@hotmail.com Vehiculos:1,2,
                str+=$"{i},";
            }
            // 1: DNI:44130300 Apellido:Macias Nombre:Luciano Direccion:25 N545 Telefono:221-1231234 Email:lumacias@hotmail.com Vehiculos:1,2
            str = str.Remove(str.Length-1);
        }

        return str;
    }
}
