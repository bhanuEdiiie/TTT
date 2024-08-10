using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

public class LoadMultipleObjectsFromRemoteServer : MonoBehaviour
{
    public List<string> objectNames;  

    void Start()
    {
        LoadAssetsByName(objectNames);
    }

    private void LoadAssetsByName(List<string> names)
    {
        foreach (var name in names)
        {
            
            Addressables.LoadAssetAsync<GameObject>(name).Completed += OnAssetLoaded;
        }
    }

    private void OnAssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Loaded from remote server: " + obj.DebugName);
            Instantiate(obj.Result);
        }
        else
        {
            Debug.LogError("Failed to load asset from remote server: " + obj.DebugName);
        }
    }

    // you can use this commented code for video load and play

    /*public AssetReference videoReference;  // AssetReference for the MP4 video

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane; // or other render modes like RenderTexture, MaterialOverride, etc.

        LoadAndPlayVideo();
    }

    private void LoadAndPlayVideo()
    {
        videoReference.LoadAssetAsync<VideoClip>().Completed += OnVideoLoaded;
    }

    private void OnVideoLoaded(AsyncOperationHandle<VideoClip> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            videoPlayer.clip = obj.Result;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Failed to load video from Addressable Asset.");
        }
    }*/
}
