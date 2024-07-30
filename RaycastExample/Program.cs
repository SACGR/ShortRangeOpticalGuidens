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
        IMyCameraBlock Camera;
        IMyCameraBlock cam1,cam2;


        IMyTextSurface LCD;
        IMyCockpit Cockpit;
        IMyMotorStator pitchRotor, yawRotor;
        bool Scan = false;
        int trakLost = 0;
        bool vemSkanar = false;
        int scanermengd = 0;


        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update10;
            List<IMyCameraBlock> cameras = new List<IMyCameraBlock>();
            GridTerminalSystem.GetBlocksOfType(cameras);
            Camera = cameras[0];
            cam1 = cameras[1];
            cam2 = cameras[2];
            LCD = GridTerminalSystem.GetBlockWithName("LCD") as IMyTextSurface;
            pitchRotor = GridTerminalSystem.GetBlockWithName("pitchRotor") as IMyMotorStator;
            yawRotor = GridTerminalSystem.GetBlockWithName("yawRotor") as IMyMotorStator;
            Cockpit = GridTerminalSystem.GetBlockWithName("Cockpit") as IMyCockpit;
            cam1.EnableRaycast = false;
            cam2.EnableRaycast = false;
            Camera.EnableRaycast = false;
        }

        public void Save()
        {

        }
        double maxRange;
        string strOut = "";
        MyDetectedEntityInfo hitInfo;
        public void Main(string argument, UpdateType updateSource)
        {
            pitchRotor.TargetVelocityRPM = Cockpit.RotationIndicator.X / 2;
            yawRotor.TargetVelocityRPM = Cockpit.RotationIndicator.Y / 2;

            strOut = "C0: " + Camera.EnableRaycast + Camera.AvailableScanRange + " m";

            strOut += "\nC1: " + cam1.EnableRaycast+cam1.AvailableScanRange + " m";
            strOut += "\nC2: " + cam2.EnableRaycast + cam2.AvailableScanRange + " m";
          
            strOut += "\nförsökAttSkana"+ scanermengd;

            if (argument.Equals("switch"))
            {
                Camera.EnableRaycast = !Camera.EnableRaycast;
                cam1.EnableRaycast = !cam1.EnableRaycast;
                cam2.EnableRaycast = !cam2.EnableRaycast;

            }
            if (argument.Equals("cast"))
            {
                hitInfo = Camera.Raycast(1000);
                if (hitInfo.IsEmpty()) { Surch(); }
                Scan = true;
            }
            else if (argument.Equals("stop")|| trakLost > 3) { Scan = false;trakLost = 0; }



                if (!hitInfo.IsEmpty())
            {
                
                strOut += ("Hit Pos: " + hitInfo.HitPosition + "\nTarget Pos: " + hitInfo.Position + "\nName:" + hitInfo.Name + "\nType: " + hitInfo.Type + "\nVelocity: " + hitInfo.Velocity + "\n\nDistance: " + (hitInfo.Position - Camera.GetPosition()).Length());
            }
            else
            {
                trakLost++;
                strOut += ("No Target\n"+trakLost);
            }
            if (Scan == true)
            {
                Tracker();


            }

            LCD.WriteText(strOut);
        }
        public void Tracker()
        {
            if (vemSkanar)
            {
                hitInfo = cam1.Raycast(hitInfo.Position + hitInfo.Velocity / 6);
                vemSkanar = !vemSkanar;
            }
            else
            {
                hitInfo = cam2.Raycast(hitInfo.Position + hitInfo.Velocity / 6);
                vemSkanar = !vemSkanar;
            }



                Echo("tracking");
        }
        public void Surch()
        {
            scanermengd = 0;
            for (int xi = -5; xi < 5; xi++)
            {
                for (int yi = -5; yi < 5; yi++)
                {
                    if (!hitInfo.IsEmpty()) { break; }
                    hitInfo = Camera.Raycast(1000,2*xi,2*yi);
                    scanermengd++;


                }

            }
        }



        //slut

    }
}
