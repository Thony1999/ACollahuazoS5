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
            conn = new(ruta);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string path)
        {
            ruta = path; //asigno un valor
        }

        public void addNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerimiento");
                Persona persona = new() { Name = nombre };
                result = conn.Insert(persona);
                mensaje = string.Format("Dato Ingresado");
            }
            catch (Exception ex)
            {
                mensaje = string.Format("Error " + ex.Message);
                throw;
            }
        }

        public List<Persona> GetAllPeople() 
        {
            try
            {
                init();// lista todas las personas
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                mensaje = string.Format("Error " + ex.Message);
            }
            return new List<Persona>(); // lista con los datos
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
                mensaje = $"Persona eliminada: {persona.Name}";
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
                persona.Name = nuevoNombre;
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
