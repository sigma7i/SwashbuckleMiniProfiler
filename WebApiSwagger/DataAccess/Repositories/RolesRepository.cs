using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using LinqToDB;

namespace DataAccess.Repositories
{
	public class RolesRepository : IRolesRepository
	{
		private readonly BiometryDbConnectionFactory _connectionFactory;

		public RolesRepository(BiometryDbConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public List<Role> Get()
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				var q = from r in conn.GetTable<Role>()
						select r;

				return q.ToList();
			}
		}

		public Role GetById(int roleId)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				var q = from r in conn.GetTable<Role>()
						where r.Id == roleId
						select r;

				return q.FirstOrDefault();
			}
		}

		public int Create(Role role)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				var identity = conn.InsertWithIdentity(role);
				role.Id = (int)Convert.ToInt64(identity);
				return role.Id;
			}
		}

		public int Update(Role role)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				return conn.Update(role);
			}
		}

		public int Lock(int roleId)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				return conn.GetTable<Role>()
					.Where(x => x.Id == roleId && x.IsLocked == false)
					.Set(x => x.IsLocked, true)
					.Update();
			}
		}

		public int Unlock(int roleId)
		{
			using (var conn = _connectionFactory.GetDataConnection())
			{
				return conn.GetTable<Role>()
					.Where(x => x.Id == roleId && x.IsLocked == true)
					.Set(x => x.IsLocked, false)
					.Update();
			}
		}
	}
}
