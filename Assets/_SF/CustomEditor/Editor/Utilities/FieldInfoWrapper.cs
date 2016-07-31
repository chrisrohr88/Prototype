using System.Reflection;

namespace SF.CustomInspector.Utilities
{
	public class FieldInfoWrapper : MemberInfoWrapper
	{
		public FieldInfo Info { get; private set; }

		public override System.Type ValueType 
		{ 
			get
			{
				return Info.FieldType;
			}
		}

		public FieldInfoWrapper(FieldInfo info, string label, object reflectedObject)
		{
			Info = info;
			Label = label;
			ReflectedObject = reflectedObject;
		}

		public override object GetValue()
		{
			return Info.GetValue(ReflectedObject);
		}

		public override void SetValue<T>(T valueToSet)
		{
			Info.SetValue(ReflectedObject, valueToSet);
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
