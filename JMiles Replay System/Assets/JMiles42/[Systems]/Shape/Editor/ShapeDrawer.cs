using JMiles42.Editor;
using JMiles42.Editor.PropertyDrawers;
using JMiles42.Extensions;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.Shapes
{
	[CustomPropertyDrawer(typeof (Shape))]
	public class ShapeDrawer: JMilesPropertyDrawer
	{
		private ReorderableListProperty listProperty;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.height = singleLine;
			label.DrawGUIContent(position);
			//EditorHelpers.CopyPastObjectButtons(property);
			var list = property.FindPropertyRelative("Points");

			if (listProperty.IsNull())
			{
				listProperty = list;
			}
			listProperty.HandleDrawing();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return singleLine; }
	}
}