using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid.LevelElements
{
    public class LevelStats : ILevelStats
    {
        public event Action Changed = () => { };


        private float _ballSpeed = 5;
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

        private float _racketWidth;
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


