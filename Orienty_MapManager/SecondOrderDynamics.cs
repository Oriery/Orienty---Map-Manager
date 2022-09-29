using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Orienty_MapManager
{
    class SecondOrderDynamics
    {
        private Vector2 xp; // previous input 
        private Vector2 y, yd; // output and it's derivitive
        private Vector2 xd; // derivitive of input
        private float k1, k2, k3; // dynamics 

        public SecondOrderDynamics(float f, float z, float r, Vector2 x0, Vector2 y0) 
        { 
            k1 = (float)(z / (Math.PI * f));
            k2 = (float)(1 / (4 * Math.PI * Math.PI * f * f));
            k3 = (float)(r * z / (2 * Math.PI * f));

            xp = x0;
            y = y0;
            yd = new Vector2(0f, 0f);
        }


        public Vector2 Update(float DeltaTime, Vector2 x)
        {
            DeltaTime /= 1000;
            xd = (x - xp) / DeltaTime;
            xp = x;

            float k2_stable = Max(k2, 1.1f * (DeltaTime * DeltaTime / 4 + DeltaTime * k1 / 2));
            y += DeltaTime * yd; // integrate position by velocity
            yd += DeltaTime * (x + k3 * xd - y - k1 * yd) / k2_stable; // Integrate velocity by acceleration
            
            return y;
        }
    }
}
