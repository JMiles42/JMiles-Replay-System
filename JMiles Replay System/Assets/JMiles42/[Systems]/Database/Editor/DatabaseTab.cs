using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.Database
{
    public class DatabaseTab: Tab<DatabaseWindow>
    {
        public override string TabName {
            get { return Table.TableName ?? ""; }
        }

        public DatabaseTab(Table table) { Table = table; }

        private readonly Table Table;

        public override void DrawTab(Window<DatabaseWindow> owner)
        {
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                foreach (var collem in Table.Collems)
                {
                    using (new GUILayout.VerticalScope())
                    {
                        var style = new GUIStyle(EditorStyles.toolbarButton) {fontStyle = FontStyle.BoldAndItalic};
                        style.fixedHeight *= 2;
                        using (new EditorColorChanger(new Color(0.7f, 0.7f, 0.7f, 1f)))
                        {
                            GUILayout.Label(collem, style);
                        }
                        DrawCollem(collem);
                    }
                }
            }
        }

        private void DrawCollem(string collem)
        {
            foreach (var s in Table.GetCollemData(collem))
            {
                GUILayout.Label(s.ToString(), EditorStyles.toolbarButton);
            }
        }
    }
}