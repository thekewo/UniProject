using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physical;

    private GameObject hero;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallBack(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallBack(string btn)
    {
        if (btn.CompareTo("MeleeButton") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("melee");
        } else if (btn.CompareTo("RangedButton") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("ranged");
        } else
        {
            hero.GetComponent<FighterAction>().SelectAttack("run");
        }
    }
}
