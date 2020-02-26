using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid.LevelElements 
{
	public interface ILevelStats  
	{
		event Action Changed;

		float BallSpeed { get; }
		float RacketWidth { get; }
	}
} 


