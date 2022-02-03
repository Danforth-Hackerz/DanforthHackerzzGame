using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController cursor;
    public Texture2D cursorTexture;
    public void Start() {
        cursor = this;
        DontDestroyOnLoad(this);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
    }


}
