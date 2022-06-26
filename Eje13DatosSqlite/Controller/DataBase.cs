using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eje13DatosSqlite.Models;
using SQLite;

namespace Eje13DatosSqlite.Controller
{
    
    
    public class DataBase
    {
        readonly SQLiteAsyncConnection dbase;
        
        public DataBase (string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);

            /* Crearemos tablas de BD*/

            dbase.CreateTableAsync<Models.Personas>();//se esta creando la tabla de empleados con tan solo llamar la clase de empleado con su estructura de clase.
           

        }

        #region OperacionesPersona
        /*CRUD*/
        //create
        public Task<int> SavePersona(Personas persona)
        {
            if (persona.id != 0)//update del registro
            {
                return dbase.UpdateAsync(persona);
            }
            else
            {
                return dbase.InsertAsync(persona);//inserter nuevo registro
            }

        } //tareas asincronas, no para la aplicacion se ejecuta la linea de codigo y se espera el resultado. Devuelve un valor de estado entero 0/1.

        //read
        public Task<List<Personas>> getListPersonas()
        { 
            return dbase.Table<Personas>().ToListAsync();//se convierte el resultado a una lista.
        }

        //Read por empleado

        public Task<Personas> getPersona(int pid)
        {
            return dbase.Table<Personas>()//se usa explesion lamba
                .Where(i => i.id == pid)
                .FirstOrDefaultAsync();
        }

        //delete
        public Task<int> DeletePersona(Personas persona)
        {
            return dbase.DeleteAsync(persona);
        }
        #endregion OperacionesPersona

    }
}
