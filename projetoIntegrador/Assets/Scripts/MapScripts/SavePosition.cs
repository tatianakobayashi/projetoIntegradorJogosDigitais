using UnityEngine;

public class SavePosition : MonoBehaviour
{
    private void OnDestroy()
    {
        SaveObjectPosition();
    }

    public void SaveObjectPosition()
    {
        PlayerPrefs.SetFloat("ObjectPosX", transform.position.x);
        PlayerPrefs.SetFloat("ObjectPosY", transform.position.y);
        PlayerPrefs.SetFloat("ObjectPosZ", transform.position.z);
        PlayerPrefs.SetFloat("ObjectRotationZ", transform.rotation.z);
        PlayerPrefs.SetFloat("ObjectRotationW", transform.rotation.w);
        PlayerPrefs.SetString("SpriteVan", GetComponent<SpriteRenderer>().sprite.name);
        PlayerPrefs.Save();
    }
}
