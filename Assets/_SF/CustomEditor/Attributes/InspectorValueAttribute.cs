﻿using System;

namespace SF.CustomInspector.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class InspectorValueAttribute : BaseInspectorAttribute
	{
	}
}
