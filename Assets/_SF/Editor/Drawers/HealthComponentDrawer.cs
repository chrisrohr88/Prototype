using UnityEngine;
using System.Reflection;

namespace UnityEditor.Custom
{
	public class HealthComponentDrawer
	{
		private bool _foldout = true;
		private ModifiableAttributeDrawer _maxHealthAttributeDrawer;
		
		public HealthComponentDrawer()
		{
			_maxHealthAttributeDrawer = new ModifiableAttributeDrawer();
		}
		
		public void Draw(HealthComponent healthComponent)
		{
			_foldout = EditorGUILayout.Foldout(_foldout, "Health", EditorStyles.foldout);
			
			if(_foldout)
			{
				DrawCurrentHealth(healthComponent);
				DrawMaxHealth(healthComponent);
			}
		}
		
		private void DrawCurrentHealth(HealthComponent healthComponent)
		{
			FieldInfo healthInfo = healthComponent.GetType().GetField("_currentHealth", BindingFlags.NonPublic | BindingFlags.Instance);
			FieldInfoDrawer.DrawFloat(healthInfo, healthComponent, "Current Health");
		}
		
		private void DrawMaxHealth(HealthComponent healthComponent)
		{
			EditorGUI.indentLevel++;
			FieldInfo maxHealth = healthComponent.GetType().GetField("_maxHealthAttribute", BindingFlags.NonPublic | BindingFlags.Instance);
			ModifiableAttribute maxHealthAttribute = (ModifiableAttribute)maxHealth.GetValue(healthComponent);
			_maxHealthAttributeDrawer.Draw(maxHealthAttribute, "Max Health Attribute");
			EditorGUI.indentLevel--;
		}
	}
}

