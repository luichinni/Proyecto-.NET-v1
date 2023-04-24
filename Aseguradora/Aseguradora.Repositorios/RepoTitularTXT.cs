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
        // el siguiente using solo se asegura de que aunque sea exista el archivo vacio
        using (StreamWriter sw = new StreamWriter(s_Archivo, append: true)){}
        try{
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
        }catch/*(Exception e)*/{ //Console.WriteLine("No se pudo recuperar ningun ID"); 
            //Console.WriteLine(e.Message);
        }
        return n;
    }

    public void AgregarTitular(Titular T){
        try{
            // abrimos el archivo en modo lectura para comprobar si existe el Titular
            string str;
            using (StreamReader sr = new StreamReader(s_Archivo)){
                str = sr.ReadToEnd(); // leemos todo el archivo
            }
            int i = str.IndexOf(T.DNI); // buscamos el DNI
            if(i != -1){ // si ya existe lo modificamos
                Console.WriteLine($"Ya existe el titular con DNI:{T.DNI}");
                Console.WriteLine($"Modificando el titular con DNI:{T.DNI}");
                ModificarTitular(T);
            }else{ // si no existe lo agregamos
                // usamos append:true para que las lineas se escriban al final del archivo y no que se sobreescriba
                T.ID = ID;
                ID++;
                using (StreamWriter sw = new StreamWriter(s_Archivo, append: true)){
                    sw.WriteLine(T.ToString());
                }
            }
        }catch(Exception e){
            Console.WriteLine("RIP en Agregar Titular");
            Console.WriteLine(e.Message);
        }
    }

    public void ModificarTitular(Titular T){
        try{
            // abrimos en modo lectura
            string str;
            using (StreamReader sr = new StreamReader(s_Archivo)){
                // leemos todo el archivo
                str = sr.ReadToEnd();
            }
            // comprobamos de forma general si existe el DNI buscado
            if(str.IndexOf(T.DNI) != -1){
                // si existe buscamos la linea que le corresponde
                string[] titulares = str.Split('\n'); // nota: al hacer split quedan algunos campos vacios
                for(int j=0;j<titulares.Length;j++){
                    titulares[j] = titulares[j].ReplaceLineEndings(""); // elimina todo salto de linea
                    //Console.WriteLine($"Linea {j}: "+titulares[j]);
                }
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
                using (StreamWriter sw = new StreamWriter(s_Archivo,append:false)){
                    foreach(string titular in titulares){
                        if(titular != ""){ // solo vuelve a guardar los campos que tienen datos realmente
                            sw.WriteLine(titular);
                        }
                    }
                }
            }else{
                throw new Exception($"No existe titular con DNI: {T.DNI}");
            }
        }catch(Exception e){
            if(e.Message == $"No existe titular con DNI: {T.DNI}"){
                throw e;
            }else{
                Console.WriteLine("RIP en Modificar Titular");
                Console.WriteLine(e.Message);
            }
        }
    }
    
    public void EliminarTitular(Titular T){  
        try{
            //leo todo el archivo y lo guardo en texto
            string texto;
            using(StreamReader sr = new StreamReader(s_Archivo)){
                texto = sr.ReadToEnd();
            }

            //busco el titular, teniendo en cuenta el dni
            //IndexOf indica el índice de base cero de la primera aparición de un carácter Unicode especificado o de una cadena en la instancia en cuestión. El método devuelve -1, si el carácter o cadena no se encuentran en esta instancia. 
            int indice = texto.IndexOf(T.DNI);

            //si encontre el titular a borrar
            if(indice != -1){
                //guardo en un vector el archivo donde c/pos tiene una linea 
                string [] vector = texto.Split("/n");
                //abro el archivo para leer
                using(StreamWriter sw = new StreamWriter(s_Archivo)){
                    //recorro el vector y si encuentro el titular a borrar directamente no lo escribo en el texto
                    for(int i=0; i < vector.Length; i++){
                        if( vector[i].IndexOf(T.DNI) != -1) 
                            sw.WriteLine(vector[i]);
                    }
                }
            }
            else{
                throw new Exception("No se encontro titular a borrar");
            }
        }
        catch(Exception e)
            Console.WriteLine(e.Message);
    }

    public List<Titular> ListarTitulares(){
        /*

        //hace falta chequear q mi archino no ta vacio?

        
        List<Titular> lista=new List<Titular>();
        string linea;
        Titular t;

        using(StreamReader sr = new StreamReader(s_Archivo)){
            while(! sr.EndOfStream){
                linea = sr.ReadLine();

                //esta bien si con ese string voy buscando campo x campo y asignandole esos 
                //valores a la variable de tipo Titular asi despues agregarlo a la lista de ese tipo?

                
                //TENGO Q DEVOLVER DE TIPO TITULAR
                lista.Add(t);
            }
        }
        
        */
        
        return null;
    }
}
