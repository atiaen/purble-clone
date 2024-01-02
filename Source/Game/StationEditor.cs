#if FLAX_EDITOR
using System;
using System.Collections.Generic;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// StationEditor Script.
    /// </summary>
    [CustomEditor(typeof(Station))]
    public class StationEditor : GenericEditor
    {
        public override void Initialize(LayoutElementsContainer layout)
        {
            // (Values[0] as Station).stationType = StationType.BatterStation;
            // (Values[0] as Station).showBatterDic = true;

            //layout.Label("Station Editor", TextAlignment.Center);
            //var enu = layout.Enum("Station Types", typeof(StationType), null, "Select a type", EnumDisplayAttribute.FormatMode.Default);
            //// group.AddElement(enu);
            //// enu.ComboBox.EnumTypeValue = StationType.BatterStation;

            //enu.ComboBox.EnumValueChanged += (e) =>
            //{
            //    enu.ComboBox.EnumTypeValue = e.Value;
            //    (Values[0] as Station).stationType = (StationType)e.Value;

            //    if ((Values[0] as Station).stationType == StationType.BatterStation)
            //    {
            //        (Values[0] as Station).showBatterDic = true;
            //        (Values[0] as Station).showPanDic = false;
            //        (Values[0] as Station).showMainToppingDic = false;
            //        (Values[0] as Station).showOtherToppingDic = false;
            //        (Values[0] as Station).showSemiLayerDic = false;
            //        (Values[0] as Station).showLayerDic = false;
            //    }

            //    if ((Values[0] as Station).stationType == StationType.PanStation)
            //    {
            //        (Values[0] as Station).showBatterDic = false;
            //        (Values[0] as Station).showPanDic = true;
            //        (Values[0] as Station).showMainToppingDic = false;
            //        (Values[0] as Station).showOtherToppingDic = false;
            //        (Values[0] as Station).showSemiLayerDic = false;
            //        (Values[0] as Station).showLayerDic = false;
            //    }

            //    if ((Values[0] as Station).stationType == StationType.LayerStation)
            //    {
            //        (Values[0] as Station).showBatterDic = false;
            //        (Values[0] as Station).showPanDic = false;
            //        (Values[0] as Station).showMainToppingDic = false;
            //        (Values[0] as Station).showOtherToppingDic = false;
            //        (Values[0] as Station).showSemiLayerDic = false;
            //        (Values[0] as Station).showLayerDic = true;
            //    }

            //    if ((Values[0] as Station).stationType == StationType.SemiLayerStation)
            //    {
            //        (Values[0] as Station).showBatterDic = false;
            //        (Values[0] as Station).showPanDic = false;
            //        (Values[0] as Station).showMainToppingDic = false;
            //        (Values[0] as Station).showOtherToppingDic = false;
            //        (Values[0] as Station).showSemiLayerDic = true;
            //        (Values[0] as Station).showLayerDic = false;
            //    }

            //    if ((Values[0] as Station).stationType == StationType.ToppingStation)
            //    {
            //        (Values[0] as Station).showBatterDic = false;
            //        (Values[0] as Station).showPanDic = false;
            //        (Values[0] as Station).showMainToppingDic = true;
            //        (Values[0] as Station).showOtherToppingDic = false;
            //        (Values[0] as Station).showSemiLayerDic = false;
            //        (Values[0] as Station).showLayerDic = false;
            //    }

            //    if ((Values[0] as Station).stationType == StationType.OtherToppingStation)
            //    {
            //        (Values[0] as Station).showBatterDic = false;
            //        (Values[0] as Station).showPanDic = false;
            //        (Values[0] as Station).showMainToppingDic = false;
            //        (Values[0] as Station).showOtherToppingDic = true;
            //        (Values[0] as Station).showSemiLayerDic = false;
            //        (Values[0] as Station).showLayerDic = false;
            //    }
            //};

            base.Initialize(layout);
            // (Values[0] as Station).stationType = StationType.BatterStation;
            // (Values[0] as Station).showBatterDic = true;
            // Values[1]



            // Use Values[] to access the script or value being edited.
            // It is an array, because custom editors can edit multiple selected scripts simultaneously.
            // button.Button.Clicked += () => Debug.Log("Button clicked! The speed is " + (IsSingleObject ? (Values[0] as MyScript).Speed : ""));
        }

        // public override void Refresh()
        // {
        //     base.Refresh();
        // }
    }

}
#endif