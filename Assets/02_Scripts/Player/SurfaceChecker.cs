using Pokemon3D.Surface;
using UnityEngine;

namespace Pokemon3D.Player
{
    public class SurfaceChecker : MonoBehaviour
    {
        [Header("refereces")]
        [SerializeField] Transform checkPoint;

        [Header("variables")]
        [SerializeField] float checkRadius = 0.3f;
        [SerializeField] LayerMask surfaceLayerMask;

        public SurfaceBase CheckSurface()
        {
            Collider[] checkedSurfaces = Physics.OverlapSphere(checkPoint.position, checkRadius, surfaceLayerMask, QueryTriggerInteraction.Collide);
            if (checkedSurfaces.Length > 0)
            {
                if (checkedSurfaces.Length == 1)
                {
                    Debug.Log($"surface type: {checkedSurfaces[0].gameObject.name}");
                    return checkedSurfaces[0].GetComponent<SurfaceBase>();
                }
                else
                {
                    SurfaceBase result = checkedSurfaces[0].GetComponent<SurfaceBase>();
                    if (result is not WildOccurrenceSurface)
                    {
                        SurfaceBase temp;
                        for (int i = 1; i < checkedSurfaces.Length; ++i)
                        {
                            temp = checkedSurfaces[i].GetComponent<SurfaceBase>();
                            if (temp is WildOccurrenceSurface)
                            {
                                result = temp;
                                break;
                            }
                        }
                    }
                    Debug.Log($"surface type: {result.gameObject.name}");
                    return result;
                }
            }
            return null;
        }

        private void OnDrawGizmosSelected()
        {
            if (checkPoint == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(checkPoint.position, checkRadius);
        }
    }
}