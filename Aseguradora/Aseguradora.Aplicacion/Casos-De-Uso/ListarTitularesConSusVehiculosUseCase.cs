namespace Aseguradora.Aplicacion;
public class ListarTitularesConSusVehiculosUseCase{
    IRepoVehiculo _repoV;
    IRepoTitular _repoT;
    public ListarTitularesConSusVehiculosUseCase(IRepoVehiculo repoV, IRepoTitular repoT){
        _repoV = repoV;
        _repoT = repoT;
    }
    public List<Titular> Ejecutar(){
        List<Titular> titulares = _repoT.ListarTitulares(); // recuperamos todos los titulares del archivo
        List<Vehiculo> vehiculos = _repoV.ListarVehiculos(); // recuperamos los vehiculos del archivo
        // primero recorremos los titulares para cargar a partir de sus listas
        foreach(Titular T in titulares){ // para cada titular
            foreach(Vehiculo V in vehiculos){ // recorremos todos los vehiculos
                foreach(int ID in T.ListaVehiculos){ // y recorremos la lista de ID's del titular
                    if(V.ID == ID){ // si alguna ID coincide con el vehiculo se le asigna al titular
                        T.ListaVehiculosInfo.Add(V);
                    }
                }
            }
        }
        // recorremos los titulares para cargar a partir de los titulares a los que corresponden los vehiculos
        foreach(Vehiculo V in vehiculos){ // para cada vehiculo
            foreach(Titular T in titulares){ // comprobamos cada titular
                if(V.Titular == T.ID && !T.ListaVehiculosInfo.Contains(V)){ // y si no fue agregado antes se agrega
                    T.ListaVehiculosInfo.Add(V);
                }
            }
        }
        return titulares;
    }
}