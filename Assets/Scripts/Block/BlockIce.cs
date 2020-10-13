using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIce : BlockNormal
{
    public PhysicsMaterial2D ice;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRb.sharedMaterial = null;

            if (playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE)
            {
                playerObjWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN);
            }

            playerObjWithFlick.SetIfOnBlock(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerObjWithFlick.GetIfOnBlock())
        {
            playerRb.sharedMaterial = ice;

            playerObjWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);

            SetScore();
        }
    }
}
