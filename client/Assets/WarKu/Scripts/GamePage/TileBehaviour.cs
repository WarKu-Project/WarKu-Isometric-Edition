﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {

    public GameObject tooltip,explosion;
    public bool canMove;
    public int x, y;

    void Start()
    {
        tooltip = Instantiate(tooltip, transform.position+new Vector3(0f,1.5f),Quaternion.identity);
        explosion = Instantiate(explosion, transform.position + new Vector3(0f, 0.35f), Quaternion.identity);
        explosion.transform.SetParent(transform);
        explosion.SetActive(false);
        tooltip.transform.SetParent(transform);
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (GetComponentInChildren<Animator>())
            if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Out"))
                explosion.SetActive(false);
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseDown()
    {
        if (GameObject.FindObjectOfType<Selector>().IsCreation())
        {
            DGTProxyRemote.GetInstance().RequestSpawnUnit(x, y, GameObject.FindObjectOfType<Selector>().GetUnitCreationType());
            GameObject.FindObjectOfType<Selector>().ResetState();
        }
        else if (GameObject.FindObjectOfType<Selector>().IsListen())
        {
            GameObject.FindObjectOfType<Selector>().GetWillMoveUnit().GetComponent<UnitBehaviour>().SetTarget(x, y);
            GameObject.FindObjectOfType<Selector>().ResetState();
        }
        else if (GameObject.FindObjectOfType<Selector>().IsBuild())
        {
            TileBehaviour currentTile = GameObject.FindObjectOfType<Selector>().GetCurrentTile().GetComponent<TileBehaviour>();
            if (x== currentTile.x+1|| x == currentTile.x - 1 || y == currentTile.y + 1 || y == currentTile.y - 1)
            {
                DGTProxyRemote.GetInstance().BuildRequest(currentTile.x, currentTile.y, x, y);
            } 
            GameObject.FindObjectOfType<Selector>().ResetState();
        }
        else
        {
            if (GetComponentInChildren<UnitBehaviour>() && GetComponentInChildren<UnitBehaviour>().isOwner)
            {
                GetComponentInChildren<UnitBehaviour>().Stop();
                tooltip.SetActive(true);
            }
        }
    }

    public void OnSpawnUnit(int status)
    {
        if (status == -1)
            Debug.Log("Nooooooooooooooooo");
        else
            Debug.Log("Spawn");
    }

    public void Explosion()
    {
        Debug.Log("AAAAAA");
        explosion.SetActive(true);
    }
}
