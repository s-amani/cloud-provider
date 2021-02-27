using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CloudProvider.SDK.Common;
using Microsoft.Extensions.Logging;

namespace CloudProvider.SDK.Persistence.Provider
{
    public class JsonFileProvider : IFileProvider
    {
        #region Fields

        private readonly ILogger<JsonFileProvider> _logger;

        #endregion

        #region Ctor

        public JsonFileProvider(ILogger<JsonFileProvider> logger)
        {
            logger.ThrowExceptionIfNull(nameof(logger));

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<bool> Save(string path, object data)
        {
            try
            {
                using FileStream fs = File.Create(path);
                await JsonSerializer.SerializeAsync(fs, data);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);

                return false;
            }
        }

        #endregion
    }
}
