using UnityEngine;
using UnityEditor;
using UnityEditor.Custom;
using System.Collections;
using System.Reflection;

[CustomEditor(typeof(BaseEnemy))]
[CanEditMultipleObjects]
public class EnemyInspector : Editor 
{
	private EnemyDrawer _enemyDrawer;

	public void OnEnable()
	{
		_enemyDrawer = new EnemyDrawer();
	}

	public override void OnInspectorGUI()
	{
		BaseEnemy enemy = (BaseEnemy)target;
		DrawEnemy(enemy);
		if(enemy.Enemy != null)
		{
			DrawEnemyEntity(enemy.Enemy);
			Repaint();
		}
		else
		{
			EditorGUILayout.HelpBox("Enemy is populated at runtime.", MessageType.Info);
		}
	}

	private void DrawEnemy(BaseEnemy enemy)
	{
		FieldInfoDrawer.DrawObject<Transform>(enemy.GetType().GetField("_spawnTransform", BindingFlags.NonPublic | BindingFlags.Instance), enemy, "Spawn Transform", false);
		FieldInfoDrawer.DrawObject<GameObject>(enemy.GetType().GetField("_deathEffectPrefab", BindingFlags.NonPublic | BindingFlags.Instance), enemy, "Death Effect", false);
	}

	private void DrawEnemyEntity(Enemy enemy)
	{
		_enemyDrawer.Draw(enemy);
	}
}
