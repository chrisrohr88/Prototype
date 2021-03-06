using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SF.CustomInspector.Attributes;
using SF.CustomInspector.Utilities;

namespace SF.CustomInspector.Drawers.Helper
{
	public static class CustomInspectorReflector
	{
		public static void GatherMembers(object objectToReflect, List<MemberInfoWrapper> valueList, List<KeyValuePair<GenericDrawer, MemberInfoWrapper>> objectList)
		{
			if(objectToReflect != null)
			{
				GatherFromFields(objectToReflect, valueList, objectList);
				GatherFromProperties(objectToReflect, valueList, objectList);
			}
		}

		private static void GatherFromFields(object objectToReflect, List<MemberInfoWrapper> valueList, List<KeyValuePair<GenericDrawer, MemberInfoWrapper>> objectList)
		{
			var fields = objectToReflect.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			foreach(var field in fields)
			{
				var attributes = field.GetCustomAttributes(typeof(InspectorValueAttribute), true) as InspectorValueAttribute[];
				if(attributes.Length > 0)
				{
					var attribute = attributes[0];
					var fieldInfoWrapper = new FieldInfoWrapper(field, attribute.Label, objectToReflect, attribute.Options);
					valueList.Add(fieldInfoWrapper);
				}

				var objectAttributes = field.GetCustomAttributes(typeof(InspectorObjectAttribute), true) as InspectorObjectAttribute[];
				if(objectAttributes.Length > 0)
				{
					var attribute = objectAttributes[0];
					var memberInfoWrapper = new FieldInfoWrapper(field, attribute.Label, objectToReflect, attribute.Options);
					objectList.Add(new KeyValuePair<GenericDrawer, MemberInfoWrapper>(new InspectorObjectDrawer(memberInfoWrapper.GetValue(), !attribute.DisableFoldout, memberInfoWrapper.Label, memberInfoWrapper.IsReadOnly), memberInfoWrapper));
				}
			}
		}

		private static void GatherFromProperties(object objectToReflect, List<MemberInfoWrapper> valueList, List<KeyValuePair<GenericDrawer, MemberInfoWrapper>> objectList)
		{
			var properties = objectToReflect.GetType().GetProperties();

			foreach(var property in properties)
			{
				var attributes = property.GetCustomAttributes(typeof(InspectorValueAttribute), true) as InspectorValueAttribute[];
				if(attributes.Length > 0)
				{
					var attribute = attributes[0];
					var propertyInfoWrapper = new PropertyInfoWrapper(property, attribute.Label, objectToReflect, attribute.Options);
					valueList.Add(propertyInfoWrapper);
				}

				var objectAttributes = property.GetCustomAttributes(typeof(InspectorObjectAttribute), true) as InspectorObjectAttribute[];
				if(objectAttributes.Length > 0)
				{
					var attribute = objectAttributes[0];
					var memberInfoWrapper = new PropertyInfoWrapper(property, attribute.Label, objectToReflect, attribute.Options);
					objectList.Add(new KeyValuePair<GenericDrawer, MemberInfoWrapper>(new InspectorObjectDrawer(memberInfoWrapper.GetValue(), !attribute.DisableFoldout, memberInfoWrapper.Label, memberInfoWrapper.IsReadOnly), memberInfoWrapper));
				}
			}
		}
	}
}
