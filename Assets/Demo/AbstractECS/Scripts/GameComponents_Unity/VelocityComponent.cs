using System;
using ECS.Callbacks;
using UnityEngine;

namespace GameComponents.Unity {
	[DisallowMultipleComponent]
	public class VelocityComponent : MonoBehaviour
		, IVelocityComponent
		, IRemovable
	{
		public Vector3 Velocity;

		//
		// IVelocityComponent
		//

		Vector3 IVelocityComponent.velocity {
			get { return Velocity; }
			set { Velocity = value; }
		}

		//
		// IRemovable
		//

		Action IRemovable.callback { get; set; }

		//
		// Callbacks from Unity
		//

		private UnityGameContext UnityGameContext;
		private void Awake() {
			this.UnityGameContext = UnityGameContext.instance;
			UnityGameContext.AddComponent(
				base.GetComponent<ECS.IEntity>(), component: this
			);
		}

		private void OnDestroy() {
			((IRemovable)this).callback?.Invoke();
		}
	}
}
