using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maps
{
    public class SplatGenerator : MonoBehaviour
    {
        public List<Splat> splats;


        public float minScale;
        public float maxScale;
        public float heightOffset;
        public float maxRadius;
        public float moveSpeed;
        public Transform parentTransform;
        public int sortingOrder;

        [Header("ColorConfig")]
        public float minH;
        public float maxH;
        public float minS;
        public float maxS;
        public float minV;
        public float maxV;

        public void DrawSplat(Vector3 worldHitPosition)
        {
            var scale = Random.Range(minScale, maxScale);
            var radius = Random.Range(0f, maxRadius);

            var splat = Instantiate(splats[Random.Range(0, splats.Count)]);

            splat.moveSpeed = moveSpeed;
            splat.transform.SetParent(parentTransform);

            var position = worldHitPosition + ((Vector3)Random.insideUnitCircle * radius) + Vector3.up * heightOffset;
            position.z = 0f;
            //position.y = -1f;

            splat.transform.position = position;
            splat.transform.localScale = Vector3.one * scale;

            var color = Color.HSVToRGB(Random.Range(minH, maxH), Random.Range(minS, maxS), Random.Range(minV, maxV));

            var spriteRenderer = splat.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.color = color;

            splat.gameObject.SetActive(true);
        }
    }

}
