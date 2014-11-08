using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour
{
    private static FieldInteractable Field;

    public static Transform FireTransform
    {
        get
        {
            return Field.FireTransform;
        }
    }

	private void Start()
    {
        Field = (Instantiate(Resources.Load("Game/Field/Field")) as GameObject).GetComponent<FieldInteractable>();
        Player.Create(100);
        EnemyManager.EnableSpawning();
	}
}
