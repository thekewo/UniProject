﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject magicFill;

    [Header("Stats")]
    public float health;
    public float magic;
    public float attack;
    public float defense;
    public float intelligence;
    public float speed;
    public float block;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    //Resize health and magic bar
    private Transform healthTransform;
    private Transform magicTransform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;

    private GameObject gameControllerObj;

    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        magicTransform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;

        startHealth = health;
        startMagic = magic;

        gameControllerObj = GameObject.Find("GameControllerObject");
    }

    internal void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        animator.Play("damage");

        //Set damage text

        if(health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
        } else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }

        gameControllerObj.GetComponent<GameController>().battleText.gameObject.SetActive(true);
        gameControllerObj.GetComponent<GameController>().battleText.text = damage.ToString();
        gameControllerObj.GetComponent<GameController>().battleMenu.SetActive(false);

        Invoke("ContinueGame", 2);
    }

    public void UpdateMagicFill(float cost)
    {
        if(cost > 0)
        {
            magic -= cost;
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
        }
    }

    public int CompareTo(object otherStats)
    {
        int next = nextActTurn.CompareTo(
            ((FighterStats)otherStats).nextActTurn);
        return next;
    }

    public bool GetDead()
    {
        return dead;
    }

    void ContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }
}
