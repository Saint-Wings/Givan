using UnityEngine;

using UnityEngine;

public class AreaBoundary : MonoBehaviour
{
    public static Bounds CalculateTotalBounds(GameObject[] boundaryObjects)
    {
        if (boundaryObjects == null || boundaryObjects.Length == 0)
            return new Bounds(Vector3.zero, Vector3.zero);

        Bounds totalBounds = boundaryObjects[0].GetComponent<SpriteRenderer>().bounds;

        for (int i = 1; i < boundaryObjects.Length; i++)
        {
            SpriteRenderer renderer = boundaryObjects[i].GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                totalBounds.Encapsulate(renderer.bounds);
            }
        }

        // Уменьшаем границы на размер животного, чтобы они не застревали
        float padding = 0.5f;
        totalBounds.min += new Vector3(padding, padding, 0);
        totalBounds.max -= new Vector3(padding, padding, 0);

        return totalBounds;
    }
}