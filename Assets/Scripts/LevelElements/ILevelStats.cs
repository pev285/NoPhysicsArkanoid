using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements 
{
	public interface ILevelStats  
	{
		event Action Modified;

		float BallSpeed { get; }
		float BallRadius { get; }
		
		int BallForce { get; }
		float RacketWidth { get; }
	}
} 


