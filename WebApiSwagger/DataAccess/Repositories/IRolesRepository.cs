using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
	public interface IRolesRepository
	{
		List<Role> Get();
		Role GetById(int roleId);

		int Create(Role role);
		int Update(Role role);
		int Lock(int roleId);
		int Unlock(int roleId);
	}
}
