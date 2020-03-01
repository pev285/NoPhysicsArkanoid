using NoPhysArkanoid.Forms;
using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid
{
	public static class EventBuss
	{
		public static class Input
		{
			public static event Action ExitButtonPressed = () => { };
			public static event Action StartButtonPressed = () => { };

			public static event Action<float> RacketPositionRequested = (p) => { };

			public static void InvokeExitButton()
			{
				ExitButtonPressed.Invoke();
			}

			public static void InvokeStartButton()
			{
				StartButtonPressed.Invoke();
			}

			public static void RequestRacketPosition(float xPosition)
			{
				RacketPositionRequested.Invoke(xPosition);
			}
		}

		public static event Action LevelIsFailed = () => { };
		public static event Action LevelIsCleared = () => { };

		public static event Action<Powerup> PowerupCollected = (p) => { };
		public static event Action<Powerup.Kind, Vector3> PowerupCreationRequested = (a, b) => { };

		public static event Func<Vector3, Ball> CreateNewBall = (p) => null;

		public static void InvokeLevelIsCleared()
		{
			LevelIsCleared.Invoke();
		}

		public static void InvokeLevelIsFailed()
		{
			LevelIsFailed.Invoke();
		}

		public static void InvokePowerupCollected(Powerup powerup)
		{
			PowerupCollected.Invoke(powerup);
		}

		public static void RequestPowerupCreation(Powerup.Kind kind, Vector3 position)
		{
			PowerupCreationRequested.Invoke(kind, position);
		}

		public static Ball RequestNewBallCreation(Vector3 position)
		{
			return CreateNewBall.Invoke(position);
		}
	} 
} 


