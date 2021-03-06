using System;
using ECS.Callbacks;
using UnityEngine;

namespace GameComponents.Unity {
	[DisallowMultipleComponent]
	public class PositionComponent : MonoBehaviour
		, IPositionComponent
		, IRemovable
	{
		//
		// IPositionComponent
		//

		Vector3 IPositionComponent.position {
			get { return transform.position; }
			set { transform.position = value; }
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
