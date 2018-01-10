using System.ComponentModel;
using System.Configuration;

namespace Common.Extensions
{
	public static class AppSettings
	{
		/// <summary>
		/// Получение типизированного значения из конфига
		/// </summary>
		///<example>{T} serviceUri = AppSettings.Get{T}("ServiceEndpoint");</example>
		public static T Get<T>(string key)
		{
			var appSetting = ConfigurationManager.AppSettings[key];
			if (string.IsNullOrWhiteSpace(appSetting))
				throw new ConfigurationErrorsException($"В конфигурации отсутствует ключ: {key}");

			var converter = TypeDescriptor.GetConverter(typeof(T));
			return (T)(converter.ConvertFromInvariantString(appSetting));
		}

		public static string GetConnectionString(string key)
		{
			var connectionString = ConfigurationManager.ConnectionStrings[key]?.ConnectionString;
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new ConfigurationErrorsException($"В конфигурации отсутствует ключ: {key}");
			return connectionString;
		}
	}
}
