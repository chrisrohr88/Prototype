using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
	[SerializeField] private float _speed = 5; 
	public void Update()
	{
		transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * _speed);
	}
}
