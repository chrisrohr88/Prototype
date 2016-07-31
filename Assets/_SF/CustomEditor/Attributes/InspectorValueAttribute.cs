using System;

namespace SF.CustomInspector.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class InspectorValueAttribute : Attribute
	{
		protected string _label = ""; 
		public string Label
		{
			get
			{
				return _label;
			}
			set
			{
				_label = value;
			}
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class InspectorObjectAttribute : Attribute
	{
		protected string _label = ""; 
		public string Label
		{
			get
			{
				return _label;
			}
			set
			{
				_label = value;
			}
		}

		protected bool _enableFoldout = true; 
		public bool EnableFoldout
		{
			get
			{
				return _enableFoldout;
			}
			set
			{
				_enableFoldout = value;
			}
		}
	}
}
