namespace SF.EventSystem
{
	public interface SFEventListner
	{
		long? TargetId { get; set; }
		void EventHandlerMethod(SFEventData eventData);
	}

	public class ConcreteSFEventListner<T> : SFEventListner where T : SFEventData
	{
		public long? TargetId { get; set; }
		public System.Action<T> MethodToExecute { get; set; }

		public void EventHandlerMethod(SFEventData eventData)
		{
			MethodToExecute((T)eventData);
		}
	}
}
