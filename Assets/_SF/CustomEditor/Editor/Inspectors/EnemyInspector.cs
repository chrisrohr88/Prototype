using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using SF.GameLogic.Entities.Logic.Charaters.Enemies;
using SF.CustomInspector.Drawers;

namespace SF.CustomInspector.Inspectors
{
	public abstract class GenericInspector : UnityEditor.Editor 
	{
		private GenericDrawer _drawer;

		protected abstract string Label { get; }

		private void OnEnable()
		{
			if(_drawer == null)
			{
				_drawer = new GenericDrawer(target, false, Label);
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

	[CustomEditor(typeof(BaseEnemy))]
	[CanEditMultipleObjects]
	public class EnemyInspector : GenericInspector
	{
		protected override string Label
		{ 
			get 
			{
				return "Enemy";
			}
		}
	}
}
