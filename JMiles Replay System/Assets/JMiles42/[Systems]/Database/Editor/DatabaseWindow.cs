using System.Collections.Generic;
using JMiles42.Editor;
using UnityEditor;

namespace JMiles42.Systems.Database
{
    public class DatabaseWindow: TabedWindow<DatabaseWindow>
    {
        private List<Tab<DatabaseWindow>> _tabs = new List<Tab<DatabaseWindow>> {new DatabaseManagerTab()};

        public override Tab<DatabaseWindow>[] Tabs {
            get { return _tabs.ToArray(); }
        }
        public Database Database;

        private const string Title = "DatabaseWindow";

        [MenuItem("JMiles42/" + Title)]
        private static void Init()
        {
            GetWindow();
            window.titleContent.text = Title;
        }

        public void OnEnable()
        {
            TitleBarPosition = TitleBarPos.Top;
            Database = new Database(FileAndPathHelpers.GetStreamingAssetsPath("Test.db"), true);
            foreach (var table in Database.Tables)
            {
                _tabs.Add(new DatabaseTab(table));
            }
        }

        void OnDisable()
        {
            if (Database != null)
            {
                Database.Close();
                Database.Dispose();
                Database = null;
            }
        }
    }
}