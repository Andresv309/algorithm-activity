using System.Drawing;
using System.Globalization;
using AlgorithmActivity.Entities;
using AlgorithmActivity.Services;
using Console = Colorful.Console;

namespace AlgorithmActivity;

public static class Program
{
    private static readonly ActivityService _activityService = new();

    public static void Main(string[] args)
    {
        SeedInitialData();

        PrintWelcomeMessage();

        while (true)
        {
            PrintMenuOptions();

            var option = Console.ReadLine();
            Console.Clear();

            switch (option)
            {
                case "1":
                    ShowAllActivities();
                    break;
                case "2":
                    AddNewActivity();
                    break;
                case "3":
                    FilterActivitiesByDate();
                    break;
                case "4":
                    RemoveActivityByDescription();
                    break;
                case "5":
                    return;
            }
        }
    }

    private static void SeedInitialData()
    {
        _activityService.AddNewActivity(
            new Activity
            {
                Name = "Ir al parque",
                Description = "Sacar al perro y jugar con él",
                ScheduledAt = new DateTime(2024, 03, 10, 16, 0, 0)
            }
        );

        _activityService.AddNewActivity(
            new Activity
            {
                Name = "Estudiar",
                Description = "Realizar ejercicios de programación en C#",
                ScheduledAt = new DateTime(2024, 03, 10, 18, 0, 0)
            }
        );

        _activityService.AddNewActivity(
            new Activity
            {
                Name = "Cocinar",
                Description = "Preparar la cena para la familia",
                ScheduledAt = new DateTime(2024, 03, 12, 20, 0, 0)
            }
        );
    }

    private static void PrintWelcomeMessage()
    {
        Console.WriteLine(
            "--------------------------------------------------------------",
            Color.BlueViolet
        );
        Console.WriteLine(
            "--------- Bienvenido a la Aplicación de Actividades ----------",
            Color.BlueViolet
        );
        Console.WriteLine(
            "--------------------------------------------------------------",
            Color.BlueViolet
        );
        Console.WriteLine();
    }

    private static void PrintMenuOptions()
    {
        Console.WriteLine(
            "--------------------------------------------------------------",
            Color.Lime
        );
        Console.WriteLine(
            "<<<<<<<<<<<<<<<<<<< Menu de Opciones >>>>>>>>>>>>>>>>>>>>>>",
            Color.Lime
        );
        Console.WriteLine(
            "--------------------------------------------------------------",
            Color.Lime
        );
        Console.WriteLine("1. Mostrar todas las actividades", Color.Lime);
        Console.WriteLine("2. Añadir una nueva actividad", Color.Lime);
        Console.WriteLine("3. Filtrar actividades por fecha", Color.Lime);
        Console.WriteLine("4. Eliminar actividad por descripción", Color.Lime);
        Console.WriteLine("5. Salir", Color.Lime);
        Console.WriteLine(
            "--------------------------------------------------------------",
            Color.Lime
        );
        Console.Write("\nSelecciona una opción: ", Color.Indigo);
    }

    private static void ShowAllActivities()
    {
        var activities = _activityService.GetAllActivities();
        Console.WriteLine("Lista de actividades:\n", Color.BlueViolet);

        foreach (var activity in activities)
        {
            PrintActivityDetails(activity);
        }
    }

    private static void AddNewActivity()
    {
        Console.WriteLine("Crea una nueva actividad\n", Color.BlueViolet);
        Console.WriteLine("Introduce el nombre de la actividad:");
        Console.Write("> ");
        var name = Console.ReadLine();
        Console.WriteLine("Introduce la descripción de la actividad:");
        Console.Write("> ");
        var description = Console.ReadLine();
        Console.WriteLine(
            "Introduce la fecha de realización de la actividad en formato dd/MM/yyyy:"
        );
        Console.Write("> ");
        var date = Console.ReadLine();
        Console.WriteLine("Introduce la hora de realización de la actividad en formato HH:mm:");
        Console.Write("> ");
        var time = Console.ReadLine();

        if (
            !DateTime.TryParseExact(
                $"{date} {time}",
                "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var scheduledAt
            )
        )
        {
            Console.WriteLine(
                "\nFormato de fecha u hora inválida\nOperación Cancelada\n",
                Color.Red
            );
            return;
        }

        _activityService.AddNewActivity(
            new Activity
            {
                Name = $"{name}",
                Description = $"{description}",
                ScheduledAt = scheduledAt
            }
        );

        Console.WriteLine("\nActividad creada con éxito\n", Color.Blue);
    }

    private static void FilterActivitiesByDate()
    {
        Console.WriteLine("Lista de actividades por fecha\n", Color.BlueViolet);
        Console.WriteLine("Introduce la fecha para filtrar las actividades en formato dd/MM/yyyy:");
        Console.Write("> ");

        if (
            !DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date
            )
        )
        {
            Console.WriteLine("\nFecha inválida\nOperación Cancelada\n", Color.Red);
            return;
        }
        var filteredActivities = _activityService.FilterActivitiesByDate(date);

        if (filteredActivities.Count == 0)
        {
            Console.WriteLine("\nNo hay actividades para la fecha seleccionada\n", Color.Blue);
        }
        else
        {
            Console.WriteLine(
                $"\nLista de actividades para el {date:dd/MM/yyyy}:\n",
                Color.BlueViolet
            );

            foreach (var activity in filteredActivities)
            {
                PrintActivityDetails(activity);
            }
        }
    }

    private static void RemoveActivityByDescription()
    {
        Console.WriteLine("Eliminar actividad\n", Color.BlueViolet);
        Console.WriteLine("Introduce la descripción de la actividad a eliminar");
        Console.Write("> ");
        var descriptionToRemove = Console.ReadLine();

        if (_activityService.RemoveActivityByDescription(descriptionToRemove))
        {
            Console.WriteLine("\nActividad eliminada con éxito\n", Color.Blue);
        }
        else
        {
            Console.WriteLine(
                "\nNo se encontró ninguna actividad con la descripción proporcionada\n",
                Color.Red
            );
        }
    }

    private static void PrintActivityDetails(Activity activity)
    {
        Console.WriteLine(
            $"Nombre: {activity.Name}\nDescripción: {activity.Description}\nFecha: {activity.ScheduledAt}\n"
        );
    }
}
