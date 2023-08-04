using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Models;
using CDPHE.H20.Data.Queries;
using CDPHE.H20.Data.ViewModels;
using Dapper;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;

namespace CDPHE.H20.Services
{
    public interface IFileService
    {
        public Task<IEnumerable<WebFile>> GetFiles(int requestId);
        public Task<string> AddFolder(string folderName);
        public Task<string> AddFile(string fileData, string fileType, string extension, int requestId);
    }
    public class FileService : IFileService
    {
        private readonly DapperContext _dbContext = new DapperContext();
        public string bucketName;
        public string baseFolder;
        public AmazonS3Client client;

        public FileService() 
        {
            baseFolder = "ServiceRequests";
            bucketName = "cdphe-dev";
            string awsAccessKey = "";
            string awsSecretKey = "";
            client = new AmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.USWest1);
        }

        public async Task<IEnumerable<WebFile>> GetFiles(int requestId)
        {
            List<WebFile> webFiles= new List<WebFile>();
            

            ListObjectsV2Request request = new ListObjectsV2Request
            {
                BucketName = bucketName,
                Prefix = baseFolder + "/" + requestId + "/"
            };

            ListObjectsV2Response response = await client.ListObjectsV2Async(request);
            foreach (S3Object obj in response.S3Objects)
            {
                // Add to WebFiles
                WebFile webFile = new WebFile();
                webFile.Name = GetFileName(obj.Key);
                webFile.Url = GeneratePreSignedUrl(obj.Key).Result;
                webFile.FileType = GetFileType(webFile.Name);
                webFiles.Add(webFile);
            }

            return webFiles;
        }

        public string GetFileName(string inputName)
        {
            int lastIndex = inputName.LastIndexOf('/');
            if (lastIndex >= 0 && lastIndex < inputName.Length - 1) // Checking if '/' exists in the string and it's not the last character
            {
                return inputName.Substring(lastIndex + 1); // We add 1 to the last index of '/' to get the characters after it
            }
            else
            {
                return string.Empty; // If there's no '/' in the string or it's the last character, return an empty string
            }
        }

        private async Task<string> GeneratePreSignedUrl(string objectKey)
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                Expires = DateTime.Now.AddMinutes(30)
            };

            return client.GetPreSignedURL(request);
        }

        public string GetFileType(string inputName)
        {
            if(inputName.ToLower().IndexOf("invoice") > -1)
            {
                return "Invoice";
            }
            else if (inputName.ToLower().IndexOf("receipt") > -1)
            {
                return "Receipt";
            }
            else
            {
                return "W-9";
            }
        }

        public async Task<string> AddFolder(string folderName)
        {
            
            string folderId = String.Empty;

            string folderPath = "ServiceRequests/" + folderName + "/";

            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = folderPath // <-- in S3 key represents a path
            };

            PutObjectResponse response = await client.PutObjectAsync(request);

            return response.ETag.ToString();
        }

        public async Task<string> AddFile(string fileData, string fileType, string extension, int requestId)
        {
            string message = "Success";
            string unique = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string keyName = "ServiceRequests/" + requestId + "/" + fileType + "_" + unique + "." + extension;

            try
            {
                var fileTransferUtility = new TransferUtility(client);

                byte[] fileBytes = Convert.FromBase64String(fileData);
                using (var fileToUpload = new MemoryStream(fileBytes))
                {
                    await fileTransferUtility.UploadAsync(fileToUpload, bucketName, keyName);
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

    }
}
