namespace DataAccess.Entities
{
	/// <summary>
	/// Пользователь системы
	/// </summary>
	public class User
	{
		/// <summary>
		/// ID пользователя
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// ФИО пользователя
		/// </summary>
		public string FullName { get; set; }
		/// <summary>
		/// Учетная запись пользователя
		/// </summary>
		public string IdentityName { get; set; }
		/// <summary>
		/// ID роли пользователя
		/// </summary>
		public int RoleId { get; set; }
		/// <summary>
		/// Статус временной блокировки
		/// </summary>
		public bool IsLocked { get; set; }
		/// <summary>
		/// Причина блокировки
		/// </summary>
		public string LockReason { get; set; }

		/// <summary>
		/// Роль пользователя в системе
		/// </summary>
		public Role Role { get; set; }
	}
}
