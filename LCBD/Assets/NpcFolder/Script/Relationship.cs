using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relationship : MonoBehaviour
{
    private const string FRIENDSHIP_LAYER = "Friendship";
    private const string NEUTRALITY_LAYER = "Neutrality";
    private const string HOSTILITY_LAYER = "Hostility";
    private const string SKILL_EFFECT_LAYER = "SkillEffect";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;
        int otherLayer = otherObject.layer;

        if (otherLayer == LayerMask.NameToLayer(SKILL_EFFECT_LAYER))
        {
            if (gameObject.layer == LayerMask.NameToLayer(FRIENDSHIP_LAYER))
            {
                Debug.Log("우호모드 - SkillEffect와 충돌");
            }
            else if (gameObject.layer == LayerMask.NameToLayer(NEUTRALITY_LAYER))
            {
                Debug.Log("중립이 적대 - SkillEffect와 충돌");
                ChangeLayerAndLog(HOSTILITY_LAYER, "적대중");
            }
            else if (gameObject.layer == LayerMask.NameToLayer(HOSTILITY_LAYER))
            {
                Debug.Log("적대중 - SkillEffect와 충돌");
            }
        }
    }

    private void ChangeLayerAndLog(string newLayerName, string logMessage)
    {
        gameObject.layer = LayerMask.NameToLayer(newLayerName);
    }
}
