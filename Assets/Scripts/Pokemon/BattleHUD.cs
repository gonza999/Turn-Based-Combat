using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Image hpBar;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = unit.unitLevel.ToString();
        hpBar.fillAmount = unit.currentHp / unit.maxHp;
    }

    public void SetHp(Unit unit)
    {
        hpBar.fillAmount = unit.currentHp / unit.maxHp;
    }

}
