using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject BLUE, RED;
    bool isPlayer, has_finished;

    [SerializeField]
    Text turnmsg;

    const string BLUE_TURN = "Blue's turn";
    const string RED_TURN = "Red's turn";

    Color BLUE_COLOR = new Color(1, 152, 206, 255)/255;
    Color RED_COLOR = new Color(207, 0, 0, 255) / 255;
    Board myBoard;


    private void Awake()
    {
        isPlayer = true;
        has_finished = false;
        turnmsg.text = BLUE_TURN;
        turnmsg.color = BLUE_COLOR;
        myBoard = new Board();
    }

    public void Reset()
    {
        Debug.Log("check3");
        bool sesw = reset.getDestroy();
        if (sesw==true)
        {
            Debug.Log("check2");
            myBoard = null;
            Debug.Log("check");
            myBoard = new Board();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("start");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if (has_finished) return;

            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousepos2d = new Vector2(mousepos.x, mousepos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousepos2d, Vector2.zero);

            if (!hit.collider) return;

            if (hit.collider.CompareTag("press"))
            {
                if (hit.collider.gameObject.GetComponent<Columns>().targetlocation.y > 4.66f) return;

                Vector3 spawnPos = hit.collider.gameObject.GetComponent<Columns>().spawnlocation;
                Vector3 targetPos = hit.collider.gameObject.GetComponent<Columns>().targetlocation;
                GameObject circle = Instantiate(isPlayer ? BLUE : RED);
                circle.transform.position = spawnPos;
                circle.GetComponent<Move>().targetposition = targetPos;


                hit.collider.gameObject.GetComponent<Columns>().targetlocation = new Vector3(targetPos.x, targetPos.y + 1.3f, targetPos.z);

                myBoard.updateBoard(hit.collider.gameObject.GetComponent<Columns>().col - 1, isPlayer);
                if (myBoard.result(isPlayer))
                {
                    turnmsg.text = (isPlayer ? "Blue" : "Red") + " Wins!";
                    has_finished= true;
                    Debug.Log(turnmsg.text);
                    return;
                }


                turnmsg.text = !isPlayer ? BLUE_TURN : RED_TURN;
                turnmsg.color = !isPlayer ? BLUE_COLOR: RED_COLOR;

                isPlayer = !isPlayer;
            }
        }
    }
}
