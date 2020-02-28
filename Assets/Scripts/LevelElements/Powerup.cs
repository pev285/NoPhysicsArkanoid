using NoPhysArkanoid.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements
{
	public class Powerup : CircleFigure	
	{
		public enum Kind
		{
			SpeedUp,
			SpeedDown,
			ExtraBall,
			RocketSizeUp,
			BallSizeUp,
		}

		public Kind PowerupKind;

		internal void Apply()
		{
			Level.Instance.ApplayPowerup(PowerupKind);
			Destroy(gameObject);
		}
	} 
} 


