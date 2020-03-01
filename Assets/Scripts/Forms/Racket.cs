using NoPhysArkanoid.Collisions;
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
		private List<Powerup> _powerupsToApplay = new List<Powerup>();

		protected override void Awake()
		{
			base.Awake();
			_transform = transform;
		}

		private void Start()
		{
			Subscribe();
			UpdateWidth();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe()
		{
			Level.Instance.Stats.Modified += UpdateWidth;
			EventBuss.Input.RacketPositionRequested += UpdateTargetPosition;
		}

		private void Unsubscribe()
		{
			Level.Instance.Stats.Modified -= UpdateWidth;
			EventBuss.Input.RacketPositionRequested -= UpdateTargetPosition;
		}

		private void UpdateWidth()
		{
			var scale = _transform.localScale;
			var width = Level.Instance.Stats.RacketWidth;

			scale.x = width;
			_transform.localScale = scale;
		}

		private void UpdateTargetPosition(float x)
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
				Hit hit;

				if (_boxFigure.CheckCollision(pw, out hit))
					_powerupsToApplay.Add(pw);
			}

			foreach(var pw in _powerupsToApplay)
				EventBuss.InvokePowerupCollected(pw);

			_powerupsToApplay.Clear();
		}
	}
} 


