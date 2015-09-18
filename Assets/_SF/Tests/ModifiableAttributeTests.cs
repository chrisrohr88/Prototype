using SFUnitTest;
using UnityEngine;
using System.Collections;

[TestClass()]
public class ModifiableAttributeTests : Test
{
	private ModifiableAttribute _attribute;
	
	public void Setup()
	{
		_attribute = ModifiableAttribute.Create(100);
		Debug.Log("Setup!!!");
	}

	[Test()]
	public void CheckBaseValue()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}

	[Test()]
	public void AddModifierTest()
	{
		_attribute.AddModifierAndUpdateAttribute(1.1f);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
	}

	[Test()]
	public IEnumerator ModifierExpiredTest()
	{
		_attribute.AddModifierAndUpdateAttribute(1.1f, 1);
		yield return new WaitForSeconds(2);
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}

	[Test()]
	public IEnumerator LastTest()
	{
		var go = (GameObject.Instantiate(Resources.Load("Test")) as GameObject).GetComponent<TestFoTests>();
		yield return StartCoroutine(go.Test2());
	}
}