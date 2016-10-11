using UnityEngine;
using System.Collections;

public class SwordBehaviour : MonoBehaviour
{
    public Sword sword;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            sword.DamageTarget(other);
        }
    }
}
