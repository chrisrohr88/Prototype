using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLIstner : MonoBehaviour
{
	[SerializeField] private Text _text;

	public int Score { get; set; }

	private void Start()
	{
		_text.text = "0";
	}

	public void UpdateScore(int score)
	{
		_text.text = score.ToString();
	}
}
