﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour {

    void OnMouseDown()
    {
        GameObject.FindObjectOfType<Selector>().ReadyToMove(transform.parent.parent.GetChild(2).gameObject);
        transform.parent.parent.GetChild(1).gameObject.SetActive(false);
    }
}
