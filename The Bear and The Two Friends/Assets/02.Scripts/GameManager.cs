using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //-----------------------------------------------------------------------------------------
    // 캐릭터
    public string lanky = "홀쭉이";
    public string fatty = "통통이";
    public string bear = "곰";

    //-----------------------------------------------------------------------------------------
    // 대사
    public string lines1_1 = "홀쭉이와 통통이는 절친한 친구 사이에요.";
    public string lines1_2 = "둘은 함께 숲 속을 여행하는 중이었지요.";
    public string lines1_3 = "이야~! 날씨가 너무 화창하고 기분이 좋아!";    // 홀쭉이
    public string lines1_4 = "게다가 울창한 수풀이 만들어주는 그늘이 너무 시원해!";   // 통통이
    public string lines1_5 = "덕분에 우리가 챙겨온 양산은 필요가 없는걸?, 하하하";   // 홀쭉이
    public string lines1_6 = "너와 이렇게 함께 여행을 하니 정말 행복해.";   // 통통이
    public string lines1_7 = "우리 우정 앞으로도 영원하자.";

    public string selectableLines1_1 = "당연하지. 기쁠 때나 힘들 때나 우리 항상 함께하자!";
    public string selectableLines1_2 = "영원한 건 절대 없어. 언젠가는 우리도 멀어질거야.";
    public string selectableLines1_3 = "헛소리 하지 말고 그냥 걷기나 하렴.";

    public string resultLines1_1 = "나도 네가 어려울 때 가장 먼저 도와주는 친구가 될거야.";   // 통통이
    public string resultLines1_2 = "통통이는 마음이 무척 상했어요.";
    public string resultLines1_3 = "두 친구는 여행이 끝날 때 까지 거의 대화를 하지 않았답니다.";
    public string resultLines1_4 = "결국 두 친구는 어색한 사이가 되고 말았어요.";

    public string lines2_1 = "이제 슬슬 배가 고픈데. 너는 배 안고프니, 친구야?";   // 통통이
    public string lines2_2 = "나도 막 배가 고프던 참이야.";    // 홀쭉이
    public string lines2_3 = "우리 저 큰 나무 밑에 앉아서 챙겨온 음식을 먹자.";    // 홀쭉이
    public string lines2_4 = "그래, 그러자꾸나.";  // 통통이
    public string lines2_5 = "그 때, 갑자기 두 친구 앞에 커다랗고 무서운 곰이 나타났어요!";
    public string lines2_6 = "으악... 고, 고, 곰이다!!";   // 홀쭉이

    public string selectableLines2_1 = "곰과 싸운다";
    public string selectableLines2_2 = "혼자서 잽싸게 나무 위로 도망간다";
    public string selectableLines2_3 = "양산을 펼친다";
    public string selectableLines2_4 = "곰에게 대화를 시도한다";
    
    public string resultLines2_1_1 = "두 친구는 무시무시한 곰에게 목숨을 잃고 말았어요.";
    public string resultLines2_1_2 = "야생곰은 정말 무서운 동물이랍니다.";
    public string resultLines2_2_1 = "달리기가 느린 통통이는 도망칠 수가 없었어요.";
    public string resultLines2_2_2 = "그래서 바닥에 엎드려 죽은 척을 했지요.";
    public string resultLines2_2_3 = "곰이 통통이에게 다가왔어요.";
    public string resultLines2_2_4 = "그리고는 냄새를 킁킁 맡더니 그냥 가버렸답니다.";
    public string resultLines2_2_5 = "곰이 돌아간 후에 홀쭉이는 다시 나무 밑으로 내려왔어요.";
    public string resultLines2_2_6 = "통통이도 다시 일어났답니다.";
    public string resultLines2_3 = "양산을 펼치자 곰은 놀라서 돌아가버렸어요.";
    public string resultLines2_4_1 = "이봐, 곰 친구. 우리는 조용히 여행 중이니까 길을 비켜주지 않겠니?";  // 홀쭉이
    public string resultLines2_4_2 = "하지만 곰은 사람 말을 알아듣지 못하는군요.";

    public string resultLines3_1_1 = "그런데 친구야. 아까 곰이 너한테 뭐라고 속삭이는 것 같던데.";    // 홀쭉이
    public string resultLines3_1_2 = "뭐라고 그랬니?";
    public string resultLines3_1_3 = "위기에 처했을 때 혼자만 살겠다고 도망가는 녀석은 친구가 아니라더군.";  // 통통이
    public string resultLines3_2_1 = "정말 놀랬다니까. 휴우. 천만다행이야.";   // 홀쭉이
    public string resultLines3_2_2 = "그런데 어떻게 양산을 펼칠 생각을 했니. 친구야?"; // 통통이
    public string resultLines3_2_3 = "곰이 나타났을 때는 절대 등을 보이며 도망가서는 안되고,"; // 홀쭉이
    public string resultLines3_2_4 = "곰보다 더 커보이게끔 우산 같은 걸 펼치라고 배운 적이 있거든."; // 홀쭉이
    public string resultLines3_2_5 = "너는 정말 똑똑https://www.googleadservices.com/pagead/aclk?sa=L&ai=CIiVOA4-xW__QFISZ2gSStIbQAYaxvLtT_bXmwuAH2dkeEAEgz-6GPmCbo-eEvCmgAcLd2ssDyAECqQI2xp6daPkOPuACAKgDAcgDmQSqBJoCT9DTB7Npux2e6vCxVrnsSkKdo_k33uAuUWxomgD6z_kO0KpHtF9V2wZuAsJu_AbTxRpxWpzyAAM6d21SjPZKG68qMbWab3KwZnpE__6N-4UA_pZBg4oTOLZ88Tg68agYwobzkH1Zs1_Do2P-fspcpZMaBPPkmMkGsZhvJujZdJWgW_WI58E_8rotKdtfNg0XipvGOfBMZQgoYHCOOut2nVppB4J9Wmsvx4ketpUj-wmroLC2554lpEW5G2ijG1LKsa4YlLB1WBFTRTrUpgmzuMzdtTHMqvQ9SQhtRvumf4mb-45UN7x0SHOO1-p_h33_frvMSMruXzDSC5L2__JM_DVjS8Er302cCnpkycx0f16LwLgzknEVwBD-4AQBoAYCgAemoqU0qAeOzhuoB9XJG6gHqAaoB7oGqAfZyxuoB8_MG6gHpr4bqAeYzhuoB5oG2AcB0ggHCIBhEAEYAbEJa2dPFRK3M_KACgPYEws&num=1&cid=CAASEuRoNWxfeTFv_wcmne81E7yClg&sig=AOD64_3VNsPqXD5VCGjdYExrBIrggXaMzg&client=ca-pub-3899087055267599&nm=1&nx=10&ny=198&mb=1&adurl=https://store.musinsa.com/app/plan/views/5363%3Fsource%3DGDN_RE_IT_003한 친구구나. 네가 정말 자랑스러워!";    // 통통이
    public string resultLines3_2_6 = "둘은 무사히 여행을 마치고 더욱 절친한 친구가 되었답니다.";

    //-----------------------------------------------------------------------------------------

    public int ending;  // 0 : 오리지날 엔딩, 1 : 우산 엔딩

    //-----------------------------------------------------------------------------------------
    public bool inputAllowed = true;

    //-----------------------------------------------------------------------------------------
    // 싱글턴 인스턴스에 접근하기 위한 C# 프로퍼티
    // get 접근자만 가지는 읽기 전용의 프로퍼티
    public static GameManager Instance
    {
        // private 변수 instance의 참조를 반환한다
        get
        {
            return instance;
        }
    }
    //------------------------------------------------------------------------------------------
    private static GameManager instance = null;
    //------------------------------------------------------------------------------------------

    void Awake()
    {
        Application.targetFrameRate = 60;

        // --------------------------------- 싱글턴 -------------------------------------
        // 씬에 이미 인스턴스가 존재하는지 검사한다
        // 존재하는 경우 이 인스턴스는 소멸시킨다
        if (instance)   //get으로 자동으로 접근하게된다.
        {
            DestroyImmediate(gameObject);

            return; // return은 메소드 중간에 호출되어 메소드를 종결시킨다.. 따라서 Awake()를 완전히 빠져나온다.
        }

        // 이 인스턴스를 유효한 유일 오브젝트로 만든다
        instance = this;

        // 게임 매니저가 지속되도록 한다
        DontDestroyOnLoad(gameObject);
        //--------------------------------------------------------------------------------
    }
}
