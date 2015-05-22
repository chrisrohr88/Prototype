using UnityEngine;
using UnityEditor.Custom;
using UnityEditor;
using System.Reflection;

namespace UnityEditor.Custom
{
	public static class EnemyDrawer
	{
		private static bool _foldout = true;

		public static void Draw(Enemy enemy)
		{
			_foldout = EditorGUILayout.Foldout(_foldout, "Enemy", EditorStyles.foldout);
			if(_foldout)
			{
				EditorGUI.indentLevel++;
				DrawId(enemy);
				DrawSpeed(enemy);
				DrawHealth(enemy);
				EditorGUI.indentLevel--;
			}
		}
		
		private static void DrawId(Enemy enemy)
		{
			GUI.enabled = false;
			EditorGUILayout.LongField("Entity ID", enemy.id);
			GUI.enabled = true;
		}
		
		private static void DrawSpeed(Enemy enemy)
		{
			PropertyInfo speedInfo = enemy.GetType().GetProperty("Speed");
			PropertyInfoDrawer.DrawFloat(speedInfo, enemy, "Speed");
		}
		
		private static void DrawHealth(Enemy enemy)
		{
			HealthComponentDrawer.Draw(enemy.Health);
		}
	}
}

namespace UnityEditor.Custom
{
	public class HealthComponentDrawer
	{
		private static bool _foldout;

		public static void Draw(HealthComponent healthComponent)
		{
			_foldout = EditorGUILayout.Foldout(_foldout, "Health", EditorStyles.foldout);

			if(_foldout)
			{
				DrawCurrentHealth(healthComponent);
				DrawMaxHealth(healthComponent);
			}
		}
		
		private static void DrawCurrentHealth(HealthComponent healthComponent)
		{
			FieldInfo healthInfo = healthComponent.GetType().GetField("_currentHealth", BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfoDrawer.DrawFloat(healthInfo, healthComponent, "Current Health");
		}
		
		private static void DrawMaxHealth(HealthComponent healthComponent)
		{
			EditorGUI.indentLevel++;
			FieldInfo maxHealth = healthComponent.GetType().GetField("_maxHealthAttribute", BindingFlags.NonPublic | BindingFlags.Instance);
			ModifiableAttribute maxHealthAttribute = (ModifiableAttribute)maxHealth.GetValue(healthComponent);
			ModifiableAttributeDrawer.Draw(maxHealthAttribute, "Max Health Attribute");
			EditorGUI.indentLevel--;
		}
	}
}

namespace UnityEditor.Custom
{
	public static class ModifiableAttributeDrawer
	{
		private static bool _foldout;

		public static void Draw(ModifiableAttribute modifiableAttribute, string foldoutLabel)
		{
			_foldout = EditorGUILayout.Foldout(_foldout, foldoutLabel, EditorStyles.foldout);
			
			if(_foldout)
			{
				FieldInfo baseHealth = modifiableAttribute.GetType().GetField("_baseValue", BindingFlags.NonPublic | BindingFlags.Instance);
				FieldInfoDrawer.DrawFloat(baseHealth, modifiableAttribute, "Base Value");
				
				if(GUI.changed)
				{
					modifiableAttribute.UpdateAttribute();
				}
				
				GUI.enabled = false;
				EditorGUILayout.FloatField("Max Value", modifiableAttribute.ModifiedValue);
				GUI.enabled = true;
			}
		}
	}
}

namespace UnityEditor.Custom
{
	public static class PropertyInfoDrawer
	{
		public static void DrawFloat(PropertyInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.FloatField(label, float.Parse(info.GetValue(obj, null).ToString())), null);
		}
		
		public static void DrawObject<T>(PropertyInfo info, System.Object obj, string label, bool allowSceneObjects) where T : Object
		{
			info.SetValue(obj, EditorGUILayout.ObjectField(label, info.GetValue(obj, null) as T, typeof(T), true) as T, null);
		}
	}
	
	public static class FieldInfoDrawer
	{
		public static void DrawFloat(FieldInfo info, System.Object obj, string label)
		{
			info.SetValue(obj, EditorGUILayout.FloatField(label, float.Parse(info.GetValue(obj).ToString())));
		}

		public static void DrawObject<T>(FieldInfo info, System.Object obj, string label, bool allowSceneObjects) where T : Object
		{
			info.SetValue(obj, EditorGUILayout.ObjectField(label, info.GetValue(obj) as T, typeof(T), allowSceneObjects) as T);
		}
	}
}