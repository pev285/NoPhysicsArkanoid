using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Brick : BallsReflector
	{
		[SerializeField]
		private int _health = 1;

		private bool _isAlive;

		private void Start()
		{
			Level.Instance.AddBrick();
			ProcessCollision += TakeDamage;

			_isAlive = true;
		}

		private void TakeDamage(Ball ball, Vector3 point, EdgeAngle angle)
		{
			if (_isAlive == false)
				return;

			_health -= Level.Instance.Stats.BallForce;
			_isAlive = _health > 0;

			AdjustColor();
			if (_isAlive) return;

			Level.Instance.RemoveBrick();
			Destroy(gameObject);
		}

		private void AdjustColor()
		{
			//throw new NotImplementedException();
		}
	}
} 


