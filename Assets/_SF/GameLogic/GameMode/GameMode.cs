using UnityEngine;
using System.Collections;

//TODO Do this better
public class GameMode
{
    private FieldInteractable _field;

    public Transform FireTransform
    {
        get
        {
            return _field.FireTransform;
        }
    }

	public void StartGame()
    {
        LoadGameModeDependancies();
	}

    private void LoadGameModeDependancies()
	{
		_field = (GameManager.Instantiate(Resources.Load("Game/Field/Field")) as GameObject).GetComponent<FieldInteractable>();
		GameManager.Instantiate(Resources.Load("InputManager"));
		var playerWall = (GameManager.Instantiate(Resources.Load("Game/Field/Barrier")) as GameObject).GetComponent<PlayerWall>();
        playerWall.AssignPlayer(Player.Create(1000));
		playerWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 100)) + new Vector3(-80, 4, 0);
        EnemyManager.EnableSpawning();
    }
}
