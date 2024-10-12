using Newtonsoft.Json;

class Task {
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }

    public Task(string name, string description, bool completed) {
        Name = name;
        Description = description;
        Completed = completed;
    }
}

class TaskManager {
    private readonly List<Task> tasks;

    public TaskManager() {
        tasks = new List<Task>();
    }

    public void AddTask(string name, string description) {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description)) {
            Console.WriteLine("Name and description cannot be empty.");
            return;
        }

        Task newTask = new Task(name, description, false);
        tasks.Add(newTask);
    }

    public void ViewTasks() {
        if (tasks.Count == 0) {
            Console.WriteLine("No tasks found.");
            return;
        }
        
        Console.WriteLine("Tasks:");
        foreach (Task task in tasks) {
            Console.WriteLine($"Name: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Completed: {task.Completed}");
        }
    }

    public void markAsCompleted(string name) {
        foreach (Task task in tasks) {
            if (task.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) {
                task.Completed = true;
                Console.WriteLine("Task marked as completed: " + task.Name);
                return;
            }
        }

        Console.WriteLine("Task not found: " + name);
    }

    public void SaveTasksToJson() {
        Console.WriteLine("Saving tasks in JSON File...");
        string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText("tasks.json", json);
        Console.WriteLine("Tasks saved to tasks.json");
    }
}

class Program {
    static void Main() {
        TaskManager taskManager = new TaskManager();

        string choice;

        do {
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Save Tasks to JSON File");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine()!;

            switch (choice) {
                case "1":
                    Console.Write("Enter task name: ");
                    string name = Console.ReadLine()!;
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine()!;
                    taskManager.AddTask(name, description);
                    break;
                case "2":
                    taskManager.ViewTasks();
                    break;
                case "3":
                    Console.Write("Enter task name to mark as completed: ");
                    string taskName = Console.ReadLine()!;
                    taskManager.markAsCompleted(taskName);
                    break;
                case "4":
                    taskManager.SaveTasksToJson();
                    Console.WriteLine("Saving tasks in JSON File.");
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        } while (choice != "5");
    }
}