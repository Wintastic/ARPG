using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
	{
		private PlayerCharacter m_Character;
		private Transform myTransform;
		private Transform cameraTransform;
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		
		private Vector3 desiredRelativePosition;
		private Vector3 cameraTargetPosition;
		
		
		private void Start()
		{
			myTransform = gameObject.transform;
			cameraTransform  = transform.FindChild("PlayerCamera");
			Camera camera = cameraTransform.gameObject.GetComponent<Camera>();
			camera.enabled = true;
			cameraTransform.parent = null;
			desiredRelativePosition = new Vector3(0, 8, -8);
			cameraTargetPosition = myTransform.position + desiredRelativePosition;

			m_Character = GetComponent<PlayerCharacter>();
		}
		
		
		private void Update()
		{
			if (!m_Jump)
			{
				m_Jump = Input.GetButtonDown("Jump");
			}
			
			cameraTargetPosition = myTransform.position + desiredRelativePosition;
			cameraTransform.position = cameraTargetPosition;
		}
		
		private void FixedUpdate()
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			bool crouch = Input.GetKey(KeyCode.C);
						
			m_Move = new Vector3(h, 0, v);;
			
			m_Character.Move(m_Move, crouch, m_Jump);
			m_Jump = false;
		}
	}

