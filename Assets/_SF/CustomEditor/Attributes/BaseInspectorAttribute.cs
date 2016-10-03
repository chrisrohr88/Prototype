using System;
using UnityEngine;
using System.Collections.Generic;

namespace SF.CustomInspector.Attributes
{
	public static class EnumExtention
	{
		public static bool HasFlag(this OptionType flags, OptionType flag)
		{
			return (flags & flag) != 0;
		}
	}

	public enum OptionType
	{
		None = 0,
		EnumMask = 1,
		ReadOnly = 2
	}

	public abstract class BaseInspectorAttribute : Attribute
	{
		public string Label { get; set; }
		public OptionType Options { get; set; }
	}	
}
