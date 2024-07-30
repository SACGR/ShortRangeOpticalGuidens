using Sandbox.Game.EntityComponents;
using Sandbox.Game.WorldEnvironment.ObjectBuilders;
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
//using static VRageRender.MyShadowsSettings;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        int cameraSelecter = 0;
        int antalCamrer;
        new ScanSlot obiektNumer = new ScanSlot(10);

        IMyTextSurface LCD;
        bool raycastScargeOn= false;
        int scanermengd;
        bool scaningOn = false;


        int scanSlot = 9;
        MyDetectedEntityInfo[] targetList = new MyDetectedEntityInfo[10];
        
        MyDetectedEntityInfo hitInfo;
        MyDetectedEntityInfo hitInfoPreliminär;
        List<IMyCameraBlock> cameras = new List<IMyCameraBlock>();

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
            GridTerminalSystem.GetBlocksOfType(cameras);

            LCD = GridTerminalSystem.GetBlockWithName("Diagnos") as IMyTextSurface;
            antalCamrer = cameras.Count(); 
        }

        public void Save()
        {

        }

        public void Main(string argument, UpdateType updateSource)
        {
            //obiektNumer.clear();
            if (argument.Equals("on"))
            {
                Echo("on");
                raycastScargeOn = true;
                foreach (IMyCameraBlock block in cameras)
                {
                    block.EnableRaycast = raycastScargeOn;
                    
                }
            }
            if (argument.Equals("off"))
            {
                Echo("of");
                raycastScargeOn = false;
                foreach (IMyCameraBlock block in cameras)
                {
                    block.EnableRaycast = raycastScargeOn;
                }
            }
           if (argument.Equals("scan")) {
                Echo("scaning");
                Surch(1000);
                scaningOn = true;

            }
            if (scaningOn)
            {
                int t = obiektNumer.antal();
                for (int i = 0; i < 10; i++)
                {
                    if (t < i) { i = 100; }
                    enRayCastPoint(targetPredictor(targetList[i], 10));
                }
            }
          


            LCD.WriteText("detta är bara ett test");



            }

        public void Surch(int range)
        {
            int[] scanMengd = new int[] {1,1}; ;
   
            scanermengd = 0;
            /*for (int xi = -scanMengd[0]; xi < scanMengd[0]; xi++)
            {
                for (int yi = -scanMengd[1]; yi < scanMengd[1]; yi++)
                {

                    targetList[obiektNumer.add()] = enRayCastVinkel(range, (float)(2 * xi), (float)(2 * yi));
                    scanermengd++;


                }

            }*/
            hitInfo = enRayCastVinkel(range, 0, 0);
            if(!hitInfo.IsEmpty())
             targetList[obiektNumer.add()] = hitInfo;
            




        }

        public MyDetectedEntityInfo enRayCastVinkel(double range, float x, float y)
        {
            cameraSelecter= cameraSelecter % antalCamrer;



            hitInfoPreliminär = cameras[cameraSelecter].Raycast(range,x,y);
            if (!hitInfoPreliminär.IsEmpty())
                hitInfo = hitInfoPreliminär;
            cameraSelecter++;
            return hitInfo;
            
        }
        public MyDetectedEntityInfo enRayCastPoint(Vector3D positionTarget)
        {
            cameraSelecter = cameraSelecter % antalCamrer;
            hitInfoPreliminär = cameras[cameraSelecter].Raycast(positionTarget);
            if (!hitInfoPreliminär.IsEmpty())
                hitInfo = hitInfoPreliminär;
            cameraSelecter++;
            return hitInfo;
        }

        public Vector3D targetPredictor(MyDetectedEntityInfo target,int timeStep)
        {
            timeStep = 60 / timeStep;
            return (target.Position + (target.Velocity / timeStep));

        }

        internal class ScanSlot
        {
            int max;
            int nuvarande = 0;
            public ScanSlot(int antal)
            {
                max = antal;
            }
            public int add()
            {
                nuvarande++;
                if (nuvarande >= max )
                    nuvarande = 0;
                return nuvarande;

            }
            public int antal() {  return (nuvarande-1); }
            public void clear() { nuvarande = 0; }
        }
        //slut
    }
}
