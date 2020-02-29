using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Racket : BallsReflector
	{
		private Transform _transform;

		protected override void Awake()
		{
			base.Awake();
			_transform = transform;
		}

		private void Start()
		{
			EventBuss.Input.RacketPositionRequested += AdjustTargetPosition;
		}

		private void OnDestroy()
		{
			EventBuss.Input.RacketPositionRequested -= AdjustTargetPosition;
		}

		private void AdjustTargetPosition(float x)
		{
			var position = _transform.position;
			position.x = x;
			_transform.position = position;
		}

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


