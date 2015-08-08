using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace TestMultiplayer {
	public class PlayerNetworkSetup : NetworkBehaviour {

		void Start () {
			if (isLocalPlayer) {
				GameObject.Find("SceneCamera").SetActive(false);
				GetComponent<PlayerController>().enabled = true;
				Transform cameraTransform  = transform.FindChild("CameraObject");
				Camera camera = cameraTransform.gameObject.GetComponent<Camera>();
				camera.enabled = true;
				cameraTransform.parent = null;
			}
		}

	}
}