﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NPC Army Creator script created by Oussama Bouanani, SoumiDelRio.
 * This script is part of the Unity RTS Engine */

namespace RTSEngine
{
    public class NPCArmyCreator : NPCComponent
    {
        //holds the regulators of the army units
        public List<NPCUnitRegulator> armyUnitRegulators = new List<NPCUnitRegulator>();
        //holds the active instances of the above regulators:
        private List<NPCUnitRegulator> armyUnitRegulatorIns = new List<NPCUnitRegulator>();

        //forcing army creation = minimum amount of the active unit regulator instances will be incremented to force faction to create them.
        public bool forceArmyCreation = false;
        //timer at which the minimum amount of the above active regulators will be incremented in order to push for the army units creation
        public FloatRange forceCreationReloadRange = new FloatRange(10.0f, 15.0f);
        private float forceCreationTimer;

        private bool isActive = false; //is the component active?

        private void Start()
        {
            //if forcing the creation of the attack units is disabled
            if (forceArmyCreation == true)
                isActive = false; //deactivate component
            else //if the feature is enabled.
                isActive = true; //activate component

            ActivateArmyUnitRegulators();
        }

        //activate army unit regulators:
        public void ActivateArmyUnitRegulators ()
        {
            if (armyUnitRegulators.Count > 0)
            {
                armyUnitRegulatorIns.Clear(); //clear active instances list.
                //go through the unit regulator prefabs
                foreach (NPCUnitRegulator nue in armyUnitRegulators)
                {
                    //activate them and add them to the active regulators list
                    armyUnitRegulatorIns.Add(npcMgr.unitCreator_NPC.ActivateUnitRegulator(nue));
                }
            }
        }

        void Update()
        {
            if(isActive == true && forceArmyCreation == false)
            {
                isActive = false;

                //go through the active instances of the army unit regulators:
                foreach(NPCUnitRegulator nue in armyUnitRegulatorIns)
                {
                    //if the minimum amount hasn't hit the actual max amount
                    if(nue.GetMaxAmount() > nue.GetMinAmount())
                    {
                        isActive = true;
                        //increment the minimum amount:
                        nue.IncMinAmount();
                    }
                }
            }
        }
    }
}
