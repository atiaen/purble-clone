using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Submitter Script.
/// </summary>
public class Submitter : Script
{
    public Tag checkingTag;

    /// <inheritdoc/>
    /// 
    public static event Action<PhysicsColliderActor> onCakeSubmitted;

    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
    }

    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Register for event
        Actor.As<Collider>().TriggerEnter += OnTriggerEnter;
    }

    public override void OnDisable()
    {
        // Unregister for event
        Actor.As<Collider>().TriggerEnter -= OnTriggerEnter;
    }

    void OnTriggerEnter(PhysicsColliderActor collider)
    {
        // Check for player
        if (collider.HasTag(checkingTag))
        {
            onCakeSubmitted?.Invoke(collider);
        }
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
    }
}
