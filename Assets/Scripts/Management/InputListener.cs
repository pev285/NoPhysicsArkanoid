using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Management
{
	public class InputListener : MonoBehaviour 
	{
		private Camera _camera;

		private void Awake()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				EventBuss.Input.InvokeExitButton();

			if (Input.GetKeyDown(KeyCode.Space))
				EventBuss.Input.InvokeStartButton();

			if (Input.GetMouseButton(0))
				RequestRacketPosition();

		}

		private void RequestRacketPosition()
		{
			var mousePosition = Input.mousePosition;
			var worldPosition = _camera.ScreenToWorldPoint(mousePosition);

			EventBuss.Input.RequestRacketPosition(worldPosition.x);
		}
	} 
} 


