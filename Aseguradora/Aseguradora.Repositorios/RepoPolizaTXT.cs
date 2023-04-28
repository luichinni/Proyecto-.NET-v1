namespace Aseguradora.Repositorio;
using Aseguradora.Aplicacion;
using System.IO;

public class RepoPolizaTXT : IRepoPoliza
{
    static string s_Archivo { get; } = "Poliza.txt";
    int ID { get; set; } = obtenerID();
    static int obtenerID()
    {
        /*
        Obtener ID restablece el contador de ID's si el programa se reinicia
        */
        int n = 1;
        // el siguiente using solo se asegura de que aunque sea exista el archivo vacio
        using (StreamWriter sw = new StreamWriter(s_Archivo, append: true)){}
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

    public void AgregarPoliza(Poliza P)
    {
        try
        {
            // usamos append:true para que las lineas se escriban al final del archivo y no que se sobreescriba
            using (StreamWriter sw = new StreamWriter(s_Archivo, append: true)){
                P.ID = ID; // asignamos la ID
                ID++; // incrementamos para la proxima
                sw.WriteLine(P.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("RIP en Agregar Poliza");
            Console.WriteLine(e.Message);
        }
    }

    public void ModificarPoliza(Poliza P)
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
            if (str.IndexOf(P.ID + ":") != -1)
            {
                // si existe buscamos la linea que le corresponde
                string[] polizas = str.Split('\n');
                for (int j = 0; j < polizas.Length; j++)
                {
                    polizas[j] = polizas[j].ReplaceLineEndings(""); // elimina todo salto de linea
                    //Console.WriteLine($"Linea {j}: "+polizas[j]);
                }
                int indice = -1;
                int i = 0;
                while (indice == -1)
                {
                    if (polizas[i].IndexOf(P.ID + ":") != -1)
                    {
                        indice = i;
                    }
                    i++;
                }
                // sobrescribimos el vehiculo viejo con el nuevo
                polizas[indice] = P.ToString();
                // rescribimos el archivo con el vehiculo modificado
                using (StreamWriter sw = new StreamWriter(s_Archivo)){
                    foreach (string poliza in polizas)
                    {
                        if(poliza != ""){// solo vuelve a guardar los campos que tienen datos realmente
                            sw.WriteLine(poliza);
                        }
                    }
                }
            }
            else
            {
                throw new Exception($"No existe la poliza con ID: {P.ID}");
            }
        }
        catch (Exception e)
        {
            if(e.Message == $"No existe la poliza con ID: {P.ID}"){
                throw e;
            }else{
                Console.WriteLine("RIP en Modificar Poliza");
                Console.WriteLine(e.Message);
            }
        }
    }

    public void EliminarPoliza(int ID)
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

                string [] vectorLineas = texto.Split("/n");

                using(StreamWriter sw = new StreamWriter(s_Archivo) ){

                    for(int i=0; i < vectorLineas.Count(); i++){
                        //si no encunetro el elem a eliminar,lo agrego
                        if(vectorLineas[i].IndexOf( ID.ToString() ) == -1){
                            sw.WriteLine( vectorLineas[i] );
                        }
                    }
                }
            }
            else{
                throw new Exception($"No se encontro poliza con ID = {ID} para eliminar");
            }
        }
        catch(Exception e){
            Console.WriteLine( e.Message );
        }
    }


    public List<Poliza> ListarPolizas()
    {
        List<Poliza> lista = new List<Poliza>();

        string? linea;
        using(StreamReader sr = new StreamReader(s_Archivo)){
            while(! sr.EndOfStream ){
                linea = sr.ReadLine();

                string []? vector = linea != null ? linea.Split(' ',':'): null;

                if(vector != null){
                    //variables para instanciar depsues el objeto
                    int vehiculo = -1;
                    double valor = -1;
                    string franquicia= "",cobertura = "";
                    DateTime desde = new DateTime(-1,-1,-1);
                    DateTime hasta = new DateTime( -1,-1,-1);

                    for(int i=1; i < vector.Count(); i++){
                        switch(vector[i]){
                            case "VehiculoAsegurado":
                                i++;
                                vehiculo = int.Parse(vector[i]);
                                break;
                            
                            case "ValorAsegurado":
                                i++;
                                valor = int.Parse(vector[i]);
                                break;
                            case "Franquicia":
                                i++;
                                string auxF = "";

                                while( ( vector[i] != "cobertura")&&( vector[i] != "VigenteDesde")&&( vector[i] != "VigenteHasta") ){
                                    auxF += vector[i] + " ";
                                    i++;
                                }

                                franquicia = auxF;
                                 break;
                            
                            case "Cobertura":
                                i++;
                                string auxC ="";

                                while( ( vector[i] != "VigenteDesde")&&(vector[i] != "VigenteHasta") ){
                                    auxC += vector[i] + " ";
                                    i++;
                                }

                                cobertura = auxC;
                                break;
                            case "VigenteDesde":
                                //se usa el formato DateTime(Int32, Int32, Int32)->  año/mes/día
                                i++;
                                string []? vectorDesde = vector[i] != null? vector[i].Split('/') : null;
                                if( vectorDesde != null){
                                    desde = new DateTime(int.Parse(vectorDesde[0]),int.Parse(vectorDesde[1]),int.Parse(vectorDesde[2]));
                                }
                                break;
                            case "VigenteHasta":
                                //se usa el formato DateTime(Int32, Int32, Int32)->  año/mes/día
                                i++;
                                string []? vectorHasta = vector[i] != null? vector[i].Split('/') : null;
                                if( vectorHasta != null){
                                    hasta = new DateTime(int.Parse(vectorHasta[0]),int.Parse(vectorHasta[1]),int.Parse(vectorHasta[2]));
                                }
                                break;
                        }
                    }
                    
                    Poliza p = new Poliza(vehiculo,valor,franquicia,cobertura,desde,hasta){ID = int.Parse(vector[0])};

                    lista.Add(p);
                }
            }
        }
        return lista;
    }
}
