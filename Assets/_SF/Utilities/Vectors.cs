using UnityEngine;
using System.Collections;

[System.Serializable]
public class MyVector3
{
	public float X { get; set; }
	public float Y { get; set; }
	public float Z { get; set; }

	public static Vector3 RandomShellVector(Vector3 min, Vector3 max)
	{
		Vector3 vector = Random.onUnitSphere;
		vector.x *= Random.Range(min.x, max.x);
		vector.y *= Random.Range(min.y, max.y);
		vector.z *= Random.Range(min.z, max.z);
		return vector;
	}
}
