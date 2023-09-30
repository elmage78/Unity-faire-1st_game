using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerVariables
{
    public static float LastXspeed = 0f;
    public static float LastYspeed = 0f;
    public static Vector2 LastSpeed = Vector2.zero;
    public static bool Interact;
    public static float SpeedChange = 1f;
    public static float knockback = 300f;
    public static int damage = 1;
    public static int health = 3;
    public static Vector2 LastPosition = Vector2.zero;
    public static int Score = 0;
    public static bool SecretRevealed = false;
}

public class PlayerVariablesSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
