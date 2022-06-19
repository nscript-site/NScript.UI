using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI
{
    public struct Range
    {
        public float Min, Max;

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetDistance(float val)
        {
            if (val >= Min && val <= Max) return 0;
            else return Math.Max(Min - val, val - Max);
        }
    }
}
