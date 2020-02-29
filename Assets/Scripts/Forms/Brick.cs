using NoPhysArkanoid.Collisions;
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

		[SerializeField]
		private Powerup.Kind _powerup;

		private bool _isAlive;
		private Transform _transform;

		protected override void Awake()
		{
			base.Awake();
			_transform = transform;
		}

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

			InstantiatePowerup();

			Destroy(gameObject);
			Level.Instance.RemoveBrick();
		}

		private void InstantiatePowerup()
		{
			if (_powerup == Powerup.Kind._none_)
				return;

			EventBuss.RequestPowerupCreation(_powerup, _transform.position);
		}

		private void AdjustColor()
		{
			throw new NotImplementedException();
		}
	}
} 


