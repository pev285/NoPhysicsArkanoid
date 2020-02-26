using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid.LevelElements
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

        public ILevelStats Stats { get; private set; } = new LevelStats();

		public Ball Ball
		{
			get
			{
				return _ball;
			}
		}

		[SerializeField]
		private Ball _ball;

	} 
} 


