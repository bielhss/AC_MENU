using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_MENU
{
    public class Offsets
    {

        public static int

            iViewMatrix = 0x00101AE8, //visão da matrix
            iLocalPlayer = 0x109B74, //Player 1
            iEntityList = 0x00110D90, // Entidades do jogo

            // offsets para classe entity

            vHead = 0x4, 
            vFeet = 0x34,
            vAngles = 0x40,
            iHealth = 0xF8,
            iDead = 0x138,
            sName = 0x225,
            iTeam = 0x32C,
            iCurrentAmmo = 0x150
        ;
    };
}
