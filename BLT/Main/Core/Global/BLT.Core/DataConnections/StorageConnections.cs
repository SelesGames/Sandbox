using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLT.Core.DataConnections
{
    public class StorageConnections
    {
        private string _accountsStorageName { get; set; }
        private string _accountsStorageKey { get; set; }

        private string _platformStorageName { get; set; }
        private string _platformStorageKey { get; set; }


        public StorageConnections(string platformStorageName, string platformStorageKey, string accountsStorageName, string accountsStorageKey)
        {
            _platformStorageName = platformStorageName;
            _platformStorageKey = platformStorageKey;

            _accountsStorageName = accountsStorageName;
            _accountsStorageKey = accountsStorageKey;
        }


        public CloudStorageAccount AccountsStorage
        {
            get
            {
                CloudStorageAccount _storageAccount;

                StorageCredentials _storageCredentials = new StorageCredentials(_accountsStorageName, _accountsStorageKey);

                _storageAccount = new CloudStorageAccount(_storageCredentials, false);

                return _storageAccount;
            }
        }

        public CloudStorageAccount PlatformStorage
        {
            get
            {
                CloudStorageAccount _storageAccount;

                StorageCredentials _storageCredentials = new StorageCredentials(_platformStorageName, _platformStorageKey);

                _storageAccount = new CloudStorageAccount(_storageCredentials, false);

                return _storageAccount;
            }
        }

    }
}
