using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    private const string Bearer =
        "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMDI0MzMzNmUyZjk0OWVkYmEwNWZjNjU1ZGE0NTEwZSIsInN1YiI6IjVhYzFjM2IxMGUwYTI2NGE1NzA1NmEwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.uy3Lj5gCGGhxulu3ocPzJVh10f7KE_x1IDSE16CGzKw";

    private const string BaseUrl = "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc";
    private const string BearerToken = "Bearer " + Bearer;
    
    public IEnumerator GetRequest(System.Action<string> callback)
    {
        var request = UnityWebRequest.Get(BaseUrl);
        
        request.SetRequestHeader("Authorization", BearerToken);
        
        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success)
            Debug.Log("Error: " + request.error);
        else
            callback?.Invoke(request.downloadHandler.text);
    }
}
