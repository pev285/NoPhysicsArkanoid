using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements 
{
	public interface ILevelStats  
	{
		event Action Changed;

		float BallSpeed { get; }
		float RacketWidth { get; }
		float BallForce { get; }
	}
} 


