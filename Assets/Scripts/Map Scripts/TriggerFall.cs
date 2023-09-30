using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerFall : MonoBehaviour
{
    public TilemapCollider2D TileCollider;
    public TilemapRenderer Render;
    bool Fall = false;
    Color Col;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Fall)
        {
            if (collision.tag == "Player")
            {
                TileCollider.enabled = true;
                Render.enabled = true;
                Fall = true;
                Col = Render.material.GetColor("_Color");
                Col.a = 0.05f;
            }
        }
    }
    private void Update()
    {
        if (Fall == true)
        {
            Render.material.SetColor("_Color", Col);
            if (Render.material.GetColor("_Color").a < 1f)
            {
                Col.a *= 1.01f;
            }
            if (Render.material.GetColor("_Color").a >= 1f)
            {
                Col.a = 1f;
            }
        }
    }
}
