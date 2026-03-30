using SOA3.Domain;
using SOA3.DomainServices;

namespace SOA3.ConsoleApplication
{
    public class ProjectRenders
    {
        public static void LoadMainMenu()
        {
            HelperMethods.RenderNewMenu("Main Menu");
            Console.WriteLine("[1] = Create Project");
            Console.WriteLine("[2] = Open Project");
            Console.WriteLine("[3] = Demo");
            Console.WriteLine("[exit] = Exit the program");
            HelperMethods.RenderSeperator();

            string input = Console.ReadLine();
            switch (input)
            {
                case "1": // Create Project
                    CreateProject();
                    break;
                case "2": // Open  a project
                    OpenProject();
                    break;
                case "3": // Run Demo
                    Demo();
                    break;
                case "exit":
                    HelperMethods.ExitProgram();
                    break;
                default: // Faulty input
                    Console.WriteLine("reloading main menu");
                    LoadMainMenu();
                    break;
            }
        }

        public static void CreateProject()
        {
            HelperMethods.RenderNewMenu("Create Project");
            Project? project = null;

            string projectName = HelperMethods.RequireInput("Project Name (\"-\" for default)");
            if (projectName == "-")
            {
                // Create a default project
                project = ProjectService.CreateProject("Default Project Name", "Default description", DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(1).AddYears(1), "Renze Westerink", "This should definitely be done");
            } else
            {
                string description = HelperMethods.RequireInput("Project Description");

                DateTime startDate;
                while (true)
                {
                    if (DateTime.TryParse(HelperMethods.RequireInput("Project Start Date"), out startDate)) break;
                    else
                    {
                        Console.WriteLine("Error: Faulty input");
                        CreateProject();
                    }
                }

                DateTime endDate;
                while (true)
                {
                    if (DateTime.TryParse(HelperMethods.RequireInput("Project End Date"), out endDate)) break;
                    else
                    {
                        Console.WriteLine("Error: Faulty input");
                        CreateProject();
                    }
                }

                string productOwner =   HelperMethods.RequireInput("Product Owner"); // Load possible 
                string definitionOfDone = HelperMethods.RequireInput("Project Definition of Done");

                project = ProjectService.CreateProject(projectName, description, startDate, endDate, productOwner, definitionOfDone);
            }
            OpenProject();
        }
        public static void OpenProject()
        {
            // Check if there is a project
            Project? project = ProjectService.GetProject();

            // There is no project
            if (project == null)
            {
                Console.WriteLine("Create a project first");
                LoadMainMenu();
            }
            else { 
                // Open the project
                HelperMethods.RenderNewMenu("Project Menu");
                Console.WriteLine($"Project Information\nName: {project.Name}\nDescription: {project.Description}\nStart Date: {project.StartDate.ToShortDateString()}\nEnd Date: {project.EndDate.ToShortDateString()}\nProduct Owner: {project.ProductOwner.Name}\nDefinition of Done: {project.DefinitionOfDone}\nItems in Backlog: ---");
                HelperMethods.RenderSeperator();
                Console.WriteLine("[1] = Create Sprint");
                Console.WriteLine("[2] = Open Sprint");
                Console.WriteLine("[3] = View Closed Sprints");
                Console.WriteLine("[4] = View Product Backlog");
                HelperMethods.RenderSeperator();
                string input = Console.ReadLine();

                switch (input) {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    default:
                        OpenProject();
                        break;
                }
            }
        }

        public static void Demo()
        {
            HelperMethods.RenderNewMenu();
            // Setup a default project
        }
    }
}
