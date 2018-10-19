using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PWMgr.Models;

namespace PWMgr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PWController : ControllerBase
    {
        public CloudStorageAccount storageAccount;
        public CloudTableClient tableClient;
        public CloudTable passwordsTable;

        public PWController()
        {
            ///TODO:  Need to see if there is a more elegant location to put this code rather than having a constructor that gets called for every request.  
            ///Given my service is stateless I'm at a loss right now...
            storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
            "mwpwstorage", "Zb416ihBwBdSd09+k2paoJtRENAExomfa4MpEtY0UcXxLUwG0caqa/LP3rGxAXHTfHFbvbeG9hH9lQIx3SczMQ=="), true);
            // Create the table client.
            tableClient = storageAccount.CreateCloudTableClient();
            // Get a reference to a table named "passwordsTable"
            passwordsTable = tableClient.GetTableReference("passwordsTable");

            async void CreatePasswordsTableAsync()
            {
                // Create the CloudTable if it does not exist
                await passwordsTable.CreateIfNotExistsAsync();
            }

            CreatePasswordsTableAsync();

        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(PasswordEntity item)
        {
            //Todo A good coder would put error handling in here...
            async void Insert()
            {
                TableOperation insertOperation = TableOperation.Insert(item);
                await passwordsTable.ExecuteAsync(insertOperation);
            }

            Insert();

           

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}