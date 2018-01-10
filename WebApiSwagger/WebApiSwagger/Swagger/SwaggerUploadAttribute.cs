using System;

namespace WebApiSwagger.Swagger
{
	/// <summary>
	/// Указывает swagger о необходимости добавления контрола выбора файла
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class SwaggerUploadAttribute : Attribute
	{

		public SwaggerUploadAttribute(string parameterName = "ImportFile", string description = "Upload file")
		{
			ParameterName = parameterName;
			Description = description;
		}

		/// <summary>
		/// Имя параметра
		/// </summary>
		public string ParameterName { get; private set; }

		/// <summary>
		///  Описание параметра
		/// </summary>
		public string Description { get; private set; }

	}
}