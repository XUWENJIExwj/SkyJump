﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float framesPerSec;

    private SpriteRenderer spriteRender;

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void PlayerIdle()
    {
        spriteRender.sprite = sprites[0];
    }

    public void PlayerTap()
    {
        spriteRender.sprite = sprites[1];
    }

    public void PlayerJump()
    {
        int index = (int)(Time.fixedTime * framesPerSec) % 2 + 2;
        spriteRender.sprite = sprites[index];
    }

    public void UpdateEnemyAnimation()
    {
        int index = (int)(Time.time * framesPerSec) % 2;
        spriteRender.sprite = sprites[index];
    }

    public void SetDirection(bool is_right)
    {
        if(is_right)
        {
            spriteRender.flipX = !is_right;
        }
        else
        {
            spriteRender.flipX = !is_right;
        }
    }
}
