using System;
using System.Collections.Generic;

namespace JMiles42.Systems.Database {
	[Serializable]
	public class Table {
		public string TableName;
		[NonSerialized] public Database Database;
		public List<string> Collems = new List<string>();

		public Table(string tableName, Database database) {
			TableName = tableName;
			Database = database;
			GetCollems();
		}

		public object[] GetCollemData(int collem) { return GetCollemData(Collems[collem]); }

		public object[] GetCollemData(string collem) { return Database.GetQuery(string.Format("SELECT {0} FROM {1}", collem, TableName), 0); }

		private void GetCollems() {
			var dbIsOpen = Database.IsOpen;
			Database.IsOpen = true;

			using (var command = Database.Connection.CreateCommand()) {
				command.CommandText = string.Format("PRAGMA table_info('{0}');", TableName);
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						Collems.Add(reader[1].ToString());
					}
				}

				Database.IsOpen = dbIsOpen;
			}
		}
	}
}