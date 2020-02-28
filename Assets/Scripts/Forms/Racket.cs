using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Racket : BallsReflector
	{
		protected override void Update()
		{
			base.Update();

			if (Level.Instance == null)
				return;

			var powerups = Level.Instance.Powerups;

			foreach (var pw in powerups)
			{
				Vector3 point = Vector3.zero;
				EdgeAngle angle = EdgeAngle.Zero;

				if (_boxFigure.CheckCollision(pw, out point, out angle))
					pw.Apply();
			}
		}


	}
} 


