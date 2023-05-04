namespace Aseguradora.Repositorio;
using Aseguradora.Aplicacion;
using System.IO;

public class RepoVehiculoTXT : IRepoVehiculo
{
    static string s_Archivo { get; } = "Vehiculos.txt";
    int ID { get; set; } = obtenerID();
    static string s_AIDIS { get; } = "AlmacenDeIds.txt";
    private static int obtenerID()
    {
        /*
        Obtener ID restablece el contador de ID's si el programa se reinicia
        */
        int n = 1;
        // el siguiente using solo se asegura de que aunque sea exista el archivo vacio
        using (StreamWriter sw = new StreamWriter(s_AIDIS, append: true)) { }
        try
        {
            // abrir archivo de titulares
            using (StreamReader sr = new StreamReader(s_AIDIS))
            {
                string? l = "";

                // recorrerlo hasta el final
                while (!sr.EndOfStream && l != null && !(l.IndexOf("Vehiculo:") != -1))
                {
                    l = sr.ReadLine();
                }
                // ID es lo primero que hay hasta el primer : asi q lo buscamos
                int i = l != null ? l.IndexOf("Vehiculo:")+9 : -1;
                // establecemos el valor de ID en la ultima econtrada +1
                n = (l != null) && (i != -1) ? int.Parse(l.Substring(i).ReplaceLineEndings("")) : 1;
            }
        }
        catch/*(Exception e)*/
        { //Console.WriteLine("No se pudo recuperar ningun ID"); 
            //Console.WriteLine(e.Message);
        }
        return n;
    }
    private void aumentarID()
    {
        string id_txt;
        // lee archivo de id's
        using (StreamReader sr = new StreamReader(s_AIDIS))
        {
            id_txt = sr.ReadToEnd();
        }
        string[] vID = id_txt.Split('\n');
        // sobreescribe el archivo de id's
        using (StreamWriter sw = new StreamWriter(s_AIDIS))
        {
            foreach (string ids in vID)
            {
                // solo guardamos las ids q no modificamos
                if (ids != "" && ids.IndexOf("Vehiculo:") == -1)
                {
                    sw.WriteLine(ids);
                }
            }
            // actualizamos las id titular
            sw.WriteLine("Vehiculo:{0}", ID);
        }
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

    public void EliminarVehiculo(int ID)
    {
        try{
            string texto;
            using(StreamReader sr = new StreamReader(s_Archivo) ){
                texto = sr.ReadToEnd();
            }

            //busco si existe el id
            //IndexOf solo trabaja con strings/char
            int indice = texto.IndexOf( ID.ToString() );

            if( indice != -1){

                string [] vectorLineas = texto.Split("\n");

                using(StreamWriter sw = new StreamWriter(s_Archivo) ){

                    foreach(string linea in vectorLineas){
                        int encontre = linea.IndexOf( ID.ToString()+":");

                        //si mi linea no tiene el elem a eliminar ni es vacia la agrego
                        if( ( encontre == -1)&&(linea != "") ) 
                            //replace borra los saltos de linea que se encuentran en mi linea
                            sw.WriteLine(linea.ReplaceLineEndings(""));
                    }
                }
            }
            else{
                throw new Exception($"No existe el vehiculo con ID = { ID } para eliminar");
            }
        }
        catch(Exception e){
            Console.WriteLine( e.Message );
        }
    }



    public List<Vehiculo> ListarVehiculos()
    {
        List<Vehiculo> lista=new List<Vehiculo>();
        string? linea;
        
        using(StreamReader sr = new StreamReader(s_Archivo)){
            while(! sr.EndOfStream){
                linea = sr.ReadLine();

                string []? vector = linea != null ? linea.Split(' ',':') : null;

                if(vector != null){
                    //no puedo usar el constructor porque mis primeras variables del texto son string con dimension variable
                    //el id se que esta en el principio

                    //LAS INSTANCIE CON UN VALOR X,ESTA BIEN ??????????????????
                    string dominio="",marca ="";
                    int ano=-1,titular= -1;

                    for(int i=1; i < vector.Count(); i++){
                        switch(vector[i]){
                            case "Dominio":
                                i++;
                                string auxD;
                                string campoD = "";
                                //en aux me va quedando los campos q voy leyendo del vector
                                auxD = vector[i];
                                //si lo que leo pertenece a dominio lo voy agregando al string
                                while( ( auxD != "Marca")&&( auxD != "AnoFabricacion")&&( auxD != "Titular") ){
                                    campoD += auxD+" "; 
                                    i++;
                                    auxD = vector[i];
                                }
                                dominio = campoD;
                                break;

                            case "Marca":
                                i++;
                                string auxM;
                                string campoM = "";
                                auxM = vector[i];
                                while(( auxM != "AnoFabricacion")&&( auxM != "Titular") ){
                                    campoM += auxM+" "; 
                                    i++;
                                    auxM = vector[i];
                                }
                                marca = campoM;
                                break;

                            case "AnoFabricacion":
                                i++;
                                ano = int.Parse(vector[i]);
                                break;

                            case "Titular":
                                i++;
                                titular = int.Parse(vector[i]);
                                break;
                        }
                       
                    }
                    Vehiculo v = new Vehiculo(dominio,marca,ano,titular){ID=int.Parse(vector[0])};

                    lista.Add(v);
                }
            }
        }
        return lista;
    }
}
