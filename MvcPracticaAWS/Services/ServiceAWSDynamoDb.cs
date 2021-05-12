using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MvcPracticaAWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPracticaAWS.Services {
    public class ServiceAWSDynamoDb {

        private DynamoDBContext context;

        public ServiceAWSDynamoDb () {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            this.context = new DynamoDBContext(client);
        }

        public async Task CreateUsuarioAsync (Usuario usuario) {
            await this.context.SaveAsync<Usuario>(usuario);
        }

        public async Task DeleteUsuarioAsync (int idUsuario) {
            await this.context.DeleteAsync<Usuario>(idUsuario);
        }

        public async Task UpdateUsuarioAsync (Usuario usuario) {
            await this.context.SaveAsync<Usuario>(usuario);
        }

        public async Task<List<Usuario>> GetUsuariosAsync () {
            var tabla = this.context.GetTargetTable<Usuario>();
            var scanOptions = new ScanOperationConfig();
            var results = tabla.Scan(scanOptions);

            List<Document> data = await results.GetNextSetAsync();
            IEnumerable<Usuario> usuarios = this.context.FromDocuments<Usuario>(data);
            return usuarios.ToList();
        }

        public async Task<Usuario> GetUsuarioAsync (int idUsuario) {
            return await this.context.LoadAsync<Usuario>(idUsuario);
        }

    }
}
