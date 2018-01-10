using Common.Extensions;
using DataAccess.Entities;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Mapping;

namespace DataAccess
{
	public class BiometryDbConnectionFactory
	{
		private IDataProvider _dataProvider;
		private string _connectionString;

		public BiometryDbConnectionFactory()
		{
			_dataProvider = new SqlServerDataProvider("LinqToDB", SqlServerVersion.v2012);
			_connectionString = AppSettings.Get<string>("LinqDbTest");

			var mappingBuilder = _dataProvider.MappingSchema.GetFluentMappingBuilder();
			BuildMappings(mappingBuilder);
		}

		public virtual DataConnection GetDataConnection()
		{
			return new DataConnection(_dataProvider, _connectionString);
		}

		private void BuildMappings(FluentMappingBuilder mappingBuilder)
		{
			mappingBuilder.Entity<User>()
				.HasSchemaName("um")
				.HasTableName("Users")
				.Property(x => x.Id).IsPrimaryKey().IsIdentity()
				.Association(x => x.Role, x => x.RoleId, x => x.Id);

			mappingBuilder.Entity<Role>()
				.HasTableName("Roles")
				.Property(x => x.Id).IsPrimaryKey().IsIdentity();
		}
	}
}
