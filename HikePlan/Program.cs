class HikingPlan
{
    private int days;
    private int givenPathNumber;
    private int[] givenPaths;

    public int[] plannedPaths;
    public int maxPath;

    public void GetHikingPlanFromFile(string? planFilePath)
    {
        if (planFilePath == null || planFilePath == "")
        {
            Console.WriteLine("You betrayed me! I haven´t recieved any file path! :(");
            return;
        }
        StreamReader sr = new StreamReader(planFilePath);

        if (!int.TryParse(sr.ReadLine(), out this.givenPathNumber) || this.givenPathNumber == 0)
        {
            throw new Exception("Something is wrong in your file. Path number is missing or zero. o.O");
        }

        if (!int.TryParse(sr.ReadLine(), out this.days) || this.days == 0)
        {
            throw new Exception("Something is wrong in your file. Days are missing or zero. o.O");
        }

        int[] tempPaths = new int[this.givenPathNumber];
        for (int i = 0; i < this.givenPathNumber; i++)
        {
            if (!int.TryParse(sr.ReadLine(), out tempPaths[i]) || tempPaths[i] == 0)
            {
                throw new Exception($"Something is wrong in your file. Path {i + 1} is missing or zero. o.O");
            }
        }

        this.givenPaths = tempPaths;

        return;
    }

    public void PrintHikingPlan()
    {
        if (this.plannedPaths == null || this.plannedPaths.Length < 1)
        {
            Console.WriteLine("No hiking planned yet!");
            return;
        }

        Console.WriteLine("\nYour plan:\n");

        for (int i = 0; i < this.days; i++)
        {
            Console.WriteLine($"Day {i + 1}: {this.plannedPaths[i]} km");
        }

        Console.WriteLine($"\nMaximum: {this.maxPath} km");
    }

    public void PlanHike()
    {
        if (days == 1)
        {
            this.plannedPaths = [givenPaths.Sum()];
            this.maxPath = this.plannedPaths[0];
            return;
        }

        if (days == this.givenPaths.Length)
        {
            this.plannedPaths = this.givenPaths;
            this.maxPath = this.plannedPaths.Max();
            return;
        }

        if (this.days > this.givenPathNumber)
        {
            Console.WriteLine("I think you have to bring a tent. You have not planned enough stops for your trip!");
            return;
        }

        int maxPathLength = findOptimalMaxPathLength();
        this.fillHikePlan(maxPathLength);
    }

    private int findOptimalMaxPathLength()
    {
        int maxSinglePathLength = givenPaths.Max();
        int totalPathLength = givenPaths.Sum();
        int optimalMaxPathLength = 0;

        while (maxSinglePathLength <= totalPathLength)
        {
            int midPathLength = (maxSinglePathLength + totalPathLength) / 2;

            if (this.canPlanBeSplit(this.givenPaths, this.days, midPathLength))
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

    private void fillHikePlan(int optimalMaxPathLength)
    {
        this.maxPath = optimalMaxPathLength;
        this.plannedPaths = new int[this.days];
        int currentPathSum = 0;
        int dayIndex = 0;

        for (int i = 0; i < this.givenPathNumber; i++)
        {
            if (currentPathSum + this.givenPaths[i] > optimalMaxPathLength)
            {
                this.plannedPaths[dayIndex++] = currentPathSum;
                currentPathSum = this.givenPaths[i];
            }
            else
            {
                currentPathSum += this.givenPaths[i];
            }
        }

        this.plannedPaths[dayIndex] = currentPathSum;
    }

    private bool canPlanBeSplit(int[] givenPlan, int numberOfDays, int maxPathLengthPerDay)
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
}

class HikingPath
{
    static void Main()
    {
        Console.WriteLine("Give me the path of the file with your plan!");
        string? hikingPlanFilePath = Console.ReadLine();

        HikingPlan hikingPlan = new HikingPlan();
        try
        {
            hikingPlan.GetHikingPlanFromFile(hikingPlanFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            return;
        }

        hikingPlan.PlanHike();
        hikingPlan.PrintHikingPlan();
    }
}
