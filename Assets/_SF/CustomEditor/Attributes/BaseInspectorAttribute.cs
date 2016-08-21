using System;

namespace SF.CustomInspector.Attributes
{
	public abstract class BaseInspectorAttribute : Attribute
	{
		public string Label { get; set; }
		public bool IsreadOnly { get; set; }
	}	
}
