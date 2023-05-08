using Aseguradora.Repositorio;
using Aseguradora.Aplicacion;

/*
// caso de uso con su inyeccion de dependencia
RepoVehiculoTXT repoV = new RepoVehiculoTXT();
RepoTitularTXT repoT = new RepoTitularTXT();
ListarTitularesConSusVehiculosUseCase listarTconV = new ListarTitularesConSusVehiculosUseCase(repoV,repoT);

List<Titular> listaT = listarTconV.Ejecutar();
foreach(Titular T in listaT){
    Console.WriteLine(T);
    Console.WriteLine($"Titular {T.Apellido}, {T.Nombre} tiene {T.ListaVehiculosInfo.Count} vehiculos.");
    if(T.ListaVehiculosInfo.Count > 0){
        foreach(Vehiculo V in T.ListaVehiculosInfo){
            Console.WriteLine("└──> "+V);
        }
    }
}
Console.ReadKey();

// instanciamos casos de uso e inyectamos las dependencias
RepoPolizaTXT repoPoli = new RepoPolizaTXT();
AgregarPolizaUseCase agregarPoliza = new AgregarPolizaUseCase(repoPoli);
ModificarPolizaUseCase modificarPoliza = new ModificarPolizaUseCase(repoPoli);
EliminarPolizaUseCase eliminarPoliza = new EliminarPolizaUseCase(repoPoli);
ListarPolizasUseCase listarPolizas = new ListarPolizasUseCase(repoPoli);

// instanciamos una poliza
// id_asegurado, valor_asegurado, franquicia, cobertura, vigenteDesde, vigenteHasta
Poliza P = new Poliza(1,20000,"0","Todo Riesgo",new DateTime(2020,3,14),new DateTime(2024,3,14));
Console.WriteLine("ID poliza antes de persistir: {0}", P.ID);

// persistimos la poliza
agregarPoliza.Ejecutar(P);
Console.WriteLine("ID poliza luego de persistir: {0}", P.ID);

// agregamos unas polizas mas
agregarPoliza.Ejecutar(new Poliza(2, 10000, "2500", "Todo Riesgo", new DateTime(2021, 3, 14), new DateTime(2025, 3, 14)));
agregarPoliza.Ejecutar(new Poliza(3, 15000, "15000", "Responsabilidad Civil", new DateTime(2019, 7, 14), new DateTime(2023, 7, 14)));

// listamos polizas
List<Poliza> polizas = listarPolizas.Ejecutar();
foreach(Poliza po in polizas){
    Console.WriteLine(po);
}

// modificamos una poliza existente
Console.WriteLine("Modificando la poliza de ID: "+polizas[2].ID);
polizas[2].Cobertura = "Todo Riesgo";
polizas[2].ValorAsegurado = 50000;
// la modificacion usa la ID de la poliza
modificarPoliza.Ejecutar(polizas[2]);

// listamos para ver la modificacion
polizas = listarPolizas.Ejecutar();
foreach (Poliza po in polizas){
    Console.WriteLine(po);
}

// eliminamos una poliza existente por medio de su ID
Console.WriteLine("Eliminando poliza con ID: 1");
eliminarPoliza.Ejecutar(1);

// listamos para ver la modificacion
polizas = listarPolizas.Ejecutar();
foreach (Poliza po in polizas){
    Console.WriteLine(po);
}

Console.ReadKey();
/*
ID poliza antes de persistir: -1
ID poliza luego de persistir: 1
1: VehiculoAsegurado:1 ValorAsegurado:20000 Franquicia:0  Cobertura:Todo Riesgo  VigenteDesde:14/3/2020 VigenteHasta:14/3/2024
2: VehiculoAsegurado:2 ValorAsegurado:10000 Franquicia:2500  Cobertura:Todo Riesgo  VigenteDesde:14/3/2021 VigenteHasta:14/3/2025
3: VehiculoAsegurado:3 ValorAsegurado:15000 Franquicia:15000  Cobertura:Responsabilidad Civil  VigenteDesde:14/7/2019 VigenteHasta:14/7/2023
Modificando la poliza de ID: 3
1: VehiculoAsegurado:1 ValorAsegurado:20000 Franquicia:0  Cobertura:Todo Riesgo  VigenteDesde:14/3/2020 VigenteHasta:14/3/2024
2: VehiculoAsegurado:2 ValorAsegurado:10000 Franquicia:2500  Cobertura:Todo Riesgo  VigenteDesde:14/3/2021 VigenteHasta:14/3/2025
3: VehiculoAsegurado:3 ValorAsegurado:50000 Franquicia:15000   Cobertura:Todo Riesgo  VigenteDesde:14/7/2019 VigenteHasta:14/7/2023
Eliminando poliza con ID: 1
2: VehiculoAsegurado:2 ValorAsegurado:10000 Franquicia:2500  Cobertura:Todo Riesgo  VigenteDesde:14/3/2021 VigenteHasta:14/3/2025
3: VehiculoAsegurado:3 ValorAsegurado:50000 Franquicia:15000   Cobertura:Todo Riesgo  VigenteDesde:14/7/2019 VigenteHasta:14/7/2023
*/
/*
// instanciamos casos de uso e inyectamos dependencia
RepoVehiculoTXT repoVehi = new RepoVehiculoTXT();
AgregarVehiculoUseCase agregarVehiculo = new AgregarVehiculoUseCase(repoVehi);
ModificarVehiculoUseCase modificarVehiculo = new ModificarVehiculoUseCase(repoVehi);
EliminarVehiculoUseCase eliminarVehiculo = new EliminarVehiculoUseCase(repoVehi);
ListarVehiculosUseCase listarVehiculos = new ListarVehiculosUseCase(repoVehi);

// instanciamos un vehiculo
Vehiculo V = new Vehiculo("AB123EF","Mercedes",2023,1);
Console.WriteLine("ID vehiculo antes de persistir: {0}",V.ID);
// persistimos el vehiculo
agregarVehiculo.Ejecutar(V);
Console.WriteLine("ID vehiculo luego de persistir: {0}",V.ID);

// agregamos unos vehiculos mas
agregarVehiculo.Ejecutar(new Vehiculo("EF320DE","Ford",2023,1));
agregarVehiculo.Ejecutar(new Vehiculo("DRO 345","Ford",2010,2));

// listamos los vehiculos
List<Vehiculo> listaV = listarVehiculos.Ejecutar();
foreach(Vehiculo ve in listaV){
    Console.WriteLine(ve);
}

// tratamos de persistir un vehiculo con un dominio existente
V = new Vehiculo("AB123EF","Renault",2022,2);
try{
    agregarVehiculo.Ejecutar(V);
}catch(Exception e){
    if(e.Message == $"Ya existe Vehiculo con Dominio: {V.Dominio}"){
        Console.WriteLine(e.Message);
        Console.WriteLine("Modificando Vehiculo con Dominio {0}",V.Dominio);
        modificarVehiculo.Ejecutar(V);
    }
}

// listamos vehiculos para comprobar la modificacion
listaV = listarVehiculos.Ejecutar();
foreach (Vehiculo ve in listaV){
    Console.WriteLine(ve);
}

// eliminaremos vehiculo por ID
Console.WriteLine("Eliminando vehiculo con ID: 1");
eliminarVehiculo.Ejecutar(1);
// nota: para eliminar el vehiculo de la lista de un titular se debe hacer de manera manual

// listamos vehiculos para comprobar la modificacion
listaV = listarVehiculos.Ejecutar();
foreach (Vehiculo ve in listaV){
    Console.WriteLine(ve);
}
Console.ReadKey();
*/
/*
ID vehiculo antes de persistir: -1
ID vehiculo luego de persistir: 1
1: Dominio:AB123EF  Marca:Mercedes  AnoFabricacion:2023 Titular:1
2: Dominio:EF320DE  Marca:Ford  AnoFabricacion:2023 Titular:1
3: Dominio:DRO 345  Marca:Ford  AnoFabricacion:2010 Titular:2
Ya existe Vehiculo con Dominio: AB123EF
Modificando Vehiculo con Dominio AB123EF
1: Dominio:AB123EF  Marca:Renault  AnoFabricacion:2022 Titular:2
2: Dominio:EF320DE  Marca:Ford  AnoFabricacion:2023 Titular:1
3: Dominio:DRO 345  Marca:Ford  AnoFabricacion:2010 Titular:2
Eliminando vehiculo con ID: 1
2: Dominio:EF320DE  Marca:Ford  AnoFabricacion:2023 Titular:1
3: Dominio:DRO 345  Marca:Ford  AnoFabricacion:2010 Titular:2
*/

/*
// instanciamos caso de uso e inyectamos la dependencia
RepoTitularTXT repoTitu = new RepoTitularTXT();
AgregarTitularUseCase agregarTitular = new AgregarTitularUseCase(repoTitu);
ListarTitularesUseCase listarTitulares = new ListarTitularesUseCase(repoTitu);
ModificarTitularUseCase modificarTitular = new ModificarTitularUseCase(repoTitu);
EliminarTitularUseCase eliminarTitular = new EliminarTitularUseCase(repoTitu);

// instanciamos un titular
Titular T = new Titular(27354200,"Suster","Laura"){Email="layu208@hotmail.com"};
Console.WriteLine("ID del titular recien instaciado: {0}",T.ID);

// persistimos el titular
agregarTitular.Ejecutar(T);
Console.WriteLine("ID del titular luego de persistirlo: {0}",T.ID);

// agregamos unos titulares más
agregarTitular.Ejecutar(new Titular(44130300,"Macias","Luciano"){Direccion="15 e/ 54 y 55"});
agregarTitular.Ejecutar(new Titular(44130303,"Loyola","Nicole"));

// listamos los titulares
List<Titular> listaTitulares = listarTitulares.Ejecutar();
foreach(Titular ti in listaTitulares){
    Console.WriteLine(ti);
}

// tratamos de agregar un titular con dni ya existente
T = new Titular(44130300,"Picapiedra","Pedro");
Console.WriteLine("Intentando agregar titular con DNI: {0}",T.DNI);
try{
    // si ya existe retorna una excepcion
    agregarTitular.Ejecutar(T);
}catch (Exception e){
    // si la excepcion es que ya existe, lo modificamos
    if(e.Message == $"Ya existe Titular con DNI: {T.DNI}"){
        Console.WriteLine(e.Message);
        Console.WriteLine($"Modificando Titular con DNI: {T.DNI}");
        modificarTitular.Ejecutar(T);
    }
}

// vemos los cambios de modificar el titular
listaTitulares = listarTitulares.Ejecutar();
foreach (Titular ti in listaTitulares){
    Console.WriteLine(ti);
}

// eliminamos un titular
Console.WriteLine("Eliminando titular con DNI: {0}",listaTitulares[1].DNI);
eliminarTitular.Ejecutar(listaTitulares[1].ID);

// vemos los titulares sin el eliminado
listaTitulares = listarTitulares.Ejecutar();
foreach (Titular ti in listaTitulares){
    Console.WriteLine(ti);
}

Console.ReadKey();
*/
/*

ID del titular recien instaciado: -1
ID del titular luego de persistirlo: 1
1: DNI:27354200 Apellido:Suster Nombre:Laura Email:layu208@hotmail.com
2: DNI:44130300 Apellido:Macias Nombre:Luciano Direccion:15 e/ 54 y 55
3: DNI:44130303 Apellido:Loyola Nombre:Nicole
Intentando agregar titular con DNI: 44130300
Ya existe Titular con DNI: 44130300
Modificando Titular con DNI: 44130300
1: DNI:27354200 Apellido:Suster Nombre:Laura Email:layu208@hotmail.com
2: DNI:44130300 Apellido:Picapiedra Nombre:Pedro
3: DNI:44130303 Apellido:Loyola Nombre:Nicole
Eliminando titular con DNI: 44130300
1: DNI:27354200 Apellido:Suster Nombre:Laura Email:layu208@hotmail.com
3: DNI:44130303 Apellido:Loyola Nombre:Nicole
*/
/*
modificarTitular.Ejecutar(new Titular(44130305,"Macias","Laureano"));

Console.ReadKey();

agregarTitular.Ejecutar(new Titular(44130305, "Paez", "Maia"));
agregarTitular.Ejecutar(new Titular(44130306, "Suster", "Laura"));

Console.ReadKey();
/* PRUEBA AGREGAR Y MODIFICAR POLIZA
RepoPolizaTXT repoPoli = new RepoPolizaTXT();
AgregarPolizaUseCase agregarPoliza = new AgregarPolizaUseCase(repoPoli);
ModificarPolizaUseCase modificarPoliza = new ModificarPolizaUseCase(repoPoli);
agregarPoliza.Ejecutar(new Poliza(1,20000,"1000","Todo Riesgo",new DateTime(2023,4,1),new DateTime(2024,4,1)));
agregarPoliza.Ejecutar(new Poliza(1, 40000, "40000", "Responsabilidad Civil", new DateTime(2023, 6, 1), new DateTime(2024, 6, 1)));

Console.ReadKey();

modificarPoliza.Ejecutar(new Poliza(1, 40000, "0", "Todo Riesgo", new DateTime(2023, 6, 1), new DateTime(2024, 6, 1)){ID=2});

Console.ReadKey();

agregarPoliza.Ejecutar(new Poliza(1,20000,"0","Todo Riesgo",new DateTime(2023,5,23),new DateTime(2024,5,23)));

Console.ReadKey();
*/
/*
RepoVehiculoTXT repoVehi = new RepoVehiculoTXT();
AgregarVehiculoUseCase agregarVehiculo = new AgregarVehiculoUseCase(repoVehi);
ModificarVehiculoUseCase modificarVehiculo = new ModificarVehiculoUseCase(repoVehi);

agregarVehiculo.Ejecutar(new Vehiculo("Prenda","Ford",2022,1));
agregarVehiculo.Ejecutar(new Vehiculo("Prenda", "Mercedes", 2020, 1));

Console.ReadKey();

modificarVehiculo.Ejecutar(new Vehiculo("Robo","Ford",2022,1){ID=1});

Console.ReadKey();

agregarVehiculo.Ejecutar(new Vehiculo("Embargo","Audi",2019,2));

Console.ReadKey();
*/


//PARTE NICKY   ---->

/*

//creo el repositorio para titular
RepoTitularTXT repoTitular = new RepoTitularTXT();

//instancio el caso de uso de titular con mi repositorio
EliminarTitularUseCase eliminarTitular = new EliminarTitularUseCase(repoTitular);
ListarTitularesUseCase listarTitular = new ListarTitularesUseCase(repoTitular);

//listarTitular.Ejecutar();

Console.WriteLine("Eliminando el Titular con ID {0}",2);
eliminarTitular.Ejecutar(3);


List<Titular> listaTitular = listarTitular.Ejecutar();
foreach(Titular elem in listaTitular){
    Console.WriteLine(elem);
}

//----------------------------------------------------------

RepoPolizaTXT repoPoliza = new RepoPolizaTXT();

EliminarPolizaUseCase eliminarPoliza = new EliminarPolizaUseCase(repoPoliza);
ListarPolizasUseCase listarPoliza = new ListarPolizasUseCase(repoPoliza);

Console.WriteLine("Eliminando la Poliza con ID {0}",1);
eliminarPoliza.Ejecutar(2);

List<Poliza> listaPoliza = listarPoliza.Ejecutar();
foreach(Poliza elem in listaPoliza){
    Console.WriteLine(elem);
}



//------------------------------------------------------------

RepoVehiculoTXT repoVehiculo = new RepoVehiculoTXT();

EliminarVehiculoUseCase eliminarVehiculo = new EliminarVehiculoUseCase(repoVehiculo);
ListarVehiculosUseCase listarVehiculo = new ListarVehiculosUseCase(repoVehiculo);

Console.WriteLine("Eliminando el Vehiculo con ID {0}",2);
eliminarVehiculo.Ejecutar(2);

List<Vehiculo> listaVehiculo = listarVehiculo.Ejecutar();
foreach(Vehiculo elem in listaVehiculo){
    Console.WriteLine(elem);
}

*/
