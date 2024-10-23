using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class BounceTrajectory : MonoBehaviour
    {
        [SerializeField] private List<AnimationCurve> _bounceCurves;
        [SerializeField] private float maxBounceRadius = 1f;

        public int CurveCount => _bounceCurves.Count;

        public float GetBounceRadius(float time, int curve)
        {
            return _bounceCurves[curve].Evaluate(time) * maxBounceRadius;
        }
    }
}