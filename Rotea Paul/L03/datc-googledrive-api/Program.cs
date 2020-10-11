using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;


namespace datc_googledrive_api
{
    class Program
    {
         static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "DATC2020";

        static DriveService service;
        
          static  UserCredential credential;
        static void Main(string[] args)
        { 
            Initialize();

            GetAllFiles();
        }

        static void Initialize(){

       /*   var  clientId= "215906492701-m8d0gq7678n0g9j1aa0hkarmjarkalod.apps.googleusercontent.com";
          var clientSecret = "hxoGN2Ln_rjbdmaPAfl8OhdT";
          var cre = GoogleWebAuthorizationBroker.AuthorizeAsync(){
              new ClientSecrets{
                  ClientId = clientId,
                  ClientSecret = clientSecret
              }
          }*/

            using (var stream =
                new FileStream("client-datc2.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

        }
        static void GetAllFiles(){
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/drive/v3/files?q='root'%20in%20parents");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + credential.Token.AccessToken);

            using(var response = request.GetResponse())
            {
                using (Stream data = response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    string text = reader.ReadToEnd();
                    var myData = JObject.Parse(text);
                    foreach(var file in myData["files"])
                    {
                        if (file["mimeType"].ToString() != "application/vnd.giigke-apps.folder")
                        {
                            Console.WriteLine("File name: " + file["name"]);
                        }

                    }

                }

            }

        }

        public async Task<Google.Apis.Drive.v3.Data.File> Upload(IFormFile file, string documentId)
        {
            var name = ($"{DateTime.UtcNow.ToString()}.{Path.GetExtension(file.FileName)}");
            var mimeType = file.ContentType;

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = mimeType,
                Parents = new[] { documentId }
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = file.OpenReadStream())
            {
                request = service.Files.Create(
                    fileMetadata, stream, mimeType);
                request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType, thumbnailLink";
                await request.UploadAsync();
            }


            return request.ResponseBody;
        }
        
    }
}
