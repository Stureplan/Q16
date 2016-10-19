using UnityEngine;
using System.Collections;

public class ThrowGibs : MonoBehaviour
{
    Rigidbody[] rbs;

    float bForce;
    float uForce;

    public void Throw(Vector3 dir)
    {

        rbs = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < rbs.Length; i++)
        {
            bForce = Random.Range(1.0f, 7.5f);
            uForce = Random.Range(5.0f, 7.5f);

            //rbs[i].AddForce(-rbs[i].transform.forward * bForce, ForceMode.Impulse);
            rbs[i].AddForce(Utility.RandomVector3(dir, -rbs[i].transform.forward) * bForce, ForceMode.Impulse);
            rbs[i].AddForce(transform.up * uForce, ForceMode.Impulse);
            rbs[i].AddTorque(Utility.RandomVector3() * 5.0f, ForceMode.Impulse);
        }
    }
}
