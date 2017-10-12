using System;
using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Systems.Shapes
{
	[CustomEditor(typeof (ShapeCreator))]
	public class ShapeCreatorEditor: CustomEditorBase
	{
		private bool SceneGUINeedsRepaint = false;
		private ShapeCreator shapeCreator;
		private SelectionInformation SceneGUISelectionInformation = new SelectionInformation();

		protected override void OnEnable()
		{
			base.OnEnable();
			SceneGUISelectionInformation = new SelectionInformation();
			shapeCreator = (ShapeCreator) target;
		}

		#region EditorGUI
		public override void DrawGUI()
		{
			int width = (int) Mathf.Min(Screen.width, Screen.height * 0.6f);
			width = (int) (width *  0.75f);
			Texture2D texture2D = ShapeAsTexture.RenderShapeToTexture(shapeCreator.Shape, width, width);
			var content = new GUIContent(texture2D);
			var rect = GUILayoutUtility.GetRect(width, width);

			EditorGUI.LabelField(rect, content);
		}
		#endregion

		#region SceneGUI
		private void OnSceneGUI()
		{
			var e = Event.current;

			if (e.type == EventType.layout)
			{
				HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
			}
			else if (e.type == EventType.repaint)
			{
				SceneGUIDrawPointsLines();
				SceneGUIDrawPoints();
			}
			else
			{
				SceneGUIHandleInput(e);
			}

			if (SceneGUINeedsRepaint)
			{
				HandleUtility.Repaint();
				SceneGUINeedsRepaint = false;
			}
		}

		private void SceneGUIHandleInput(Event @event)
		{
			var mouseRay = HandleUtility.GUIPointToWorldRay(@event.mousePosition);
			var point = SceneGUIGetPointPosition(mouseRay);
			if (@event.type == EventType.mouseDown && @event.button == 0 && @event.modifiers == EventModifiers.None)
			{
				SceneGUIHandleMouse0Down(point);
			}
			else if (@event.type == EventType.mouseUp && @event.button == 0 && @event.modifiers == EventModifiers.None)
			{
				SceneGUIHandleMouse0Up(point);
			}
			else if (@event.type == EventType.mouseDrag && @event.button == 0 && @event.modifiers == EventModifiers.None)
			{
				SceneGUIHandleMouse0Drag(point);
			}

			if (!SceneGUISelectionInformation.pointSelected)
			{
				SceneGUIUpdateSelectionInformation(point);
			}

			SceneGUINeedsRepaint = true;
		}

		private void SceneGUIHandleMouse0Down(Vector3 mousePos)
		{
			if (!SceneGUISelectionInformation.mouseOverPoint)
			{
				int newPointIndex = (SceneGUISelectionInformation.MouseOverLine)? SceneGUISelectionInformation.LineIndex + 1 : shapeCreator.Shape.Points.Count;
				Undo.RecordObject(shapeCreator, string.Format("Add Point To:{0}", shapeCreator.name));
				shapeCreator.Shape.Points.Insert(newPointIndex, mousePos);
				SceneGUISelectionInformation.PointIndex = newPointIndex;
			}
			SceneGUISelectionInformation.pointSelected = true;
			SceneGUISelectionInformation.posAtStart = shapeCreator.Shape.Points[SceneGUISelectionInformation.PointIndex];
			SceneGUINeedsRepaint = true;
		}

		private void SceneGUIHandleMouse0Up(Vector3 mousePos)
		{
			if (SceneGUISelectionInformation.pointSelected)
			{
				shapeCreator.Shape.Points[SceneGUISelectionInformation.PointIndex] = SceneGUISelectionInformation.posAtStart;
				Undo.RecordObject(shapeCreator, string.Format("Moved Point On:{0}", shapeCreator.name));
				shapeCreator.Shape.Points[SceneGUISelectionInformation.PointIndex] = mousePos;

				SceneGUISelectionInformation.pointSelected = false;
				SceneGUISelectionInformation.PointIndex = -1;
			}
			SceneGUINeedsRepaint = true;
		}

		private void SceneGUIHandleMouse0Drag(Vector3 mousePos)
		{
			if (SceneGUISelectionInformation.pointSelected)
			{
				shapeCreator.Shape.Points[SceneGUISelectionInformation.PointIndex] = mousePos;
			}
			SceneGUINeedsRepaint = true;
		}

		private void SceneGUIDrawPoints()
		{
			using (new EditorColorChanger(Color.white, EditorColourType.Handles))
			{
				for (var i = 0; i < shapeCreator.Shape.Points.Count; i++)
				{
					if (i == SceneGUISelectionInformation.PointIndex)
					{
						using (new EditorColorChanger(SceneGUISelectionInformation.pointSelected? Color.green : Color.red, EditorColourType.Handles))
							Handles.DrawSolidDisc(shapeCreator.Shape.Points[i], Vector3.up, shapeCreator.HandleRadius);
					}
					else
						Handles.DrawSolidDisc(shapeCreator.Shape.Points[i], Vector3.up, shapeCreator.HandleRadius);
				}
			}
		}

		private void SceneGUIDrawPointsLines()
		{
			using (new EditorColorChanger(Color.black, EditorColourType.Handles))
			{
				for (var i = 0; i < shapeCreator.Shape.Points.Count; i++)
				{
					var nextPoint = shapeCreator.Shape.Points[(i + 1) % shapeCreator.Shape.Points.Count];
					if (i == SceneGUISelectionInformation.LineIndex)
						using (new EditorColorChanger(Color.red, EditorColourType.Handles))
							Handles.DrawLine(shapeCreator.Shape.Points[i], nextPoint);
					else
						Handles.DrawDottedLine(shapeCreator.Shape.Points[i], nextPoint, 5);
				}
			}
		}

		public static Vector3 SceneGUIGetPointPosition(Ray ray, float height = 0)
		{
			var distToPlane = (height - ray.origin.y) / ray.direction.y;
			return ray.GetPoint(distToPlane);
		}

		private void SceneGUIUpdateSelectionInformation(Vector3 mousePos)
		{
			int mouseOverPointIndex = -1;
			for (var i = 0; i < shapeCreator.Shape.Points.Count; i++)
			{
				if (Vector3.Distance(mousePos, shapeCreator.Shape.Points[i]) <= shapeCreator.HandleRadius)
				{
					mouseOverPointIndex = i;
					break;
				}
			}
			if (mouseOverPointIndex != SceneGUISelectionInformation.PointIndex)
			{
				SceneGUISelectionInformation.PointIndex = mouseOverPointIndex;
				SceneGUISelectionInformation.mouseOverPoint = mouseOverPointIndex != -1;
				SceneGUINeedsRepaint = true;
			}
			if (SceneGUISelectionInformation.mouseOverPoint)
			{
				SceneGUISelectionInformation.MouseOverLine = false;
				SceneGUISelectionInformation.LineIndex = -1;
			}
			else
			{
				int mouseOverLineIndex = -1;
				float closestLineDist = shapeCreator.HandleRadius;
				for (var i = 0; i < shapeCreator.Shape.Points.Count; i++)
				{
					var nextPoint = shapeCreator.Shape.Points[(i + 1) % shapeCreator.Shape.Points.Count];
					float distanceFromMouseToLine = HandleUtility.DistancePointToLineSegment(mousePos.ToXZ(), shapeCreator.Shape.Points[i].ToXZ(), nextPoint.ToXZ());

					if (distanceFromMouseToLine < closestLineDist)
					{
						closestLineDist = distanceFromMouseToLine;
						mouseOverLineIndex = i;
					}
				}
				if (SceneGUISelectionInformation.LineIndex != mouseOverLineIndex)
				{
					SceneGUISelectionInformation.LineIndex = mouseOverLineIndex;
					SceneGUISelectionInformation.MouseOverLine = mouseOverLineIndex != -1;
				}
				SceneGUINeedsRepaint = true;
			}
		}
		#endregion

		public class SelectionInformation
		{
			public int PointIndex = -1;
			public bool mouseOverPoint = false;
			public bool pointSelected = false;
			public Vector3 posAtStart = Vector3.zero;

			public int LineIndex = -1;
			public bool MouseOverLine = false;
		}
	}
}