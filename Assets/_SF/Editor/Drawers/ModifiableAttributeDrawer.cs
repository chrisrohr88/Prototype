using UnityEngine;
using System.Reflection;

namespace UnityEditor.Custom
{
	public class ModifiableAttributeDrawer
	{
		private bool _foldout;
		
		public void Draw(ModifiableAttribute modifiableAttribute, string foldoutLabel)
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