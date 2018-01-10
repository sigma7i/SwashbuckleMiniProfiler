using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
	public interface IUsersRepository
	{
		List<User> Get(int page, int count);
		User GetById(int userId);
		User GetByIdentityName(string name);

		int Create(User user);
		int Update(User user);
		int Lock(int userId, string reason);
		int Unlock(int userId);
	}
}
