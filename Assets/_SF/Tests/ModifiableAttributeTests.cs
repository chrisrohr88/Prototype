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
	}

	[Test()]
	public void BaseValueShoulrBeUnmodified()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}

	[Test()]
	public void ValueShouldAlterWhenAModifierIsApplied()
	{
		_attribute.AddModifierAndUpdateAttribute(1.1f);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
	}
	
	[Test()]
	public IEnumerator ValueShouldAlterWhenAModifierIsExpired()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
		_attribute.AddModifierAndUpdateAttribute(1.1f, 1);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
		yield return new WaitForSeconds(2);
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}
	
	[Test()]
	public void ValueShouldAlterWhenAModifierIsRemoved()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
		var key = _attribute.AddModifierAndUpdateAttribute(1.1f, 1);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
		_attribute.RemoveModifierAndUpdateValue(key);
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}
	
	[Test()]
	public IEnumerator FoYoTests()
	{
		var component = GameObject.Instantiate(Resources.Load("Test")) as GameObject;
		if(component != null)
		{
			yield return StartCoroutine(component.GetComponent<TestFoTests>().Test2());
		}
	}
	
	[Test()]
	public IEnumerator FoYoTests2()
	{
		var component = GameObject.Instantiate(Resources.Load("Test")) as GameObject;
		if(component != null)
		{
			yield return StartCoroutine(component.GetComponent<TestFoTests>().Test4());
		}
	}
}