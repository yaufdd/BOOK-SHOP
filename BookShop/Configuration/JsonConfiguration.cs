using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace BookShop.Configuration
{
    public static class JsonConfiguration
    {
        public static void AddJson(this IServiceCollection services)
        {
            services.AddSingleton<IJsonSerializerSettingsProvider, MvcJsonSerializerSettingsProvider>();
            services.AddSingleton(sp => JsonSerializer.CreateDefault());
        }

        public static void ConfigureMvc(this MvcJsonOptions mvcJsonOptions)
        {
            mvcJsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            mvcJsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            mvcJsonOptions.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            mvcJsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            mvcJsonOptions.SerializerSettings.Converters = new List<JsonConverter>
            {
                new StringEnumConverter(),
            };
        }
    }

    //TODO: Вынести в shared модуль
    #region Shared
    public interface IJsonSerializerSettingsProvider
    {
        JsonSerializerSettings GetSettings();
    }

    public class MvcJsonSerializerSettingsProvider : IJsonSerializerSettingsProvider
    {
        private readonly IOptions<MvcJsonOptions> _options;

        public MvcJsonSerializerSettingsProvider(IOptions<MvcJsonOptions> options)
        {
            _options = options ?? throw new ArgumentException(nameof(options));
        }

        public JsonSerializerSettings GetSettings()
            => _options.Value.SerializerSettings;
    }
    #endregion
}