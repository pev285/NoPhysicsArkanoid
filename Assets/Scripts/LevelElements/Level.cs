using NoPhysArkanoid.Collisions;
using NoPhysArkanoid.Forms;
using NoPhysArkanoid.Management;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		private BallStarter _starter;

		[SerializeField]
		private LevelStats _stats = new LevelStats();

		private int _bricksNumber = 0;
		private List<Powerup> _powerups = new List<Powerup>();

		private List<CircleFigure> _outOfScreenFigures = new List<CircleFigure>();

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
			CreateNewBall();

			StartCoroutine(ClearOutOfScreenFigures());
		}

		private void OnDestroy()
		{
			Unsubscribe();
			DestroyLevelObjects();
		}

		private void DestroyLevelObjects()
		{
			foreach (var ball in _balls)
				if (ball != null && ball.gameObject != null)
					Destroy(ball.gameObject);

			foreach (var pw in _powerups)
				if (pw != null && pw.gameObject != null)
					Destroy(pw.gameObject);

			foreach (var fig in _outOfScreenFigures)
				if (fig != null && fig.gameObject != null)
					Destroy(fig.gameObject);
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

		private IEnumerator ClearOutOfScreenFigures()
		{
			while (true)
			{
				for (int i = _outOfScreenFigures.Count - 1; i >= 0; i--)
				{
					var fig = _outOfScreenFigures[i];
					if (fig == null) _outOfScreenFigures.RemoveAt(i);
				}

				yield return new WaitForSeconds(3f);
			}
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
					var fig = figures[i];
					fig.MarkOutOfScreen();

					figures.RemoveAt(i);
					_outOfScreenFigures.Add(fig);
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
			_powerups.Add(pw);
		}

		public void ApplayPowerup(Powerup powerup)
		{
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
					CreateNewBall();
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

		private void CreateNewBall()
		{
			var ball = EventBuss.RequestNewBallCreation(_starter.Position);

			if (ball == null)
				throw new Exception("Couldn't create a ball");

			_balls.Add(ball);
			_starter.Activate();
		}

		private void StartABall()
		{
			Vector3 direction = _starter.Direction;

			foreach(var ball in _balls) 
				if (ball.IsMoving == false)
				{ 
					ball.StartBall(direction);
					break;
				}

			if (_balls.Any(b => b.IsMoving == false))
				return;

			_starter.Deactivate();
		}

	}
} 


