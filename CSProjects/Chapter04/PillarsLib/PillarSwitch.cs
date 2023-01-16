using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillarsLib
{
    public class PillarSwitch
    {
        public List<int> PillarsIdLinked = new();

        public PillarSwitch()
        {

        }
        /*
        public PillarSwitch(int[] pillarLinkedId, PillarsSet pillarsSet)
        {
            if (pillarLinkedId is null)
            {
                throw new System.Exception(message: "pillarLinkedId provided is null.");
            }
            if (pillarLinkedId.Length == 0)
            {
                throw new System.Exception(message: "pillarLinkedId provided is empty.");
            }
            
            
            foreach (int i in pillarLinkedId)
            {
                if (i >= pillarsSet.Number)
                {
                    throw new System.Exception(message: $"pillarLinkedId provided contains the value {i}, which is an invalid pillar Id.");
                }
                PillarsLinked.Add(pillarsSet.GetPillar(i));
            }
        }
        */

        public void Activate(PillarsSet pillarsSet)
        {
            foreach (Pillar pillar in pillarsSet)
            {
                if (PillarsIdLinked.Contains(pillar.Id))
                {
                    pillar.PillarSwitch();
                }
            }
        }
        public void Link(int pillarId)
        {
            PillarsIdLinked.Add(pillarId);
        }
    }
}