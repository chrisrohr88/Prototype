namespace SF.EventSystem
{
	public class OnGameStarted : SFEventListner
	{
		public System.Action GameStartMethod { get; set; }

		public override void EventHandlerMethod(SFEventData eventData)
		{
			GameStartMethod();
		}
	}
}
