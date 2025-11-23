using ACollahuazoS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACollahuazoS5.Clases
{
    public class PersonRepository
    {
        string ruta;
        private SQLiteConnection conn;

        public string mensaje {  get; set; }

        private void init()
        {
            if (conn is not null)
                return;

            conn = new SQLiteConnection(ruta);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string path)
        {
            ruta = path;
        }

        public void addNewPerson(string nombre, string apellido, string edad)
        {
            try
            {
                init();  // ← IMPORTANTE

                var persona = new Persona
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Edad = edad
                };

                conn.Insert(persona);  // ← CORREGIDO
                mensaje = "Persona agregada correctamente";
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message;
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                mensaje = "Error " + ex.Message;
                return new List<Persona>();
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                init();
                var persona = conn.Find<Persona>(id);
                if (persona == null)
                {
                    mensaje = "Persona no encontrada";
                    return;
                }
                conn.Delete(persona);
                mensaje = $"Persona eliminada: {persona.Nombre}";
            }
            catch (Exception ex)
            {
                mensaje = $"Error al eliminar: {ex.Message}";
            }
        }

        public void UpdatePerson(int id, string nuevoNombre)
        {
            try
            {
                init();
                var persona = conn.Find<Persona>(id);
                if (persona == null)
                {
                    mensaje = "Persona no encontrada";
                    return;
                }
                persona.Nombre = nuevoNombre;
                conn.Update(persona);
                mensaje = $"Persona actualizada: {nuevoNombre}";
            }
            catch (Exception ex)
            {
                mensaje = $"Error al actualizar: {ex.Message}";
            }
        }
    }
}
