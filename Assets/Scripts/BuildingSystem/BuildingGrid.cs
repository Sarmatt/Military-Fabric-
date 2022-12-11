using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    public static BuildingGrid singleton;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
    private PlacingObject[,] _gridObjects;
    private PlacingObject _selectedObject;
    private Camera _mainCamera;
    [SerializeField] private Transform _ground;

    private void Awake()
    {
        singleton = this;
        _mainCamera = Camera.main;
        _gridObjects = new PlacingObject[_gridSize.x, _gridSize.y];
    }

    private void Update()
    {
        if(_selectedObject != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);
                _selectedObject.transform.position = new Vector3(x, 0, z);
                _selectedObject.SetTransparent(ObjectCanBePlaced(x, z));

                if (Input.GetMouseButtonDown(0) && ObjectCanBePlaced(x, z))
                    PlaceObject(x, z);
            }          
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int z = 0; z < _gridSize.y; z++)
            {
                if ((x + z) % 2 == 0) Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
                else Gizmos.color = new Color(0f, 5f, 5f, 0.4f);
                Gizmos.DrawCube(_ground.position + new Vector3(x, 0, z), new Vector3(1f, 0.1f, 1f));
            }
        }
    }

    private bool PlaceTaken(int placeX, int placeZ)
    {
        for (int x = 0; x < _selectedObject.Size.x; x++)
        {
            for (int z = 0; z < _selectedObject.Size.y; z++)
            {
               if(_gridObjects[placeX + x, placeZ + z] != null) return true;
            }
        }
        return false;
    }

    private bool ObjectCanBePlaced(int x, int z)
    {
        if (x < 0 || x > _gridSize.x - _selectedObject.Size.x) return false;
        if (z < 0 || z > _gridSize.y - _selectedObject.Size.y) return false;
        if (PlaceTaken(x, z)) return false;
        return true;
    }

    private void PlaceObject(int placeX, int placeZ)
    {
        for (int x = 0; x < _selectedObject.Size.x; x++)
        {
            for (int z = 0; z < _selectedObject.Size.y; z++)
            {
                _gridObjects[placeX + x, placeZ + z] = _selectedObject;
            }
        }
        int neededMoney = _selectedObject.GetComponent<Equipment>().NeededMoney;
        EconomyFunctional.singleton.MinusMoney(neededMoney);
        _selectedObject.PlaceX = placeX;
        _selectedObject.PlaceZ = placeZ;
        _selectedObject.Placed = true;
        _selectedObject.PlaceObject();
        _selectedObject = null;
    }

    public void StartPlacing(PlacingObject prefab)
    {
        int neededMoney = prefab.GetComponent<Equipment>().NeededMoney;
        if (EconomyFunctional.singleton.EnoughMoney(neededMoney))
        {
            if (_selectedObject != null)
                Destroy(_selectedObject.gameObject);
            _selectedObject = Instantiate(prefab);
            
        }     
    }

    public void MoveObject(PlacingObject obj)
    {
        for (int x = 0; x < obj.Size.x; x++)
        {
            for (int z = 0; z < obj.Size.y; z++)
            {
                _gridObjects[obj.PlaceX + x, obj.PlaceZ + z] = null;
            }
        }
        _selectedObject = obj;
    }
}
