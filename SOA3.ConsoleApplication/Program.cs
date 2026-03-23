Console.WriteLine("Launching Console");

LoadMainMenu();

static void LoadMainMenu()
{
    Console.WriteLine("================ Avans Devops Systeem (Main Menu) ================");
    Console.WriteLine("[1] = Create Project");
    Console.WriteLine("[2] = Select Project");
    Console.WriteLine("[3] = Demo");
    Console.WriteLine("[exit] = Exit the program");
    Console.WriteLine("==================================================================");

    string input = Console.ReadLine();
    switch (input)
    {
        case "1": // Create Project
            CreateProject();
            break;
        case "2": // Select a project
            SelectProject(); 
            break;
        case "3": // Run Demo
            Demo();
            break;
        case "exit":
            ExitProgram();
            break;
        default: // Faulty input
            Console.WriteLine("reloading main menu");
            LoadMainMenu();
            break;
    }
}

static void CreateProject()
{
    RenderNewMenu();
    Console.WriteLine("================ Avans Devops Systeem (Create Project) ================");
    Console.WriteLine("[exit] = Exit the program");

    string projectName = RequireInput("Project Name");
    string description = RequireInput("Project Description");

    DateTime startDate;
    while (true)
    {
        if (DateTime.TryParse(RequireInput("Project Start Date"), out startDate)) break;
        else Console.WriteLine("Error: Faulty input");
    }

    DateTime endDate;
    while (true)
    {
        if (DateTime.TryParse(RequireInput("Project End Date"), out endDate)) break;
        else Console.WriteLine("Error: Faulty input");
    }

    string productOwner = RequireInput("Product Owner"); // Load possible 

    // If any input is faulty reload the menu at the end
    CreateProject();
}
static void SelectProject()
{

}

static void Demo()
{

}

static void ExitProgram()
{
    // Save
}

static string RequireInput(string message)
{
    Console.WriteLine($"[please enter] {message}");
    Console.Write("[input] ");
    return Console.ReadLine();
}

static void RenderNewMenu()
{
    Console.Clear();
}