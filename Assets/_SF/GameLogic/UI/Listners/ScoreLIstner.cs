using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SF.EventSystem;

public class ScoreListner : MonoBehaviour
{
	[SerializeField] private Text _text;

	public int Score { get; set; }

	private void Start()
	{ 
		SFEventManager.RegisterEventListner(SFEventType.SinglePlayerScoreUpdate, new ConcreteSFEventListner<SinglePlayerScoreUpdateEventData> { MethodToExecute = UpdateScore });
		_text.text = "0";
	}

	public void UpdateScore(SinglePlayerScoreUpdateEventData eventData)
	{
		_text.text = eventData.NewPointValue.ToString();
	}
}
