using UnityEngine;
using System.Collections;

public class TestEnemy : BaseEnemy
{
    [SerializeField] private float _speed = 50f;

    protected override void PreStart()
    {
    }

    protected override void Start()
    {
    }

    protected override void PostStart()
    {
    }

    protected override void Update()
    {
    }

    protected override void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var position = transform.position;
        position.y -= _speed * Time.deltaTime;
        transform.position = position;
    }
}
