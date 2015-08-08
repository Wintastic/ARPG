using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace TestMultiplayer {
	public class PlayerSyncPosition : NetworkBehaviour {

		public float lerpRate = 15;

		[SyncVar]
		private Vector3 syncPosition;

		[SyncVar]
		private Quaternion syncRotation;

		private Transform myTransform;

		void Start () {
			myTransform = gameObject.transform;
		}

		void FixedUpdate () {
			transmitPosition();
			lerpPosition();
		}

		void lerpPosition(){
			if (!isLocalPlayer) {
				myTransform.position = Vector3.Lerp(myTransform.position, syncPosition, Time.deltaTime * lerpRate);
				myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRotation, Time.deltaTime * lerpRate);
			}
		}

		[ClientCallback]
		void transmitPosition(){
			if (isLocalPlayer) {
				CmdProvidePositionToServer(myTransform.position, myTransform.rotation);
			}
		}

		[Command]
		void CmdProvidePositionToServer(Vector3 position, Quaternion rotation){
			syncPosition = position;
			syncRotation = rotation;
		}
	}
}