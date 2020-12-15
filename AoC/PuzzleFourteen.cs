using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AoC
{
    public class PuzzleFourteen : Puzzle
    {
        public PuzzleFourteen() : base("14"){}

        public override object GetAnswerOne()
        {
            string currentMask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            Dictionary<long, bool[]> memory = new Dictionary<long, bool[]>();

            foreach (string line in Lines)
            {

                if (line.StartsWith("mask = "))
                {
                    currentMask = line.Substring(7);
                }
                else if (line.StartsWith("mem["))
                {
                    long address = Convert.ToInt64(line.Substring(4, line.IndexOf("]") - 4));
                    long val = Convert.ToInt64(line.Substring(line.IndexOf("=") + 1).Trim());

                    memory[address] = ApplyMask(LongToBoolArray(val), currentMask);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return memory.Sum(it => BoolArrayToLong(it.Value));
        }

        public override object GetAnswerTwo()
        {
            string currentMask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            Dictionary<long, bool[]> memory = new Dictionary<long, bool[]>();

            foreach (string line in Lines)
            {
                if (line.StartsWith("mask = "))
                {
                    currentMask = line.Substring(7);
                }
                else if (line.StartsWith("mem["))
                {
                    long address = Convert.ToInt64(line.Substring(4, line.IndexOf("]") - 4));
                    long val = Convert.ToInt64(line.Substring(line.IndexOf("=") + 1).Trim());

                    bool?[] maskAppliedAddress = ApplyMask2(LongToBoolArray(address), currentMask);

                    bool[][] addressesToApply = GetAddresses(maskAppliedAddress).ToArray();

                    foreach (var a in addressesToApply)
                    {
                        memory[BoolArrayToLong(a)] = LongToBoolArray(val);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return memory.Sum(it => BoolArrayToLong(it.Value));
        }

        private IEnumerable<bool[]> GetAddresses(bool?[] floatingAddress)
        {
            int indexOfX = -1;
            for (int x = 0; x < floatingAddress.Length; x++)
            {
                if (!floatingAddress[x].HasValue)
                {
                    indexOfX = x;
                    break;
                }
            }

            if (indexOfX >= 0)
            {
                bool?[] newAddress = new bool?[36];
                floatingAddress.CopyTo(newAddress, 0);

                newAddress[indexOfX] = false;
                foreach (var s in GetAddresses(newAddress))
                {
                    yield return s;
                }
                newAddress[indexOfX] = true;
                foreach (var s in GetAddresses(newAddress))
                {
                    yield return s;
                }
            }
            else
            {
                yield return floatingAddress.Select(it => it.Value).ToArray();
            }
        }

        private static bool[] LongToBoolArray(long l)
        {
            return Convert.ToString(l, 2).PadLeft(36, '0').Select(it => it == '1').ToArray();
        }
        private static long BoolArrayToLong(bool[] input)
        {
            string s = new string(input.Select(x => x ? '1' : '0').ToArray());
            return Convert.ToInt64(s, 2);
        }

        private static bool[] ApplyMask(bool[] input, string mask)
        {
            return mask.Select((it, i) =>
            {
                if (it == 'X')
                {
                    return input[i];
                }
                return it == '1';
            }).ToArray();
        }


        private static bool?[] ApplyMask2(bool[] input, string mask)
        {
            return mask.Select((it, i) =>
            {
                if (it == '0')
                {
                    return input[i];
                }
                if (it == '1')
                {
                    return true;
                }

                return (bool ?)null;
            }).ToArray();
        }
    }
}