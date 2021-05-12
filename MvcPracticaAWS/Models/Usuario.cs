using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPracticaAWS.Models {
    [DynamoDBTable("Usuario")]
    public class Usuario {
    
        [DynamoDBProperty("IdUsuario")]
        [DynamoDBHashKey]
        public int IdUsuario { get; set; }
        [DynamoDBProperty("Nombre")]
        public String Nombre { get; set; }
        [DynamoDBProperty("Descripcion")]
        public String Descripcion { get; set; }
        [DynamoDBProperty("FechaAlta")]
        public DateTime FechaAlta { get; set; }
        [DynamoDBProperty("Fotos")]
        public List<Foto> Fotos { get; set; }

        public Usuario () { }

        public Usuario (int idUsuario, string nombre, string descripcion, 
            DateTime fechaAlta, List<Foto> fotos) {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaAlta = fechaAlta;
            Fotos = fotos;
        }
    }
}
