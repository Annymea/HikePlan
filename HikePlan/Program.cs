using System;
using System.IO;
using System.Linq;

public class HikingPlan
{
    public int days;
    public int givenPathNumber;
    public int[] givenPaths;

    public int[] plannedPaths;
    public int maxPath;

    public HikingPlan()
    {
        days = 0;
        givenPathNumber = 0;
        givenPaths = new int[0];
        plannedPaths = new int[0];
        maxPath = 0;
    }
}
public class OptimizeHikePath
{
    static void Main()
    {
        string hikingPlanFilePath = GetPathFromUser();
        if (string.IsNullOrEmpty(hikingPlanFilePath))
        {
            return; 
        }

        HikingPlan hikingPlan;
        try
        {
            hikingPlan = GetHikingPlanFromFile(hikingPlanFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            return;
        }

        hikingPlan = PlanHike(hikingPlan);
        PrintHikingPlan(hikingPlan);
    }

    static string GetPathFromUser()
    {
        Console.WriteLine("Give me the path of the file with your plan!");
        string hikingPlanFilePath = Console.ReadLine();

        int retry = 3;

        while (retry > 0)
        {
            if (string.IsNullOrEmpty(hikingPlanFilePath))
            {
                Console.WriteLine("Try again! You can do it!");
                hikingPlanFilePath = Console.ReadLine();
                retry--;
            }
            else
            {
                retry = -1;
            }
        }

        if (retry == 0)
        {
            Console.WriteLine("You betrayed me! I haven´t recieved any file path! :(");
            return "";
        }

        return hikingPlanFilePath;
    }

    static bool CanPlanBeSplit(int[] givenPlan, int numberOfDays, int maxPathLengthPerDay)
    {
        int requiredDays = 1;
        int currentPathSum = 0;

        for (int i = 0; i < givenPlan.Length; i++)
        {
            if (currentPathSum + givenPlan[i] > maxPathLengthPerDay)
            {
                requiredDays++;
                currentPathSum = givenPlan[i];
            }
            else
            {
                currentPathSum += givenPlan[i];
            }
        }

        return requiredDays <= numberOfDays;
    }

    public static HikingPlan FillHikePlan(int optimalMaxPathLength, HikingPlan hikingPlan)
    {
        if (!CheckIfHikingPlanIsValid(hikingPlan))
        {
            Console.WriteLine("There is something wrong with the plan!");
            return hikingPlan;
        }
        if(optimalMaxPathLength == 0)
        {
            Console.WriteLine("Canot fill the plan. Something went wrong when calculating!");
            return hikingPlan;
        }

        hikingPlan.maxPath = optimalMaxPathLength;
        hikingPlan.plannedPaths = new int[hikingPlan.days];
        int currentPathSum = 0;
        int dayIndex = 0;

        for (int i = 0; i < hikingPlan.givenPathNumber; i++)
        {
            if (currentPathSum + hikingPlan.givenPaths[i] > optimalMaxPathLength)
            {
                hikingPlan.plannedPaths[dayIndex++] = currentPathSum;
                currentPathSum = hikingPlan.givenPaths[i];
            }
            else
            {
                currentPathSum += hikingPlan.givenPaths[i];
            }
        }

        hikingPlan.plannedPaths[dayIndex] = currentPathSum;
        return hikingPlan;
    }

    static bool CheckIfHikingPlanIsValid(HikingPlan hikePlan)
    {
        if(hikePlan.days == 0 || hikePlan.givenPaths.Length == 0 || hikePlan.givenPathNumber == 0)
        {
            return false;
        }
        return true;
    }

    public static int FindOptimalMaxPathLength(HikingPlan hikePlan)
    {
        if (!CheckIfHikingPlanIsValid(hikePlan))
        {
            return 0;
        }
        int maxSinglePathLength = hikePlan.givenPaths.Max();
        int totalPathLength = hikePlan.givenPaths.Sum();
        int optimalMaxPathLength = 0;

        while (maxSinglePathLength <= totalPathLength)
        {
            int midPathLength = (maxSinglePathLength + totalPathLength) / 2;

            if (CanPlanBeSplit(hikePlan.givenPaths, hikePlan.days, midPathLength))
            {
                optimalMaxPathLength = midPathLength;
                totalPathLength = midPathLength - 1;
            }
            else
            {
                maxSinglePathLength = midPathLength + 1;
            }
        }

        return optimalMaxPathLength;
    }

    static HikingPlan GetHikingPlanFromFile(string planFilePath)
    {
        HikingPlan hikingPlan = new HikingPlan();

        if (string.IsNullOrEmpty(planFilePath))
        {
            throw new Exception("You betrayed me! I haven’t received any file path! :(");
        }

        using (StreamReader sr = new StreamReader(planFilePath))
        {

            if (!int.TryParse(sr.ReadLine(), out hikingPlan.givenPathNumber) || hikingPlan.givenPathNumber == 0)
            {
                throw new Exception("Something is wrong in your file. Path number is missing or zero.");
            }

            if (!int.TryParse(sr.ReadLine(), out hikingPlan.days) || hikingPlan.days == 0)
            {
                throw new Exception("Something is wrong in your file. Days are missing or zero.");
            }

            int[] tempPaths = new int[hikingPlan.givenPathNumber];
            for (int i = 0; i < hikingPlan.givenPathNumber; i++)
            {
                if (!int.TryParse(sr.ReadLine(), out tempPaths[i]) || tempPaths[i] == 0)
                {
                    throw new Exception($"Something is wrong in your file. Path {i + 1} is missing or zero.");
                }
            }

            hikingPlan.givenPaths = tempPaths;
        }

        return hikingPlan;
    }

    static void PrintHikingPlan(HikingPlan hikingPlan)
    {
        if (hikingPlan.plannedPaths == null || hikingPlan.plannedPaths.Length < 1)
        {
            Console.WriteLine("No hiking planned yet!");
            return;
        }

        Console.WriteLine("\nYour plan:\n");

        for (int i = 0; i < hikingPlan.days; i++)
        {
            Console.WriteLine($"Day {i + 1}: {hikingPlan.plannedPaths[i]} km");
        }

        Console.WriteLine($"\nMaximum: {hikingPlan.maxPath} km");
    }

    static HikingPlan PlanHike(HikingPlan hikePlan)
    {
        if (hikePlan.days == 1)
        {
            hikePlan.plannedPaths = new int[] { hikePlan.givenPaths.Sum() };
            hikePlan.maxPath = hikePlan.plannedPaths[0];
            return hikePlan;
        }

        if (hikePlan.days == hikePlan.givenPaths.Length)
        {
            hikePlan.plannedPaths = hikePlan.givenPaths;
            hikePlan.maxPath = hikePlan.plannedPaths.Max();
            return hikePlan;
        }

        if (hikePlan.days > hikePlan.givenPathNumber)
        {
            Console.WriteLine("I think you have to bring a tent. You have not planned enough stops for your trip!");
            return hikePlan;
        }

        int maxPathLength = FindOptimalMaxPathLength(hikePlan);
        hikePlan = FillHikePlan(maxPathLength, hikePlan);

        return hikePlan;
    }
}

