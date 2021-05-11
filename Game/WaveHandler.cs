using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class WaveHandler
    {
        private int wave;
        private Timer waveTimer;
        private int killed;
        private int spawned;
        private int maxspawn;
        GameHandler game;



        public WaveHandler(GameHandler gameData)
        {
            game = gameData;
            wave = 1;
            waveTimer = new Timer();
            waveTimer.Interval = 5000;
            waveTimer.Start();
            waveTimer.Tick += Timer_tick;
            maxspawn = 2;
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            if (maxspawn>spawned)
            {
                if (wave%10==0&&spawned==0)
                {
                    game.spawnZombie(2, 150 + wave * 10, 1,50);
                    spawned++;
                }
                else
                {
                game.spawnZombie(3, 40 + wave * 5, 0);
                spawned++;
                }
            }
        }

        public void newWave()
        {
            waveTimer.Stop();
            waveTimer.Interval = (4000 - (wave * 100)<4000? wave * 100:4000) +1000;
            waveTimer.Start();
            game.newWave();
            wave++;
            spawned = 0;
            killed = 0;
            maxspawn = wave+1;
        }

        public void zombieKilled()
        {
            killed++;
            if (killed == maxspawn)
            {
                newWave();
            }
        }

        public void play()
        {
            waveTimer.Start();
        }

        public void pause()
        {
            waveTimer.Stop();
        }

        public void setWave(int waveData)
        {
            wave = waveData;
        }

        public int getWave()
        {
            return wave;
        }

        public Timer getTimer()
        {
            return waveTimer;
        }
    }
}
