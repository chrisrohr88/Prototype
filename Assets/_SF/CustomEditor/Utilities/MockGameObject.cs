using UnityEngine;
using SF.CustomInspector.Attributes;

namespace SF.CustomInspector.Utilities
{
	public class MockGameObject : MonoBehaviour
	{
		[InspectorValue] public string Name { get; set; }
		[InspectorObject] public object ActualObject { get; set; }
	}
}
