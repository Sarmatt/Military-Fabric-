using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Tooltip("1 - Sewing machine, 2 -...")]
    public int Id;
    public Action TimerValueWasChanged;
    public string Name;
    public int NeededMoney;
    [SerializeField] private float _timeCoef = 1;
    public CreatingStaff SelectedStaff;

    [HideInInspector] public bool CanMakeStaff;
    private bool _creatingItem;

    public float Timer;

    private void Update()
    {
        if (CanMakeStaff)
        {
            if (Timer > 0)
            {
                TimerValueWasChanged?.Invoke();
                Timer -= Time.deltaTime;
            }
            else
            {
                InventoryFunctional.singleton.AddItem(SelectedStaff);
                SetTimerValue();
            }
        }
    }

    public float GetTimerValue() => SelectedStaff.TimeForCreating * _timeCoef;
    public void SetTimerValue() => Timer = GetTimerValue();
}
