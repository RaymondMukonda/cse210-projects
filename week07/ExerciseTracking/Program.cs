using System;
using System.Collections.Generic;

// Base class: Activity
public abstract class Activity
{
    // Encapsulated fields
    private DateTime _date;
    private int _lengthInMinutes;

    // Constructor
    public Activity(DateTime date, int lengthInMinutes)
    {
        _date = date;
        _lengthInMinutes = lengthInMinutes;
    }

    // Protected access for derived classes
    protected DateTime Date => _date;
    protected int LengthInMinutes => _lengthInMinutes;

    // Abstract methods (polymorphism through overriding)
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Virtual method (can be overridden but works fine as-is)
    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({LengthInMinutes} min) - " +
               $"Distance: {GetDistance():F1} km, Speed: {GetSpeed():F1} kph, Pace: {GetPace():F2} min per km";
    }
}

// Derived class: Running
public class Running : Activity
{
    private double _distanceKm;

    public Running(DateTime date, int lengthInMinutes, double distanceKm)
        : base(date, lengthInMinutes)
    {
        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;
    public override double GetSpeed() => (_distanceKm / LengthInMinutes) * 60;
    public override double GetPace() => LengthInMinutes / _distanceKm;
}

// Derived class: Cycling
public class Cycling : Activity
{
    private double _speedKph;

    public Cycling(DateTime date, int lengthInMinutes, double speedKph)
        : base(date, lengthInMinutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance() => (_speedKph * LengthInMinutes) / 60;
    public override double GetSpeed() => _speedKph;
    public override double GetPace() => 60 / _speedKph;
}

// Derived class: Swimming
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int lengthInMinutes, int laps)
        : base(date, lengthInMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // 1 lap = 50 meters → convert to km
        return (_laps * 50) / 1000.0;
    }

    public override double GetSpeed()
    {
        double distanceKm = GetDistance();
        return (distanceKm / LengthInMinutes) * 60;
    }

    public override double GetPace()
    {
        double distanceKm = GetDistance();
        return LengthInMinutes / distanceKm;
    }
}

// Program entry
class Program
{
    static void Main(string[] args)
    {
        // Create one of each activity type
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 4.8),
            new Cycling(new DateTime(2022, 11, 3), 45, 20),
            new Swimming(new DateTime(2022, 11, 3), 30, 40)
        };

        // Display summaries
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
