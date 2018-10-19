using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace PWMgr.Models
{
    public class PasswordEntity: TableEntity
    {
        public PasswordEntity(string userName, string row)
        {
            this.PartitionKey = userName;  //when this becomes multi-user I will simply just query off the user to ensure tenancy
            //for now I"m passing this in.  In the future I need to persist the last key, read it, then increment to avoid having to do this.
            this.RowKey = row;
        }

        public string email { get; set; }

        public string password { get; set; }

        public string notes { get; set; }

        public Uri siteURL { get; set; }

        public string siteName { get; set; }

        public string siteCategory { get; set; }
    }
}
