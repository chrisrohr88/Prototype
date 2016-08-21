using System;

namespace SF.CustomInspector.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class InspectorObjectAttribute : BaseInspectorAttribute
	{
		public bool EnableFoldout { get; set; }
	}
}
