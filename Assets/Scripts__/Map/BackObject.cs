using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BackObject : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Bloom bloom;

    void Start()
    {
        if (volume != null && volume.profile != null)
        {
            // Bloom 효과 가져오기
            if (volume.profile.TryGet(out bloom))
            {
                Debug.Log("Bloom 효과를 가져왔습니다!");
                bloom.intensity.value = 7.0f; 
            }
            else
            {
                Debug.LogWarning("Bloom 효과가 설정되지 않았습니다.");
            }
        }
        else
        {
            Debug.LogError("Volume 또는 Volume Profile을 찾을 수 없습니다.");
        }
    }
    
}
