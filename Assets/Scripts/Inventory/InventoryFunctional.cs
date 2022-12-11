using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFunctional : MonoBehaviour
{
    public static InventoryFunctional singleton;
    [SerializeField] private List<InventoryCell> _cells = new List<InventoryCell>();

    private void Awake() => singleton = this;

    public bool ContainsStaff(int id, int count)
    {
        foreach (var cell in _cells)
            if (cell.ItemId == id && cell.Count >= count)
                return true;
        return false;
    }

    private InventoryCell GetInventoryCell(CreatingStaff item)
    {
        foreach (var cell in _cells)
            if (cell.ItemId == item.Id)
                return cell;
        return null;
    }

    private void AddItemToCell(InventoryCell cell) => cell.Count++;

    private void CreateNewCell(CreatingStaff item)
    {
        InventoryCell addingCell = new InventoryCell();
        addingCell.Count = 1;
        addingCell.ItemId = item.Id;
        _cells.Add(addingCell);
    }

    public void AddItem(CreatingStaff item)
    {
        InventoryCell cell = GetInventoryCell(item);
        if (cell != null) AddItemToCell(cell);
        else CreateNewCell(item);
    }

    public void RemoveItem(CreatingStaff item, int count)
    {
        InventoryCell cell = GetInventoryCell(item);
        if (cell != null) cell.Count -= count;
        else
            Debug.LogError("Can't find item: " + item.Name);
    }

    public int GetCountOfItem(int id)
    {
        foreach (var cell in _cells)
            if (cell.ItemId == id)
                return cell.Count;
        return 0;
    }
}
