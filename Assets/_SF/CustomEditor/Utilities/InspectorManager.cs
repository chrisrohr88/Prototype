using UnityEngine;
using System.Collections.Generic;
using SF.CustomInspector.Attributes;

namespace SF.CustomInspector.Utilities
{
	public static class InspectorManager
	{
		private static GameObject _rootGameObject;
		private static Dictionary<string, GameObject> _storedGameObjects;

		static InspectorManager()
		{
			_storedGameObjects = new Dictionary<string, GameObject>();
		}

		public static bool Add(string name, object obj)
		{
			EnsureRootObjectExists();
			return CreateMockGameObject(name, obj);
		}

		private static void EnsureRootObjectExists()
		{
			if(_rootGameObject == null)
			{
				_rootGameObject = new GameObject("Debug");
			}
		}

		private static bool CreateMockGameObject(string name, object obj)
		{
			var mockGameObject = new GameObject(name).AddComponent<MockGameObject>();
			mockGameObject.Name = name;
			mockGameObject.ActualObject = obj;
			mockGameObject.transform.parent = _rootGameObject.transform;
			return Add(mockGameObject);
		}

		private static bool Add(MockGameObject mockGameObject)
		{
			if(_storedGameObjects.ContainsKey(mockGameObject.Name))
			{
				GameObject.Destroy(mockGameObject.gameObject);
				return false;
			}
			else
			{
				_storedGameObjects.Add(mockGameObject.name, mockGameObject.gameObject);
				return true;
			}
		}

		public static void Remove(string name)
		{
			GameObject obj;
			if(_storedGameObjects.TryGetValue(name, out obj))
			{
				GameObject.Destroy(obj);
				_storedGameObjects.Remove(name);
			}
		}

		public static void ClearAll()
		{
			GameObject.Destroy(_rootGameObject);
		}
	}
}
