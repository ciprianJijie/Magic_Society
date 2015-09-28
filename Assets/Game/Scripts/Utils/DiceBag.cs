using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MS.Tools
{
    public static class DiceBag
    {
        public static int Roll(int amount, int faceNumber, int bonus)
        {
            int result;

            result = 0;

            for (int i = 0; i < amount; i++)
            {
                result += Random.Range(1, faceNumber);
            }

            result += bonus;

            return result;
        }

        public static int RollAndDiscardLowers(int amount, int faceNumber, int discard)
        {
            List<int>   rolls;
            int         result;

            rolls = new List<int>();
            result = 0;

            for (int i = 0; i < amount; i++)
            {
                rolls.Add(Roll(1, faceNumber, 0));
            }

            rolls.Sort(delegate(int a, int b)
            {
                return b.CompareTo(a);
            }
            );

            for (int i = 0; i < amount - discard; i++)
            {
                result += rolls[i];
            }

            return result;
        }
    }
}
