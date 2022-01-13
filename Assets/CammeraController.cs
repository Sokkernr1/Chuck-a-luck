using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CammeraController : MonoBehaviour
{
    [SerializeField]private GameObject coinPrefab;
    [SerializeField]private TextMeshProUGUI walletText;
    [SerializeField]private TextMeshPro goodWizardText;
    [SerializeField]private TextMeshPro badWizardText;
    [SerializeField]private List<string> goodWizardQuotes = new List<string>();
    [SerializeField]private List<string> badWizardQuotes = new List<string>();
    [SerializeField]private Button betBtn;
    [SerializeField]private Canvas gameOverCanvas;

    private int betOn1 = 0;
    private int betOn2 = 0;
    private int betOn3 = 0;
    private int betOn4 = 0;
    private int betOn5 = 0;
    private int betOn6 = 0;
    private int wallet = 5;
    private int goodDialogeCount = 0;
    private int badDialogeCount = 0;

    private void Start() {
        StartCoroutine(goodWizard());
        StartCoroutine(badWizard());
    }

    void Update () 
    {    
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 999f) && wallet > 0) 
            {
                Vector3 coinPos = new Vector3(hit.point.x, hit.point.y + 30, hit.point.z);
                switch(hit.collider.tag){
                    case "1":
                        betOn1++;
                        wallet--;
                        Instantiate(coinPrefab,coinPos,Quaternion.identity,GameObject.Find("Coins").transform);
                        break;
                    case "2":
                        betOn2++;
                        wallet--;
                        Instantiate(coinPrefab,coinPos,Quaternion.identity,GameObject.Find("Coins").transform);
                        break;
                    case "3":
                        betOn3++;
                        wallet--;
                        Instantiate(coinPrefab,coinPos,Quaternion.identity,GameObject.Find("Coins").transform);
                        break;
                    case "4":
                        betOn4++;
                        wallet--;
                        Instantiate(coinPrefab,coinPos,Quaternion.identity,GameObject.Find("Coins").transform);
                        break;
                    case "5":
                        betOn5++;
                        wallet--;
                        Instantiate(coinPrefab,coinPos,Quaternion.identity,GameObject.Find("Coins").transform);
                        break;
                    case "6":
                        betOn6++;
                        wallet--;
                        Instantiate(coinPrefab,coinPos,Quaternion.identity,GameObject.Find("Coins").transform);
                        break;
                }
                walletText.text = "Your wallet: " + wallet + " coins";
            } 
        }  
    }

    public void placeBet(){
        StopAllCoroutines();
        StartCoroutine(badWizard());
        StartCoroutine(goodWizard());

        foreach(Transform coinX in GameObject.Find("Coins").transform){
            Destroy(coinX.gameObject);
        }

        if(betOn1 + betOn2 + betOn3 + betOn4 + betOn5 + betOn6 > 0){
            int chosenNumber = Random.Range(1,7);
            int walletBefore = wallet;
            badWizardText.text = "I choose...\n" + chosenNumber;
            switch(chosenNumber){
                case 1:
                    wallet = wallet + betOn1 * 2;
                    break;
                case 2:
                    wallet = wallet + betOn2 * 2;
                    break;
                case 3:
                    wallet = wallet + betOn3 * 2;
                    break;
                case 4:
                    wallet = wallet + betOn4 * 2;
                    break;
                case 5:
                    wallet = wallet + betOn5 * 2;
                    break;
                case 6:
                    wallet = wallet + betOn6 * 2;
                    break;
            }
            if(walletBefore <= wallet){
                goodWizardText.text="Oh no...";
            }
            else {
                goodWizardText.text="Nice!";
            }

            walletText.text = "Your wallet: " + wallet + " coins";

            betOn1 = 0;
            betOn2 = 0;
            betOn3 = 0;
            betOn4 = 0;
            betOn5 = 0;
            betOn6 = 0;

            if(wallet <= 0){
                goodWizardText.text = "NOOOOOOOOO!";
                badWizardText.text = "YOU LOSER! MUAHAHA All the water will be mine";
                StopAllCoroutines();
                gameOverCanvas.gameObject.SetActive(true);
                betBtn.interactable = false;
            }
        } else {
            goodWizardText.text = "You have to bet moron...";
            badWizardText.text = "You want to beat me? You're not even smart enough for this game!";
        }
    }

    private IEnumerator goodWizard(){
        Debug.Log("started");
        while(true){
            goodWizardText.text = goodWizardQuotes[goodDialogeCount];
            goodDialogeCount++;
            if(goodDialogeCount >= goodWizardQuotes.Count){
                goodDialogeCount = 0;
            }
            yield return new WaitForSecondsRealtime(7f);
        }
    }

    private IEnumerator badWizard(){
        Debug.Log("started");
        while(true){
            badWizardText.text = badWizardQuotes[badDialogeCount];
            badDialogeCount++;
            if(badDialogeCount >= badWizardQuotes.Count){
                badDialogeCount = 0;
            }
            yield return new WaitForSecondsRealtime(7f);
        }
    }
}
