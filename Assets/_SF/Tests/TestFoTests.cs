#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class TestFoTests : MonoBehaviour
{
	public IEnumerator Test1()
	{
		yield return StartCoroutine(Test3());
		Debug.Log("Done in Test1");
	}

	public IEnumerator Test2()
	{
		yield return StartCoroutine(Test1());
		Debug.Log("Done in Test2");
	}

	public IEnumerator Test3()
	{
		yield return new WaitForSeconds(1);
		Debug.Log("Done in Test3");

	}
	public IEnumerator Test4()
	{
		yield return StartCoroutine(Test5());
		Debug.Log("Done in Test3");
	}

	public IEnumerator Test5()
	{
		yield return new WaitForSeconds(1);
		throw new System.Exception("Error!");
	}
}
#endif