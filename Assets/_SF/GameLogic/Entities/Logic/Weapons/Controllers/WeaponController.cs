using UnityEngine;
using System.Collections;
using SF.EventSystem;
using SF.GameLogic.EventSystem.EventData;
using SF.Utilities;

namespace SF.GameLogic.Entities.Logic.Weapons.Controllers
{
	public class WeaponController
	{
		private Weapon _weapon;
		private DummyGameObject _dummyGameObject;

		public WeaponController(Weapon weapon)
		{
			_weapon = weapon;
			_dummyGameObject = new GameObject("Dummy", typeof(DummyGameObject)).GetComponent<DummyGameObject>();
			_dummyGameObject.OnUpdate += HandleUpdate;
			SFEventManager.RegisterEventListener(SFEventType.EnemyDeath, new ConcreteSFEventListener<EnemyDeathEventData> { MethodToExecute = HandleEnemyDeath, TargetId = _weapon.PlayerEntityId });
		}

		private void HandleEnemyDeath(EnemyDeathEventData eventData)
		{
			Debug.Log("Destroy");
			GameObject.Destroy(_dummyGameObject.gameObject);
		}

		public void StartFiring(Vector3 targetPostion)
		{
			_weapon.TriggerAdapter.TargetPosition = targetPostion;
			_weapon.TriggerAdapter.StartFiring();
		}

		public void StopFiring()
		{
			_weapon.TriggerAdapter.StopFiring();
		}

		private void HandleUpdate()
		{
			_weapon.TriggerAdapter.Update();
		}
	}
}
