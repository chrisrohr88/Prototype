using SFUnitTest;
using UnityEngine;
using System.Collections;

public class ModifiableAttributeTests : Test
{
	private ModifiableAttribute _attribute;
	
	public void Setup()
	{
		_attribute = ModifiableAttribute.Create(100);
		Debug.Log("Setup!!!");
	}

	public void CheckBaseValue()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}
	
	public void AddModifierTest()
	{
		_attribute.AddModifierAndUpdateAttribute(1.1f);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
	}
	
	public IEnumerator ModifierExpiredTest()
	{
		_attribute.AddModifierAndUpdateAttribute(1.1f, 1);
		yield return new WaitForSeconds(2);
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}
}