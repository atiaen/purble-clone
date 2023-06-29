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
        [HideInEditor]
        public StationType stationType;

        public Collider stationCollider;

        public Collider triggerCollider;

        public Actor currentCake;

        public Actor spawnPoint;

        public Actor currentSpawn;

        [HideInEditor]
        public bool showPanDic;

        [VisibleIf(nameof(showPanDic))]
        public Dictionary<string, Prefab> panDic;


        [HideInEditor]
        public bool showBatterDic;

        [VisibleIf(nameof(showBatterDic))]
        public Dictionary<string, Prefab> batterDic;

        [HideInEditor]
        public bool showLayerDic;

        [VisibleIf(nameof(showLayerDic))]
        public Dictionary<string, Prefab> layerDic;

        [HideInEditor]
        public bool showSemiLayerDic;

        [VisibleIf(nameof(showSemiLayerDic))]
        public Dictionary<string, Prefab> semiLayerDic;

        [HideInEditor]
        public bool showMainToppingDic;

        [VisibleIf(nameof(showMainToppingDic))]
        public Dictionary<string, Prefab> mainToppingDic;

        [HideInEditor]
        public bool showOtherToppingDic;

        [VisibleIf(nameof(showOtherToppingDic))]
        public Dictionary<string, Prefab> otherToppingDic;

        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            Debug.Log(GameManager.difficulty);
            if (GameManager.difficulty == "Beginner")
            {
                if (stationType == StationType.SemiLayerStation || stationType == StationType.OtherToppingStation)
                {
                    Actor.IsActive = false;
                    triggerCollider.As<Actor>().IsActive = false;
                    stationCollider.As<Actor>().IsActive = false;
                }
            }

            if (GameManager.difficulty == "Intermediate")
            {
                if (stationType == StationType.OtherToppingStation)
                {
                    Actor.IsActive = false;
                    triggerCollider.As<Actor>().IsActive = false;
                    stationCollider.As<Actor>().IsActive = false;
                }
            }
        }

        /// <inheritdoc/>
        public override void OnEnable()
        {
            stationCollider.As<Collider>().TriggerEnter += OnTriggerEnter;
            ColliderInvoker.itemToSpawn += SpawnItem;
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            stationCollider.As<Collider>().TriggerEnter -= OnTriggerEnter;
            ColliderInvoker.itemToSpawn -= SpawnItem;
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
        }

        void OnTriggerEnter(PhysicsColliderActor collider)
        {
            if (currentCake == null && !collider.HasTag("Floor"))
            {
                currentCake = collider;
            }
        }

        void SpawnItem(string itemName)
        {

            switch (stationType)
            {
                case StationType.PanStation:
                    PrefabManager.SpawnPrefab(panDic[itemName]);
                    // currentCake.GetChild<Joint>().Target = currentSpawn;
                    break;

                case StationType.BatterStation:
                    PrefabManager.SpawnPrefab(batterDic[itemName], spawnPoint);
                    currentCake.GetChild<Joint>().Target = currentSpawn;
                    break;

                case StationType.LayerStation:
                    currentSpawn = PrefabManager.SpawnPrefab(layerDic[itemName], spawnPoint);
                    currentCake.GetChild<Joint>().Target = currentSpawn;
                    break;

                case StationType.OtherToppingStation:
                    currentSpawn = PrefabManager.SpawnPrefab(otherToppingDic[itemName], spawnPoint);
                    currentCake.GetChild<Joint>().Target = currentSpawn;
                    break;


                case StationType.SemiLayerStation:
                    currentSpawn = PrefabManager.SpawnPrefab(semiLayerDic[itemName], spawnPoint);
                    currentCake.GetChild<Joint>().Target = currentSpawn;
                    break;

                case StationType.ToppingStation:
                    currentSpawn = PrefabManager.SpawnPrefab(mainToppingDic[itemName], spawnPoint);
                    currentCake.GetChild<Joint>().Target = currentSpawn;
                    break;

                default:
                    currentSpawn = PrefabManager.SpawnPrefab(panDic[itemName], spawnPoint);
                    currentCake.GetChild<Joint>().Target = currentSpawn;
                    break;


            }
            // var itm = PrefabManager.SpawnPrefab(panDic[itemName]);
        }
    }
}
