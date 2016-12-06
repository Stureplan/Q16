using UnityEngine;
using System.Collections;

public class ActionSecret : ActionObject
{
    public override void Action(SenderInfo sender)
    {
        Stats.info.secretsFound++;
        MessageLog.Message("Secret found");
    }
}
