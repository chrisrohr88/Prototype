using System.Collections.Generic;
using SF.CustomInspector.Attributes;

namespace SF.CustomInspector.Utilities
{
	public abstract class MemberInfoWrapper
	{
		private string _label = "";
		public string Label 
		{
			get 
			{
				return GetValidLabel(_label);
			}
			private set
			{
				_label = value;
			}
		}

		public object ReflectedObject { get; private set; }
		public bool IsReadOnly
		{
			get
			{
				return Options.HasFlag(OptionType.ReadOnly);
			}
		}

		public OptionType Options { get; set; }
 
		public MemberInfoWrapper(string label, object reflectedObject, OptionType options)
		{
			Label = label;
			ReflectedObject = reflectedObject;
			Options = options;
		}


		public abstract System.Type ValueType { get; }

		public abstract object GetValue ();
		public abstract void SetValue<T> (T valueToSet);
		protected abstract string GetValidLabel(string label);
	}
}
