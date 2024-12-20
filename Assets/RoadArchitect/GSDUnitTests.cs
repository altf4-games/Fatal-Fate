﻿using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
#endif
namespace GSD.Roads {
#if UNITY_EDITOR
    public static class GSDUnitTests
    {
        /// <summary>
        /// WARNING: Only call this on an empty scene that has some terrains on it. MicroGSD LLC is not responsbile for data loss if this function is called by user.
        /// </summary>
        public static void RoadArchitectUnitTests() {
            //Get the existing road system, if it exists:
            GameObject GSDRS = (GameObject)GameObject.Find("RoadArchitectSystem1");

            //Destroy the terrain histories:
            if (GSDRS != null) {
                Object[] tRoads = GSDRS.GetComponents<GSDRoad>();
                foreach (GSDRoad xRoad in tRoads) {
                    GSD.Roads.GSDTerraforming.TerrainsReset(xRoad);
                }
                Object.DestroyImmediate(GSDRS);
            }

            //Reset all terrains to 0,0
            Object[] zTerrains = Object.FindObjectsOfType<Terrain>();
            foreach (Terrain xTerrain in zTerrains) {
                xTerrain.terrainData.SetHeights(0, 0, new float[513, 513]);
            }

            //Create new road system and turn off updates:
            GameObject tRoadSystemObj = new GameObject("RoadArchitectSystem1");
            GSDRoadSystem tRoadSystem = tRoadSystemObj.AddComponent<GSDRoadSystem>(); 	//Add road system component.
            tRoadSystem.opt_bAllowRoadUpdates = false;

            //Perform unit tests:
            RoadArchitectUnitTest1();   //Bridges
            RoadArchitectUnitTest2();   //2L intersections
            RoadArchitectUnitTest3();   //4L intersections
            RoadArchitectUnitTest4();   //Large suspension bridge

            //Turn updates back on and update road:
            tRoadSystem.opt_bAllowRoadUpdates = true;
            tRoadSystem.UpdateAllRoads();
        }

        private static void RoadArchitectUnitTest1() {
            //Create node locations:
            List<Vector3> tLocs = new List<Vector3>();
            int MaxCount = 18;
            float tMod = 100f;
            Vector3 xVect = new Vector3(50f, 40f, 50f);
            for (int i = 0; i < MaxCount; i++) {
                //tLocs.Add(xVect + new Vector3(tMod * Mathf.Pow((float)i / ((float)MaxCount * 0.15f), 2f), 1f*((float)i*1.25f), tMod * i));
                tLocs.Add(xVect + new Vector3(tMod * Mathf.Pow((float)i / ((float)25 * 0.15f), 2f), 0f, tMod * i));
            }

            //Get road system create road:
            GSDRoadSystem GSDRS = (GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>();
            GSDRoad tRoad = GSDRoadAutomation.CreateRoad_Programmatically(GSDRS, ref tLocs);

            //Bridge0: (Arch)
            tRoad.GSDSpline.mNodes[4].bIsBridgeStart = true;
            tRoad.GSDSpline.mNodes[4].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[7].bIsBridgeEnd = true;
            tRoad.GSDSpline.mNodes[7].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[4].BridgeCounterpartNode = tRoad.GSDSpline.mNodes[7];
            tRoad.GSDSpline.mNodes[4].LoadWizardObjectsFromLibrary("Arch12m-2L", true, true);

            //Bridge1: (Federal causeway)
            tRoad.GSDSpline.mNodes[8].bIsBridgeStart = true;
            tRoad.GSDSpline.mNodes[8].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[8].BridgeCounterpartNode = tRoad.GSDSpline.mNodes[10];
            tRoad.GSDSpline.mNodes[8].LoadWizardObjectsFromLibrary("Causeway1-2L", true, true);
            tRoad.GSDSpline.mNodes[10].bIsBridgeEnd = true;
            tRoad.GSDSpline.mNodes[10].bIsBridgeMatched = true;

            //Bridge2: (Steel)
            tRoad.GSDSpline.mNodes[11].bIsBridgeStart = true;
            tRoad.GSDSpline.mNodes[11].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[11].BridgeCounterpartNode = tRoad.GSDSpline.mNodes[13];
            tRoad.GSDSpline.mNodes[11].LoadWizardObjectsFromLibrary("Steel-2L", true, true);
            tRoad.GSDSpline.mNodes[13].bIsBridgeEnd = true;
            tRoad.GSDSpline.mNodes[13].bIsBridgeMatched = true;

            //Bridge3: (Causeway)
            tRoad.GSDSpline.mNodes[14].bIsBridgeStart = true;
            tRoad.GSDSpline.mNodes[14].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[16].bIsBridgeEnd = true;
            tRoad.GSDSpline.mNodes[16].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[14].BridgeCounterpartNode = tRoad.GSDSpline.mNodes[16];
            tRoad.GSDSpline.mNodes[14].LoadWizardObjectsFromLibrary("Causeway4-2L", true, true);
        }

        /// <summary>
        /// Create 2L intersections:
        /// </summary>
        private static void RoadArchitectUnitTest2() {
            //Create node locations:
            List<Vector3> tLocs = new List<Vector3>();
            float StartLocX = 800f;
            float StartLocY = 200f;
            float StartLocYSep = 200f;
            float tHeight = 20f;
            GSDRoad bRoad = null; //Buffer
            GSDRoad tRoad = null; //Buffer
            if (tRoad == null) {

            }

            //Create base road:
            tLocs.Clear();
            for (int i = 0; i < 9; i++) {
                tLocs.Add(new Vector3(StartLocX + (i * 200f), tHeight, 600f));
            }
            bRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);

            //Get road system, create road #1:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX, tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            //UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.NoTurnLane);

            //Get road system, create road #2:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 2f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            //UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            //Get road system, create road #3:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 4f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            //UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.BothTurnLanes);

            //Get road system, create road #4:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 6f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            //UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            //Get road system, create road #4:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 8f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            //UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            GSDRoadAutomation.CreateIntersections_ProgrammaticallyForRoad(bRoad, GSDRoadIntersection.iStopTypeEnum.None, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            //Now count road intersections, if not 5 throw error
            int iCount = 0;
            foreach (GSDSplineN tNode in bRoad.GSDSpline.mNodes) {
                if (tNode.bIsIntersection) {
                    iCount += 1;
                }
            }
            if (iCount != 5) {
               Debug.LogError("Unit Test #2 failed: " + iCount.ToString() + " intersections instead of 5.");
            }
        }

        /// <summary>
        /// This will create an intersection if two nodes overlap on the road. Only good if the roads only overlap once.
        /// </summary>
        /// <param name="bRoad"></param>
        /// <param name="tRoad"></param>
        private static void UnitTest_IntersectionHelper(GSDRoad bRoad, GSDRoad tRoad, GSDRoadIntersection.iStopTypeEnum iStopType, GSDRoadIntersection.RoadTypeEnum rType) {
            GSDSplineN tInter1 = null;
            GSDSplineN tInter2 = null;
            foreach (GSDSplineN tNode in bRoad.GSDSpline.mNodes) {
                foreach (GSDSplineN xNode in tRoad.GSDSpline.mNodes) {
                    if (GSDRootUtil.IsApproximately(Vector3.Distance(tNode.transform.position, xNode.transform.position), 0f, 0.05f)) {
                        tInter1 = tNode;
                        tInter2 = xNode;
                        break;
                    }
                }
            }

            if (tInter1 != null && tInter2 != null) {
                GameObject tInter = GSD.Roads.GSDIntersections.CreateIntersection(tInter1, tInter2);
                GSDRoadIntersection GSDRI = tInter.GetComponent<GSDRoadIntersection>();
                GSDRI.iStopType = iStopType;
                GSDRI.rType = rType;
            }
        }

        

        /// <summary>
        /// Create 4L intersections:
        /// </summary>
        private static void RoadArchitectUnitTest3() {
            //Create node locations:
            List<Vector3> tLocs = new List<Vector3>();
            float StartLocX = 200f;
            float StartLocY = 2500f;
            float StartLocYSep = 300f;
            float tHeight = 20f;
            GSDRoad bRoad; //Buffer
            GSDRoad tRoad; //Buffer

            //Create base road:
            tLocs.Clear();
            for (int i = 0; i < 9; i++) {
                tLocs.Add(new Vector3(StartLocX + (i * StartLocYSep), tHeight, StartLocY + (StartLocYSep * 2f)));
            }
            bRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            bRoad.opt_Lanes = 4;


            //Get road system, create road #1:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX, tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            tRoad.opt_Lanes = 4;
            UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            //Get road system, create road #2:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 2f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            tRoad.opt_Lanes = 4;
            UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.BothTurnLanes);

            //Get road system, create road #3:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 4f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            tRoad.opt_Lanes = 4;
            UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            //Get road system, create road #4:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 6f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            tRoad.opt_Lanes = 4;
            UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            //Get road system, create road #5:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(StartLocX + (StartLocYSep * 8f), tHeight, StartLocY + (i * StartLocYSep)));
            }
            tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);
            tRoad.opt_Lanes = 4;
            UnitTest_IntersectionHelper(bRoad, tRoad, GSDRoadIntersection.iStopTypeEnum.TrafficLight1, GSDRoadIntersection.RoadTypeEnum.TurnLane);

            //Now count road intersections, if not 5 throw error
            int iCount = 0;
            foreach (GSDSplineN tNode in bRoad.GSDSpline.mNodes) {
                if (tNode.bIsIntersection) {
                    iCount += 1;
                }
            }
            if (iCount != 5) {
                Debug.LogError("Unit Test #3 failed: " + iCount.ToString() + " intersections instead of 5.");
            }
        }

        //Large suspension bridge:
        private static void RoadArchitectUnitTest4() {
            //Create node locations:
            List<Vector3> tLocs = new List<Vector3>();

            //Create base road:
            tLocs.Clear();
            for (int i = 0; i < 5; i++) {
                tLocs.Add(new Vector3(3500f, 90f, 200f + (800f * i)));
            }
            GSDRoad tRoad = GSDRoadAutomation.CreateRoad_Programmatically((GSDRoadSystem)GameObject.Find("RoadArchitectSystem1").GetComponent<GSDRoadSystem>(), ref tLocs);

            //Suspension bridge:
            tRoad.GSDSpline.mNodes[1].bIsBridgeStart = true;
            tRoad.GSDSpline.mNodes[1].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[3].bIsBridgeEnd = true;
            tRoad.GSDSpline.mNodes[3].bIsBridgeMatched = true;
            tRoad.GSDSpline.mNodes[1].BridgeCounterpartNode = tRoad.GSDSpline.mNodes[3];
            tRoad.GSDSpline.mNodes[1].LoadWizardObjectsFromLibrary("SuspL-2L", true, true);
        }


    }
#endif
}
