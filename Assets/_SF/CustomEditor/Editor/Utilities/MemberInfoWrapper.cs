namespace SF.CustomInspector.Utilities
{
	public abstract class MemberInfoWrapper
	{
		private string _label = "";
		public string Label 
		{
			get 
			{
				return _label;
			}
			protected set 
			{
				_label = GetValidLabel(value);
			}
		}
		public object ReflectedObject { get; protected set; }

		public abstract System.Type ValueType { get; }

		public abstract object GetValue ();
		public abstract void SetValue<T> (T valueToSet);
		protected abstract string GetValidLabel(string label);
	}
}
