using Sandbox.Game.EntityComponents;
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
        //amount of elements in each scaning element max 5 fore scaning att 5km 
        const int antaletElement = 5;
        
        //Raycost per sec
        const int frequensy = 2;



        List<IMyCameraBlock> cameras = new List<IMyCameraBlock>();
        ScaningElementManiger[] element = new ScaningElementManiger[antaletElement];
        
        

        MyDetectedEntityInfo[] targetList = new MyDetectedEntityInfo[antaletElement];


        public Program()
        {

            GridTerminalSystem.GetBlocksOfType(cameras);
            //adds the elemetn to the tracking grups
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
            for (int i = 0; i < antaletElement; i++) {
                if (targetList[i].IsEmpty())
                {
                    return;
                }
                targetList[i] = element[i].scan(targetList[i].Position+(targetList[i].Velocity/frequensy));









            
            
            }





        }
    }
}
