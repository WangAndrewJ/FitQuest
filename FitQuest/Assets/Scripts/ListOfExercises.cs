using System.Collections.Generic;
using Newtonsoft.Json;

public struct Exercise
{
    public Exercise(string name, bool isCardio)
    {
        this.name = name;
        this.isCardio = isCardio;
    }

    public string name;
    public bool isCardio;
}

public static class ListOfExercises
{
    private static string exercises = "[{\"name\":\"Ab Wheel\",\"isCardio\":false}, {\"name\":\"Arnold Press\",\"isCardio\":false}, {\"name\":\"Bench Press\",\"isCardio\":false}, {\"name\":\"Bent Over Row\",\"isCardio\":false}, {\"name\":\"Bicep Curl\",\"isCardio\":false}, {\"name\":\"Box Jump\",\"isCardio\":false}, {\"name\":\"Bulgarian Split Squat\",\"isCardio\":false}, {\"name\":\"Burpee\",\"isCardio\":true}, {\"name\":\"Cable Crunch\",\"isCardio\":false}, {\"name\":\"Calf Raise\",\"isCardio\":false}, {\"name\":\"Chest Fly\",\"isCardio\":false}, {\"name\":\"Chin Up\",\"isCardio\":false}, {\"name\":\"Crunch\",\"isCardio\":false}, {\"name\":\"Cycling\",\"isCardio\":true}, {\"name\":\"Deadlift\",\"isCardio\":false}, {\"name\":\"Decline Bench Press\",\"isCardio\":false}, {\"name\":\"Dip\",\"isCardio\":false}, {\"name\":\"Dumbell Press\",\"isCardio\":false}, {\"name\":\"Front Raise\",\"isCardio\":false}, {\"name\":\"Front Squat\",\"isCardio\":false}, {\"name\":\"Hack Squat\",\"isCardio\":false}, {\"name\":\"Hammer Curl\",\"isCardio\":false}, {\"name\":\"Hanging Leg Raise\",\"isCardio\":false}, {\"name\":\"Hiking\",\"isCardio\":true}, {\"name\":\"Hip Thrust\",\"isCardio\":false}, {\"name\":\"Incline Bench Press\",\"isCardio\":false}, {\"name\":\"Incline Dumbell Press\",\"isCardio\":false}, {\"name\":\"Jump Rope\",\"isCardio\":true}, {\"name\":\"Jumping Jack\",\"isCardio\":true}, {\"name\":\"Kettlebell Swing\",\"isCardio\":false}, {\"name\":\"Lat Pulldown\",\"isCardio\":false}, {\"name\":\"Lateral Raise\",\"isCardio\":false}, {\"name\":\"Leg Extension\",\"isCardio\":false}, {\"name\":\"Leg Press\",\"isCardio\":false}, {\"name\":\"Lunge\",\"isCardio\":false}, {\"name\":\"Muscle Up\",\"isCardio\":false}, {\"name\":\"Overhead Press\",\"isCardio\":false}, {\"name\":\"Plank\",\"isCardio\":true}, {\"name\":\"Preacher Curl\",\"isCardio\":false}, {\"name\":\"Pull Up\",\"isCardio\":false}, {\"name\":\"Push Up\",\"isCardio\":false}, {\"name\":\"Reverse Fly\",\"isCardio\":false}, {\"name\":\"manian Deadlift\",\"isCardio\":false}, {\"name\":\"Rowing\",\"isCardio\":true}, {\"name\":\"Running\",\"isCardio\":true}, {\"name\":\"Russian Twist\",\"isCardio\":false}, {\"name\":\"Seated Calf Raise\",\"isCardio\":false}, {\"name\":\"Shoulder Press\",\"isCardio\":false}, {\"name\":\"Shrug\",\"isCardio\":false}, {\"name\":\"Sit Up\",\"isCardio\":false}, {\"name\":\"Skating\",\"isCardio\":true}, {\"name\":\"Skiing\",\"isCardio\":true}, {\"name\":\"Squat\",\"isCardio\":false}, {\"name\":\"Swimming\",\"isCardio\":true}, {\"name\":\"Tricep Extension\",\"isCardio\":false}, {\"name\":\"Tricep Pushdown\",\"isCardio\":false}, {\"name\":\"Walking\",\"isCardio\":true}, {\"name\":\"Yoga\",\"isCardio\":true}]";

    public static List<Exercise> Exercises()
    {
        return JsonConvert.DeserializeObject<List<Exercise>>(exercises);
    }
}
