using UnityEngine;
using System.Reflection;
using UnityEditor;
using SF.CustomInspector.Utilities;

namespace SF.CustomInspector.Drawers
{
	public static class MemberInfoDrawer
	{
		public static void DrawForType(MemberInfoWrapper member)
		{
			if(member.ValueType == typeof(float))
			{
				DrawFloat(member, member.Label);
			}
			else if(member.ValueType == typeof(int))
			{
				DrawInt(member, member.Label);
			}
			else
			{
				DrawObject(member, member.Label);
			}
		}

		public static void DrawFloat(MemberInfoWrapper info, string label)
		{
			info.SetValue<float>(EditorGUILayout.FloatField(label, float.Parse(info.GetValue().ToString())));
		}

		public static void DrawInt(MemberInfoWrapper info, string label)
		{
			info.SetValue<int>(EditorGUILayout.IntField(label, int.Parse(info.GetValue().ToString())));
		}
		
		public static void DrawVector3(MemberInfoWrapper info, string label)
		{
			info.SetValue<Vector3>(EditorGUILayout.Vector3Field(label, (Vector3)info.GetValue()));
		}
		
		public static void DrawString(MemberInfoWrapper info, string label)
		{
			info.SetValue<string>(EditorGUILayout.TextField(label, (string)info.GetValue()));
		}

		public static void DrawObject(MemberInfoWrapper info, string label)
		{
			var fieldValue = info.GetValue() as Object;
			info.SetValue<Object>(EditorGUILayout.ObjectField(label, fieldValue, info.ValueType, true));
		}
	}
}
