using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour
{
    private static FieldInteractable _field;

    public static Transform FireTransform
    {
        get
        {
            return _field.FireTransform;
        }
    }

	private void Start()
    {
        LoadGameModeDependancies();
	}

    private void LoadGameModeDependancies()
    {
        _field = (Instantiate(Resources.Load("Game/Field/Field")) as GameObject).GetComponent<FieldInteractable>();
        var playerWall = (Instantiate(Resources.Load("Game/Field/Barrier")) as GameObject).GetComponent<PlayerWall>();
        playerWall.AssignPlayer(Player.Create(1000));
        EnemyManager.EnableSpawning();
    }
}
