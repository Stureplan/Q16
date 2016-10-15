using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public GameObject[] weaponObjects;
    Weapon currentWeapon;

    int amount;
    bool[] hasWeapon;
    int amtOfSlowmo = 0;

    void Start()
    {

        hasWeapon = new bool[weaponObjects.Length];

        for (int i = 0; i < weaponObjects.Length; i++)
        {
            SetHasWeapon(i, true);
            weaponObjects[i].GetComponent<Weapon>().index = i;
        }

        // START WITH SG ACTIVE
        SetHasWeapon(0, true);     
        weaponObjects[0].SetActive(true);

        currentWeapon = weaponObjects[0].GetComponent<Weapon>();
    }

    public void FireWeapon(Vector3 pos, Vector3 dir)
    {
        currentWeapon.Fire(pos, dir);
    }

    public void ChangeWeapon(int index)
    {
        if (currentWeapon.index == index)
        {
            return;
        }

        if (HasWeapon(index))
        {
            weaponObjects[currentWeapon.index].SetActive(false);
            weaponObjects[index].SetActive(true);

            currentWeapon = weaponObjects[index].GetComponent<Weapon>();

            currentWeapon.PlayAnimation("WeaponSwitch");

            MessageLog.AddMessage("Changed weapon to " + currentWeapon.name);
        }
        else
        {
            MessageLog.AddMessage("No weapon in slot " + index + ".");
        }
    }

    public bool CheckWeaponCD()
    {
        return currentWeapon.CheckCD();
    }

    public void SetHasWeapon(int index, bool hasWpn)
    {
        hasWeapon[index] = hasWpn;
    }

    bool HasWeapon(int index)
    {
        if (index > hasWeapon.Length-1)
        {
            return false;
        }
        else
        {
            return hasWeapon[index];
        }
    }

    public void IncreaseDecreaseSlowmo(int amt)
    {
        amtOfSlowmo += amt;
    }

    public int GetAmtOfSlowmo()
    {
        return amtOfSlowmo;
    }
}
