using Pokemon3D.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Surface
{
    public class TallGrassSurface : WildOccurrenceSurface
    {
        [Header("variables")]
        [SerializeField] private LayerMask TallGrassLayerMask;

        // variables
        private Dictionary<Collider, TallGrass> tallGrasses = new();

        private void Start()
        {
            foreach (TallGrass tallGrass in GetComponentsInChildren<TallGrass>())
            {
                tallGrasses.TryAdd(tallGrass.Coll, tallGrass);
            }
        }

        public override void ExecuteSurfaceEvent(PlayerController player)
        {
            base.ExecuteSurfaceEvent(player);
            InteractRelatedTallGrass(player);
        }

        private void InteractRelatedTallGrass(PlayerController player)
        {
            Collider[] checkedSurfaces = Physics.OverlapSphere(player.transform.position, 0.2f, TallGrassLayerMask, QueryTriggerInteraction.Collide);
            if (checkedSurfaces.Length > 0)
            {
                for (int i = 0; i < checkedSurfaces.Length; ++i)
                {
                    if (!tallGrasses.TryGetValue(checkedSurfaces[i], out TallGrass tallGrass)) continue;
                    tallGrass.Interact(player.MoveDirection);
                }
            }
        }
    }
}
