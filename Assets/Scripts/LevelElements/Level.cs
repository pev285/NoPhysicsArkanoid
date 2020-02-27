using NoPhysArkanoid.Forms;
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


		private void Start()
		{
			Balls[0].StartBall(new Vector3(1, 1, 0));
		}
	} 
} 


