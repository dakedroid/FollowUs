﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool opened;
    public Vector2 direction;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (opened)
            {
                Vector2 pPos = playerController.transform.position;
                playerController.rb.position = new Vector2(pPos.x + direction.x, pPos.y + direction.y);
            }
        }
    }
}