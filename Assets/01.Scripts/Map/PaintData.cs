using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fruit.CameraM;
using Fruit.UIF;

namespace Fruit.Map
{
    public class PaintData : MonoBehaviour
    {
        public GameObject paintPanel;        
        public CameraMode_SeeingSomething seeingSomething;
        public PaintBoard paintBoard;
        public List<PaintScriptableObject> paints;
    }
}
