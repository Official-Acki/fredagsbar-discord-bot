public class TimeHandler {
    
    DateTime dt = DateTime.Now;
    DateTime startTime;
    DateTime endTime;
    DateTime time;

    public TimeHandler (DateTime time) {
        this.time = time;
    }


    public Boolean CheckDate(DateTime time) {
        if (time.DayOfWeek == dt.DayOfWeek) {
            return true;
        } else {
            return false;
        }
    }
}

public enum TimeHandlerStatus
{
    BeforeEvent,
    DuringEvent,
    AfterEvent
}