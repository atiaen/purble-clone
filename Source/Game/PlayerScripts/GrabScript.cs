using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// GrabScript Script.
/// </summary>
public class GrabScript : Script
{
    public Camera playerCamera;

    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        RayCastHit hit;
        if (Physics.RayCast(playerCamera.Position, playerCamera.Direction, out hit))
        {
            DebugDraw.DrawSphere(new BoundingSphere(hit.Point, 50), Color.Red);
        }
    }
}
