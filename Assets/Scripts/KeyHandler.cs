using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler : MonoBehaviour
{
    //public static Dictionary<KeyCode, string> keyCodes;

    public static bool enableSpace = true;
    public static bool enableMovement = true;
    public static bool enableMouse = true;


    public static Vector3 ReadDirInput()
    {
        if (!enableMovement) return Vector3.zero;
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) dir.x += 1;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) dir.x -= 1;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) dir.z += 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) dir.z -= 1;
        return dir;
    }

    public static Vector2 ReadJumpInput()
    {
        if (!enableMovement) return Vector3.zero;
        Vector2 dir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) dir.y += 1;
        return dir;
    }

    public static Vector2 ReadMousePos()
    {
        return Input.mousePosition;
    }

    public static bool ReadRespawnButtonDown()
    {
        return Input.GetKey(KeyCode.R);
    }

    public static bool ReadSelectMenu()
    {
        return Input.GetKeyDown(KeyCode.M);
    }
}
