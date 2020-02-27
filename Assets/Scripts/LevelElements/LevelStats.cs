using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements
{
    [Serializable]
    public class LevelStats : ILevelStats
    {
        public event Action Changed = () => { };

        [SerializeField]
        private float _ballSpeed = 5f;
        public float BallSpeed
        {
            get
            {
                return _ballSpeed;
            }
            set 
            {
                _ballSpeed = value;
                Changed();
            }
        }

        [SerializeField]
        private float _ballForce = 1f;
        public float BallForce
        {
            get
            {
                return _ballForce;
            }
            set
            {
                _ballForce = value;
                Changed();
            }
        }

        [SerializeField]
        private float _racketWidth = 3f;
        public float RacketWidth
        {
            get
            {
                return _racketWidth;
            }
            set
            {
                _racketWidth = value;
                Changed();
            }
        }
    }
}


