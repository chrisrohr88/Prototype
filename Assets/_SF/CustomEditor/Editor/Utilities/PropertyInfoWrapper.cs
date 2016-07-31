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

		public PropertyInfoWrapper(PropertyInfo info, string label, object reflectedObject)
		{
			Info = info;
			Label = label;
			ReflectedObject = reflectedObject;
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
