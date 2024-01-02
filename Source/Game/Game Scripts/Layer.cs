﻿using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Layers Script.
/// </summary>
public class Layers : Script
{

    public bool isFilled = false;
    public bool isFrosted = false;

    public string shapeType;
    public string flavourType;
    public string frostingFlavour;

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
       
        // Here you can add code that needs to be called every frame
    }

    public void addBase()
    {

    }
}
