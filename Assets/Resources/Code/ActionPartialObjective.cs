using UnityEngine;
using System.Collections;

public class ActionPartialObjective : ActionObject
{
    public ActionObject actionWhenFinished;
    public string finishMessage;
    public int objectivesToComplete;


    private int objectivesDone = 0;
    
    public override void Action(SenderInfo sender)
    {
        objectivesDone++;


        if (objectivesDone >= objectivesToComplete)
        {
            actionWhenFinished.Action(sender);
            MessageLog.Message(finishMessage);
        }
        else
        {
            MessageLog.Message((objectivesToComplete - objectivesDone).ToString() + " more to go");
        }
    }
}
