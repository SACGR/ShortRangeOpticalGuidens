using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections;
using System.Collections.Immutable;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;
using VRage.Serialization;

namespace IngameScript
{
    internal class ScaningElementManiger
    {
        List<IMyCameraBlock> Cameras = new List<IMyCameraBlock>();
        int elemet = -1;
        int nuvarandeElement = 0;
        public ScaningElementManiger() { }


        public void add(IMyCameraBlock camera)
        {
            Cameras.Add(camera);
            elemet++;

        }
        public MyDetectedEntityInfo scan(Vector3D Target)
        {
            MyDetectedEntityInfo info;
            nuvarandeElement = nuvarandeElement % elemet;
            info =  Cameras[nuvarandeElement].Raycast(Target);
            
            nuvarandeElement++;
            return info;

        }
        public int range() { 
        int t = 0;  
            foreach(IMyCameraBlock camera in Cameras)
            {
                t += (int)camera.AvailableScanRange;
            }
            return t/elemet;

        }
        public void onOff(bool stat)
        {
            foreach (IMyCameraBlock camera in Cameras)
            {
                camera.EnableRaycast = stat;
            }

        }
        public MyDetectedEntityInfo point(int range)
        {
            MyDetectedEntityInfo info;
            nuvarandeElement = nuvarandeElement % elemet;
            info = Cameras[nuvarandeElement].Raycast(range);

            nuvarandeElement++;
            return info;


        }
    }
}
