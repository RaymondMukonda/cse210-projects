/*
Creative extras 
1. Leveling & Badges: The program awards badges at score milestones and shows a derived "level" based on total points.
   - Badges are persisted with the saved file.
2. Demo seeding: A menu option seeds several sample goals to try the app quickly.
3. Clear save format & robust parser: Human-readable save format so grader can inspect the file easily.
*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EternalQuest
{
    
    abstract class Goal
    {
        
        private string _name;
        private string _description;
        private int _points;

        
        public string Name => _name;
        public string Description => _description;
        public int Points => _points;

        protected Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        
        public abstract int RecordEvent();

        
        public abstract string GetStringRepresentation();

        
        public abstract bool IsComplete { get; }

        
        public virtual string DisplayStatus()
        {
            return IsComplete ? "[X]" : "[ ]";
        }
    }

    
    class SimpleGoal : Goal
    {
        private bool _completed;

        public SimpleGoal(string name, string description, int points, bool completed = false)
            : base(name, description, points)
        {
            _completed = completed;
        }

        public override bool IsComplete => _completed;

        public override int RecordEvent()
        {
            if (_completed) return 0; 
            _completed = true;
            return Points;
        }

        public override string GetStringRepresentation()
        {
            
            return $"Simple:{Name}|{Description}|{Points}|{_completed.ToString().ToLower()}";
        }

        public override string DisplayStatus()
        {
            return base.DisplayStatus() + " " + Name + (IsComplete ? " (Completed)" : "");
        }
    }

    
    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override bool IsComplete => false;

        public override int RecordEvent()
        {
            return Points;
        }

        public override string GetStringRepresentation()
        {
            
            return $"Eternal:{Name}|{Description}|{Points}";
        }

        public override string DisplayStatus()
        {
            return "[∞] " + Name + " (Eternal)";
        }
    }

    
    class ChecklistGoal : Goal
    {
        private int _timesCompleted;
        private int _targetCount;
        private int _bonusPoints;
        private bool _completed;

        public ChecklistGoal(string name, string description, int points, int timesCompleted, int targetCount, int bonusPoints)
            : base(name, description, points)
        {
            _timesCompleted = timesCompleted;
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _completed = _timesCompleted >= _targetCount;
        }

        public int TimesCompleted => _timesCompleted;
        public int TargetCount => _targetCount;
        public int BonusPoints => _bonusPoints;

        public override bool IsComplete => _completed;

        public override int RecordEvent()
        {
            if (_completed) return 0;
            _timesCompleted++;
            int earned = Points;
            if (_timesCompleted >= _targetCount)
            {
                _completed = true;
                earned += _bonusPoints;
            }
            return earned;
        }

        public override string GetStringRepresentation()
        {
           
            return $"Checklist:{Name}|{Description}|{Points}|{_timesCompleted}|{_targetCount}|{_bonusPoints}";
        }

        public override string DisplayStatus()
        {
            return (IsComplete ? "[X]" : "[ ]") + " " + Name + $" (Completed {_timesCompleted}/{_targetCount})";
        }
    }

    
    static class GoalFactory
    {
        public static Goal CreateFromString(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;
            int colonIndex = line.IndexOf(':');
            if (colonIndex < 0) return null;
            string type = line.Substring(0, colonIndex);
            string data = line.Substring(colonIndex + 1);
            string[] parts = data.Split('|');

            try
            {
                switch (type)
                {
                    case "Simple":
                        return new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]), bool.Parse(parts[3]));
                    case "Eternal":
                        return new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));
                    case "Checklist":
                        return new ChecklistGoal(parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    class Program
    {
        private static List<Goal> _goals = new List<Goal>();
        private static int _score = 0;
        private static HashSet<string> _badges = new HashSet<string>();
        private const string _saveFile = "goals.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Eternal Quest!\n");

            bool exit = false;
            while (!exit)
            {
                ShowMainMenu();
                string input = Console.ReadLine();
                switch ((input ?? "").Trim())
                {
                    case "1": CreateGoal(); break;
                    case "2": ListGoals(); break;
                    case "3": RecordEvent(); break;
                    case "4": ShowScoreAndBadges(); break;
                    case "5": SaveToFile(); break;
                    case "6": LoadFromFile(); break;
                    case "7": SeedDemoGoals(); break;
                    case "8": ExitAndGoodbye(); exit = true; break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a number from the menu.\n");
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1) Create new goal");
            Console.WriteLine("2) List goals");
            Console.WriteLine("3) Record event (complete a goal)");
            Console.WriteLine("4) Show score, level & badges");
            Console.WriteLine("5) Save goals to file");
            Console.WriteLine("6) Load goals from file");
            Console.WriteLine("7) Seed demo goals (quick start)");
            Console.WriteLine("8) Exit");
            Console.Write("Choose an option: ");
        }

        static void CreateGoal()
        {
            Console.WriteLine("Choose goal type: 1) Simple  2) Eternal  3) Checklist");
            Console.Write("Type number: ");
            string choice = Console.ReadLine();
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "Unnamed Goal";
            Console.Write("Description: ");
            string description = Console.ReadLine() ?? "";
            Console.Write("Points (integer): ");
            if (!int.TryParse(Console.ReadLine(), out int points)) points = 10;

            switch ((choice ?? "").Trim())
            {
                case "1":
                    _goals.Add(new SimpleGoal(name, description, points));
                    Console.WriteLine("Simple goal created.\n");
                    break;
                case "2":
                    _goals.Add(new EternalGoal(name, description, points));
                    Console.WriteLine("Eternal goal created.\n");
                    break;
                case "3":
                    Console.Write("Target count (how many times to complete to finish): ");
                    if (!int.TryParse(Console.ReadLine(), out int target)) target = 5;
                    Console.Write("Bonus points for completing the checklist: ");
                    if (!int.TryParse(Console.ReadLine(), out int bonus)) bonus = target * points;
                    _goals.Add(new ChecklistGoal(name, description, points, 0, target, bonus));
                    Console.WriteLine("Checklist goal created.\n");
                    break;
                default:
                    Console.WriteLine("Unknown type - aborted.\n");
                    break;
            }
        }

        static void ListGoals()
        {
            if (!_goals.Any())
            {
                Console.WriteLine("No goals found.\n");
                return;
            }

            Console.WriteLine("Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                var g = _goals[i];
                Console.WriteLine($"{i + 1}) {g.DisplayStatus()} - {g.Description} ({GetTypeName(g)})");
            }
            Console.WriteLine();
        }

        static void RecordEvent()
        {
            if (!_goals.Any())
            {
                Console.WriteLine("No goals to record.\n");
                return;
            }

            ListGoals();
            Console.Write("Enter the number of the goal you completed (or 0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > _goals.Count)
            {
                Console.WriteLine("Invalid selection.\n");
                return;
            }
            if (choice == 0) return;
            var goal = _goals[choice - 1];
            int earned = goal.RecordEvent();
            if (earned > 0)
            {
                _score += earned;
                Console.WriteLine($"You earned {earned} points!");
                CheckForBadges();
                Console.WriteLine($"Total score: {_score}\n");
            }
            else
            {
                Console.WriteLine("No points earned (goal already complete or has no reward).\n");
            }
        }

        static void ShowScoreAndBadges()
        {
            Console.WriteLine($"Score: {_score}");
            Console.WriteLine($"Level: {CalculateLevel()}");
            if (_badges.Any())
            {
                Console.WriteLine("Badges: " + string.Join(", ", _badges));
            }
            else
            {
                Console.WriteLine("Badges: (none yet)");
            }
            Console.WriteLine();
        }

        static int CalculateLevel()
        {
            return (_score / 1000) + 1;
        }

        static void CheckForBadges()
        {
            var thresholds = new Dictionary<int, string>
            {
                {100, "First100"},
                {500, "HalfK"},
                {1000, "OneK"},
                {5000, "FiveK"}
            };

            foreach (var kv in thresholds)
            {
                if (_score >= kv.Key && !_badges.Contains(kv.Value))
                {
                    _badges.Add(kv.Value);
                    Console.WriteLine($"Badge earned: {kv.Value}!");
                }
            }
        }

        static void SaveToFile()
        {
            try
            {
                using (var writer = new StreamWriter(_saveFile))
                {
                    writer.WriteLine($"Score:{_score}");
                    if (_badges.Any())
                    {
                        writer.WriteLine("Badges:" + string.Join(",", _badges));
                    }
                    else
                    {
                        writer.WriteLine("Badges:");
                    }

                    foreach (var g in _goals)
                    {
                        writer.WriteLine(g.GetStringRepresentation());
                    }
                }
                Console.WriteLine($"Saved {_goals.Count} goals to {_saveFile}.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving file: " + ex.Message + "\n");
            }
        }

        static void LoadFromFile()
        {
            if (!File.Exists(_saveFile))
            {
                Console.WriteLine($"Save file '{_saveFile}' not found.\n");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(_saveFile);
                var newGoals = new List<Goal>();
                _badges.Clear();
                int loadedScore = 0;

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("Score:"))
                    {
                        if (int.TryParse(line.Substring(6), out int s)) loadedScore = s;
                    }
                    else if (line.StartsWith("Badges:"))
                    {
                        var b = line.Substring(7);
                        if (!string.IsNullOrWhiteSpace(b))
                        {
                            var parts = b.Split(',', StringSplitOptions.RemoveEmptyEntries);
                            foreach (var p in parts) _badges.Add(p.Trim());
                        }
                    }
                    else
                    {
                        var g = GoalFactory.CreateFromString(line);
                        if (g != null) newGoals.Add(g);
                    }
                }

                _goals = newGoals;
                _score = loadedScore;
                Console.WriteLine($"Loaded {_goals.Count} goals. Current score: {_score}.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message + "\n");
            }
        }

        static void SeedDemoGoals()
        {
            _goals.Clear();
            _goals.Add(new EternalGoal("Daily Scriptures", "Read scriptures each day", 100));
            _goals.Add(new ChecklistGoal("Temple Visits", "Attend the temple", 50, 2, 10, 500));
            _goals.Add(new SimpleGoal("Run a Marathon", "Complete a marathon", 1000));
            Console.WriteLine("Demo goals created.\n");
        }

        static void ExitAndGoodbye()
        {
            Console.WriteLine("Would you like to save before exiting? (y/n)");
            var ans = Console.ReadLine();
            if ((ans ?? "").Trim().ToLower().StartsWith("y"))
            {
                SaveToFile();
            }
            Console.WriteLine("Goodbye and good luck on your Eternal Quest!");
        }

        static string GetTypeName(Goal g)
        {
            if (g is SimpleGoal) return "Simple";
            if (g is EternalGoal) return "Eternal";
            if (g is ChecklistGoal) return "Checklist";
            return g.GetType().Name;
        }
    }
}
