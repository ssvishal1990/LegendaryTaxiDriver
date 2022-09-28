using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class TrackPlayer : MonoBehaviour
    {
        [SerializeField] GameObject player;
        // Update is called once per frame
        void Update()
        {
            Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = newPos;    
        }
    }
}

