using UnityEngine;

public class ToggleAllLights : MonoBehaviour
{
    private bool lightsOn = true;
    private bool lightmapsOn = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleLights(lightsOn, lightmapsOn);
        }
    }

    private void ToggleLights(bool state, bool lightmapState)
    {
        lightsOn = !state;
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            light.enabled = state;
        }

        ToggleLightmaps(lightmapState);

    }

    // separate function if you want to toggle lightmaps separately
    private void ToggleLightmaps(bool state)
{
    // if the state is the same as the current state, turn lightmaps off
    if (state == lightmapsOn)
    {
        state = false;
    }
    // if the state is different from the current state, turn lightmaps on
    else
    {
        state = true;
    }
    
    lightmapsOn = state;
    
    LightmapData[] lightmaps = LightmapSettings.lightmaps;
    foreach (LightmapData lightmap in lightmaps)
    {
        lightmap.lightmapColor = state ? lightmap.lightmapColor : null;
        lightmap.lightmapDir = state ? lightmap.lightmapDir : null;
    }
    LightmapSettings.lightmaps = lightmaps;
}

}
