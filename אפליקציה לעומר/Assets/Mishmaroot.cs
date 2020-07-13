﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Mishmaroot : MonoBehaviour
{
    public string shifts;
    public Text shiftsText;
    float wholeShifts;

    public string howMuchShiftsWorkerOpened;
    public Text howMuchShiftsWorkerOpenedText;
    int _howMuchShiftsWorkerOpened;

    public string howMuchShiftsInMaskit;
    public Text howMuchShiftsInMaskitText;
    int _howMuchShiftsInMaskit;

    float percent;
    public Text _percent;

    public float howMuchShiftsWorkerDeserve;
    public Text _howMuchShiftsWorkerDeserve;

    bool shabat;
    float shabatTovault;
    public Text yes;
    public Text no;

    bool ifThereisMaskit;
    public GameObject howMuchMaskitPositions;

    private void Start()
    {
        no.gameObject.SetActive(true);
        yes.gameObject.SetActive(false);
        ifThereisMaskit = true;
        howMuchMaskitPositions.SetActive(true);
    }

    public void Calculation()
    {
        shifts = shiftsText.text;
        wholeShifts = Convert.ToInt32(shifts);

        if (ifThereisMaskit == true)
        {
            howMuchShiftsInMaskit = howMuchShiftsInMaskitText.text;
            _howMuchShiftsInMaskit = Convert.ToInt32(howMuchShiftsInMaskit);
        }

        howMuchShiftsWorkerOpened = howMuchShiftsWorkerOpenedText.text;
        _howMuchShiftsWorkerOpened = Convert.ToInt32(howMuchShiftsWorkerOpened);

        IfTheresAMaskit();

        IfASabbathWatchman();

        _howMuchShiftsWorkerDeserve.text = "" + howMuchShiftsWorkerDeserve.ToString("f0");

        percent *= 100;
        _percent.text = "" + percent.ToString("f0") +"%";
    }

    public void IfASabbathWatchmanWorks()
    {
        shabat = !shabat;
        if (shabat == true)
        {
            no.gameObject.SetActive(false);
            yes.gameObject.SetActive(true);
        }
        else
        {
            no.gameObject.SetActive(true);
            yes.gameObject.SetActive(false);
        }
    }
    void IfASabbathWatchman()
    {
        if (shabat == true)
        {
            shabatTovault = percent * 8;
            howMuchShiftsWorkerDeserve = percent * _howMuchShiftsWorkerOpened;
            howMuchShiftsWorkerDeserve += shabatTovault;
        }
        else
        {
            howMuchShiftsWorkerDeserve = percent * _howMuchShiftsWorkerOpened;
        }
        //Debug.Log(howMuchShiftsWorkerDeserve);
    }

    public void TheresAMaskit()
    {
        ifThereisMaskit = !ifThereisMaskit;
        if (ifThereisMaskit==true)
        {
            howMuchMaskitPositions.SetActive(true);
        }
        else
        {
            howMuchMaskitPositions.SetActive(false);
        }
    }
    void IfTheresAMaskit()
    {
        if (ifThereisMaskit == true)
        {
            percent = (84 + _howMuchShiftsInMaskit) / wholeShifts;
        }
        else
        {
            percent = 84 / wholeShifts;
        }
    }
}
