using UnityEngine;
using Photon.Pun; 

public class Explosion : MonoBehaviourPunCallbacks
{
    public float explosionRadius = 10f; // 폭발 반경
    public float explosionForce = 10f; // 폭발 힘
    public LayerMask affectedLayers;   // 영향을 받을 레이어
    public Transform BoomEffect; 

    [PunRPC]
    public void Explode()
    {
        // 폭발 중심 좌표
        Vector2 explosionPosition = transform.position;

        // 폭발 반경 내의 모든 콜라이더 찾기
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius, affectedLayers);

        foreach (Collider2D hit in colliders)
        {
            // Rigidbody2D를 가진 오브젝트에만 힘을 가함
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 오브젝트 방향 설정 (폭발 중심에서 오브젝트까지)
                Vector2 direction = (rb.position - explosionPosition).normalized;

                // 거리 비례 감쇠 적용
                float distance = Vector2.Distance(rb.position, explosionPosition);
                float force = Mathf.Lerp(explosionForce, 0, distance / explosionRadius);

                if (BoomEffect != null)
                {
                    BoomEffect.gameObject.SetActive(true);    
                    Invoke("Off" , 1.0f);
                }
                
                Debug.Log("Booooom!!");
                // 힘 적
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    private void Off()
    {
        BoomEffect.gameObject.SetActive(false);
    }

    // 디버그용 폭발 반경 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}