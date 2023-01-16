// See https://aka.ms/new-console-template for more information
using PillarsLib;
using Pillars;

InitialConfig myConfig = new();

PillarsSet mySet = myConfig.PillarsSetConfig;

CanvasBuilder myCanvas = new(myConfig.PillarNumber, myConfig.SwitchNumber);

List < PillarSwitch > pillarSwitchesSet = new();

for (int i = 0; i < myConfig.SwitchNumber; i++)
{
    pillarSwitchesSet.Add(myConfig.PillarSwitchConfig[i]);
}


myCanvas.Game(mySet, pillarSwitchesSet);

