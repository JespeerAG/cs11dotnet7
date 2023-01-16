using System.Collections;
using System;

namespace PillarsLib
{
    public class PillarsSet : IEnumerable<Pillar>
    {
        private readonly int MaxPillars = 50;
        public int Number;

        public List<Pillar> PillarList = new();
        /// <summary>
        /// Generates an initial PillarSet. This can only have at most MaxPillars (default: 50) total pillars.
        /// </summary>
        /// <param name="configuration">An array of booleans for the initial configuration of the pillars. The position in the array matches the position of the pillar.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public PillarsSet(bool[] configuration)
        {
            if (configuration is null)
            {
                throw new System.ArgumentNullException();
            }
            if (configuration.Length == 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (configuration.Length > MaxPillars)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            this.Number = configuration.Length;

            for (int i = 0; i < configuration.Length; i++)
            {
                PillarList.Add(new Pillar(id: i, status: configuration[i]));
            }
        }

        public IEnumerator<Pillar> GetEnumerator()
        {
            foreach(Pillar pillar in PillarList)
            {
                yield return pillar;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Pillar GetPillar(int position)
        {
            return PillarList[position];
        }

    }
}
