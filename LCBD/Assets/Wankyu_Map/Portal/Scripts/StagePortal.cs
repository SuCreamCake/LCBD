using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StagePortal : MonoBehaviour
{
    public void TeleportToNext()
    {
        string sceneName  = SceneManager.GetActiveScene().name;  //현재 씬 이름

        /* 스테이지 순서
         * 프롤로그 -> 스테이지 1               (유아)
         * -> 스테이지 2                        (아동)
         * -> 스테이지 3                        (청년)
         * -> 스테이지 4                        (성인)
         * -> 스테이지 5 -> 에필로그 -> 엔딩      (노년) */

        //프롤로그, 스테이지1~5, 에필로그, 엔딩 중 / 스테이지1~4 씬 일때만 다음 스테이지로 이동
        if (sceneName.Equals("Stage1") && sceneName.Equals("Stage2") && sceneName.Equals("Stage3") && sceneName.Equals("Stage4"))  // TODO 아마도 씬 이름 변경 필요
        {
            char lastNum = sceneName.ElementAt(sceneName.Length - 1);   //씬 이름 끝에 가져와서
            lastNum++;  //1 더하고? (다음 번호로 바꾸고)

            sceneName = sceneName.Substring(0, sceneName.Length - 1) + lastNum; //씬이름 바꿔주고
            LoadingSceneController.LoadScene(sceneName);    //비동기 로딩 씬 호출
        }
        else if (sceneName.Equals("프롤로그")) //예시
        {
            sceneName = "Stage1";
            LoadingSceneController.LoadScene(sceneName);
        }


        //디버그용
        if (sceneName.Equals("RandomMap"))
        {
            LoadingSceneController.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}