using SF.CustomInspector.Drawers.Helper;

namespace SF.CustomInspector.Drawers
{
	public class InspectorObjectDrawer : GenericDrawer
	{
		private object _objectToDraw;

		public InspectorObjectDrawer(object objectToDraw, bool enableFoldout = false, string label = "", bool isReadOnly = false)
		{
			_objectToDraw = objectToDraw;
			_foldoutEnabled = enableFoldout;
			_label = label;
			_isReadOnly = isReadOnly;
			CustomInspectorReflector.GatherMembers(_objectToDraw, _valuesToDraw, _objectsToDraw);
		}
	}
}