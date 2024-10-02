using System.Timers;

public class TimeHandler {
    private readonly List<System.Timers.Timer> timers;
    private static TimeHandler singleton = new();

    private TimeHandler() {
        timers = [];
    }

    public static TimeHandler GetInstance() {
        singleton ??= new();
        return singleton;
    }

    public void Stop() {
        throw new NotImplementedException();
    }

    public void AddDateTimeEvent(DateTime time, Action action) {
        DateTime currentTime = DateTime.Now;
        TimeSpan timeSpan = time - currentTime;
        System.Timers.Timer timer = new System.Timers.Timer(timeSpan);
        timer.Elapsed += CreateEventHandler(action);
        timer.AutoReset = false;
        timer.Enabled = true;
        timers.Add(timer);
    }

    private ElapsedEventHandler CreateEventHandler(Action action) {
        return (Object? src, ElapsedEventArgs e) => {
            action();
            CleanUpTimers();
        };
    }

    private void CleanUpTimers() {
        foreach (System.Timers.Timer timer in timers) {
            if (timer.Enabled == false) {
                timer.Dispose();
                timers.Remove(timer);
            }
        }
    }
}