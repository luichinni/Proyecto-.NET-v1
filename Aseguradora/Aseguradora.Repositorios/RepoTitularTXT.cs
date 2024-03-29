namespace Aseguradora.Repositorio;
using Aseguradora.Aplicacion;
using System.IO;

public class RepoTitularTXT : IRepoTitular
{
    static string s_Archivo { get; } = "Titulares.txt";
    static string s_AIDIS { get; } = "AlmacenDeIds.txt";
    int ID { get; set; } = obtenerID();
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
                while (!sr.EndOfStream && l != null && l.IndexOf("Titular:") == -1)
                {
                    l = sr.ReadLine();
                }
                // ID es lo primero que hay hasta el primer : asi q lo buscamos
                int i = l != null ? l.IndexOf("Titular:") + 8 : -1;
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
                if (ids != "" && ids.IndexOf("Titular:") == -1)
                {
                    sw.WriteLine(ids.ReplaceLineEndings(""));
                }
            }
            // actualizamos las id titular
            sw.WriteLine("Titular:{0}", ID);
        }
    }

    public void AgregarTitular(Titular T)
    {
        try
        {
            // abrimos el archivo en modo lectura para comprobar si existe el Titular
            string str;
            using (StreamReader sr = new StreamReader(s_Archivo))
            {
                str = sr.ReadToEnd(); // leemos todo el archivo
            }
            int i = str.IndexOf(T.DNI.ToString()); // buscamos el DNI
            if (i != -1)
            { // si ya existe lanzamos una excepcion
                throw new Exception($"Ya existe Titular con DNI: {T.DNI}");
            }
            else
            { // si no existe lo agregamos
                // usamos append:true para que las lineas se escriban al final del archivo y no que se sobreescriba
                T.ID = ID;
                ID++;
                using (StreamWriter sw = new StreamWriter(s_Archivo, append: true))
                {
                    sw.WriteLine(T.ToString());
                }
                aumentarID();
            }
        }
        catch (Exception e)
        {
            if(e.Message != $"Ya existe Titular con DNI: {T.DNI}"){
                Console.WriteLine("RIP en Agregar Titular");
                Console.WriteLine(e.Message);
            }else{
                throw e;
            }
        }
    }

    public void ModificarTitular(Titular T)
    {
        try
        {
            // abrimos en modo lectura
            string str;
            using (StreamReader sr = new StreamReader(s_Archivo))
            {
                // leemos todo el archivo
                str = sr.ReadToEnd();
            }
            // comprobamos de forma general si existe el DNI buscado
            if (str.IndexOf(T.DNI.ToString()) != -1)
            {
                // si existe buscamos la linea que le corresponde
                string[] titulares = str.Split('\n'); // nota: al hacer split quedan algunos campos vacios
                for (int j = 0; j < titulares.Length; j++)
                {
                    titulares[j] = titulares[j].ReplaceLineEndings(""); // elimina todo salto de linea
                    //Console.WriteLine($"Linea {j}: "+titulares[j]);
                }
                int indice = -1;
                int i = 0;
                while (indice == -1)
                {
                    if (titulares[i].IndexOf(T.DNI.ToString()) != -1)
                    {
                        indice = i;
                    }
                    i++;
                }
                // le pasamos la id correspondiente que está entre la pos 0 y el primer : del string
                T.ID = int.Parse(titulares[indice].Substring(0, titulares[indice].IndexOf(":")));
                // sobrescribimos el titular viejo con el nuevo
                titulares[indice] = T.ToString();
                // rescribimos el archivo con el titular modificado
                using (StreamWriter sw = new StreamWriter(s_Archivo, append: false))
                {
                    foreach (string titular in titulares)
                    {
                        if (titular != "")
                        { // solo vuelve a guardar los campos que tienen datos realmente
                            sw.WriteLine(titular);
                        }
                    }
                }
            }
            else
            {
                throw new Exception($"No existe titular con DNI: {T.DNI}");
            }
        }
        catch (Exception e)
        {
            if (e.Message == $"No existe titular con DNI: {T.DNI}")
            {
                throw e;
            }
            else
            {
                Console.WriteLine("RIP en Modificar Titular");
                Console.WriteLine(e.Message);
            }
        }
    }

    public void EliminarTitular(int ID)
    {
        try
        {
            //leo todo el archivo y lo guardo en texto
            string texto;
            using (StreamReader sr = new StreamReader(s_Archivo))
            {
                texto = sr.ReadToEnd();
            }

            Console.WriteLine(texto);

            //busco el titular, teniendo en cuenta el dni
            //IndexOf indica el índice de base cero de la primera aparición de un carácter Unicode especificado o de una cadena en la instancia en cuestión. El método devuelve -1, si el carácter o cadena no se encuentran en esta instancia. 

            int indice = texto.IndexOf(ID.ToString());

            //si encontre el titular a borrar
            if (indice != -1)
            {
                //guardo en un vector el archivo donde c/pos tiene una linea 
                //aca me guarda lineas vacias si es q hay
                string[] vector = texto.Split("\n");

                //abro el archivo para leer
                using (StreamWriter sw = new StreamWriter(s_Archivo))
                {
                    //recorro el vector y si encuentro el titular a borrar directamente no lo escribo en el texto
                    foreach (string linea in vector)
                    {
                        int encontre = linea.IndexOf(ID.ToString() + ":");
                        //SI NO LO ENCUENTRO LO ESCRIBO
                        //me aseguro tmb d no escribir lineas vacias
                        if ((encontre == -1) && (linea != ""))
                            //replace borra los saltos de linea que se encuentran en mi linea
                            sw.WriteLine(linea.ReplaceLineEndings(""));                  
                    }
                }
            }
            else
            {
                throw new Exception($"No se encontro titular con ID = {ID} para eliminar");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }
    }


    public List<Titular> ListarTitulares()
    {
        List<Titular> lista = new List<Titular>();

        string? linea;
        Titular t;
        using (StreamReader sr = new StreamReader(s_Archivo))
        {
            while (!sr.EndOfStream)
            {
                linea = sr.ReadLine();

                string[]? vector = linea != null && linea != "" ? linea.Split(' ', ':', ',') : null;

                if (vector != null)
                {
                    t = new Titular(int.Parse(vector[3]), vector[5], vector[7]) { ID = int.Parse(vector[0]) };

                    for (int i = 7; i < vector.Count(); i++)
                    {
                        switch (vector[i])
                        {
                            case "Direccion":
                                string campo = "";
                                //estoy en la pos direccion asi q me interesa la siguiente
                                i++;
                                //tengo que leer el vector hasta que encuentre telefono, email, listaVehiculos o llegue al final del arreglo
                                while ((i!=vector.Length)&&(vector[i] != "Telefono") && (vector[i] != "Email") && (vector[i] != "Vehiculos"))
                                {
                                    campo += vector[i] + " ";
                                    i++;
                                }

                                t.Direccion = campo;
                                break;
                            case "Telefono":
                                string campoTel = "";
                                //estoy en la pos direccion asi q me interesa la siguiente
                                i++;
                                //tengo que leer el vector hasta que encuentre telefono, email, listaVehiculos o fin del arreglo
                                while ((i != vector.Length) && (vector[i] != "Email") && (vector[i] != "Vehiculos"))
                                {
                                    campoTel += vector[i] + " ";
                                    i++;
                                }

                                t.Telefono = campoTel;
                                break;
                            case "Email":
                                i++;
                                t.Email = vector[i];
                                break;
                            case "Vehiculos":
                                //listaDeVehiculos es una lista<int>
                                i++;
                                List<int> listita = new List<int>();
                                for (int j = i; j < vector.Count(); j++)
                                    listita.Add(int.Parse(vector[j]));
                                t.ListaVehiculos = listita;
                                break;
                        }
                    }
                    lista.Add(t);
                }
            }
        }
        return lista;
    }
}