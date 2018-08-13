using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI for the tutorial.
/// </summary>
public class UITutorial : MonoBehaviour
{
    /// <summary>
    /// The images to show.
    /// </summary>
    public Sprite[] sprites;

    public Image img;

    public int ptr;

    public GameObject prevButton;
    public GameObject nextButton;

    public void Prev()
    {
        this.ptr--;
    }

    public void Next()
    {
        this.ptr++;
    }

    public void Update()
    {
        this.img.sprite = this.sprites[this.ptr];

        this.prevButton.SetActive(this.ptr > 0);
        this.nextButton.SetActive(this.ptr < this.sprites.Length-1);
    }
}
