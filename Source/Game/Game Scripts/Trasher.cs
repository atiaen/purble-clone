using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Trasher Script.
/// </summary>
public class Trasher : Script
{
    public Tag tagToDestroy;

    /// <inheritdoc/>
    /// 
    public static event Action onCakeDestroy;

    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }

    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Register for event
        Actor.GetChild<Collider>().TriggerEnter += OnTriggerEnter;
    }

    public override void OnDisable()
    {
        // Unregister for event
        Actor.GetChild<Collider>().TriggerEnter -= OnTriggerEnter;
    }

    void OnTriggerEnter(PhysicsColliderActor collider)
    {
        // Check for player
        if (collider.HasTag(tagToDestroy))
        {
            onCakeDestroy?.Invoke();
        }
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
    }
}
