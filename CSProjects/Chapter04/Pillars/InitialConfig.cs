using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PillarsLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Pillars
{
    public class InitialConfig
    {
        public int PillarNumber;
        public bool[] PillarConfig;
        public int SwitchNumber;

        public PillarsSet PillarsSetConfig;
        public List<PillarSwitch> PillarSwitchConfig = new();

        public InitialConfig(string filename = "config.json")
        {
            var overallConfig = new { PillarNumber = 0, PillarInitialStatus = "", SwitchNumber = 0, SwitchLinks = ""};
            string json = System.IO.File.ReadAllText(filename);

            overallConfig = JsonConvert.DeserializeAnonymousType(json, overallConfig);
            PillarNumber = overallConfig.PillarNumber;

            PillarConfig = new bool[PillarNumber];


            string[] inputPillarConfig = (overallConfig.PillarInitialStatus).Split(' ');
            for (int i = 0; i < PillarNumber; i++)
            {
                PillarConfig[i] = bool.Parse(inputPillarConfig[i]);
            }

            PillarsSetConfig = new PillarsSet(PillarConfig);
            

            SwitchNumber = overallConfig.SwitchNumber;

            string[] inputSwitchConfig = (overallConfig.SwitchLinks).Split('|');
            for (int i = 0; i < SwitchNumber; i++)
            {
                string[] individualSwitchConfig = (inputSwitchConfig[i]).Split(' ');
                PillarSwitch addedSwitch = new();


                for (int j = 0; j < individualSwitchConfig.Length; j++)
                {
                    int linkedPillarId = int.Parse(individualSwitchConfig[j]);

                    addedSwitch.Link(linkedPillarId);
                }

                PillarSwitchConfig.Add(addedSwitch);
            }
        }
    }
}