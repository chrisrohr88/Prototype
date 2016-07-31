using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.CustomInspector.Drawers;

namespace SF.CustomInspector.Inspectors
{
	[CustomEditor(typeof(BaseEnemy))]
	[CanEditMultipleObjects]
	public class EnemyInspector : UnityEditor.Editor 
	{
		private GenericDrawer _drawer;

		public void OnEnable()
		{
			if(_drawer == null)
			{
				_drawer = new GenericDrawer(target, false);
			}
		}

		public override void OnInspectorGUI()
		{
			DrawObject();
		}

		private void DrawObject()
		{
			_drawer.Draw();
		}
	}
}
