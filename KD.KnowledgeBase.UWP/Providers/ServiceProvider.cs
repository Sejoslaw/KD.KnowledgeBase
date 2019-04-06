using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KD.KnowledgeBase.UWP.Models;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Streams;

namespace KD.KnowledgeBase.UWP.Providers
{
    public class ServiceProvider : IServiceProvider
    {
        private string FileName { get; }
        private IStorageFolder StorageFolder { get; }

        public ServiceProvider(string fileName, IStorageFolder storageFolder)
        {
            this.FileName = fileName;
            this.StorageFolder = storageFolder;
        }

        public async Task<IEnumerable<SingleServiceModel>> ReadServicesAsync()
        {
            var storageFile = await this.StorageFolder.GetItemAsync(FileName) as IStorageFile;

            IEnumerable<SingleServiceModel> services = null;

            if (storageFile != null)
            {
                using (var stream = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    using (var reader = new DataReader(stream))
                    {
                        var streamSize = (uint)stream.Size;

                        await reader.LoadAsync(streamSize);
                        var json = reader.ReadString(streamSize);
                        services = JsonConvert.DeserializeObject<List<SingleServiceModel>>(json);
                    }
                }
            }

            return services ?? new List<SingleServiceModel>();
        }

        public async Task WriteServicesAsync(IEnumerable<SingleServiceModel> services)
        {
            var storageFile = await this.StorageFolder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);

            using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var writer = new DataWriter(stream))
                {
                    var json = JsonConvert.SerializeObject(services, Formatting.Indented);
                    writer.WriteString(json);
                    await writer.StoreAsync();
                }
            }
        }
    }
}
