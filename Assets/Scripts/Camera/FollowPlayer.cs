using UnityEngine;

namespace Fruit.Camera
{
    public class FollowPlayer : MonoBehaviour
    {
        public GameObject player;
        public Vector3 delta;
        
        void LateUpdate()
        {
            transform.position = player.transform.position + delta;
            transform.LookAt(player.transform);
        }
    }
}