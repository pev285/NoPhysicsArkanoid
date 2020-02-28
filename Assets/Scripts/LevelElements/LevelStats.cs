using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements
{
    [Serializable]
    public class LevelStats : ILevelStats
    {
        public event Action Modified = () => { };

        [SerializeField]
        private float _ballRadius = 0.25f;
        public float BallRadius
        {
            get
            {
                return _ballRadius;
            }
            set
            {
                _ballRadius = value;
                Modified.Invoke();
            }
        }

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
                Modified.Invoke();
            }
        }

        [SerializeField]
        private int _ballForce = 1;
        public int BallForce
        {
            get
            {
                return _ballForce;
            }
            set
            {
                _ballForce = value;
                Modified.Invoke();
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
                Modified.Invoke();
            }
        }

        [Space(15)]

        [SerializeField]
        private float _racketSizeStep = 1;
        [SerializeField]
        private float _ballSpeedStep = 1f;
        [SerializeField]
        private float _ballSizeStep = 0.5f;


        public void IncrementBallRadius()
        {
            BallRadius += _ballSizeStep;
        }

        public void IncrementBallSpeed()
        {
            BallSpeed += _ballSpeedStep;
        }

        public void DecrementBallSpeed()
        {
            BallSpeed -= _ballSpeedStep;
        }

        public void IncrementRacketSize()
        {
            RacketWidth += _racketSizeStep;
        }
    }
}


