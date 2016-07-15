#if UNITY_EDITOR
using SFUnitTestCore;
using UnityEngine;
using System.Collections;
using SF.Utilities.ModifiableAttributes;

[TestClass()]
public class ModifiableAttributeTests : Test
{
	private ModifiableAttribute _attribute;
	
	public void Setup()
	{
		_attribute = ModifiableAttribute.Create(100);
	}

	[Test(OverrideClearScene = true, ContinuousIntegrationOverride = true)]
	public void BaseValueShouldBeUnmodified()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}

	[Test(OverrideClearScene = true, ContinuousIntegrationOverride = true)]
	public void ValueShouldAlterWhenAModifierIsApplied()
	{
		_attribute.AddModifierAndUpdateAttribute(1.1f);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
	}
	
	[Test(OverrideClearScene = true, Timeout = 2, ContinuousIntegrationOverride = true)]
	public IEnumerator ValueShouldAlterWhenAModifierIsExpired()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
		_attribute.AddModifierAndUpdateAttribute(1.1f, 1);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
		yield return new WaitForSeconds(1.5f);
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}
	
	[Test(OverrideClearScene = true, ContinuousIntegrationOverride = true)]
	public void ValueShouldAlterWhenAModifierIsRemoved()
	{
		Assert.AssertEquals(100, _attribute.ModifiedValue);
		var key = _attribute.AddModifierAndUpdateAttribute(1.1f, 1);
		Assert.AssertEquals(110, _attribute.ModifiedValue);
		_attribute.RemoveModifierAndUpdateValue(key);
		Assert.AssertEquals(100, _attribute.ModifiedValue);
	}
	
	[Test(ContinuousIntegrationOverride = true)]
	public IEnumerator FoYoTests()
	{
		var component = GameObject.Instantiate(Resources.Load("Test")) as GameObject;
		if(component != null)
		{
			yield return StartCoroutine(component.GetComponent<TestFoTests>().Test2());
		}
	}
	
	[Test(ContinuousIntegrationOverride = true)]
	public IEnumerator NewTest()
	{
		var component = GameObject.Instantiate(Resources.Load("Test")) as GameObject;
		if(component != null)
		{
			yield return StartCoroutine(component.GetComponent<TestFoTests>().Test2());
		}
	}
	
	[Test(ContinuousIntegrationOverride = true)]
	public IEnumerator FoYoTests2()
	{
		var component = GameObject.Instantiate(Resources.Load("Test")) as GameObject;
		if(component != null)
		{
			yield return StartCoroutine(component.GetComponent<TestFoTests>().Test4());
		}
	}
}
#endif