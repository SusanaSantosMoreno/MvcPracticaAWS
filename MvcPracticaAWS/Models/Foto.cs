using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPracticaAWS.Models {
    public class Foto {

        [DynamoDBProperty("Titulo")]
        public String Titulo { get; set; }
        [DynamoDBProperty("Imagen")]
        public String Imagen { get; set; }

        public Foto () {}

        public Foto (string titulo, string imagen) {
            Titulo = titulo;
            Imagen = imagen;
        }
    }
}
