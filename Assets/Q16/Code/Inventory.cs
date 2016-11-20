using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    public GameObject[] weaponObjects;
    Weapon currentWeapon;

    private Dictionary<Item, bool> itemObjects;

    int amount;
    bool[] hasWeapon;
    int amtOfSlowmo = 0;

    void Start()
    {
        SetupItems();
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

    void SetupItems()
    {
        itemObjects = new Dictionary<Item, bool>();

        itemObjects.Add(Item.SkeletonKey(), false);
        itemObjects.Add(Item.FleshKey(), false);
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

            MessageLog.Notify("Changed weapon to " + currentWeapon.name);
        }
        else
        {
            MessageLog.Notify("No weapon in slot " + index + ".");
        }
    }

    public bool CheckWeaponCD()
    {
        return currentWeapon.CheckCD();
    }

    public void SetItem(Item item, bool hasItem)
    {
        itemObjects[item] = hasItem;
    }

    public bool HasItem(Item item)
    {
        return itemObjects.ContainsKey(item);
    }

    public void SetHasWeapon(int index, bool hasWpn)
    {
        hasWeapon[index] = hasWpn;
    }

    public void SetHasAllWeapons(bool unlock)
    {
        for (int i = 0; i < hasWeapon.Length; i++)
        {
            hasWeapon[i] = unlock;
        }
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
