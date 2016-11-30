using UnityEngine;
using System.Collections;

public class ActionAddItem : ActionObject
{
    public string item;

    public override void Action(SenderInfo sender)
    {
        Inventory inv = sender.s_Transform.GetComponent<Inventory>();
        inv.SetItem(Item.Get(item), true);
    }

}
