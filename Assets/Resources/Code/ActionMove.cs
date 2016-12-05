using UnityEngine;
using System.Collections;

public class ActionMove : ActionObject
{
    public Move move;


    public override void Action(SenderInfo sender)
    {
        Move();
    }



    void Move()
    {
        StartCoroutine(IEMove(move.transformToMove.position + move.toPosition, move.duration));
    }

    IEnumerator IEMove(Vector3 to, float time)
    {
        float t = 0.0f;
        float d = Vector3.Distance(move.transformToMove.position, to);

        while (t < time)
        {
            float delta = (d * Time.deltaTime) / time;

            move.transformToMove.position = Vector3.MoveTowards(move.transformToMove.position, to, delta);
            t += Time.deltaTime;

            yield return null;
        }

        //done
    }
}
