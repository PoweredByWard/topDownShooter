﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class SoundHandler
    {
        static SoundPlayer gunShot = new SoundPlayer($"{Environment.CurrentDirectory}\\Sounds\\gun.wav");
        static SoundPlayer turretShot = new SoundPlayer($"{Environment.CurrentDirectory}\\Sounds\\turret.wav");
        public static bool muted { get; set; }
        static public void playGun()
        {
            if (muted) return;
            gunShot.Play();
        }
        
        static public void playTurret()
        {
            if (muted) return;
            turretShot.Play();
        }
    }
}
