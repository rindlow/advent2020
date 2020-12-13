using System;
namespace Advent2020
{
    public class Ferry
    {
        private int X;
        private int Y;
        private int Direction;
        private int Wx;
        private int Wy;
        public Ferry()
        {
            X = 0;
            Y = 0;
            Direction = 0;
            Wx = 10;
            Wy = -1;
        }
        public void NavigateFromFile(string filename)
        {
            foreach (string instruction in FileReader.ReadFileOfStrings(filename))
            {
                Navigate(instruction);
            }
        }

        public void NavigateByWaypointFromFile(string filename)
        {
            foreach (string instruction in FileReader.ReadFileOfStrings(filename))
            {
                NavigateByWaypoint(instruction);
            }
        }
        public void Navigate(string instruction)
        {
            char action = instruction[0];
            int value = int.Parse(instruction.Substring(1));
            switch (action)
            {
            case 'N':
                Y -= value; 
                break;
            case 'E':
                X += value; 
                break;
            case 'S':
                Y += value; 
                break;
            case 'W':
                X -= value; 
                break;
            case 'L':
                Direction = (Direction + value) % 360; 
                break;
            case 'R':
                Direction = (Direction + 360 - value) % 360;
                break;
            case 'F':
                switch (Direction)
                {
                case 0:
                    X += value;
                    break;
                case 90:
                    Y -= value;
                    break;
                case 180:
                    X -= value;
                    break;
                case 270:
                    Y += value;
                    break;
                default:
                    throw new Exception($"Unexpected direction {Direction}");
                }
                break;
            default:
                throw new Exception($"Unexpected action {action}");
            }
        }
        public void NavigateByWaypoint(string instruction)
        {
            char action = instruction[0];
            int value = int.Parse(instruction.Substring(1));
            // Console.WriteLine($"X={X} Y={Y} Wx={Wx} Wy={Wy} action={action} value={value}");
            switch (action)
            {
            case 'N':
                Wy -= value; 
                break;
            case 'E':
                Wx += value; 
                break;
            case 'S':
                Wy += value; 
                break;
            case 'W':
                Wx -= value; 
                break;
            case 'L':
                for (int i = value; i > 0; i -= 90)
                {
                    (Wx, Wy) = (Wy, -Wx);
                }
                break;
            case 'R':
                for (int i = value; i > 0; i -= 90)
                {
                    (Wx, Wy) = (-Wy, Wx);
                }
                break;
            case 'F':
                X += Wx * value;
                Y += Wy * value;
                break;
            default:
                throw new Exception($"Unexpected action {action}");
            }
        }
        public int DistanceFromStart()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
    }
}