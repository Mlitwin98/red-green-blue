using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesColor : MonoBehaviour
{
    public void SetTileColor(Transform player)
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        Color color = new Color(player.GetComponent<SpriteRenderer>().color.r, player.gameObject.GetComponent<SpriteRenderer>().color.g, player.gameObject.GetComponent<SpriteRenderer>().color.b, 0.5f);
        tilemap.SetTileFlags(tilemap.WorldToCell(player.transform.position), TileFlags.None);
        tilemap.SetColor(tilemap.WorldToCell(player.transform.position), color);
    }
}
