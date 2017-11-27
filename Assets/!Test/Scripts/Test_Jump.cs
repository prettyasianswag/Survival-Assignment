using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GGL; // << ADDED THIS

[RequireComponent(typeof(PlayerController))]
public class Test_Jump : Test
{
    [Header("Test Parameters")]
    public float minHeight = 1f;

    private PlayerController player;
    private float originalY;
    private float jumpApex;

    void Start()
    {
        player = GetComponent<PlayerController>();
        originalY = transform.position.y;
    }

    public override void Simulate()
    {
        // Simulate jump mechanic on the player
        player.Jump();
    }

    public override void Check()
    {
        // Get current player Y position
        float playerY = player.transform.position.y;
        float height = playerY - originalY;
        // Get the heighest point that player jumps to (the apex)
        if (height > jumpApex)
            jumpApex = height;

        // Check if the Y went up from original position
        if (jumpApex > minHeight)
        {
            // The test has succeeded
            IntegrationTest.Pass(gameObject);
        }
    }

    public override void Debug()
    {
        GizmosGL.color = Color.red;
        Vector3 originalPos = new Vector3(0, originalY, 0);
        Vector3 playerPos = transform.position;

        // Draw the min height the player needs to jump to
        GizmosGL.AddLine(originalPos, originalPos + Vector3.up * minHeight);

        // Draw the player's current height
        GizmosGL.AddLine(originalPos, playerPos, 0.35f, 0.35f);
    }
}
