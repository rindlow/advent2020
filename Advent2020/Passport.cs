using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class PassportInfo
    {
        public int byr { get; set; }
        public int iyr { get; set; }
        public int eyr { get; set; }
        public string hgt { get; set; }
        public string hcl { get; set; }
        public string ecl { get; set; }
        public string pid { get; set; }
        public int cid { get; set; }

        public bool ValuesPresent()
        {
            return (byr != 0 && iyr != 0 && eyr != 0 && hgt != null && hcl != null && ecl != null && pid != null);
        }
        public bool IsValid() 
        {
            if (!ValuesPresent())
            {
                return false;
            }
            return byrValid() && iyrValid() && eyrValid() && hgtValid() && hclValid() && eclValid() && pidValid();
        }
        private bool byrValid()
        {
            return byr >= 1920 && byr <= 2002;
        }
        private bool iyrValid()
        {
            return iyr >= 2010 && iyr <= 2020;
        }
        private bool eyrValid()
        {
            return eyr >= 2020 && eyr <= 2030;
        }
        private bool hgtValid()
        {
            Regex regex = new Regex(@"^(\d+)(cm|in)$");
            Match match = regex.Match(hgt);
            if (!match.Success) {
                return false;
            }
            int height = int.Parse(match.Groups[1].Value);
            return ((match.Groups[2].Value == "cm" && height >= 150 && height <= 193)
                    || (match.Groups[2].Value == "in" && height >= 59 && height <= 76));
        }
        private bool hclValid()
        {
            Regex regex = new Regex(@"^#[0-9a-f]{6}$");
            Match match = regex.Match(hcl);
            return match.Success;
        }
        private bool eclValid()
        {
            Regex regex = new Regex(@"^amb|blu|brn|gry|grn|hzl|oth$");
            Match match = regex.Match(ecl);
            return match.Success;
        }
        private bool pidValid()
        {
            Regex regex = new Regex(@"^\d{9}$");
            Match match = regex.Match(pid);
            return match.Success;
        }
    }
    public class Passport
    {
        public static List<PassportInfo> ReadPassportsFromFile(string filename)
        {
            List<PassportInfo> passportList = new List<PassportInfo>();
            PassportInfo passport = new PassportInfo();
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                if (line == "")
                {
                    passportList.Add(passport);
                    passport = new PassportInfo();
                }
                else
                {
                    foreach (string keyvalue in line.Split(' '))
                    {
                        string[] kv = keyvalue.Split(':');
                        switch (kv[0])
                        {
                            case "byr":
                                passport.byr = int.Parse(kv[1]);
                                break;
                            case "iyr":
                                passport.iyr = int.Parse(kv[1]);
                                break;
                            case "eyr":
                                passport.eyr = int.Parse(kv[1]);
                                break;
                            case "hgt":
                                passport.hgt = kv[1];
                                break;
                            case "hcl":
                                passport.hcl = kv[1];
                                break;
                            case "ecl":
                                passport.ecl = kv[1];
                                break;
                            case "pid":
                                passport.pid = kv[1];
                                break;
                            case "cid":
                                passport.cid = int.Parse(kv[1]);
                                break;
                        }
                    }
                }
            }
            passportList.Add(passport);
            return passportList;
        }
        public static int CheckPassportsFromFile(string filename)
        {
            return ReadPassportsFromFile(filename).Count(passport => passport.ValuesPresent());
        }
        public static int ValidatePassportsFromFile(string filename)
        {
            return ReadPassportsFromFile(filename).Count(passport => passport.IsValid());
        }
    }
}