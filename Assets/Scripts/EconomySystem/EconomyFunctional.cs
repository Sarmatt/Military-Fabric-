using UnityEngine;
using TMPro;

public class EconomyFunctional : MonoBehaviour
{
    public static EconomyFunctional singleton;
    [SerializeField] private TMP_Text _moneyText;
    public float Money;

    private void Awake() => singleton = this;

    private void Start() => DisplayText();

    public void AddMoney(int money)
    {       
        Money += money;
        DisplayText();
    }

    public void MinusMoney(int money)
    {
        if (EnoughMoney(money))
        {          
            Money -= money;
            DisplayText();
        }
    }

    public bool EnoughMoney(int money)
    {
        if (Money >= money) return true;
        return false;
    }

    private void DisplayText()
    {
        string res = "";
        if(Money > 1000000)
            res += Money / 1000000 + "M";
        else if(Money > 1000)
            res += Money/1000 + "K";
        else
            res += Money;
        _moneyText.text = res;
    }
}
