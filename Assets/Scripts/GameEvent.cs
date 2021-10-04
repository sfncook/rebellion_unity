using System;
public class GameEvent
{
    readonly String eventMessage;
    readonly int gameTime;

    public GameEvent(String eventMessage, int gameTime)
    {
        this.eventMessage = eventMessage;
        this.gameTime = gameTime;
    }
}
