using UnityEngine;

public class Grass : MonoBehaviour
{
    private GrassPlacer grassPlacer;

    public void Initialize(GrassPlacer placer)
    {
        this.grassPlacer = placer;
    }

    private void OnDestroy()
    {
        if (grassPlacer != null)
        {
            grassPlacer.GrassEaten(gameObject);
        }
    }
}