using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderSecret : MonoBehaviour
{
    TilemapRenderer SelfRender;
    Tilemap Self;
    bool LastTrue;
    // Start is called before the first frame update
    private void Start()
    {
        SelfRender = this.GetComponent<TilemapRenderer>();
        Self = this.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LastTrue != PlayerVariables.SecretRevealed)
        {
            StartCoroutine(FadeColor(Self));
        }
        if (PlayerVariables.SecretRevealed)
        {
            SelfRender.enabled = true;
        }
        if (!PlayerVariables.SecretRevealed)
        {
            SelfRender.enabled = false;
        }
        LastTrue = PlayerVariables.SecretRevealed;
    }
    IEnumerator FadeColor(Tilemap Rendered)
    {
        Rendered.color = new Color(Rendered.color.r,Rendered.color.g,Rendered.color.b, 0.01f);
        while (Rendered.color.a < 1)
        {
            Rendered.color = new Color(Rendered.color.r, Rendered.color.g, Rendered.color.b, Rendered.color.a * 1.01f);
            yield return null;
        }
    }
}
