﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour {

	public override void OnStartLocalPlayer(){
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);		
	}
	
	public override void PreStartClient(){
		GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
	}
}
