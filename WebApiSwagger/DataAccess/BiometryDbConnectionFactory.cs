using System.Data;
using System.Data.SqlClient;
using Common.Extensions;
using DataAccess.Entities;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Mapping;
using NLog;
using StackExchange.Profiling;

namespace DataAccess
{
	public class BiometryDbConnectionFactory
	{

		private static readonly ILogger log = LogManager.GetCurrentClassLogger();
		private IDataProvider _dataProvider;
		private string _connectionString;

		public BiometryDbConnectionFactory()
		{
			_dataProvider = new SqlServerDataProvider("LinqToDB", SqlServerVersion.v2012);

			_connectionString = AppSettings.GetConnectionString("LinqDbTest");

			var mappingBuilder = _dataProvider.MappingSchema.GetFluentMappingBuilder();
			BuildMappings(mappingBuilder);

#if DEBUG
			DataConnection.TurnTraceSwitchOn();
			DataConnection.WriteTraceLine = (s1, s2) =>
			{
				log.Error(s1 + " " + s2);
			};
#endif
		}

		public virtual DataConnection GetDataConnection()
		{
			DataConnection connection;
#if !DEBUG
			connection = new DataConnection(_dataProvider, _connectionString);
#else
			connection = new DataConnection(_dataProvider, GetConnection());
#endif

			return connection;
		}

		private IDbConnection GetConnection()
		{
			LinqToDB.Common.Configuration.AvoidSpecificDataProviderAPI = true;

			var dbConnection = new SqlConnection(_connectionString);
			return new StackExchange.Profiling.Data.ProfiledDbConnection(dbConnection, MiniProfiler.Current);
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
