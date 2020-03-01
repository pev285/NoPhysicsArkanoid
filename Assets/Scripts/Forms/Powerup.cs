using NoPhysArkanoid.Collisions;
using NoPhysArkanoid.Forms;
using NoPhysArkanoid.LevelElements;
using NoPhysArkanoid.Management;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Powerup : CircleFigure	
	{
		public enum Kind
		{
			_none_,
			SpeedUp,
			SpeedDown,
			ExtraBall,
			RocketSizeUp,
			BallSizeUp,
		}

		public Kind PowerupKind;

		public float RotationSpeed = 30f;
		public Vector3 Velocity = new Vector3(0, -1, 0);

		private void Start()
		{
			Level.Instance.AddPowerup(this);
		}

		private void Update()
		{
			_transform.position += Time.deltaTime * Velocity;
			_transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);

			NextPosition = Position;
		}
	} 
} 


