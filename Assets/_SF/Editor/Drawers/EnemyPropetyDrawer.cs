using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.Editor.Drawers.PropertyDrawers;

namespace SF.Editor.Drawers
{
	public class EnemyDrawer
	{
		private bool _foldout = true;
		private WeaponDrawer _weaponDrawer;
		private HealthComponentDrawer _healthComponentDrawer;

		public EnemyDrawer()
		{
			_weaponDrawer = new WeaponDrawer();
			_healthComponentDrawer = new HealthComponentDrawer();
		}

		public void Draw(Enemy enemy)
		{
			_foldout = EditorGUILayout.Foldout(_foldout, "Enemy", EditorStyles.foldout);
			if(_foldout)
			{
				EditorGUI.indentLevel++;
				DrawId(enemy);
				DrawSpeed(enemy);
				DrawHealth(enemy);
				_weaponDrawer.Draw(enemy.Weapon);
				EditorGUI.indentLevel--;
			}
		}
		
		private void DrawId(Enemy enemy)
		{
			GUI.enabled = false;
			EditorGUILayout.LongField("Entity ID", enemy.EntityId);
			GUI.enabled = true;
		}
		
		private void DrawSpeed(Enemy enemy)
		{
			PropertyInfo speedInfo = enemy.GetType().GetProperty("Speed");
			PropertyInfoDrawer.DrawFloat(speedInfo, enemy, "Speed");
		}
		
		private void DrawHealth(Enemy enemy)
		{
			_healthComponentDrawer.Draw(enemy.Health);
		}
	}
}