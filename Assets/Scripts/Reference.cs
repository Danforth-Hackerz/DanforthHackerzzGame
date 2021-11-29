using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    [SerializeField] private PlayerInventoryManager _PIM;

    public static PlayerInventoryManager PIM;

    private void Start()
    {
        PIM = _PIM;
    }
}
