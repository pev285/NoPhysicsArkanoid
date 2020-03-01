using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Collisions
{
    public class Line
    {
        //-- Normal equation: x * normal.x + y * normal.y = coeff
        private float _coeff;
        public Vector2 Normal;

        public bool IsHorizontal
        {
            get
            {
                return Mathf.Approximately(Normal.x, 0);
            }
        }

        public bool IsVertical
        {
            get
            {
                return Mathf.Approximately(Normal.y, 0);
            }
        }

        private Line() { }

        public static Line CreateFromTwoPoints(Vector2 point1, Vector2 point2)
        {
            if (point1 == point2)
                throw new ArgumentException("Can't create a line using one point");

            var a = point2.y - point1.y;
            var b = point1.x - point2.x;
            var c = point1.x * point2.y - point1.y * point2.x;

            return CreateFromABC(a, b, c);
        }

        public static Line CreateFromPointAndDirection(Vector2 point, Vector2 directionVector)
        {
            var a = directionVector.y;
            var b = -directionVector.x;
            var c = directionVector.y * point.x - directionVector.x * point.y;

            return CreateFromABC(a, b, c);
        }

        public static Line CreateFromPointAndNormal(Vector2 point, Vector2 normal)
        {
            var a = normal.x;
            var b = normal.y;
            var c = normal.x * point.x + normal.y * point.y;

            return CreateFromABC(a, b, c);
        }

        //--- a*x + b*y = c ---
        private static Line CreateFromABC(float a, float b, float c)
        {
            var line = new Line();
            var magn = new Vector2(a, b).magnitude;

            line.Normal = new Vector2(a / magn, b / magn);
            line._coeff = c / magn;

            return line;
        }


        public float NormalFunction(Vector2 point)
        {
            return Vector2.Dot(point, Normal) - _coeff;
        }

        public Vector2 GetProjectionOf(Vector2 point)
        {
            if (Mathf.Approximately(NormalFunction(point), 0))
                return point;

            var perpendLine = CreateFromPointAndDirection(point, Normal);
            return FindIntersection(this, perpendLine);
        }

        public float GetDistanseTo(Vector2 point)
        {
            return Mathf.Abs(NormalFunction(point));
        }


        public static Vector2 FindIntersection(Line line1, Line line2)
        {
            if (Mathf.Abs(Vector2.Dot(line1.Normal, line2.Normal)) == 1)
                throw new ArgumentException("Can't find intersection of two collinear lines");

            var a1 = line1.Normal.x;
            var b1 = line1.Normal.y;
            var c1 = line1._coeff;

            var a2 = line2.Normal.x;
            var b2 = line2.Normal.y;
            var c2 = line2._coeff;

            return FindIntersection(a1, b1, c1, a2, b2, c2);
        }

        private static Vector2 FindIntersection(float a1, float b1, float c1, float a2, float b2, float c2)
        {
            if (Mathf.Approximately(a1 * b2, b1 * a2))
                throw new ArgumentException("Invalid parameters for intersecting lines.");

            float denomenator = (b1 * a2 - a1 * b2);

            var x = (b1 * c2 - c1 * b2) / denomenator;
            var y = (c1 * a2 - a1 * c2) / denomenator;

            return new Vector2(x, y);
        }

        private static void Swap(ref float a, ref float b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        public static float FindDistance(Vector2 linePoint1, Vector2 linePoint2, Vector2 targetPoint)
        {
            var line = CreateFromTwoPoints(linePoint1, linePoint2);
            return line.GetDistanseTo(targetPoint);
        }

        public static Vector2 FindProjection(Vector2 linePoint1, Vector2 linePoint2, Vector2 pointToProject)
        {
            var line = CreateFromTwoPoints(linePoint1, linePoint2);
            return line.GetProjectionOf(pointToProject);
        }
    }
}


