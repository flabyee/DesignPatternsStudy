using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class SpawnController : MonoBehaviour
    {
        private Ghost ghostPrototype;
        private Demon demonPrototype;

        private Spawner[] monsterSpawners;

        private void Start()
        {
            ghostPrototype = new Ghost(100, 10);
            demonPrototype = new Demon(200, 5);

            monsterSpawners = new Spawner[]
            {
                new Spawner(ghostPrototype),
                new Spawner(demonPrototype)
            };
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                Spawner ghostSpawner = new Spawner(ghostPrototype);
                Ghost newGhost = ghostSpawner.SpawnerMonster() as Ghost;
                newGhost.Talk();
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                Spawner demonSpanwer = new Spawner(demonPrototype);
                Demon newDemon = demonSpanwer.SpawnerMonster() as Demon;
                newDemon.Talk();
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                Spawner randomSpanwer = monsterSpawners[Random.Range(0, monsterSpawners.Length)];
                Monster newMonster = randomSpanwer.SpawnerMonster();
                newMonster.Talk();
            }
        }
    }
}

