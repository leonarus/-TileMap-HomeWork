using System;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    private List<Material> _colors = new();
    private void Awake()
    {
        var childRenderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in childRenderers)
        {
            _colors.Add(renderer.material);
        }
    }

    public void ReturnOriginalColor()
    {
        foreach (var material in _colors)
        {
            material.color = Color.white;
        }
    }
    
    public void HighlightTile(bool noOcupied)
    {
        if (noOcupied)
        {
            foreach (var material in _colors)
            {
                material.color = Color.green;
            }
        }
        else
        {
            foreach (var material in _colors)
            {
                material.color = Color.red;
            }
        }
    }
}
