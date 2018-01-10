using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using DataAccess.Entities;
using LinqToDB;

namespace DataAccess.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		private readonly BiometryDbConnectionFactory _connectionFactory;

		public UsersRepository(BiometryDbConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public List<User> Get(int page, int count)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				var q = conn.GetTable<User>().ToPagedQuery(page, count);

				return q.ToList();
			}
		}

		public User GetById(int userId)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				var q = from u in conn.GetTable<User>().LoadWith(u => u.Role)
						where u.Id == userId
						select u;

				return q.FirstOrDefault();
			}
		}

		public User GetByIdentityName(string name)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				var q = from u in conn.GetTable<User>().LoadWith(u => u.Role)
						where u.IdentityName == name
						select u;

				return q.FirstOrDefault();
			}
		}

		public int Create(User user)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				var identity = conn.InsertWithIdentity(user);
				user.Id = (int)Convert.ToInt64(identity);
				return user.Id;
			}
		}

		public int Update(User user)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				return conn.Update(user);
			}
		}

		public int Lock(int userId, string reason)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				return conn.GetTable<User>()
					.Where(x => x.Id == userId && x.IsLocked == false)
					.Set(x => x.IsLocked, true)
					.Set(x => x.LockReason, reason)
					.Update();
			}
		}

		public int Unlock(int userId)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				return conn.GetTable<User>()
					.Where(x => x.Id == userId && x.IsLocked == true)
					.Set(x => x.IsLocked, false)
					.Set(x => x.LockReason, (string)null)
					.Update();
			}
		}
	}
}
