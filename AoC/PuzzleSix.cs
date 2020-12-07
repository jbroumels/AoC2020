using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AoC
{
    public class PuzzleSix : Puzzle
    {
        public PuzzleSix() : base("06"){}

        public override object GetAnswerOne()
        {
            PersonGroup[] groups = GetGroups().ToArray();

            return groups.SelectMany(it => it.GetYesAnswers()).Count();
        }

        public override object GetAnswerTwo()
        {
            PersonGroup[] groups = GetGroups().ToArray();

            return groups.Sum(it => it.GetCountOfAllAnsweredYes());
        }


        private IEnumerable<PersonGroup> GetGroups()
        {
            PersonGroup currentGroup = null;
            foreach (var line in Lines)
            {
                if (line.Trim().Length == 0)
                {
                    yield return currentGroup;
                    currentGroup = null;
                }
                else
                {
                    currentGroup ??= new PersonGroup();

                    currentGroup.Add(new Person(line));
                }
            }

            if (currentGroup != null)
            {
                yield return currentGroup;
            }
        }

        public class Person
        {
            private int[] answers;

            public Person(string line)
            {
                answers = line.Select(it => (int) it - 96).ToArray();
            }

            public int[] GetYesAnswers()
            {
                return answers;
            }
        }

        public class PersonGroup : Collection<Person>
        {
            public int[] GetYesAnswers()
            {
                return this.SelectMany(it => it.GetYesAnswers()).Distinct().OrderBy(it => it).ToArray();
            }

            public int GetCountOfAllAnsweredYes()
            {
                return this.SelectMany(it => it.GetYesAnswers()).GroupBy(it => it).Count(it => it.Count<int>() == this.Count);
            }
        }
    }
}