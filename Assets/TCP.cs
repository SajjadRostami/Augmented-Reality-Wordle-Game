using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using System.Linq;
using System.Text.RegularExpressions;
public class TCP : MonoBehaviour
{
	#region private members 	
	/// <summary> 	
	/// TCPListener to listen for incomming TCP connection 	
	/// requests. 	
	/// </summary> 	
	private TcpListener tcpListener;
	/// <summary> 
	/// Background thread for TcpServer workload. 	
	/// </summary> 	
	private Thread tcpListenerThread;
	/// <summary> 	
	/// Create handle to connected tcp client. 	
	/// </summary> 	
	private TcpClient connectedTcpClient;
    #endregion
    int row_number = 1;
    public TextMeshPro textMeshPro = null;

	ArrayList gemeObjects = new ArrayList();
	public string step = null;
	private GameObject tempObject;
	public string messageBoard = null;
	string line;
	string temp = "";
	string tag;
	string SecretKey = "BASIN";
    char[] greenLettersFirstStep = { '-', '-', '-', '-', '-' };
    char[] greenLettersSecondStep = { '-', '-', '-', '-', '-' };
    char[] greenLettersThirdStep = { '-', '-', '-', '-', '-' };
    char[] greenLettersFourthtStep = { '-', '-', '-', '-', '-' };
    char[] greenLettersFivethStep = { '-', '-', '-', '-', '-' };
    char[] greenLettersSixthStep = { '-', '-', '-', '-', '-' };
    char[] yellowLettersFirstStep = { '-', '-', '-', '-', '-' };
    char[] yellowLettersSecondStep = { '-', '-', '-', '-', '-' };
    char[] yellowLettersThirdStep = { '-', '-', '-', '-', '-' };
    char[] yellowLettersFourthtStep = { '-', '-', '-', '-', '-' };
    char[] yellowLettersFivethStep = { '-', '-', '-', '-', '-' };
    char[] yellowLettersSixthStep = { '-', '-', '-', '-', '-' };
    char[] grayLettersFirstStep = { '-', '-', '-', '-', '-' };
    char[] grayLettersSecondStep = { '-', '-', '-', '-', '-' };
    char[] grayLettersThirdStep = { '-', '-', '-', '-', '-' };
    char[] grayLettersFourthtStep = { '-', '-', '-', '-', '-' };
    char[] grayLettersFivethStep = { '-', '-', '-', '-', '-' };
    char[] grayLettersSixthStep = { '-', '-', '-', '-', '-' };
    int dupcounter = 0;
    List<string> CandidateWords = new List<string>();
    List<string> noDupes = null;
    bool flag = false;
	
	void Start()
	{
		// Start TcpServer background thread 		
		tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
		tcpListenerThread.IsBackground = true;
		tcpListenerThread.Start();
		






	}

	// Update is called once per frame
	void Update()
	{
        Debug.Log("before if: " + flag);

        if (flag)
        {

            temp = temp.Replace("Type Here Your Guess:", "");
            temp = temp.Replace("Length of the word must be five!", "");
            temp = temp.Replace("The entered word is not in dictionary!", "");
            temp = Regex.Replace(temp, @"\t|\n|\r", "");
            if (temp.Length != 5)
            {

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("VirtualBoard"))
                {
                    TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                    if (renderer != null)
                    {

                        renderer.text = "Length of the word must be five!\n"; ;


                    }
                }

                flag = false;
            }
            else
            {
                System.IO.StreamReader file =
        new System.IO.StreamReader("Assets/Dataset.txt");
                while ((line = file.ReadLine()) != null)
                {
                    if (temp.Equals(line))
                    {
                        flag = true;
                        file.Close();
                        break;
                    }

                }



            }
            if (temp == "TRAIN") {
                temp = temp;
            
            }
            SendMessageSajjad(temp);
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("VirtualBoard"))
            {
                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                if (renderer != null)
                {

                    renderer.text = ""; ;


                }
            }

            for (int i = 0; i < SecretKey.Length; i++)
            {
                int j = i + 1;
                if (row_number == 1)
                {
                    if (SecretKey[i] == temp[i])
                    {
                        greenLettersFirstStep[i] = temp[i];

                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.color = Color.green;
                                renderer.enabled = true;
                                renderer.text = temp[i].ToString();

                            }
                        }

                        //give me all the words that index[i] == temp[i]


                    }
                    else if (SecretKey.Contains(temp[i].ToString()))
                    {
                        for (int s = 0; s < 5; s++)
                        {
                            if (temp[s] == temp[i])
                            {
                                dupcounter++;
                            }

                        }
                        if (dupcounter > 1)
                        {

                            tag = row_number + "-" + j;
                            yellowLettersFirstStep[i] = temp[i];
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.gray;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }

                        }

                        else
                        {
                            yellowLettersFirstStep[i] = temp[i];


                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.yellow;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }
                        }
                        dupcounter = 0;

                    }
                    else
                    {
                        grayLettersFirstStep[i] = temp[i];
                        //change to grey
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.enabled = true;
                                renderer.color = Color.gray;
                                renderer.text = temp[i].ToString();


                            }
                        }
                    }





                }



                //second word

                if (row_number == 2)
                {
                    if (SecretKey[i] == temp[i])
                    {
                        greenLettersSecondStep[i] = temp[i];
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.color = Color.green;
                                renderer.enabled = true;
                                renderer.text = temp[i].ToString();

                            }
                        }

                    }
                    else if (SecretKey.Contains(temp[i].ToString()))
                    {

                        for (int s = 0; s < 5; s++)
                        {
                            if (temp[s] == temp[i])
                            {
                                dupcounter++;
                            }

                        }
                        if (dupcounter > 1)
                        {
                            yellowLettersSecondStep[i] = temp[i];
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.gray;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }
                        }
                        else
                        {
                            yellowLettersSecondStep[i] = temp[i];
                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.yellow;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }




                        }
                        dupcounter = 0;

                    }
                    else
                    {
                        grayLettersSecondStep[i] = temp[i];
                        //change to grey
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.enabled = true;
                                renderer.color = Color.gray;
                                renderer.text = temp[i].ToString();


                            }
                        }
                    }
                }



                //third word

                if (row_number == 3)
                {
                    if (SecretKey[i] == temp[i])
                    {
                        greenLettersThirdStep[i] = temp[i];
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.color = Color.green;
                                renderer.enabled = true;
                                renderer.text = temp[i].ToString();

                            }
                        }

                    }
                    else if (SecretKey.Contains(temp[i].ToString()))
                    {
                        for (int s = 0; s < 5; s++)
                        {
                            if (temp[s] == temp[i])
                            {
                                dupcounter++;
                            }

                        }
                        if (dupcounter > 1)
                        {
                            yellowLettersThirdStep[i] = temp[i];

                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.gray;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }



                        }
                        else
                        {
                            yellowLettersThirdStep[i] = temp[i];

                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.yellow;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }
                        }

                        dupcounter = 0;

                    }
                    else
                    {
                        grayLettersThirdStep[i] = temp[i];

                        //change to grey
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.enabled = true;
                                renderer.color = Color.gray;
                                renderer.text = temp[i].ToString();


                            }
                        }
                    }
                }


                //fourth word

                if (row_number == 4)
                {
                    if (SecretKey[i] == temp[i])
                    {
                        greenLettersFourthtStep[i] = temp[i];
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.color = Color.green;
                                renderer.enabled = true;
                                renderer.text = temp[i].ToString();

                            }
                        }

                    }
                    else if (SecretKey.Contains(temp[i].ToString()))
                    {
                        for (int s = 0; s < 5; s++)
                        {
                            if (temp[s] == temp[i])
                            {
                                dupcounter++;
                            }

                        }
                        if (dupcounter > 1)
                        {
                            yellowLettersFourthtStep[i] = temp[i];
                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.gray;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }






                        }
                        else
                        {
                            yellowLettersFourthtStep[i] = temp[i];
                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.yellow;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }
                        }
                        dupcounter = 0;
                    }
                    else
                    {
                        grayLettersFourthtStep[i] = temp[i];
                        //change to grey
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.enabled = true;
                                renderer.color = Color.gray;
                                renderer.text = temp[i].ToString();


                            }
                        }
                    }
                }
                //five word

                if (row_number == 5)
                {
                    if (SecretKey[i] == temp[i])
                    {
                        greenLettersFivethStep[i] = temp[i];
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.color = Color.green;
                                renderer.enabled = true;
                                renderer.text = temp[i].ToString();

                            }
                        }

                    }
                    else if (SecretKey.Contains(temp[i].ToString()))
                    {
                        for (int s = 0; s < 5; s++)
                        {
                            if (temp[s] == temp[i])
                            {
                                dupcounter++;
                            }

                        }
                        if (dupcounter > 1)
                        {
                            yellowLettersFivethStep[i] = temp[i];
                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.gray;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }



                        }
                        else
                        {
                            yellowLettersFivethStep[i] = temp[i];
                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.yellow;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }
                        }
                        dupcounter = 0;
                    }
                    else
                    {
                        grayLettersFivethStep[i] = temp[i];
                        //change to grey
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.enabled = true;
                                renderer.color = Color.gray;
                                renderer.text = temp[i].ToString();


                            }
                        }
                    }
                }
                //sixth word

                if (row_number == 6)
                {
                    if (SecretKey[i] == temp[i])
                    {
                        greenLettersSixthStep[i] = temp[i];
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.color = Color.green;
                                renderer.enabled = true;
                                renderer.text = temp[i].ToString();

                            }
                        }

                    }
                    else if (SecretKey.Contains(temp[i].ToString()))
                    {
                        for (int s = 0; s < 5; s++)
                        {
                            if (temp[s] == temp[i])
                            {
                                dupcounter++;
                            }

                        }
                        if (dupcounter > 1)
                        {
                            yellowLettersSixthStep[i] = temp[i];
                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.gray;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }



                        }
                        else
                        {
                            yellowLettersSixthStep[i] = temp[i];
                            //change to orange
                            tag = row_number + "-" + j;
                            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                            {
                                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                                if (renderer != null)
                                {
                                    renderer.color = Color.yellow;

                                    renderer.enabled = true;
                                    renderer.text = temp[i].ToString();

                                }
                            }
                        }
                        dupcounter = 0;
                    }
                    else
                    {
                        grayLettersSixthStep[i] = temp[i];
                        //change to grey
                        tag = row_number + "-" + j;
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
                        {
                            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                            if (renderer != null)
                            {
                                renderer.enabled = true;
                                renderer.color = Color.gray;
                                renderer.text = temp[i].ToString();


                            }
                        }
                    }



                }

                //cheeck the candidate words first step
                if (row_number == 1 && i == 4)
                {
                    string tempCandidateWord = "";



                    char[] lisa = { '-', '-', '-', '-', '-' };
                    foreach (string line1 in System.IO.File.ReadLines(@"Assets/Dataset.txt"))
                    {

                        if (line1.Equals("AGAIN"))
                        {
                            tempCandidateWord = "";

                        }
                        lisa[0] = '-';
                        lisa[1] = '-';
                        lisa[2] = '-';
                        lisa[3] = '-';
                        lisa[4] = '-';



                        if (line1.Contains(grayLettersFirstStep[0].ToString()))
                        {
                            continue;
                        }
                        if (line1.Contains(grayLettersFirstStep[1].ToString()))
                        {
                            continue;
                        }
                        if (line1.Contains(grayLettersFirstStep[2].ToString()))
                        {
                            continue;
                        }
                        if (line1.Contains(grayLettersFirstStep[3].ToString()))
                        {
                            continue;
                        }
                        if (line1.Contains(grayLettersFirstStep[4].ToString()))
                        {
                            continue;
                        }
                        for (int ii = 0; ii < 5; ii++)
                        {
                            if (line1[ii] == greenLettersFirstStep[ii])

                            {

                                lisa[ii] = greenLettersFirstStep[ii];


                            }

                        }


                        var differentChars = lisa.Except(greenLettersFirstStep);
                        int counter = 0;
                        foreach (var character in differentChars)
                        {


                            counter++;
                        }


                        differentChars = greenLettersFirstStep.Except(lisa);

                        foreach (var character in differentChars)
                        {


                            counter++;
                        }



                        if (counter == 0)
                        {
                            lisa[0] = '-';
                            lisa[1] = '-';
                            lisa[2] = '-';
                            lisa[3] = '-';
                            lisa[4] = '-';



                            for (int ii = 0; ii < 5; ii++)
                            {
                                if (yellowLettersFirstStep[ii] != '-')

                                {

                                    lisa[ii] = yellowLettersFirstStep[ii];


                                }

                            }

                            for (int y = 0; y < 5; y++)
                            {
                                if (lisa[y] != '-' && !line1.Contains(lisa[y]))
                                {
                                    counter++;
                                }
                            }


                            if (counter == 0)
                            {
                                CandidateWords.Add(line1);

                            }



                        }

                    }
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("CandidateWords"))
                    {
                        TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                        if (renderer != null)
                        {
                            string tempVirtualBoard = "";

                            noDupes = CandidateWords.Distinct().ToList();
                            foreach (var line1 in noDupes)
                            {

                                tempVirtualBoard = tempVirtualBoard + "," + line1;
                            }

                            renderer.enabled = true;
                            renderer.text = tempVirtualBoard;

                        }
                    }

                }

                //check the candidate words for the second step
                if (row_number == 2 && i == 4)
                {
                    string tempCandidateWord = "";



                    char[] lisa = { '-', '-', '-', '-', '-' };

                    foreach (var line1 in noDupes)
                    {



                        if (line1.Equals("BRAWN"))
                        {
                            tempCandidateWord = "";

                        }
                        lisa[0] = '-';
                        lisa[1] = '-';
                        lisa[2] = '-';
                        lisa[3] = '-';
                        lisa[4] = '-';



                        if (line1.Contains(grayLettersSecondStep[0].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSecondStep[1].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSecondStep[2].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSecondStep[3].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSecondStep[4].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        for (int ii = 0; ii < 5; ii++)
                        {
                            if (line1[ii] == greenLettersSecondStep[ii])

                            {

                                lisa[ii] = greenLettersSecondStep[ii];


                            }

                        }


                        var differentChars = lisa.Except(greenLettersSecondStep);
                        int counter = 0;
                        foreach (var character in differentChars)
                        {


                            counter++;
                        }


                        differentChars = greenLettersSecondStep.Except(lisa);

                        foreach (var character in differentChars)
                        {


                            counter++;
                        }

                        if (counter != 0)
                        {
                            CandidateWords.Remove(line1);

                        }

                        if (counter == 0)
                        {
                            counter = 0;
                            for (int jj = 0; jj < 5; jj++)
                            {
                                if ((yellowLettersSecondStep[jj].ToString() != "-" && yellowLettersSecondStep[jj] == line1[jj]) || yellowLettersSecondStep[jj].ToString() != "-" && !line1.Contains(yellowLettersSecondStep[jj]))
                                {
                                    counter++;



                                }

                            }
                            if (counter == 0)
                            {
                                if (!CandidateWords.Contains(line1))
                                    CandidateWords.Add(line1);
                            }
                            else
                            {
                                CandidateWords.Remove(line1);


                            }



                        }
                        else
                        {
                            CandidateWords.Remove(line1);

                        }


                    }

                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("CandidateWords"))
                    {
                        TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                        if (renderer != null)
                        {
                            string tempVirtualBoard = "";
                            renderer.text = "";
                            noDupes = CandidateWords.Distinct().ToList();
                            foreach (var line1 in noDupes)
                            {

                                tempVirtualBoard = tempVirtualBoard + "," + line1;
                            }

                            renderer.enabled = true;
                            renderer.text = tempVirtualBoard;

                        }
                    }


                }


                //check the candidate words for the third step
                if (row_number == 3 && i == 4)
                {
                    string tempCandidateWord = "";



                    char[] lisa = { '-', '-', '-', '-', '-' };

                    foreach (var line1 in noDupes)
                    {



                        if (line1.Equals("TRAIN"))
                        {
                            tempCandidateWord = "";

                        }
                        lisa[0] = '-';
                        lisa[1] = '-';
                        lisa[2] = '-';
                        lisa[3] = '-';
                        lisa[4] = '-';



                        if (line1.Contains(grayLettersThirdStep[0].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersThirdStep[1].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersThirdStep[2].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersThirdStep[3].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersThirdStep[4].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        for (int ii = 0; ii < 5; ii++)
                        {
                            if (line1[ii] == greenLettersThirdStep[ii])

                            {

                                lisa[ii] = greenLettersThirdStep[ii];


                            }

                        }


                        var differentChars = lisa.Except(greenLettersThirdStep);
                        int counter = 0;
                        foreach (var character in differentChars)
                        {


                            counter++;
                        }


                        differentChars = greenLettersThirdStep.Except(lisa);

                        foreach (var character in differentChars)
                        {


                            counter++;
                        }

                        if (counter != 0 && !CandidateWords.Contains(line1))
                        {
                            CandidateWords.Remove(line1);

                        }

                        if (counter == 0)
                        {
                            counter = 0;
                            for (int jj = 0; jj < 5; jj++)
                            {
                                if ((yellowLettersThirdStep[jj].ToString() != "-" && yellowLettersThirdStep[jj] == line1[jj]) || yellowLettersThirdStep[jj].ToString() != "-" && !line1.Contains(yellowLettersThirdStep[jj]))

                                {
                                    counter++;



                                }
                            }
                            if (counter == 0)
                            {
                                if (!CandidateWords.Contains(line1))
                                    CandidateWords.Add(line1);
                            }
                            else
                            {
                          
                                CandidateWords.Remove(line1);

                            }



                        }
                        else
                        {
                            CandidateWords.Remove(line1);

                        }


                    }

                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("CandidateWords"))
                    {
                        TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                        if (renderer != null)
                        {
                            string tempVirtualBoard = "";
                            renderer.text = "";
                            noDupes = CandidateWords.Distinct().ToList();
                            foreach (var line1 in noDupes)
                            {

                                tempVirtualBoard = tempVirtualBoard + "," + line1;
                            }

                            renderer.enabled = true;
                            renderer.text = tempVirtualBoard;

                        }
                    }


                }





                //check the candidate words for the fourth step
                if (row_number == 4 && i == 4)
                {
                    string tempCandidateWord = "";



                    char[] lisa = { '-', '-', '-', '-', '-' };

                    foreach (var line1 in noDupes)
                    {



                        if (line1.Equals("CIVIL"))
                        {
                            tempCandidateWord = "";

                        }
                        lisa[0] = '-';
                        lisa[1] = '-';
                        lisa[2] = '-';
                        lisa[3] = '-';
                        lisa[4] = '-';



                        if (line1.Contains(grayLettersFourthtStep[0].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFourthtStep[1].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFourthtStep[2].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFourthtStep[3].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFourthtStep[4].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        for (int ii = 0; ii < 5; ii++)
                        {
                            if (line1[ii] == greenLettersFourthtStep[ii])

                            {

                                lisa[ii] = greenLettersFourthtStep[ii];


                            }

                        }


                        var differentChars = lisa.Except(greenLettersFourthtStep);
                        int counter = 0;
                        foreach (var character in differentChars)
                        {


                            counter++;
                        }


                        differentChars = greenLettersFourthtStep.Except(lisa);

                        foreach (var character in differentChars)
                        {


                            counter++;
                        }

                        if (counter != 0 && !CandidateWords.Contains(line1))
                        {
                            CandidateWords.Remove(line1);

                        }

                        if (counter == 0)
                        {
                            counter = 0;
                            for (int jj = 0; jj < 5; jj++)
                            {
                                if ((yellowLettersFourthtStep[jj].ToString() != "-" && yellowLettersFourthtStep[jj] == line1[jj]) || yellowLettersFourthtStep[jj].ToString() != "-" && !line1.Contains(yellowLettersFourthtStep[jj]))

                                {
                                    counter++;



                                }
                            }
                            if (counter == 0)
                            {
                                if (!CandidateWords.Contains(line1))
                                    CandidateWords.Add(line1);
                            }
                            else
                            {
                                CandidateWords.Remove(line1);


                            }



                        }
                        else
                        {
                            CandidateWords.Remove(line1);

                        }


                    }

                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("CandidateWords"))
                    {
                        TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                        if (renderer != null)
                        {
                            string tempVirtualBoard = "";
                            renderer.text = "";
                            noDupes = CandidateWords.Distinct().ToList();
                            foreach (var line1 in noDupes)
                            {

                                tempVirtualBoard = tempVirtualBoard + "," + line1;
                            }

                            renderer.enabled = true;
                            renderer.text = tempVirtualBoard;

                        }
                    }


                }




                //check the candidate words for the fiveth step
                if (row_number == 5 && i == 4)
                {
                    string tempCandidateWord = "";



                    char[] lisa = { '-', '-', '-', '-', '-' };

                    foreach (var line1 in noDupes)
                    {



                        if (line1.Equals("CIVIL"))
                        {
                            tempCandidateWord = "";

                        }
                        lisa[0] = '-';
                        lisa[1] = '-';
                        lisa[2] = '-';
                        lisa[3] = '-';
                        lisa[4] = '-';



                        if (line1.Contains(grayLettersFivethStep[0].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFivethStep[1].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFivethStep[2].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFivethStep[3].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersFivethStep[4].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        for (int ii = 0; ii < 5; ii++)
                        {
                            if (line1[ii] == greenLettersFivethStep[ii])

                            {

                                lisa[ii] = greenLettersFivethStep[ii];


                            }

                        }


                        var differentChars = lisa.Except(greenLettersFivethStep);
                        int counter = 0;
                        foreach (var character in differentChars)
                        {


                            counter++;
                        }


                        differentChars = greenLettersFivethStep.Except(lisa);

                        foreach (var character in differentChars)
                        {


                            counter++;
                        }

                        if (counter != 0 && !CandidateWords.Contains(line1))
                        {
                            CandidateWords.Remove(line1);

                        }

                        if (counter == 0)
                        {
                            counter = 0;
                            for (int jj = 0; jj < 5; jj++)
                            {
                                if ((yellowLettersFourthtStep[jj].ToString() != "-" && yellowLettersFourthtStep[jj] == line1[jj]) || yellowLettersFourthtStep[jj].ToString() != "-" && !line1.Contains(yellowLettersFourthtStep[jj]))
                                {
                                    counter++;



                                }
                            }
                            if (counter == 0)
                            {
                                if (!CandidateWords.Contains(line1))
                                    CandidateWords.Add(line1);
                            }
                            else
                            {
                                CandidateWords.Remove(line1);

                            }



                        }
                        else
                        {
                            CandidateWords.Remove(line1);

                        }


                    }

                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("CandidateWords"))
                    {
                        TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                        if (renderer != null)
                        {
                            string tempVirtualBoard = "";
                            renderer.text = "";
                            noDupes = CandidateWords.Distinct().ToList();
                            foreach (var line1 in noDupes)
                            {

                                tempVirtualBoard = tempVirtualBoard + "," + line1;
                            }

                            renderer.enabled = true;
                            renderer.text = tempVirtualBoard;

                        }
                    }


                }






                //check the candidate words for the sixth step
                if (row_number == 5 && i == 4)
                {
                    string tempCandidateWord = "";



                    char[] lisa = { '-', '-', '-', '-', '-' };

                    foreach (var line1 in noDupes)
                    {



                        if (line1.Equals("CIVIL"))
                        {
                            tempCandidateWord = "";

                        }
                        lisa[0] = '-';
                        lisa[1] = '-';
                        lisa[2] = '-';
                        lisa[3] = '-';
                        lisa[4] = '-';



                        if (line1.Contains(grayLettersSixthStep[0].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSixthStep[1].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSixthStep[2].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSixthStep[3].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        if (line1.Contains(grayLettersSixthStep[4].ToString()))
                        {
                            CandidateWords.Remove(line1);
                            continue;
                        }
                        for (int ii = 0; ii < 5; ii++)
                        {
                            if (line1[ii] == greenLettersSixthStep[ii])

                            {

                                lisa[ii] = greenLettersSixthStep[ii];


                            }

                        }


                        var differentChars = lisa.Except(greenLettersSixthStep);
                        int counter = 0;
                        foreach (var character in differentChars)
                        {


                            counter++;
                        }


                        differentChars = greenLettersSixthStep.Except(lisa);

                        foreach (var character in differentChars)
                        {


                            counter++;
                        }

                        if (counter != 0 && !CandidateWords.Contains(line1))
                        {
                            CandidateWords.Remove(line1);

                        }

                        if (counter == 0)
                        {
                            counter = 0;
                            for (int jj = 0; jj < 5; jj++)
                            {
                                if ((yellowLettersFivethStep[jj].ToString() != "-" && yellowLettersFivethStep[jj] == line1[jj]) || yellowLettersFivethStep[jj].ToString() != "-" && !line1.Contains(yellowLettersFivethStep[jj]))
                                {
                                    counter++;



                                }
                            }
                            if (counter == 0)
                            {
                                if (!CandidateWords.Contains(line1))
                                    CandidateWords.Add(line1);
                            }
                            else
                            {
                                CandidateWords.Remove(line1);


                            }



                        }
                        else
                        {
                            CandidateWords.Remove(line1);

                        }


                    }

                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("CandidateWords"))
                    {
                        TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                        if (renderer != null)
                        {
                            string tempVirtualBoard = "";
                            renderer.text = "";
                            noDupes = CandidateWords.Distinct().ToList();
                            foreach (var line1 in noDupes)
                            {

                                tempVirtualBoard = tempVirtualBoard + "," + line1;
                            }

                            renderer.enabled = true;
                            renderer.text = tempVirtualBoard;

                        }
                    }


                }



            }

            row_number++;

            if (row_number > 6 && !temp.Equals(SecretKey))
            {

                foreach (GameObject go in GameObject.FindGameObjectsWithTag("VirtualBoard"))
                {
                    TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                    if (renderer != null)
                    {

                        renderer.text = "Game Over. The secret key was TRAIN!";


                    }
                }



            }
            else
            {

                //update virtual board 

                flag = false;
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("VirtualBoard"))
                {
                    TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                    if (renderer != null)
                    {

                        //renderer.text = "Type Here Your Guess:\n";


                    }
                }

            }

        }

        if (temp.Equals(SecretKey))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("VirtualBoard"))
            {
                TextMeshPro renderer = go.GetComponent<TextMeshPro>();
                if (renderer != null)
                {

                    renderer.text = "Congratulation, You found the secret key!";


                }
            }


            flag = false;
        }



}



	private void ListenForIncommingRequests()
	{
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Parse("192.168.1.137"), 8053);
            tcpListener.Start();
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                            if (clientMessage != null)
                            {

                                temp = clientMessage;
                                flag = true;
                                Debug.Log("client message received as: " + flag);
                                


                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
	}
	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	
	public void SendMessageSajjad(string temp)
	{
		if (connectedTcpClient == null)
		{
			return;
		}

		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = connectedTcpClient.GetStream();
			if (stream.CanWrite)
			{
				string serverMessage = temp;
				// Convert string message to byte array.                 
				byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage);
				// Write byte array to socketConnection stream.               
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
				Debug.Log("Server sent his message - should be received by client");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}


