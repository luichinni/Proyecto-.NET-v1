namespace Aseguradora.Repositorio;
using Aseguradora.Aplicacion;
using System.IO;

public class RepoVehiculoTXT : IRepoVehiculo
{
    static string s_Archivo { get; } = "Vehiculos.txt";
    int ID { get; set; } = obtenerID();
    static int obtenerID()
    {
        /*
        Obtener ID restablece el contador de ID's si el programa se reinicia
        */
        int n = 1;
        // el siguiente using solo se asegura de que aunque sea exista el archivo vacio
        using (StreamWriter sw = new StreamWriter(s_Archivo,append:true)){}
        try
        {
            // abrir archivo de titulares
            using (StreamReader sr = new StreamReader(s_Archivo)){
                string? l = "";
                // recorrerlo hasta el final
                while (!sr.EndOfStream)
                {
                    l = sr.ReadLine();
                }
                // ID es lo primero que hay hasta el primer : asi q lo buscamos
                int i = l!=null ? l.IndexOf(":") : -1;
                // establecemos el valor de ID en la ultima econtrada +1
                n = (l != null) && (i != -1) ? int.Parse(l.Substring(0, i)) + 1 : 1;
            }
        }
        catch { //Console.WriteLine("No se pudo recuperar ningun ID"); 
        }
        return n;
    }

    public void AgregarVehiculo(Vehiculo V)
    {
        try
        {
            // usamos append:true para que las lineas se escriban al final del archivo y no que se sobreescriba
            using (StreamWriter sw = new StreamWriter(s_Archivo, append: true)){
                V.ID = ID; // asignamos la ID
                ID++; // incrementamos para la proxima
                sw.WriteLine(V.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("RIP en Agregar Vehiculo");
            Console.WriteLine(e.Message);
        }
    }

    public void ModificarVehiculo(Vehiculo V)
    {
        try
        {
            // abrimos en modo lectura
            string str;
            using (StreamReader sr = new StreamReader(s_Archivo)){
                // leemos todo el archivo
                str = sr.ReadToEnd();
            }
            // comprobamos de forma general si existe la ID buscada
            if (str.IndexOf(V.ID+":") != -1)
            {
                // si existe buscamos la linea que le corresponde
                string[] vehiculos = str.Split('\n');
                for (int j = 0; j < vehiculos.Length; j++)
                {
                    vehiculos[j] = vehiculos[j].ReplaceLineEndings(""); // elimina todo salto de linea
                    //Console.WriteLine($"Linea {j}: "+polizas[j]);
                }
                int indice = -1;
                int i = 0;
                while (indice == -1)
                {
                    if (vehiculos[i].IndexOf(V.ID + ":") != -1)
                    {
                        indice = i;
                    }
                    i++;
                }
                // sobrescribimos el vehiculo viejo con el nuevo
                vehiculos[indice] = V.ToString();
                // rescribimos el archivo con el vehiculo modificado
                using (StreamWriter sw = new StreamWriter(s_Archivo)){
                    foreach (string vehiculo in vehiculos)
                    {
                        if(vehiculo != ""){// solo vuelve a guardar los campos que tienen datos realmente
                            sw.WriteLine(vehiculo);
                        }
                    }
                }
            }
            else
            {
                throw new Exception($"No existe el vehiculo con ID: {V.ID}");
            }
        }
        catch (Exception e)
        {
            if(e.Message == $"No exise el vehiculo con ID: {V.ID}"){
                throw e;
            }else{
                Console.WriteLine("RIP en Modificar Vehiculo");
                Console.WriteLine(e.Message);
            }
        }
    }

    public void EliminarVehiculo(Vehiculo V)
    {
        // ESTO LO HACE NICKY NICOLE
    }

    public List<Vehiculo> ListarVehiculos()
    {
        // ESTO LO HACE NICKY NICOLE
        return null;
    }
}
