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
		public bool IsReadOnly { get; private set; }

		public MemberInfoWrapper(string label, object reflectedObject, bool isReadOnly)
		{
			Label = label;
			ReflectedObject = reflectedObject;
			IsReadOnly = isReadOnly;
		}


		public abstract System.Type ValueType { get; }

		public abstract object GetValue ();
		public abstract void SetValue<T> (T valueToSet);
		protected abstract string GetValidLabel(string label);
	}
}
