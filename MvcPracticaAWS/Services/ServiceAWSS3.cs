using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MvcPracticaAWS.Services {
    public class ServiceAWSS3 {

        private String bucketName;
        private IAmazonS3 awsClient;

        public ServiceAWSS3 (IAmazonS3 awsclient, IConfiguration configuration) {
            this.awsClient = awsclient;
            this.bucketName = configuration["AWSS3:BucketName"];
        }

        public async Task<bool> UploadFileAsync (Stream stream, String fileName) {
            PutObjectRequest request = new PutObjectRequest() {
                InputStream = stream,
                Key = fileName,
                BucketName = this.bucketName
            };
            PutObjectResponse response = await this.awsClient.PutObjectAsync(request);
            bool status = response.HttpStatusCode == HttpStatusCode.OK ? true : false;
            return status;
        }

        public async Task<List<String>> GetS3FilesAsync () {
            ListVersionsResponse response =
            await this.awsClient.ListVersionsAsync(this.bucketName);
            return response.Versions.Select(x => x.Key).ToList();
        }

        public async Task<bool> DeleteFileAsync (string fileName) {
            DeleteObjectResponse response =
            await this.awsClient.DeleteObjectAsync(this.bucketName, fileName);
            bool status = response.HttpStatusCode == HttpStatusCode.NoContent ? true : false;
            return status;
        }

        public async Task<Stream> GetFileAsync (String fileName) {
            GetObjectResponse response = await this.awsClient.GetObjectAsync(this.bucketName, fileName);
            Stream stream = response.HttpStatusCode == HttpStatusCode.OK ? response.ResponseStream : null;
            return stream;
        }

    }
}
