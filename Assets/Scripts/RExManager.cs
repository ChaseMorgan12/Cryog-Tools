using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
//for more API access (most aren't used lol)

public class RExManager : MonoBehaviour
{
    //String arrays to hold all ores with their respective layer
    private string[] surfaceOres;
    private string[] basaltOres;
    private string[] graniteOres;
    private string[] dioriteOres;
    private string[] obsidianOres;
    private string[] marbleOres;
    private string[] mantleOres;
    private string[] coreOres;
    private string[] subSurfaceOres;
    private string[] subMantleOres;
    private string[] subCoreOres;
    private string[] subRoccOres;

    //more string arrays to hold the banned ores and the amount of punishments
    private string[] bannedOres;
    private string[] punishAmount;
    private string[] punishArray;
    private float[] punishChances;

    //private GameObjects for things that are called in script
    private GameObject oreList;
    private GameObject mainCanvas;
    private GameObject timer;
    private GameObject punishUI;
    private GameObject percentageUI;

    //private bools to hold true or false statements relating to the timer and punishments
    private bool isPunished;
    private bool timerOn;

    //special bool to cease everything
    private bool stopAll;
    private bool cantPunish;

    //private integers for the timer and amount of layers
    private int timeOne = 5;
    private int timeTwo = 0;
    private int layerInt;

    private void Start()
    {
        punishChances = new float[1];
        mainCanvas = GameObject.FindGameObjectWithTag("MAIN");
        timer = GameObject.Find("Timer");
        punishUI = GameObject.Find("Punish");
        percentageUI = GameObject.Find("Percentage");
        surfaceOres = new string[15];
        Surface();
        oreList = GameObject.FindGameObjectWithTag("Ore List");
        mainCanvas.SetActive(false);
        basaltOres = new string[15];
        Basalt();
        graniteOres = new string[14];
        Granite();
        dioriteOres = new string[16];
        Diorite();
        obsidianOres = new string[16];
        Obsidian();
        marbleOres = new string[15];
        Marble();
        mantleOres = new string[15];
        Mantle();
        coreOres = new string[17];
        Core();
        subSurfaceOres = new string[34];
        SubSurface();
        subMantleOres = new string[31];
        SubMantle();
        subCoreOres = new string[34];
        SubCore();
        subRoccOres = new string[14];
        SubRocc();
    }

    private void Update()
    {
        if (timerOn || isPunished)
        {
            punishUI.GetComponent<TMP_InputField>().interactable = false;
        }
        else
        {
            punishUI.GetComponent<TMP_InputField>().interactable = true;
        }
        timer.GetComponent<TMP_Text>().text = timeOne.ToString() + ":" + timeTwo.ToString();

        percentageUI.GetComponent<TMP_Text>().text = GetPercentage() + "%";

        if (Int32.Parse(Regex.Match(percentageUI.GetComponent<TMP_Text>().text, @"\d+").Value) > 100)
        {
            percentageUI.GetComponent<TMP_Text>().color = Color.red;
        }
        else if (Int32.Parse(Regex.Match(percentageUI.GetComponent<TMP_Text>().text, @"\d+").Value) == 100)
        {
            percentageUI.GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            percentageUI.GetComponent<TMP_Text>().color = Color.white;
        }
    }

    private float GetPercentage()
    {
        float amount = 0f;
        if (punishChances[0] != 0)
        {
            for (int i = 0; i < punishChances.Length; i++)
            {
                amount += punishChances[i];
            }
        }
        return amount;
    }

    public void Create()
    {
        if (oreList.GetComponent<TMP_InputField>().text != String.Empty)
        {
            GameObject.FindGameObjectWithTag("SETUP").SetActive(false);
            mainCanvas.SetActive(true);
            GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text  = String.Empty;
            BannedList();
        }
    }

    private void BannedList()
    {
        switch (layerInt)
        {
            case 1:
                bannedOres = SelectOres(surfaceOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 2:
                bannedOres = SelectOres(basaltOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 3:
                bannedOres = SelectOres(graniteOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 4:
                bannedOres = SelectOres(dioriteOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 5:
                bannedOres = SelectOres(obsidianOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 6:
                bannedOres = SelectOres(marbleOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 7:
                bannedOres = SelectOres(mantleOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 8:
                bannedOres = SelectOres(coreOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 9:
                bannedOres = SelectOres(subSurfaceOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 10:
                bannedOres = SelectOres(subMantleOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 11:
                bannedOres = SelectOres(subCoreOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
            case 12:
                bannedOres = SelectOres(subRoccOres);
                for (int i = 0; i < bannedOres.Length; i++)
                {
                    GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text += bannedOres[i] + "\n";
                }
                break;
        }
    }

    public void RandomLayer(GameObject list)
    {
        int optionAmount = list.GetComponent<TMP_Dropdown>().options.Count;
        int randomOption = UnityEngine.Random.Range(1, optionAmount);
        list.GetComponent<TMP_Dropdown>().value = randomOption;
    }
    
    private string[] SelectOres(string[] ores)
    {
        string[] oresArray = new string[5];
        string[] tempArray = new string[ores.Length];
        for(int i = 0; i < ores.Length; i++)
        {
            tempArray[i] = ores[i];
        }
        for (int i = 0; i < 5; i++)
        {
            int x = UnityEngine.Random.Range(0, tempArray.Length - 1);
            if (tempArray[x] != null)
            {
                oresArray[i] = ores[x];
            }
            else
            {
                for(int y = 0; y < 25; y++)
                {
                    if (tempArray[x] != null)
                    {
                        break;
                    }
                    x = UnityEngine.Random.Range(0, ores.Length - 1);
                }
                oresArray[i] = tempArray[x];
            }
            tempArray[x] = null;
        }
        return oresArray;
    }

    public void Play()
    {
        if (!timerOn && !stopAll)
        {
            if (timeOne == 5)
            {
                timeOne = 5;
                timeTwo = 0;
            }
            timer.GetComponent<TMP_Text>().text = timeOne.ToString() + ":" + timeTwo.ToString();
            timerOn = true;
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        if (timerOn && !stopAll)
        {
            
            if (timeTwo < 0) { timeOne--; timeTwo = 59; }
            if (timeOne < 0) 
            { 
                timerOn = false; 
                timeOne = 5;
                timeTwo = 0;
                GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text = String.Empty;
                BannedList();
                Play();
                yield break;
            }
            yield return new WaitForSecondsRealtime(1f);
            timeTwo--;
            StartCoroutine(Timer());
        }
    }

    public void Stop()
    {
        timerOn = false;
        timeOne = 5;
        timeTwo = 0;
        GameObject.FindGameObjectWithTag("Banned").GetComponent<TMP_InputField>().text = String.Empty;
        //punishUI.GetComponent<TMP_InputField>().text = String.Empty;
        isPunished = false;
        BannedList();
    }

    public void Pause()
    {
        timerOn = false;
        isPunished = false;
    }

    public void Punish()
    {
        if (punishUI.GetComponent<TMP_InputField>().text != String.Empty && !isPunished && !stopAll || !cantPunish)
        {
            bool specifiedChances = true;
            punishArray = new string[punishAmount.Length];
            isPunished = true;
            for (int i = 0; i < punishAmount.Length; i++)
            {
                punishArray[i] = punishAmount[i].ToString();
                if (punishAmount[0].Contains('(') && punishAmount[0].Contains(')'))
                {
                }
                else
                {
                    specifiedChances = false;
                }
            }
            if (specifiedChances && GetPercentage() == 100)
            {
                float randomValue = UnityEngine.Random.Range(0.00f, 100.00f);

                for (int i = 0; i < punishChances.Length; i++)
                {
                    randomValue -= punishChances[i];

                    if (randomValue <= 0f)
                    {
                        punishUI.GetComponent<TMP_InputField>().text = "<color=green>" + punishAmount[i] + "</color>";
                        break;
                    }
                }
            }
            else if (!specifiedChances)
            {
                int x = UnityEngine.Random.Range(0, punishAmount.Length);
                punishUI.GetComponent<TMP_InputField>().text = "<color=green>" + punishAmount[x] + "</color>";
            }
            StartCoroutine(PunishWait());
        }
    }

    public void SetPunish()
    {
        if (punishUI.GetComponent<TMP_InputField>().text != String.Empty)
        {
            punishAmount = punishUI.GetComponent<TMP_InputField>().text.Split('\n');
            punishChances = new float[punishAmount.Length];
            for (int i = 0; i < punishAmount.Length; i++)
            {
                //punishArray[i] = punishAmount[i].ToString();
                if (punishAmount[0].Contains('(') && punishAmount[0].Contains(')'))
                {
                    string y = punishAmount[i].Split('(', '%', ')')[1];
                    punishChances[i] = float.Parse(y);
                }
            }
        }
    }

    private IEnumerator PunishWait()
    {
        stopAll = true;
        yield return new WaitForSecondsRealtime(5);
        punishUI.GetComponent<TMP_InputField>().text = String.Empty;
        for (int i = 0; i < punishArray.Length; i++)
        {
            punishUI.GetComponent<TMP_InputField>().text += punishArray[i].ToString() + "\n";
        }
        Stop();
        stopAll = false;
    }
    public void DropDown(int val)
    {
        oreList.GetComponent<TMP_InputField>().text = String.Empty;
        switch (val)
        {
            case 1:
                layerInt = 1;
                for (int i = 0; i < surfaceOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += surfaceOres[i]+"\n";
                }
                break;
            case 2:
                layerInt = 2;
                for (int i = 0; i < basaltOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += basaltOres[i] + "\n";
                }
                break;
            case 3:
                layerInt = 3;
                for (int i = 0; i < graniteOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += graniteOres[i] + "\n";
                }
                break;
            case 4:
                layerInt = 4;
                for (int i = 0; i < dioriteOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += dioriteOres[i] + "\n";
                }
                break;
            case 5:
                layerInt = 5;
                for (int i = 0; i < obsidianOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += obsidianOres[i] + "\n";
                }
                break;
            case 6:
                layerInt = 6;
                for (int i = 0; i < marbleOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += marbleOres[i] + "\n";
                }
                break;
            case 7:
                layerInt = 7;
                for (int i = 0; i < mantleOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += mantleOres[i] + "\n";
                }
                break;
            case 8:
                layerInt = 8;
                for (int i = 0; i < coreOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += coreOres[i] + "\n";
                }
                break;
            case 9:
                layerInt = 9;
                for (int i = 0; i < subSurfaceOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += subSurfaceOres[i] + "\n";
                }
                break;
            case 10:
                layerInt = 10;
                for (int i = 0; i < subMantleOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += subMantleOres[i] + "\n";
                }
                break;
            case 11:
                layerInt = 11;
                for (int i = 0; i < subCoreOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += subCoreOres[i] + "\n";
                }
                break;
            case 12:
                layerInt = 12;
                for (int i = 0; i < subRoccOres.Length; i++)
                {
                    oreList.GetComponent<TMP_InputField>().text += subRoccOres[i] + "\n";
                }
                break;
            default:
                oreList.GetComponent<TMP_InputField>().text = String.Empty;
                break;
        }
    }

    private void Surface()
    {
        surfaceOres[0] = "Copper";
        surfaceOres[1] = "Quartz";
        surfaceOres[2] = "Iron";
        surfaceOres[3] = "Amber";
        surfaceOres[4] = "Cobalt";
        surfaceOres[5] = "Chrysoberyl";
        surfaceOres[6] = "Petrified Wood";
        surfaceOres[7] = "Elusium";
        surfaceOres[8] = "Unobtanium";
        surfaceOres[9] = "Aegistone";
        surfaceOres[10] = "Scertanium";
        surfaceOres[11] = "Pasivium";
        surfaceOres[12] = "Pastelorium";
        surfaceOres[13] = "Vaporwave Crystal";
        surfaceOres[14] = "Endozivite";
    }

    private void Basalt()
    {
        basaltOres[0] = "Copper";
        basaltOres[1] = "Quartz";
        basaltOres[2] = "Iron";
        basaltOres[3] = "Cobalt";
        basaltOres[4] = "Bluesteel";
        basaltOres[5] = "Coldfirium";
        basaltOres[6] = "Tanzanite";
        basaltOres[7] = "Elysian";
        basaltOres[8] = "Nocturnite";
        basaltOres[9] = "Freon";
        basaltOres[10] = "Snoblintium";
        basaltOres[11] = "Azuryl";
        basaltOres[12] = "Glacielle";
        basaltOres[13] = "Cybernetium";
        basaltOres[14] = "Inclemetite";
    }
    private void Granite()
    {
        graniteOres[0] = "Quartz";
        graniteOres[1] = "Iron";
        graniteOres[2] = "Emerald";
        graniteOres[3] = "Olivine";
        graniteOres[4] = "Uranium";
        graniteOres[5] = "Viridian";
        graniteOres[6] = "Promethium";
        graniteOres[7] = "Newtonium";
        graniteOres[8] = "Elexinite";
        graniteOres[9] = "Astatine";
        graniteOres[10] = "Spristium";
        graniteOres[11] = "Erodimium";
        graniteOres[12] = "Candilium";
        graniteOres[13] = "Terratomere";
    }
    private void Diorite()
    {
        dioriteOres[0] = "Coal";
        dioriteOres[1] = "Quartz";
        dioriteOres[2] = "Iron";
        dioriteOres[3] = "Silver";
        dioriteOres[4] = "Chroma Contaris";
        dioriteOres[5] = "Musgravite";
        dioriteOres[6] = "Osmium";
        dioriteOres[7] = "Black Diamond";
        dioriteOres[8] = "Spatializine";
        dioriteOres[9] = "Lanthanite";
        dioriteOres[10] = "Neptunium";
        dioriteOres[11] = "Acceleratium";
        dioriteOres[12] = "Quandrium";
        dioriteOres[13] = "Lucidium";
        dioriteOres[14] = "Polarium";
        dioriteOres[15] = "Illusory Bubblegram";
    }
    private void Obsidian()
    {
        obsidianOres[0] = "Coal";
        obsidianOres[1] = "Quartz";
        obsidianOres[2] = "Nyctophyte";
        obsidianOres[3] = "Silver";
        obsidianOres[4] = "Ruby";
        obsidianOres[5] = "Obsidian Glass";
        obsidianOres[6] = "Chroma Contaris";
        obsidianOres[7] = "Painite";
        obsidianOres[8] = "Adurite";
        obsidianOres[9] = "Jet";
        obsidianOres[10] = "Zefendium";
        obsidianOres[11] = "Exolite";
        obsidianOres[12] = "Blaziune";
        obsidianOres[13] = "Speatrium";
        obsidianOres[14] = "Sentient Viscera";
        obsidianOres[15] = "Inkonium";
    }
    private void Marble()
    {
        marbleOres[0] = "Quartz";
        marbleOres[1] = "Silver";
        marbleOres[2] = "Gold";
        marbleOres[3] = "Bismuth";
        marbleOres[4] = "Delectium";
        marbleOres[5] = "Alexandrite";
        marbleOres[6] = "Rainbonite";
        marbleOres[7] = "Chromatite";
        marbleOres[8] = "Temporum";
        marbleOres[9] = "Ornalium";
        marbleOres[10] = "Aether";
        marbleOres[11] = "Trinitium";
        marbleOres[12] = "Luminatite";
        marbleOres[13] = "Elementium";
        marbleOres[14] = "Idolium";
    }
    private void Mantle()
    {
        mantleOres[0] = "Gold";
        mantleOres[1] = "Bismuth";
        mantleOres[2] = "Ancient Bronze";
        mantleOres[3] = "Pyrite";
        mantleOres[4] = "Solid Bromine";
        mantleOres[5] = "Vanadinite";
        mantleOres[6] = "Alternium";
        mantleOres[7] = "Poiseon";
        mantleOres[8] = "Vitrilyx";
        mantleOres[9] = "Euclideum";
        mantleOres[10] = "Scarfyte";
        mantleOres[11] = "Exoretic";
        mantleOres[12] = "Albinite";
        mantleOres[13] = "Magnetyx";
        mantleOres[14] = "Scribbal";
    }
    private void Core()
    {
        coreOres[0] = "Nickel";
        coreOres[1] = "Gold";
        coreOres[2] = "Palladium";
        coreOres[3] = "Magnesium";
        coreOres[4] = "Core Fragment";
        coreOres[5] = "Incinderium";
        coreOres[6] = "Sunstone";
        coreOres[7] = "Solarite";
        coreOres[8] = "Thundarian";
        coreOres[9] = "Flaeon";
        coreOres[10] = "Combustal";
        coreOres[11] = "Suncindium";
        coreOres[12] = "Cleopatrite";
        coreOres[13] = "Xynarium";
        coreOres[14] = "Dyronsinite";
        coreOres[15] = "Gargantium";
        coreOres[16] = "Dynamo of Fate";
    }
    private void SubSurface()
    {
        subSurfaceOres[0] = "Copper";
        subSurfaceOres[1] = "Tin";
        subSurfaceOres[2] = "Quartz";
        subSurfaceOres[3] = "Silver";
        subSurfaceOres[4] = "Gold";
        subSurfaceOres[5] = "Emerald";
        subSurfaceOres[6] = "Amethyst";
        subSurfaceOres[7] = "Platinum";
        subSurfaceOres[8] = "Garnet";
        subSurfaceOres[9] = "Aquamarine";
        subSurfaceOres[10] = "Topaz";
        subSurfaceOres[11] = "Titanium";
        subSurfaceOres[12] = "Lapis Lazuli";
        subSurfaceOres[13] = "Jaxite";
        subSurfaceOres[14] = "Diamond";
        subSurfaceOres[15] = "Ocean Jasper";
        subSurfaceOres[16] = "Cobalt";
        subSurfaceOres[17] = "Turquoise";
        subSurfaceOres[18] = "Vegentium";
        subSurfaceOres[19] = "Legacy Jet";
        subSurfaceOres[20] = "Vitridian";
        subSurfaceOres[21] = "Chambersite";
        subSurfaceOres[22] = "EDMium";
        subSurfaceOres[23] = "Legacy Promethium";
        subSurfaceOres[24] = "Hyperstone";
        subSurfaceOres[25] = "Inktite";
        subSurfaceOres[26] = "Legacy Freon";
        subSurfaceOres[27] = "Legacy Codex";
        subSurfaceOres[28] = "Legacy Neomandelite";
        subSurfaceOres[29] = "Soundstrocity";
        subSurfaceOres[30] = "Legacy Aurora";
        subSurfaceOres[31] = "Flare of Transcendence";
        subSurfaceOres[32] = "Legacy Vitriol";
        subSurfaceOres[33] = "Legacy Voidirinite";
    }
    private void SubMantle()
    {
        subMantleOres[0] = "Nickel";
        subMantleOres[1] = "Aluminum";
        subMantleOres[2] = "Lithium";
        subMantleOres[3] = "Jasper";
        subMantleOres[4] = "Ruby";
        subMantleOres[5] = "Jade";
        subMantleOres[6] = "Morganite";
        subMantleOres[7] = "Tourmaline";
        subMantleOres[8] = "Sapphire";
        subMantleOres[9] = "Chrome";
        subMantleOres[10] = "Lumbonium";
        subMantleOres[11] = "Sporgilian";
        subMantleOres[12] = "Legacy Cryotic";
        subMantleOres[13] = "Legacy Zefendium";
        subMantleOres[14] = "Gilded Gold";
        subMantleOres[15] = "Cerrusite";
        subMantleOres[16] = "Electrolium";
        subMantleOres[17] = "Legacy Newtonium";
        subMantleOres[18] = "Unobtanium";
        subMantleOres[19] = "Legacy Dyronsinite";
        subMantleOres[20] = "Lutetium";
        subMantleOres[21] = "Legacy Flaeon";
        subMantleOres[22] = "Sagittarius Quasar";
        subMantleOres[23] = "Protoflare";
        subMantleOres[24] = "Electrolyx";
        subMantleOres[25] = "Legacy Trinitium";
        subMantleOres[26] = "Illuminyx";
        subMantleOres[27] = "Legacy Omega";
        subMantleOres[28] = "Surrenial";
        subMantleOres[29] = "RGB Pulsar";
        subMantleOres[30] = "Armageddium";
    }

    private void SubCore()
    {
        subCoreOres[0] = "Coal";
        subCoreOres[1] = "Zinc";
        subCoreOres[2] = "Scandium";
        subCoreOres[3] = "Strontium";
        subCoreOres[4] = "Heliodor";
        subCoreOres[5] = "Beryllium";
        subCoreOres[6] = "Neodymium";
        subCoreOres[7] = "Bismuth";
        subCoreOres[8] = "Olivine";
        subCoreOres[9] = "Thorium";
        subCoreOres[10] = "Palladium";
        subCoreOres[11] = "Radium";
        subCoreOres[12] = "Javanite";
        subCoreOres[13] = "Corrodine";
        subCoreOres[14] = "Corroplat";
        subCoreOres[15] = "Leryium";
        subCoreOres[16] = "Adurite";
        subCoreOres[17] = "Viridian";
        subCoreOres[18] = "Ultranium";
        subCoreOres[19] = "Deathinium";
        subCoreOres[20] = "Frostical";
        subCoreOres[21] = "Uldarite";
        subCoreOres[22] = "Vortennial";
        subCoreOres[23] = "Legacy Poiseon";
        subCoreOres[24] = "Legacy Halcyon Emission";
        subCoreOres[25] = "Legacy Malachite";
        subCoreOres[26] = "Legacy Astatine";
        subCoreOres[27] = "Vaporwave Pulsar";
        subCoreOres[28] = "Orb of Discontent";
        subCoreOres[29] = "Epsilon";
        subCoreOres[30] = "Legacy Quasar V";
        subCoreOres[31] = "Legacy Aether";
        subCoreOres[32] = "Legacy Quasar 618";
        subCoreOres[33] = "Epinephrine";
    }
    private void SubRocc()
    {
        subRoccOres[0] = "SFoRE";
        subRoccOres[1] = "WOOD GRAIN";
        subRoccOres[2] = "WATERMELON ORE";
        subRoccOres[3] = "BANANORE";
        subRoccOres[4] = "COCONUT ORE";
        subRoccOres[5] = "the unfunny";
        subRoccOres[6] = "the funny";
        subRoccOres[7] = "CHICKEN Crystal";
        subRoccOres[8] = "waffle crystal";
        subRoccOres[9] = "SHOWER CRYSTAL";
        subRoccOres[10] = "garlic bread crystal";
        subRoccOres[11] = "tycoon crystal";
        subRoccOres[12] = "a flare v2";
        subRoccOres[13] = "Corrodine Pulsar";
    }
}
