using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manasoup.AI
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField]
        private float _combatDistance;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Vector2.Distance(transform.position, GameManager.Instance.player.transform.position) <= _combatDistance){
                Combat();
            }
        }


        private void Combat()
        {
            //Do combat stuff;
        }
    }
}