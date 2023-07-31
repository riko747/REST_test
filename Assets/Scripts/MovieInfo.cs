using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovieInfo : MonoBehaviour
{
    [SerializeField] private RawImage movieImage;
    [SerializeField] private TextMeshProUGUI movieTitle;
    [SerializeField] private TextMeshProUGUI movieDescription;

    public RawImage GetMovieImage() => movieImage;

    public void SetMovieTitle(string titleString) => movieTitle.text = titleString;

    public void SetMovieDescription(string descriptionText) => movieDescription.text = descriptionText;
}
