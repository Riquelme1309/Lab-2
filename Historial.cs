using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tarea2_de_navegador
{
    internal class Historial
    {
        private readonly string archivoHistorial;

        public Historial(string nombreArchivo)
        {
            archivoHistorial = nombreArchivo;
        }

        public List<Direccion> LeerHistorial()
        {
            if (!File.Exists(archivoHistorial))
            {
                return new List<Direccion>();
            }

            string json = File.ReadAllText(archivoHistorial);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Direccion>();
            }

            return JsonConvert.DeserializeObject<List<Direccion>>(json)
                   ?? new List<Direccion>();
        }

        public void GuardarHistorial(List<Direccion> direcciones)
        {
            string json = JsonConvert.SerializeObject(
                direcciones,
                Formatting.Indented
            );

            File.WriteAllText(archivoHistorial, json);
        }

        public void AgregarDireccion(string url)
        {
            List<Direccion> direcciones = LeerHistorial();

            Direccion existente = direcciones
                .FirstOrDefault(d => d.Url.Equals(url, StringComparison.OrdinalIgnoreCase));

            if (existente != null)
            {
                existente.Veces++;
                existente.FechaAcceso = DateTime.Now;
            }
            else
            {
                direcciones.Add(new Direccion
                {
                    Url = url,
                    Veces = 1,
                    FechaAcceso = DateTime.Now
                });
            }

            GuardarHistorial(direcciones);
        }

        public void EliminarDireccion(string url)
        {
            List<Direccion> direcciones = LeerHistorial();

            Direccion eliminar = direcciones
                .FirstOrDefault(d => d.Url.Equals(url, StringComparison.OrdinalIgnoreCase));

            if (eliminar != null)
            {
                direcciones.Remove(eliminar);
                GuardarHistorial(direcciones);
            }
        }

        public List<Direccion> OrdenarPorUrl()
        {
            return LeerHistorial()
                .OrderBy(d => d.Url)
                .ToList();
        }

        public List<Direccion> OrdenarPorVeces()
        {
            return LeerHistorial()
                .OrderByDescending(d => d.Veces)
                .ToList();
        }

        public List<Direccion> OrdenarPorFecha()
        {
            return LeerHistorial()
                .OrderByDescending(d => d.FechaAcceso)
                .ToList();
        }
    }
}

