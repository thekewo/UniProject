using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField]
    private GameObject meleePrefab;

    [SerializeField]
    private GameObject rangePrefab;

    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAttack;

    void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if (CompareTag("Hero"))
        {
            victim = enemy;
        }
        if (btn.CompareTo("melee") == 0)
        {
            meleePrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log($"Melee Attack to {victim}");
        }else if (btn.CompareTo("ranged") == 0)
        {
            rangePrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log($"Ranged Attack to {victim}");
        }
    }
}
