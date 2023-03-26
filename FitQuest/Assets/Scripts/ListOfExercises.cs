using System.Collections.Generic;
using Newtonsoft.Json;

public static class ListOfExercises
{
    public static string exercises = "[{\"goalAmount\":4,\"questName\":\"one\",\"xpAmount\":5,\"sliderValue\":0.0,\"isDisabled\":false},{\"goalAmount\":4,\"questName\":\"two\",\"xpAmount\":5,\"sliderValue\":0.0,\"isDisabled\":false},{\"goalAmount\":4,\"questName\":\"three\",\"xpAmount\":5,\"sliderValue\":0.0,\"isDisabled\":false}]";

    public static List<DailyQuest> ListOfQuests()
    {
        return JsonConvert.DeserializeObject<List<DailyQuest>>(exercises);
    }
}
