using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComputerMaintenanceTraining.Utils
{
    public static class CircleDegreeUtil
    {
        private const float SemiCircle = 180;
        private const float Circle = 360;

        public static float GetMinDegreeDifference(float from, float to)
        {
            float difference = to - from;

            float result;

            if(Mathf.Abs(difference) > SemiCircle)
            {
                if(from > to)
                {
                    result = Circle - from + to;
                }
                else
                {
                    result = - (Circle - to + from);
                }
            }
            else
            {
                return difference;
            }

            return result;
        }
    }
}