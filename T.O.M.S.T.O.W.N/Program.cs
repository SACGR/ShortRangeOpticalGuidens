﻿using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        List<IMyCameraBlock> cameras = new List<IMyCameraBlock>();
        List<ScaningElementManiger> element =new List<ScaningElementManiger>();


        //amount of elements in each scaning element max 5 fore scaning att 5km 
        int antaletElement = 5;



        public Program()
        {
            GridTerminalSystem.GetBlocksOfType(cameras);
            int i = 0;
            foreach (IMyCameraBlock block in cameras)
            {
                element[i].add(block);
                i++;
                i= i%antaletElement;


            }


        }

        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {
        }
    }
}
