using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace azure_dotnet 
{

    public class StudentRepository : IStudentRepository
    {
        private string _connectionString;
        private CloudTableClient _tableClient;

        private CloudTable _studentsTable;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("AzureStorageAccountConnectionString");
            Task.Run(async () => {await InitializeTable(); })
            .GetAwaiter()
            .GetResult();

        }
        public void CreateNewStudent(StudentEntity student)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<List<StudentEntity>> GetAllStudents()
        {
            var students = new List<StudentEntity>();

            TableQuery <StudentEntity> query = new TableQuery<StudentEntity>();
            
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await _studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                students.AddRange(resultSegment.Results);
            } while (token != null);

            return students;
        }

        private async Task InitializeTable(){
         /*   string storageConnectionString = "DefaultEndpointsProtocol=https;"
            + "AccountName=datc2020rp;"
            + "AccountKey=wq4JklVqVh0MMUo2FQZ9xOQ5u4HlJusVsbC+8RtyMR9dIAWgN2J0fXDsrjZzuAC1Z+qKDMbeXuNmAaD33MYXjw==;"
            + "EndpointSuffix=core.windows.net";
*/

            var account = CloudStorageAccount.Parse(_connectionString);
            _tableClient = account.CreateCloudTableClient();

            _studentsTable = _tableClient.GetTableReference("studenti");

            await _studentsTable.CreateIfNotExistsAsync();
            
        }
    }
}