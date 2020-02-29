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

		//public static InputEvents Input { get; private set; }

		public static void InvokeLevelIsCleared()
		{
			LevelIsCleared();
		}

		public static void InvokeLevelIsFailed()
		{
			LevelIsFailed();
		}
	} 
} 


