using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AAA
{
    string sp;
    bool isit;
    public AAA(string sp, bool mIsIt)
    {
        this.sp = sp;
        isit = mIsIt;
    }
}

public class exam
{
    int id;
    AAA[] a;

    public exam(int mId, AAA[] mA)
    {
        id = mId;
        a = mA;


        AAA[] ac = new AAA[10];
        ac[0] = new AAA("hello", false);
        ac[1] = new AAA("hello", false);
        ac[2] = new AAA("hello", false);
    }
}

public class TTT
{
    List<exam> exams = new List<exam>();

    public void make()
    {
        AAA[] temp = new AAA[3];
        temp[0] = new AAA("hello", false);
        temp[0] = new AAA("helloff", false);
        temp[0] = new AAA("hellfffo", true);
        exams.Add(new exam(1000, temp));
        
    }
}

