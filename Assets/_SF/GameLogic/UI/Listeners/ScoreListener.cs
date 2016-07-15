using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SF.EventSystem;
using SF.GameLogic.EventSystem.EventData;
using SF.GameLogic.EventSystem.EventRegistrars;

namespace SF.GameLogic.UI.Listeners
{
	public class ScoreListener : MonoBehaviour
	{
		[SerializeField] private Text _text;
		private EventRegistrar _eventRegistrar;

		public int Score { get; set; }

		private void Start()
		{ 
			_eventRegistrar = new SinglePlayerScoreListenerEventRegistrar(this);
			_text.text = "0";
		}

		public void UpdateScore(SinglePlayerScoreUpdateEventData eventData)
		{
			_text.text = eventData.NewPointValue.ToString();
		}
	}
}
