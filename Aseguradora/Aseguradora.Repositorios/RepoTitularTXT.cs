namespace Aseguradora.Repositorio;
using Aseguradora.Aplicacion;
using System.IO;

public class RepoTitularTXT : IRepoTitular{
    static string s_Archivo {get;} = "Titulares.txt";
    int ID {get;set;} = obtenerID();
    static int obtenerID(){
        /*
        Obtener ID restablece el contador de ID's si el programa se reinicia
        */
        int n=1;
        try{
            // abrir archivo de titulares
            using StreamReader sr = new StreamReader(s_Archivo);
            string l="";
            // recorrerlo hasta el final
            while (!sr.EndOfStream)
            {
                l = sr.ReadLine();
            }
            // ID es lo primero que hay hasta el primer : asi q lo buscamos
            int i = l.IndexOf(":");
            // establecemos el valor de ID en la ultima econtrada +1
            n=int.Parse(l.Substring(0,i))+1;
        }catch{}
        return n;
    }

    public void AgregarTitular(Titular T){
        try{
            // abrimos el archivo en modo lectura para comprobar si existe el Titular
            using StreamReader sr = new StreamReader(s_Archivo);
            string str = sr.ReadToEnd(); // leemos todo el archivo
            int i = str.IndexOf(T.DNI); // buscamos el DNI
            if(i != -1){ // si ya existe lo modificamos
                Console.WriteLine($"Ya existe el titular con DNI:{T.DNI}");
                Console.WriteLine($"Modificando el titular con DNI:{T.DNI}");
                ModificarTitular(T);
            }else{ // si no existe lo agregamos
                // usamos append:true para que las lineas se escriban al final del archivo y no que se sobreescriba
                using StreamWriter sw = new StreamWriter(s_Archivo, append: true);
                sw.WriteLine(T.ToString());
            }
        }catch(Exception e){
            Console.WriteLine("RIP en Agregar Titular");
            Console.WriteLine(e.Message);
        }
    }

    public void ModificarTitular(Titular T){
        try{
            // abrimos en modo lectura
            using StreamReader sr = new StreamReader(s_Archivo);
            // leemos todo el archivo
            string str = sr.ReadToEnd();
            // comprobamos de forma general si existe el DNI buscado
            if(str.IndexOf(T.DNI) != -1){
                // si existe buscamos la linea que le corresponde
                string[] titulares = str.Split('\n');
                int indice = -1;
                int i = 0;
                while(indice == -1){
                    if(titulares[i].IndexOf(T.DNI) != -1){
                        indice = i;
                    }
                    i++;
                }
                // le pasamos la id correspondiente que está entre la pos 0 y el primer : del string
                T.ID = int.Parse(titulares[indice].Substring(0,titulares[indice].IndexOf(":")));
                // sobrescribimos el titular viejo con el nuevo
                titulares[indice]=T.ToString();
                // rescribimos el archivo con el titular modificado
                using StreamWriter sw = new StreamWriter(s_Archivo);
                foreach(string titular in titulares){
                    sw.WriteLine(titular);
                }
            }else{
                throw new Exception($"No existe titular con DNI: {T.DNI}");
            }
        }catch(Exception e){
            Console.WriteLine("RIP en Modificar Titular");
            Console.WriteLine(e.Message);
        }
    }
    
    public void EliminarTitular(Titular T){
        // ESTO LO HACE NICKY NICOLE
    }

    public List<Titular> ListarTitulares(){
        // ESTO LO HACE NICKY NICOLE
        return null;
    }
}