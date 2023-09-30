using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EqualizeColor : MonoBehaviour
{
    [SerializeField] private Tilemap GetColorFrom;
    private Tilemap Self;
    // Start is called before the first frame update
    void Start()
    {
        Self = this.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Self.color = GetColorFrom.color;
    }
}
