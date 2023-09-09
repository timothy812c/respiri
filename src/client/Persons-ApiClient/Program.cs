using Microsoft.Extensions.Configuration;

// Build the configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var apiBaseUrl = config["ApiBaseUrl"];
if (string.IsNullOrWhiteSpace(apiBaseUrl))
{
    Console.WriteLine("ApiBaseUrl is missing in appsettings.json.");
    return;
}

var apiService = new ApiService(apiBaseUrl);

while (true)
{
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1. Get Hello World");
    Console.WriteLine("2. Get All Persons");
    Console.WriteLine("3. Get Person by ID");
    Console.WriteLine("4. Exit");

    Console.Write("\nEnter your choice (1/2/3/4): ");
    var choice = Console.ReadLine();

    try
    {    

        switch (choice)
        {
            case "1":
                Console.WriteLine("\nGET: Hello...");
                var helloResponse = await apiService.GetHelloWorld();
                Console.WriteLine(helloResponse);
                break;

            case "2":
                Console.WriteLine("\nGET: GetAllPersons API...");
                var allPersonsResponse = await apiService.GetAllPersons();
                Console.WriteLine(allPersonsResponse);
                break;

            case "3":
                Console.Write("\nEnter the Person ID: ");
                if (int.TryParse(Console.ReadLine(), out int personId))
                {
                    Console.WriteLine($"\nCalling GetPersonById API for Person ID {personId}...");
                    var personByIdResponse = await apiService.GetPersonById(personId);
                    Console.WriteLine(personByIdResponse);
                }
                else
                {
                    Console.WriteLine("\nInvalid input for Person ID.");
                }
                break;

            case "4":
                return;

            default:
                Console.WriteLine("\nInvalid choice. Please enter 1, 2, 3, or 4.");
                break;
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nAn error occurred: {ex.Message}");
    }
    finally
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
        Console.Clear();
    }
}
