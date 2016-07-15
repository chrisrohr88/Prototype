namespace SF.EventSystem
{
	public interface SFEventListener
	{
		long? TargetId { get; set; }
		void EventHandlerMethod(SFEventData eventData);
	}

	public class ConcreteSFEventListener<T> : SFEventListener where T : SFEventData
	{
		public long? TargetId { get; set; }
		public System.Action<T> MethodToExecute { get; set; }

		public void EventHandlerMethod(SFEventData eventData)
		{
			MethodToExecute((T)eventData);
		}
	}
}
