using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainStatsFunctional : MonoBehaviour
{
    public static MainStatsFunctional singleton;

    [Header("Functional")]   
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private float _experience;
    [SerializeField] private float _maxExperience;
    [Header("Displaying")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _fonExperience;

    private void Awake() => singleton = this;

    private void Start() => DisplayData();

    private void DisplayData()
    {
        float fill = _experience / _maxExperience;
        _fonExperience.fillAmount = fill;
        _nameText.text = _name;
        _levelText.text = "" + _level;
    }

    public void AddExperience(int adding)
    {
        _experience += adding;
        if (_experience >= _maxExperience) LevelUp();
        DisplayData();
    }

    public void LevelUp()
    {
        _level++;
        _experience -= _maxExperience;
        _maxExperience += (int)(_experience * 0.25f);
    }

    public string GetName() => _name;
    public void SetName(string name)
    { 
        _name = name;
        _nameText.text = _name;
    }
}
