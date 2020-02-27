using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid
{
	public static class EventBuss 
	{
		public static event Action LevelIsFailed = () => { };
		public static event Action LevelIsCleared = () => { };


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


