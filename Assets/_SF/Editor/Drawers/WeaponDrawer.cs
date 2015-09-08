using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Weapons;

namespace UnityEditor.Custom
{
	public class WeaponDrawer
	{
		private bool _foldout = true;
		private Weapon _weapon;
		private Dictionary<string, ModifiableAttributeDrawer> _modifiableAttributeDrawers;
		
		public WeaponDrawer()
		{
			_modifiableAttributeDrawers = new Dictionary<string, ModifiableAttributeDrawer>();
		}
		
		public void Draw(Weapon weapon)
		{
			_weapon = weapon;
			_foldout = EditorGUILayout.Foldout(_foldout, "Weapon", EditorStyles.foldout);
			if(_foldout)
			{
				EditorGUI.indentLevel++;
				DrawProperties(); 
				EditorGUI.indentLevel--;
			}
		}
		
		private void DrawProperties()
		{
			PropertyInfo[] properties = _weapon.GetType().GetProperties();
			
			foreach(var property in properties)
			{
				System.Object obj = property.GetValue(_weapon, null);
				switch(obj.GetType().Name)
				{
				case "ModifiableAttribute":
					DrawModifiableAttibute((ModifiableAttribute)obj, property);
					break;
				case "Vector3":
					DrawVector3(property);
					break;
				case "String":
					DrawString(property);
					break;
				case "AmmoType":
					DrawEnum(property);
					break;
				case "GameObject":
					DrawGameObject<GameObject>(property);
					break;
				case "Transform":
					DrawGameObject<Transform>(property);
					break;
				}
			}
		}
		
		private void DrawModifiableAttibute(ModifiableAttribute modifiableAttribute, PropertyInfo propertyInfo)
		{
			ModifiableAttributeDrawer _modifiableAttributeDrawer;
			if(_modifiableAttributeDrawers.ContainsKey(propertyInfo.Name))
			{
				_modifiableAttributeDrawer = _modifiableAttributeDrawers[propertyInfo.Name];
			}
			else
			{
				_modifiableAttributeDrawer = new ModifiableAttributeDrawer();
				_modifiableAttributeDrawers.Add(propertyInfo.Name, _modifiableAttributeDrawer);
			}
			_modifiableAttributeDrawer.Draw(modifiableAttribute, propertyInfo.Name);
		}
		
		private void DrawVector3(PropertyInfo propertyInfo)
		{
			PropertyInfoDrawer.DrawVector3(propertyInfo, _weapon, propertyInfo.Name);
		}
		
		private void DrawString(PropertyInfo propertyInfo)
		{
			PropertyInfoDrawer.DrawString(propertyInfo, _weapon, propertyInfo.Name);
		}
		
		private void DrawEnum(PropertyInfo propertyInfo)
		{
			PropertyInfoDrawer.DrawEnum(propertyInfo, _weapon, propertyInfo.Name);
		}
		
		private void DrawGameObject<T>(PropertyInfo propertyInfo) where T : Object
		{
			PropertyInfoDrawer.DrawObject<T>(propertyInfo, _weapon, propertyInfo.Name, true);
		}
	}
}
