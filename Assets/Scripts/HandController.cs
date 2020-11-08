using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;

    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetHand(int index)
    {
        sprite.sprite = sprites[index];
    }
}
