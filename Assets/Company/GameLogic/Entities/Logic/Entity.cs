using UnityEngine;
using System.Collections;
using FlakeGen;

public enum GeneratorIds
{
	Entity = 0
}
public class Entity
{
	private static Id64Generator ID_GENERATOR = new Id64Generator((int)GeneratorIds.Entity);

	public long entityId { get; private set; }

	public Entity()
	{
		entityId = ID_GENERATOR.GenerateId();
		Debug.Log(entityId);
	}
}
