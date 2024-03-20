using AlgorithmActivity.Entities;

namespace AlgorithmActivity.Services;

public class ActivityService
{
    private static List<Activity> Activities = new();

    public void AddNewActivity(Activity activity)
    {
        Activities.Add(activity);
    }

    public List<Activity> GetAllActivities()
    {
        return Activities;
    }

    public List<Activity> FilterActivitiesByDate(DateTime date)
    {
        return Activities.Where(a => a.ScheduledAt.Date == date.Date).ToList();
    }
    
    public bool RemoveActivityByDescription(string? description)
    {
        var requestedActivity = Activities.Find(a => a.Description == description);

        if (requestedActivity is not null)
        {
            Activities.Remove(requestedActivity);
        }

        return requestedActivity is not null;
    }    
}