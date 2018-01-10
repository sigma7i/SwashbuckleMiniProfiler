namespace DataAccess.Entities
{
	/// <summary>
	/// Роль
	/// </summary>
	public class Role
	{
		/// <summary>
		/// ID роли
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Комментарий
		/// </summary>
		public string Comment { get; set; }
		/// <summary>
		/// Признак блокировки роли
		/// </summary>
		public bool IsLocked { get; set; }
	}
}
