using SF.CustomInspector.Drawers.Helper;

namespace SF.CustomInspector.Drawers
{
	public class InspectorObjectDrawer :GenericDrawer
	{
		private object _objectToDraw;

		public InspectorObjectDrawer(object objectToDraw, bool enableFoldout = false, string label = "")
		{
			_objectToDraw = objectToDraw;
			_foldoutEnabled = enableFoldout;
			_label = label;
			CustomInspectorReflector.GatherMembers(_objectToDraw, _valuesToDraw, _objectsToDraw);
		}
	}
}