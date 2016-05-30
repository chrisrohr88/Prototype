namespace SF.EventSystem
{
	public abstract class SFEventListner
	{
		public long? TargetId { get; set; }

		public abstract void EventHandlerMethod(SFEventData eventData);
	}
}
