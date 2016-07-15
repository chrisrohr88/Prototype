using UnityEngine;
using System.Reflection;
using UnityEditor;

namespace SF.Editor.Drawers.PropertyDrawers
{
	public static class FieldInfoDrawer
	{
		public static void DrawFloat(FieldInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.FloatField(label, float.Parse(info.GetValue(obj).ToString())));
		}
		
		public static void DrawVector3(FieldInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.Vector3Field(label, (Vector3)info.GetValue(obj)));
		}
		
		public static void DrawString(FieldInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.TextField(label, (string)info.GetValue(obj)));
		}
		
		public static void DrawObject<T>(FieldInfo info, System.Object obj, string label, bool allowSceneObjects) where T : Object
		{
			info.SetValue(obj, EditorGUILayout.ObjectField(label, info.GetValue(obj) as T, typeof(T), allowSceneObjects) as T);
		}
	}
}
