using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderSecretWalls : MonoBehaviour
{
    public TilemapRenderer Render;
    Color Col;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerVariables.SecretRevealed == false)
        {
            if (collision.tag == "Player")
            {
                Render.enabled = true;
                PlayerVariables.SecretRevealed = true;
                Col = Render.material.GetColor("_Color");
                Col.a = 0.05f;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerVariables.SecretRevealed == true)
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
