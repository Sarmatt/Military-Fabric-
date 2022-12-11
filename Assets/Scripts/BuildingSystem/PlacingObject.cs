using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingObject : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    public Vector2Int Size = Vector2Int.one;
    [HideInInspector] public bool Placed = false;
    [HideInInspector] public int PlaceX;
    [HideInInspector] public int PlaceZ;
   
    private void OnDrawGizmos()
    {
        for(int x = 0; x < Size.x; x++)
        {
            for(int z = 0; z < Size.y; z++)
            {
                if((x+z)%2 == 0) Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
                else Gizmos.color = new Color(0f, 5f, 5f, 0.4f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, z), new Vector3(1f, 0.1f, 1f));
            }
        }
    }

    public void SetTransparent(bool avaliabe)
    {
        if (!avaliabe) _renderer.material.color = Color.red;
        else _renderer.material.color = Color.green;
    }

    public void PlaceObject() => _renderer.material.color = Color.white;
}
