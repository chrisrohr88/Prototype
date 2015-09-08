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

	public long EntityId { get; private set; }

	public Entity()
	{
		EntityId = ID_GENERATOR.GenerateId();
		Debug.Log(EntityId);
	}
}
