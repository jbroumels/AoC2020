using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class PuzzleFour : Puzzle
    {
        public PuzzleFour() : base("04")
        {

        }

        public override object GetAnswerOne()
        {
            PassportDetails[] passports = ReadPassports().ToArray();

            return passports.Count(it => it.IsValid1());
        }

        public override object GetAnswerTwo()
        {
            PassportDetails[] passports = ReadPassports().ToArray();

            return passports.Count(it => it.IsValid2());
        }

        private IEnumerable<PassportDetails> ReadPassports()
        {
            var currentPassport = new PassportDetails();
            foreach (var line in Lines)
            {
                if (line.Trim().Length == 0)
                {
                    yield return currentPassport;
                    currentPassport = null;
                }
                else
                {
                    currentPassport ??= new PassportDetails();

                    string[] lineParts = line.Split(new[] {' '});

                    foreach (var part in lineParts)
                    {
                        string[] kv = part.Split(new[] {':'});

                        currentPassport.AddData(kv[0], kv[1]);
                    }
                }
            }

            if (currentPassport != null)
            {
                yield return currentPassport;
            }
            
        }
    }

    public class PassportDetails
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        public void AddData(string key, string value)
        {
            data[key] = value;
        }


        public bool IsValid1()
        {
            return HasData("byr")
                   && HasData("iyr")
                   && HasData("eyr")
                   && HasData("hgt")
                   && HasData("hcl")
                   && HasData("ecl")
                   && HasData("pid");
        }

        private bool HasData(string key)
        {
            return data.ContainsKey(key);
        }

        public bool IsValid2()
        {
            return IsValidYear("byr", 1920, 2002)
                   && IsValidYear("iyr",2010, 2020)
                   && IsValidYear("eyr", 2020, 2030)
                   && (IsValidHeight("hgt", "cm", 150, 193) || IsValidHeight("hgt", "in", 59, 76))
                   && IsValidHairColor("hcl")
                   && IsValidEyeColor("ecl")
                   && IsValidPassport("pid")
                ;
        }

        private bool IsValidYear(string key, int min, int max)
        {
            return HasData(key) && data[key].Length == 4 && int.TryParse(data[key], out int byr) && byr >= min && byr <= max;
        }
        private bool IsValidHeight(string key, string unit, int min, int max)
        {
            return HasData(key) && int.TryParse(data[key].Substring(0, data[key].Length - unit.Length), out int hgt) && data["hgt"].EndsWith(unit) && hgt >= min && hgt <= max;
        }

        private bool IsValidHairColor(string key)
        {
            return HasData(key) && data[key].StartsWith("#") && data[key].Length == 7 && data[key].Substring(1).All(it => char.IsDigit(it) || (char.IsLetter(it) && char.IsLower(it)));
        }
        private bool IsValidEyeColor(string key)
        {
            return HasData(key) && new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(data[key]);
        }

        private bool IsValidPassport(string key)
        {
            return HasData(key) && data[key].Length == 9 && long.TryParse(data[key], out long _);
        }
    }
}