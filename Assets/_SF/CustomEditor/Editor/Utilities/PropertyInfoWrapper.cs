using System.Reflection;

namespace SF.CustomInspector.Utilities
{

	public class PropertyInfoWrapper : MemberInfoWrapper
	{
		public PropertyInfo Info { get; private set; }

		public override System.Type ValueType 
		{ 
			get
			{
				return Info.PropertyType;
			}
		}

		public PropertyInfoWrapper(
			PropertyInfo info, 
			string label, 
			object reflectedObject, 
			bool isReadOnly = false) : base(label, reflectedObject, isReadOnly)
		{
			Info = info;
		}

		public override object GetValue()
		{
			return Info.GetValue(ReflectedObject, null);
		}

		public override void SetValue<T>(T valueToSet)
		{
			Info.SetValue(ReflectedObject, valueToSet, null);
		}

		protected override string GetValidLabel(string label)
		{
			if(string.IsNullOrEmpty(label))
			{
				return Info.Name;
			}
			else
			{
				return label;
			}
		}
	}
}
