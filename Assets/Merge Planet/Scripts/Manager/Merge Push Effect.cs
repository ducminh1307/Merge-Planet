using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePushEffect : MonoBehaviour
{
    [Header(" Setting ")]
    [SerializeField] private float pushRadius;
    [SerializeField] private Vector2 minMaxMagnitude;
    [SerializeField] private float pushMagntitude;
    private Vector2 pushPosition;

    [Header(" Debugs ")]
    [SerializeField] private bool enableGizmos;
     
    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessCallBack;
        SettingManager.onPushMagnitudeSlideChanged += PushMagntitudeChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessCallBack;
        SettingManager.onPushMagnitudeSlideChanged -= PushMagntitudeChangedCallback;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MergeProcessCallBack(PlanetType planetType, Vector2 mergePos)
    {
        pushPosition = mergePos;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mergePos, pushRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Planet planet))
            {
                Vector2 force = ((Vector2)planet.transform.position - mergePos).normalized;
                force *= pushMagntitude;

                planet.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

    private void PushMagntitudeChangedCallback(float newPushMagnitude)
    {
        pushMagntitude = Mathf.Lerp(minMaxMagnitude.x, minMaxMagnitude.y, newPushMagnitude);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pushPosition, pushRadius);
    }
#endif
}
