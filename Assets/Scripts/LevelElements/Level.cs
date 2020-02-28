using NoPhysArkanoid.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements
{
	public class Level : MonoBehaviour 
	{
        #region Global access
        public static Level Instance { get; private set; }
		private void Awake()
		{
			Instance = this;
		}
		#endregion

		public event Action LevelCleared = () => { };

		[SerializeField]
		private List<Ball> _balls;

		[SerializeField]
		private LevelStats _stats = new LevelStats();

		private int _bricksNumber = 0;
		private List<Powerup> _powerups = new List<Powerup>();

        public ILevelStats Stats
		{
			get
			{
				return _stats;
			}
		}

		public IReadOnlyList<Ball> Balls
		{
			get
			{
				return _balls;
			}
		}

		public IReadOnlyList<Powerup> Powerups
		{
			get
			{
				return _powerups;
			}
		}

		private void Start()
		{
			Balls[0].StartBall(new Vector3(-1, 2, 0));
		}

		public void AddBrick()
		{
			_bricksNumber++;
		}

		public void RemoveBrick()
		{
			_bricksNumber--;

			if (_bricksNumber == 0)
				LevelCleared.Invoke();
		}

		public void ApplayPowerup(Powerup.Kind kind)
		{
			switch (kind)
			{
				case Powerup.Kind.BallSizeUp:
					_stats.IncrementBallRadius();
					break;
				case Powerup.Kind.ExtraBall:
					throw new NotImplementedException();
					break;
				case Powerup.Kind.RocketSizeUp:
					_stats.IncrementRacketSize();
					break;
				case Powerup.Kind.SpeedDown:
					_stats.IncrementBallSpeed();
					break;
				case Powerup.Kind.SpeedUp:
					_stats.DecrementBallSpeed();
					break;
				default:
					throw new ArgumentException("Unexpected powerup kind");
			}
		}
	}
} 


