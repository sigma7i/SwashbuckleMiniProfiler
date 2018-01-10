namespace WebApiSwagger.ExceptionHandling
{
	/// <summary>
	/// Описание перехваченного исключения
	/// </summary>
	internal sealed class ClientExceptionInformation
	{
		/// <summary>
		///  User friendly описание ошибки, общее описание
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Детальное описание ошибки
		/// </summary>
		public string DetailedInformation { get; set; }
	}
}