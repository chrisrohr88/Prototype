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
			var enabledStateOfGUI = GUI.enabled;
			if(member.IsReadOnly)
			{
				GUI.enabled = false;
			}

			if(member.ValueType == typeof(float))
			{
				DrawFloat(member, member.Label);
			}
			else if(member.ValueType == typeof(string))
			{
				DrawString(member, member.Label);
			}
			else if(member.ValueType == typeof(int))
			{
				DrawInt(member, member.Label);
			}
			else if(member.ValueType == typeof(long))
			{
				DrawLong(member, member.Label);
			}
			else if(member.ValueType == typeof(Vector3))
			{
				DrawVector3(member, member.Label);
			}
			else if(member.ValueType == typeof(bool))
			{
				DrawBool(member, member.Label);
			}
			else if(member.ValueType.IsEnum)
			{
				DrawEnum(member, member.Label);
			}
			else
			{
				DrawObject(member, member.Label);
			}

			if(member.IsReadOnly)
			{
				GUI.enabled = enabledStateOfGUI;
			}
		}

		public static void DrawFloat(MemberInfoWrapper info, string label)
		{
			info.SetValue(EditorGUILayout.FloatField(label, (float)info.GetValue()));
		}

		public static void DrawLong(MemberInfoWrapper info, string label)
		{
			info.SetValue(EditorGUILayout.LongField(label, (long)info.GetValue()));
		}

		public static void DrawBool(MemberInfoWrapper info, string label)
		{
			info.SetValue(EditorGUILayout.Toggle(label, (bool)info.GetValue()));
		}

		public static void DrawVaector3(MemberInfoWrapper info, string label)
		{
			info.SetValue(EditorGUILayout.Vector3Field(label, (Vector3)info.GetValue()));
		}

		public static void DrawEnum(MemberInfoWrapper info, string label)
		{
			info.SetValue(EditorGUILayout.EnumPopup(label, (System.Enum)System.Enum.Parse(info.GetValue().GetType(), info.GetValue().ToString())));
		}

		public static void DrawInt(MemberInfoWrapper info, string label)
		{
			info.SetValue<int>(EditorGUILayout.IntField(label, (int)info.GetValue()));
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
