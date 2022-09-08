using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleSystem : MonoBehaviour
{
    public BattleState battleState;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Text dialogueText;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    bool isPlayerTurn = false;

    public Image MenuAtacks;

    private void Start()
    {
        battleState = BattleState.START;
        StartCoroutine(SettupBattle());
    }

    IEnumerator SettupBattle()
    {
        Instantiate(playerPrefab);
        Instantiate(enemyPrefab);
        playerUnit = playerPrefab.GetComponent<Unit>();
        enemyUnit = enemyPrefab.GetComponent<Unit>();
        dialogueText.text = $"{enemyUnit.unitName} te esta provocando...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        battleState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        dialogueText.text = $"Elije un opción";
        isPlayerTurn = true;
    }

    public void OnFightButton()
    {
        if (battleState != BattleState.PLAYERTURN)
        {
            return;
        }
        if (isPlayerTurn)
        {
            ListAtacks();
        }
    }
    public void ListAtacks()
    {
        MenuAtacks.transform.gameObject.SetActive(true);
        if (playerUnit.atacks.Count > 0)
        {
            for (int i = 0; (i < 4); i++)
            {
                MenuAtacks.gameObject.transform.GetChild(i)
                                        .GetChild(0).GetComponent<Text>().text = playerUnit.atacks[i].nameAtack;

            }
        }
    }

    bool selecctionAtack = true;
    int saveIndex;
    public void OnAttackButton(int index)
    {
        if (saveIndex != index)
        {
            saveIndex = index;
            selecctionAtack = true;
        }
        Atack atack = playerUnit.atacks[index];
        if (selecctionAtack)
        {
            MenuAtacks.gameObject.transform.GetChild(4).GetComponent<Text>().text = atack.maxPP.ToString();
            MenuAtacks.gameObject.transform.GetChild(5).GetComponent<Text>().text = atack.currentePP.ToString();
            MenuAtacks.gameObject.transform.GetChild(6).GetComponent<Text>().text = atack.type;
            selecctionAtack = false;
        }
        else
        {
            selecctionAtack = true;
            if (atack.currentePP > 0)
            {
                MenuAtacks.gameObject.transform.GetChild(4).GetComponent<Text>().text = "";
                MenuAtacks.gameObject.transform.GetChild(5).GetComponent<Text>().text = "";
                MenuAtacks.gameObject.transform.GetChild(6).GetComponent<Text>().text = "";
                atack.currentePP--;
                MenuAtacks.transform.gameObject.SetActive(false);
                StartCoroutine(PlayerAttack(atack.damage));
            }
        }
    }

    IEnumerator PlayerAttack(float damage)
    {
        bool isDead = enemyUnit.TakeDamage(damage);

        enemyHUD.SetHp(enemyUnit);

        dialogueText.text = $"El ataque dio en el blanco!!";

        isPlayerTurn = false;

        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            battleState = BattleState.WON;
            EndBattle();
        }
        else
        {
            battleState = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator EnemyTurn()
    {
        dialogueText.text = $"{enemyUnit.unitName} te ha atacado";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(playerUnit.damage);

        playerHUD.SetHp(playerUnit);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            battleState = BattleState.LOST;
            EndBattle();
        }
        else
        {
            battleState = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if (battleState == BattleState.WON)
        {
            dialogueText.text = $"Ganaste la batalla!!";

        }
        else if (battleState == BattleState.LOST)
        {
            dialogueText.text = $"Perdiste la batalla!!";
        }
    }
}
