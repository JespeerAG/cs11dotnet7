using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packt.Shared
{
    public interface IPlayable
    {
        void Play();
        void Pause();
        
        void Stop()
        {
            WriteLine("Default implementation of Stop.");
        }
    }
}