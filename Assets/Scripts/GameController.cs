using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    public GameObject battleMenu;

    public Text battleText;

    void Start()
    {
        fighterStats = new List<FighterStats>();
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        FighterStats currentHeroStats = hero.GetComponent<FighterStats>();
        currentHeroStats.CalculateNextTurn(0);
        fighterStats.Add(currentHeroStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        fighterStats.Sort();

        NextTurn();
    }

    public void NextTurn()
    {
        battleText.gameObject.SetActive(false);
        this.battleMenu.SetActive(false);
        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);
        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();

            if(currentUnit.tag == "Hero")
            {
                this.battleMenu.SetActive(true);
            } else
            {
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "ranged";
                currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
            }
        } else
        {
            NextTurn();
        }
    }
}
