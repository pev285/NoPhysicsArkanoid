using NoPhysArkanoid.Forms;
using NoPhysArkanoid.Management;
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
			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe()
		{
			EventBuss.Input.StartButtonPressed += StartABall;
		}

		private void Unsubscribe()
		{
			EventBuss.Input.StartButtonPressed -= StartABall;
		}

		private void Update()
		{
			bool[] excludes = new bool[Balls.Count];

			for (int i = 0; i < _balls.Count; i++)
			{
				var position = _balls[i].Position;

				if (GameSpaceController.IsPointVisible(position) == false)
				{
					Debug.Log($"{position} -- {GameSpaceController.BottomLeft} < {GameSpaceController.UpperRight}");
					excludes[i] = true;
				}
			}

			for (int i = excludes.Length - 1; i >= 0; i--)
				if (excludes[i])
				{
					var ball = Balls[i];
					_balls.RemoveAt(i);

					ball.MarkOutOfScreen();

					if (_balls.Count == 0)
						EventBuss.InvokeLevelIsFailed();
				}

		}



		public void AddBrick()
		{
			_bricksNumber++;
		}

		public void RemoveBrick()
		{
			_bricksNumber--;

			if (_bricksNumber == 0)
				EventBuss.InvokeLevelIsCleared();
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


		private void StartABall()
		{
			Balls[0].StartBall(new Vector3(1, 2, 0));
		}

	}
} 


