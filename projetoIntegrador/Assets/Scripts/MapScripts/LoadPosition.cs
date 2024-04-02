using UnityEngine;
using UnityEngine.SceneManagement;
using static VanMoviment;

public class LoadPosition : MonoBehaviour
{
    private bool hasLoaded = false;

    private void Start()
    {
        if (!hasLoaded && PlayerPrefs.HasKey("ObjectPosX"))
        {
            float posX = PlayerPrefs.GetFloat("ObjectPosX");
            float posY = PlayerPrefs.GetFloat("ObjectPosY");
            float posZ = PlayerPrefs.GetFloat("ObjectPosZ");
            float rotZ = PlayerPrefs.GetFloat("ObjectRotationZ");
            float rotW = PlayerPrefs.GetFloat("ObjectRotationW");
            string spriteName = PlayerPrefs.GetString("SpriteVan");
           
            Vector3 savedPosition = new Vector3(posX, posY, posZ);

            GameObject objectToMove = GameObject.FindGameObjectWithTag("Player");

            if (objectToMove != null)
            {
                objectToMove.transform.position = savedPosition;
                objectToMove.transform.rotation = new Quaternion(objectToMove.transform.rotation.x, objectToMove.transform.rotation.y, rotZ, rotW);

                if (spriteName == "Vanteca_Sprite_Cima")
                {
                    objectToMove.GetComponent<VanMoviment>().wheelRight.SetActive(false);
                    objectToMove.GetComponent<VanMoviment>().wheelLeft.SetActive(false);

                    objectToMove.GetComponent<SpriteRenderer>().sprite = objectToMove.GetComponent<VanMoviment>().spriteVanUpAndDown;
                }
                else if (spriteName == "Vanteca_Sprite_LadoEsquerdo")
                {
                    objectToMove.GetComponent<VanMoviment>().wheelRight.SetActive(false);
                    objectToMove.GetComponent<VanMoviment>().wheelLeft.SetActive(true);

                    objectToMove.GetComponent<SpriteRenderer>().sprite = objectToMove.GetComponent<VanMoviment>().spriteVanLeft;
                }
                else if (spriteName == "Vanteca_Sprite_LadoDireito")
                {
                    objectToMove.GetComponent<VanMoviment>().wheelRight.SetActive(true);
                    objectToMove.GetComponent<VanMoviment>().wheelLeft.SetActive(false);

                    objectToMove.GetComponent<SpriteRenderer>().sprite = objectToMove.GetComponent<VanMoviment>().spriteVanRight;
                }
            }

            hasLoaded = true;
        }
    }

    private void OnDestroy()
    {
        SaveObjectPosition();
    }

    private void SaveObjectPosition()
    {
        GameObject objectToSave = GameObject.FindGameObjectWithTag("Player");

        if (objectToSave != null)
        {
            PlayerPrefs.SetFloat("ObjectPosX", objectToSave.transform.position.x);
            PlayerPrefs.SetFloat("ObjectPosY", objectToSave.transform.position.y);
            PlayerPrefs.SetFloat("ObjectPosZ", objectToSave.transform.position.z);
            PlayerPrefs.SetFloat("ObjectRotationZ", objectToSave.transform.rotation.z);
            PlayerPrefs.SetFloat("ObjectRotationW", objectToSave.transform.rotation.w);
            PlayerPrefs.SetString("SpriteVan", objectToSave.GetComponent<SpriteRenderer>().sprite.name);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Objeto Nullo");
        }
    }
}
