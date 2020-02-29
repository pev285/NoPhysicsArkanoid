using NoPhysArkanoid.Collisions;
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
			EventBuss.PowerupCollected += ApplayPowerup;
			EventBuss.Input.StartButtonPressed += StartABall;
		}

		private void Unsubscribe()
		{
			EventBuss.PowerupCollected -= ApplayPowerup;
			EventBuss.Input.StartButtonPressed -= StartABall;
		}

		private void Update()
		{
			ExcludeInvisible(_powerups);

			if (_balls.Count == 0)
				return;

			ExcludeInvisible(_balls);

			if (_balls.Count == 0)
				EventBuss.InvokeLevelIsFailed();
		}

		private void ExcludeInvisible<T>(List<T> figures) where T : CircleFigure
		{
			bool[] excludes = new bool[figures.Count];

			for (int i = 0; i < figures.Count; i++)
			{
				var position = figures[i].Position;

				if (GameSpaceController.IsPointVisible(position) == false)
					excludes[i] = true;
			}

			for (int i = excludes.Length - 1; i >= 0; i--)
				if (excludes[i])
				{
					Debug.Log($"Excluding {i}");

					var fig = figures[i];
					figures.RemoveAt(i);

					fig.MarkOutOfScreen();
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

		public void AddPowerup(Powerup pw)
		{
			Debug.Log("Add Powerup");
			_powerups.Add(pw);
		}

		public void ApplayPowerup(Powerup powerup)
		{
			Debug.Log("Apply powerup");

			if (_powerups.Contains(powerup) == false)
				return;

			_powerups.Remove(powerup);
			Powerup.Kind kind = powerup.PowerupKind;

			Destroy(powerup.gameObject);
			AddPowerup(kind);
		}

		private void AddPowerup(Powerup.Kind kind)
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
					_stats.DecrementBallSpeed();
					break;
				case Powerup.Kind.SpeedUp:
					_stats.IncrementBallSpeed();
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


