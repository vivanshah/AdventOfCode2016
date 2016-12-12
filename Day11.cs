using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day11
    {
        HashSet<string> visited = new HashSet<string>();
        int solution = int.MaxValue;
        //PriorityQueue<Building>  queue = new PriorityQueue<Building>();
        Queue<Building> queue = new Queue<Building>();
        public void calculate1()
        {
            var st = new Stopwatch();
            st.Start();
            var input = File.ReadLines("input\\day11part2.txt");
            var genRegex = "([a-z]+) generator";
            var chipRegex = "([a-z]+)-compatible microchip";
            var building = new Building();
                       
            building.Floors = new List<Floor>();
            building.Elevator = 0;
            //construct initial building
            foreach (var line in input)
            {
                var chips = Regex.Matches(line, chipRegex);
                var gens = Regex.Matches(line, genRegex);
                building.Floors.Add(new Floor()
                {
                    Chips = new List<string>(chips.Cast<Match>().Select(x=>x.Groups.Cast<Group>().Last().Value+ "_Chip")),
                    Gens = new List<string>(gens.Cast<Match>().Select(x => x.Groups.Cast<Group>().Last().Value + "_Gen"))
                });
            }
            building.Steps = 0;
            queue.Enqueue(building);
            int count = 0;
            while (queue.Count> 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current.State()))
                {
                    continue;
                }
                visited.Add(current.State());
                count++;
                if (IsComplete(current))
                {
                    if (solution > current.Steps)
                    {
                        solution = current.Steps;
                        Console.WriteLine("SOLUTION FOUND: " + solution);
                        break;
                    }
                }
                var moves = GetValidMoves(current);
                foreach(var move in moves)
                {
                    queue.Enqueue(move);
                }
            }
            st.Stop();
            Console.WriteLine(solution + " in " + st.Elapsed.TotalMilliseconds);
        }

        private List<Building> GetValidMoves(Building building)
        {
            var moves = new List<Building>();
            var currentFloor = building.Floors[building.Elevator];
            var moveGroups = GetValidGroups(currentFloor);
            foreach (var group in moveGroups)
            {
                if (building.Elevator < 3) //can go up
                {
                    var movedBuilding = ApplyMoveAndCheck(building, group, building.Elevator + 1);
                    if (movedBuilding != null)
                    {
                        moves.Add(movedBuilding);
                    }
                }
                if (building.Elevator > 0) //can go up
                {
                    var movedBuilding = ApplyMoveAndCheck(building, group, building.Elevator - 1);
                    if (movedBuilding != null)
                    {
                        moves.Add(movedBuilding);
                    }
                }
            }
            return moves.OrderBy(x=>x.Goal).ToList();
        }

        private Building ApplyMoveAndCheck(Building building, Tuple<string, string> toMove, int newFloor)
        {
            var newBuilding = building.Clone();
            newBuilding.Steps = building.Steps + 1;
            var currentFloor = newBuilding.Floors[building.Elevator];
            currentFloor.Gens.Remove(toMove.Item1);
            currentFloor.Chips.Remove(toMove.Item1);
            currentFloor.Gens.Remove(toMove.Item2);
            currentFloor.Chips.Remove(toMove.Item2);
            var toFloor = newBuilding.Floors[newFloor];
            if(toMove.Item1 != null && toMove.Item1.EndsWith("_Gen"))
            {
                toFloor.Gens.Add(toMove.Item1);
            }
            if (toMove.Item2 != null && toMove.Item2.EndsWith("_Gen"))
            {
                toFloor.Gens.Add(toMove.Item2);
            }
            if (toMove.Item1 != null && toMove.Item1.EndsWith("_Chip"))
            {
                toFloor.Chips.Add(toMove.Item1);
            }
            if (toMove.Item2 != null && toMove.Item2.EndsWith("_Chip"))
            {
                toFloor.Chips.Add(toMove.Item2);
            }
            newBuilding.Elevator = newFloor;
            if (IsValid(toFloor) && IsValid(currentFloor))
            {

                if (visited.Contains(newBuilding.State()))
                {
                    //Console.WriteLine("already visited:");
                    //Console.WriteLine(building);
                    //Console.WriteLine(toMove);
                    //Console.WriteLine(newFloor);
                    //Console.WriteLine(newBuilding);
                    return null;
                }
                return newBuilding;
            }
            //Console.WriteLine("Can't move:");
            //Console.WriteLine(building);
            //Console.WriteLine(toMove);
            //Console.WriteLine(newFloor);
            //Console.WriteLine(newBuilding);
            return null;

        }

        private List<Tuple<string,string>> GetValidGroups(Floor floor)
        {
            var validGroups = new List<Tuple<string, string>>();
            validGroups.AddRange(floor.AllItems.SelectMany(x => floor.AllItems, (x, y) => Tuple.Create(x, y)).Where(tuple => String.Compare(tuple.Item1,tuple.Item2) == -1).Where(x=>CanMoveTogether(x)));
            validGroups.AddRange(floor.AllItems.Select(x => Tuple.Create<string, string>(x, null)));
            return validGroups;

        }

        private bool CanMoveTogether(Tuple<string,string> pair)
        {
            var item1 = pair.Item1.Split('_');
            var item2 = pair.Item2.Split('_');
            return (item1.First() == item2.First()) || (item1.Last() == item2.Last());
        }

        private bool IsComplete(Building b)
        {
            var topFloor = b.Floors.Last();
            if (!IsComplete(topFloor))
            {
                return false;
            }
            var otherFloors = b.Floors.Take(3);
            if (otherFloors.Any(x => !IsEmpty(x)))
            {
                return false;
            }
            return true;
        }

        private bool IsEmpty(Floor f)
        {
            return f.Chips.Count == 0 && f.Gens.Count == 0;
        }

        private bool IsValid(Floor floor)
        {
            return floor.Chips.TrueForAll(c => floor.Gens.Contains(c.Replace("_Chip","_Gen")) || floor.Gens.Count == 0);
        }

        private bool IsComplete(Floor floor)
        {
            return IsValid(floor) && floor.Chips.Count > 0 && floor.Gens.Count > 0;
        }

        private class Building
        {
            public int Steps { get; set; }
            public List<Floor> Floors { get; set; }
            public int Elevator { get; set; }

            public string State() {
                var sb = new StringBuilder();
                int f = 0;
                foreach(var floor in Floors)
                {
                    sb.Append("floor" + f);
                    f++;
                    sb.Append(floor.SoloChips + "_" + floor.SoloGens + "_" + floor.Pairs);
                }
                sb.Append(Elevator);
                return sb.ToString();
            }


            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine("F4" + "\t\t" + String.Join("\t\t", Floors[3].AllItems) + (Elevator == 3 ? "\t\tE" : String.Empty));
                sb.AppendLine("F3" + "\t\t" + String.Join("\t\t", Floors[2].AllItems) + (Elevator == 2 ? "\t\tE" : String.Empty));
                sb.AppendLine("F2" + "\t\t" + String.Join("\t\t", Floors[1].AllItems) + (Elevator == 1 ? "\t\tE" : String.Empty));
                sb.AppendLine("F1" + "\t\t" + String.Join("\t\t", Floors[0].AllItems) + (Elevator == 0 ? "\t\tE" : String.Empty));
                return sb.ToString();
            }

            public Building Clone()
            {
                var newBuilding = new Building();
                newBuilding.Floors = new List<Floor>();
                foreach(var floor in Floors)
                {
                    newBuilding.Floors.Add(new Floor()
                    {
                        Chips = new List<string>(floor.Chips),
                        Gens = new List<string>(floor.Gens)
                    });
                }
                newBuilding.Elevator = Elevator;
                return newBuilding;
            }

            public int Goal
            {
                get
                {
                    //distance from 4 summed
                    int x = 0;
                    for (int f = 0; f < 4; f++)
                    {
                        foreach (var item in Floors[f].AllItems)
                        {
                            x += (3 - f);
                        }
                    }
                    return x;
                }
            }
        }

        private class Floor
        {
            public List<string> Gens { get; set; }
            public List<string> Chips { get; set; }

            public override string ToString()
            {
                return "Gens: " + String.Join(",", Gens) + " Chips: " + String.Join(",", Chips);
            }
            public List<string> AllItems {
                get
                {
                    var result =  new List<string>();
                    result.AddRange(Gens.OrderBy(x=> x));
                    result.AddRange(Chips.OrderBy(x=> x));
                    return result;
                }
            }
            public int Pairs {
                get
                {
                    int x = 0;
                    foreach(var gen in Gens)
                    {
                        if (Chips.Any(c => c.Contains(gen.Split('_').First())))
                        {
                            x++;
                        }
                    }
                    return x;
                }
            }
            public int SoloChips
            {
                get
                {
                    int x = 0;
                    foreach (var chip in Chips)
                    {
                        if (!Gens.Any(g => g.Contains(chip.Split('_').First())))
                        {
                            x++;
                        }
                    }
                    return x;
                }
            }
            public int SoloGens
            {
                get
                {
                    int x = 0;
                    foreach (var gen in Gens)
                    {
                        if (!Chips.Any(c => c.Contains(gen.Split('_').First())))
                        {
                            x++;
                        }
                    }
                    return x;
                }
            }
        }
    }
}
