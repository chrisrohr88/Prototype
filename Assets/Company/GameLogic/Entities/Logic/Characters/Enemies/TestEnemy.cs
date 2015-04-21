using UnityEngine;
using System.Collections;

public class TestEnemy : BaseEnemy
{
    [SerializeField] private float _speed = 50f;
	private CharacterBehavior _movementBehavior;

    protected override void PreStart()
    {
    }

    protected override void Start()
    {
		_movementBehavior = new BasicMovementBehavior(_speed, gameObject);
    }

    protected override void PostStart()
    {
    }
	
	protected override void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			_movementBehavior = new StaggerMovementBehavior(_speed, gameObject, 5f, OnMevementBehaviourComplete);
		}
		if(Input.GetKeyDown(KeyCode.B))
		{
			_movementBehavior = new BlinkMovementBehavior(gameObject, 5f, OnMevementBehaviourComplete);
		}
	}

	protected void OnMevementBehaviourComplete()
	{
		_movementBehavior = new BasicMovementBehavior(_speed, gameObject);
	}

    protected override void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
		_movementBehavior.UpdateGameObject();
    }
}
