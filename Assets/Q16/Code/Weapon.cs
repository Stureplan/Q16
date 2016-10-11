using UnityEngine;
using System.Collections;

[System.Serializable]
public enum WPN_TYPE
{
    RocketLauncher,
    Railgun,
    Shotgun,
    SuperShotgun,
    LightningGun
}



public class Weapon : MonoBehaviour 
{
    public float CD;
    public int index;
    public Cooldown cooldown;
    public bool hasWeapon = false;
    protected float power;

    // SWAY
    float mZ;
    Vector3 oPos = new Vector3(0, 0, 0);
    float amt = 10.0f;
    float speed = 10.0f;



    // VIRTAUL METHODS
    public virtual void Fire(Vector3 pos, Vector3 dir) { }
    public virtual void PlayAnimation(string anim) { }

    // METHODS
    public bool CheckCD()
    {
        if (cooldown == null)
        {
            return false;
        }
        else
        {
            if (cooldown.ActionReady())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public float GetCD()
    {
        return CD;
    }

    public void EquipWeapon()
    {
        hasWeapon = true;
    }

    public bool HasWeapon()
    {
        return hasWeapon;
    }
}
