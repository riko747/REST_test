using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class MovieData
{
    public string backdrop_path;
    public string title;
    public string overview;
}

public class MovieDataWrapper
{
    public MovieData[] results;
}

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private MovieInfo movieInfoPrefab;
    [SerializeField] private WebRequest request;
    [SerializeField] private Transform scrollViewTransform;
    private void Start()
    {
        StartCoroutine(request.GetRequest(OnResponse));
    }

    private void OnResponse(string response)
    {
        var movies = JsonUtility.FromJson<MovieDataWrapper>(response).results;
        
        foreach (var movie in movies)
        {
            var currentMovieInfo = Instantiate(movieInfoPrefab, scrollViewTransform).GetComponent<MovieInfo>();
            StartCoroutine(LoadImage("https://image.tmdb.org/t/p/w500" + movie.backdrop_path, currentMovieInfo.GetMovieImage()));
            currentMovieInfo.SetMovieTitle(movie.title);
            currentMovieInfo.SetMovieDescription(movie.overview);
        }
    }
    
    private IEnumerator LoadImage(string imageURL, RawImage image)
    {
        var imageRequest = UnityWebRequestTexture.GetTexture(imageURL);
        yield return imageRequest.SendWebRequest();

        if (imageRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failed to load image: " + imageRequest.error);
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(imageRequest);
            Debug.Log("IMAGE: " + imageRequest);
            image.texture = texture;
        }
    }
}
