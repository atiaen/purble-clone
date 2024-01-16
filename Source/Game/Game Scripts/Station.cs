using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// Station Script.
    /// </summary>
    public class Station : Script
    {
        /// <inheritdoc/>

        public string stationName;

        public StationType stationType;

        public Actor currentCake;

        bool canModifyCake = false;

        //bool canModifyCake = false;

        public Dictionary<string, Model> itemDictonary;
        public Dictionary<string, Model> filledModel;


        public override void OnStart()
        {

        }

        /// <inheritdoc/>
        public override void OnEnable()
        {
            Actor.As<Collider>().TriggerEnter += OnTriggerEnter;
            Actor.As<Collider>().TriggerExit += OnTriggerExit;
            ColliderInvoker.stationAndChoseAction += modifyCake;

        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            Actor.As<Collider>().TriggerEnter -= OnTriggerEnter;
            Actor.As<Collider>().TriggerExit -= OnTriggerExit;
            ColliderInvoker.stationAndChoseAction -= modifyCake;

        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
        }

        void OnTriggerEnter(PhysicsColliderActor collider)
        {
            var parent = collider.As<Actor>().Parent;
            if (parent.HasTag("Cake"))
            {
                canModifyCake = true;
                currentCake = collider.As<Actor>().Parent;
            }
           
        }

        void OnTriggerExit(PhysicsColliderActor collider)
        {
            canModifyCake = false;
            currentCake = null;
        }

        void modifyCake(string _stationName,string chosenItem)
        {
            

            if (canModifyCake && currentCake != null && _stationName == stationName)
            {
                var cakeChildren = currentCake.GetChildren<Actor>();
                var cakeScript = currentCake.GetScript<Cake>();
                var sheet = cakeChildren[0];

                if (stationType == StationType.PanStation)
                {
                    //if(cakeScript.currentLayer < cakeScript.maxLayers) 
                    //{
                       
                    //}

                    var sheetChildren = sheet.GetChildren<Actor>();
                    if (sheetChildren.Length == 0)
                    {
                        currentCake.GetScript<Cake>().currentLayer++;
                        var firstLayer = sheet.AddChild<StaticModel>();
                        firstLayer.AddScript<Layers>();

                        firstLayer.GetScript<Layers>().shapeType = chosenItem;
                        firstLayer.Model = itemDictonary[chosenItem];
                        firstLayer.Position = new Vector3(currentCake.Position.X, currentCake.Position.Y - 20, currentCake.Position.Z);
                    }

                    if (sheetChildren.Length != 0)
                    {
                        var lastLayer = sheetChildren[sheetChildren.Length - 1];
                        var lastLayerScript = lastLayer.GetScript<Layers>();
                        if (!lastLayerScript.isFilled)
                        {
                            var lastLayerModel = lastLayer.As<StaticModel>();
                            lastLayerModel.GetScript<Layers>().shapeType = chosenItem;
                            lastLayerModel.Model = itemDictonary[chosenItem];
                        }

                        if (lastLayerScript.isFilled && cakeScript.currentLayer < cakeScript.maxLayers)
                        {
                            currentCake.GetScript<Cake>().currentLayer++;
                            var nextLayer = sheet.AddChild<StaticModel>();
                            nextLayer.AddScript<Layers>();

                            nextLayer.GetScript<Layers>().shapeType = chosenItem;

                            nextLayer.Model = itemDictonary[chosenItem];
                            nextLayer.Position = new Vector3(lastLayer.Position.X, lastLayer.Position.Y + 20, lastLayer.Position.Z);
                        }
                    }

                }

                if (stationType == StationType.BatterStation)
                {
                    var lastLayer = sheet.GetChild(sheet.GetChildren<Actor>().Length - 1);
                    var script = lastLayer.GetScript<Layers>();

                    if (!script.isFrosted)
                    {
                        string shapeType = script.shapeType;

                        lastLayer.As<StaticModel>().Model = filledModel[shapeType];

                        var mat = lastLayer.As<StaticModel>().Model.MaterialSlots[0].Material;

                        var instance = mat.CreateVirtualInstance();

                        instance.SetParameterValue("Color", Color.ParseHex(chosenItem));

                        lastLayer.As<StaticModel>().SetMaterial(0, instance);

                        lastLayer.GetScript<Layers>().isFilled = true;
                        lastLayer.GetScript<Layers>().flavourType = chosenItem;
                    }


                }

                if (stationType == StationType.FrostingStation)
                {
                    var lastLayer = sheet.GetChild(sheet.GetChildren<Actor>().Length - 1);
                    var script = lastLayer.GetScript<Layers>();

                    string shapeType = script.shapeType;

                    if(lastLayer.Children.Length == 0)
                    {
                        lastLayer.AddChild<StaticModel>().Model = filledModel[shapeType];
                        var frosting = lastLayer.GetChild<StaticModel>();
                        var mat = frosting.As<StaticModel>().Model.MaterialSlots[0].Material;

                        var instance = mat.CreateVirtualInstance();

                        instance.SetParameterValue("Color", Color.ParseHex(chosenItem));

                        frosting.As<StaticModel>().SetMaterial(0, instance);
                        lastLayer.GetScript<Layers>().isFrosted = true;
                        lastLayer.GetScript<Layers>().frostingFlavour = chosenItem;
                    }
                    else
                    {
                        var frosting = lastLayer.GetChild<StaticModel>();
                        var mat = frosting.As<StaticModel>().Model.MaterialSlots[0].Material;

                        var instance = mat.CreateVirtualInstance();

                        instance.SetParameterValue("Color", Color.ParseHex(chosenItem));

                        frosting.As<StaticModel>().SetMaterial(0, instance);
                        lastLayer.GetScript<Layers>().isFrosted = true;
                        lastLayer.GetScript<Layers>().frostingFlavour = chosenItem;

                    }
                }
            }
        }
    }
}
