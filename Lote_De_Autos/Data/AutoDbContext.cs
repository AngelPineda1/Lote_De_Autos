using Lote_De_Autos.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lote_De_Autos.Data
{
    public class AutoDbContext
    {
        //Forma rapida para poder crear la tabla en sql y poder hacer el CRUD
        private const string _ConectionString = "Data Source=autos.db";

        private string _dataBaseFilName = "loteautos.db";
        const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            //crea la base de datos si no existe
            SQLite.SQLiteOpenFlags.Create |
            //abilita multi-threaded acces
            SQLite.SQLiteOpenFlags.SharedCache;

        public string DataBasePath => Path.Combine(FileSystem.AppDataDirectory, _dataBaseFilName);
        private string _conectionString = "";



        SQLiteAsyncConnection db;

        //Crea la tabla en sql
        public async Task Init()
        {
            if (db != null)
            {
                return;
            }
            db = new SQLiteAsyncConnection(DataBasePath, Flags);
            SQLite.CreateFlags createFlags = SQLite.CreateFlags.ImplicitPK | SQLite.CreateFlags.AutoIncPK;
            var resulr = await db.CreateTableAsync<Auto>(createFlags);
        }


        public async Task Add(Auto auto)
        {
            await Init();
            await db.InsertAsync(auto);
        }
        public async Task<Auto> GetById(int id)
        {
            await Init();
            return await db.Table<Auto>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Auto>> GetAll()
        {
            await Init();
            return await db.Table<Auto>().ToListAsync();
        }
        public async Task<Auto> Actualizar(Auto auto)
        {
            await Init();
            await db.UpdateAsync(auto);
            var auton=await GetById(auto.Id);

            return auton;
        }
        public async Task<bool> Eliminar(int id)
        {
            await Init();
            var art = await GetById(id);
            if (art != null)
            {
                await db.DeleteAsync(art);
                return true;
            }
            return false;

        }

    }
}
