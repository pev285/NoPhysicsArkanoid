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

		[SerializeField]
		private Ball _ball;

		[SerializeField]
		private LevelStats _stats = new LevelStats();
        public ILevelStats Stats
		{
			get
			{
				return _stats;
			}
		}

		public Ball Ball
		{
			get
			{
				return _ball;
			}
		}


		private void Start()
		{
			Ball.StartBall(new Vector3(1, 0, 1));
		}
	} 
} 


