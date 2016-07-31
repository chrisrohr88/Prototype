using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SF.CustomInspector.Attributes;
using SF.CustomInspector.Drawers;
using SF.CustomInspector.Utilities;
using SF.CustomInspector.Drawers.Helper;

namespace SF.CustomInspector.Drawers
{
	public class GenericDrawer
	{
		private Object _objectToDraw;
		protected bool _foldout = true;
		protected bool _foldoutEnabled = false;
		protected string _label = "";
		protected List<MemberInfoWrapper> _valuesToDraw = new List<MemberInfoWrapper>();
		protected List<KeyValuePair<GenericDrawer, MemberInfoWrapper>> _objectsToDraw = new List<KeyValuePair<GenericDrawer, MemberInfoWrapper>>();

		protected GenericDrawer()
		{
		}

		public GenericDrawer(Object objectToDraw, bool enableFoldout = false, string name = "")
		{
			_objectToDraw = objectToDraw;
			_label = name;
			_foldoutEnabled = enableFoldout;
			CustomInspectorReflector.GatherMembers(_objectToDraw, _valuesToDraw, _objectsToDraw);
		}

		public virtual void Draw()
		{
			if(_foldoutEnabled)
			{
				DrawWithFoldout();
			}
			else
			{
				DrawWithoutFoldout();
			}
		}

		protected void CheckForGUIChanges()
		{
			if(ShouldSetGUIAsDirty())
			{
				EditorUtility.SetDirty(_objectToDraw);
			}
		}

		protected virtual bool ShouldSetGUIAsDirty()
		{
			return (GUI.changed && !EditorApplication.isPlaying && _objectToDraw != null);
		}

		protected virtual void DrawWithoutFoldout()
		{			
			DrawValues();
			DrawObjects();
		}

		protected virtual void DrawWithFoldout()
		{
			_foldout = EditorGUILayout.Foldout(_foldout, _label, EditorStyles.foldout);
			if(_foldout)
			{
				EditorGUI.indentLevel++;
				DrawValues();
				DrawObjects();
				EditorGUI.indentLevel--;
			}
		}

		protected void DrawValues()
		{
			foreach(var member in _valuesToDraw)
			{
				MemberInfoDrawer.DrawForType(member);
			}
		}

		protected void DrawObjects()
		{
			foreach(var kvp in _objectsToDraw)
			{
				if(kvp.Value.GetValue() == null)
				{
					EditorGUILayout.HelpBox(string.Format("{0} has not been populated.", kvp.Value.Label), MessageType.Info);
				}
				else
				{	kvp.Key._objectToDraw = kvp.Value.GetValue() as Object;				
					kvp.Key.Draw();
				}
			}				
		}
	}
}