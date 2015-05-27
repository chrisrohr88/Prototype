using UnityEngine;
using System.Reflection;

namespace UnityEditor.Custom
{
	public static class PropertyInfoDrawer
	{
		public static void DrawFloat(PropertyInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.FloatField(label, float.Parse(info.GetValue(obj, null).ToString())), null);
		}
		
		public static void DrawVector3(PropertyInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.Vector3Field(label, (Vector3)info.GetValue(obj, null)), null);
		}
		
		public static void DrawString(PropertyInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.TextField(label, (string)info.GetValue(obj, null)), null);
		}
		
		public static void DrawEnum(PropertyInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.EnumPopup(label, (System.Enum)System.Enum.Parse(info.GetValue(obj, null).GetType(), info.GetValue(obj, null).ToString())), null);
		}
		
		public static void DrawObject<T>(PropertyInfo info, System.Object obj, string label, bool allowSceneObjects) where T : Object
		{
			info.SetValue(obj, EditorGUILayout.ObjectField(label, info.GetValue(obj, null) as T, typeof(T), true) as T, null);
		}
	}
}
