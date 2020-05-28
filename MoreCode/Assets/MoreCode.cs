using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class MoreCode : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] AidsNumbers;
    public KMSelectable AidsSubmit;
    public TextMesh[] AidsText;
    public GameObject MoreAids;
    public Material[] Aids;
    string[] AidsWords = {"ALLOCATE","BULWARKS","COMPILER","DISPOSAL","ENCIPHER","FORMULAE","GAUNTLET","HUNKERED","ILLUSORY","JOUSTING","KINETICS","LINKWORK","MONOLITH","NANOBOTS","OCTANGLE","POSTSYNC","QUARTICS","REVOLVED","STANZAIC","TOMAHAWK","ULTRAHOT","VENDETTA","WAFFLERS","YOKOZUNA","ZUGZWANG","AidsCheck"};
    string[] FakeAidsL = {"ALLOTYPE","BULKHEAD","COMPUTER","DISPATCH","ENCRYPTS","FORTUNES","GATEWAYS","HUNTRESS","ILLUSION","JUNCTION","KILOBYTE","LINKAGES","MONOGRAM","NANOGRAM","OCTUPLES","POSITRON","QUINTICS","REVEALED","STOCCATA","TOMOGRAM","ULTRARED","VENOMOUS","WEAKENED","XENOLITH","YEASAYER","ZYMOGRAM"};
    //Dit I ,|| Dot O .|| Dah A =|| Dash S -
    string[] AlphabetMoreAids = {".-.,.","=","=,,","=,-",".,.-=","-.-",".",",..","=--=,","=.,",",","=-,=",",,.","--.=","-----","-","==.",",--=","-.==","=.,,","=====","=,",",=","=.","-,",",=-"};
    string AlphbetAids = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string FuckAids = "";
    int First = 4;
    string Comma = ".";
    int Second = 2;
    int Third = 0;
    int Fourth = 0;
    bool PlacementCheck = false;
    bool Anger = false;
    string SerialNumber = "";
    int NumberYouSubmit = 0;
    int AEIOUY = 0;
    int FaultyButInt = 0;
    int Broken = 0;
    int NumberThatINeed = 0;
    bool AEIOUCheck = false;
    bool FaultyCheck = false;
    bool BrokenCheck = false;
    bool coma = false;
    private List<int> BobsDickAndVowel = new List<int>{248,243,201,238,200,291,297,268,275,269,232,211,296};
    private List<int> BobsDick = new List<int>{276,242,274,213,214,212,280,258,271,204,210,246,227};
    private List<int> Faulty = new List<int>{205,224,293,207,234,253,289,272,288,225,241,239,285};
    private List<int> OtherwiseAM = new List<int>{209,273,251,249,267,261,247,282,281,235,294,254,256};
    private List<int> TwoBatAndBroken = new List<int>{233,245,250,300,266,226,283,244,219,229,278,263};
    private List<int> TwoBat = new List<int>{216,220,240,255,215,290,223,237,284,218,206,298};
    private List<int> TRN = new List<int>{287,262,228,208,217,286,210,264,252,260,270,231};
    private List<int> OtherwiseNZ = new List<int>{222,257,295,279,259,236,292,277,299,202,203,230};
    int FanfareAids = 0;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable Number in AidsNumbers) {
            Number.OnInteract += delegate () { NumberPress(Number); return false; };
        }
        AidsSubmit.OnInteract += delegate () { SubmitPress(); return false; };
    }

    void Start () {
      SerialNumber = Bomb.GetSerialNumber();
      for (int i = 0; i < 6; i++) {
        if (SerialNumber[i] == 'A' || SerialNumber[i] == 'U' || SerialNumber[i] == 'Y') {
          AEIOUY += 1;
          FaultyButInt += 1;
        }
        if (SerialNumber[i] == 'E' || SerialNumber[i] == 'O') {
          AEIOUY += 1;
          Broken += 1;
        }
        if (SerialNumber[i] == 'I') {
          AEIOUY += 1;
        }
        if (SerialNumber[i] == 'F' || SerialNumber[i] == 'L' || SerialNumber[i] == 'T') {
          FaultyButInt += 1;
        }
        if (SerialNumber[i] == 'B' || SerialNumber[i] == 'R' || SerialNumber[i] == 'K' || SerialNumber[i] == 'N') {
          Broken += 1;
        }
      }
      if (AEIOUY == 1) {
        AEIOUCheck = true;
      }
      if (FaultyButInt == 1) {
        FaultyCheck = true;
      }
      if (Broken == 2) {
        BrokenCheck = true;
      }
      StartCoroutine(AidsPicker());
    }
    void NumberPress(KMSelectable Number){
      Number.AddInteractionPunch();
  		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Number.transform);
      if (moduleSolved == true) {
        return;
      }
      for (int i = 0; i < 5; i++) {
        if (Number == AidsNumbers[i]) {
          switch (i) {
            case 0:
            First += 1;
            First %= 10;
            AidsText[i].text = First.ToString();
            break;
            case 1:
            if (Comma == ".") {
              Comma = ",";
              coma = true;
              AidsText[i].text = Comma;
            }
            else {
              Comma = ".";
              coma = false;
              AidsText[i].text = Comma;
            }
            break;
            case 2:
            Second += 1;
            Second %= 10;
            AidsText[i].text = Second.ToString();
            break;
            case 3:
            Third += 1;
            Third %= 10;
            AidsText[i].text = Third.ToString();
            break;
            case 4:
            Fourth += 1;
            Fourth %= 10;
            AidsText[i].text = Fourth.ToString();
            break;
          }
        }
      }
    }
    void SubmitPress(){
      AidsSubmit.AddInteractionPunch();
  		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, AidsSubmit.transform);
      if (moduleSolved == true) {
        return;
      }
      NumberYouSubmit = Second * 100 + Third * 10 + Fourth;
      if (coma == true) {
        GetComponent<KMBombModule>().HandleStrike();
        StopAllCoroutines();
        MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
        Anger = false;
        PlacementCheck = false;
        Debug.LogFormat("[More Code #{0}] You submitted with a comma. Strike dumbass! Resetting...", moduleId);
        StartCoroutine(AidsPicker());
      }
      if (First != 4) {
        GetComponent<KMBombModule>().HandleStrike();
        StopAllCoroutines();
        MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
        Anger = false;
        PlacementCheck = false;
        Debug.LogFormat("[More Code #{0}] You submitted without a preceeding 4. Strike, dumbass! Resetting...", moduleId);
        StartCoroutine(AidsPicker());
      }
      if (Anger == true && NumberYouSubmit == 265) {
        StopAllCoroutines();
        StartCoroutine(SolvingAids());
      }
      if (Anger == true && NumberYouSubmit == 265) {
        return;
      }
      for (int i = 0; i < 13; i++) {
        if (FuckAids[0] == AlphbetAids[i]) {
          PlacementCheck = true;
        }
      }
      if (PlacementCheck == true) {
        if ((Bomb.IsIndicatorOn("BOB") && AEIOUCheck == true && BobsDickAndVowel[NumberThatINeed] == NumberYouSubmit) || (Bomb.IsIndicatorOn("BOB") && BobsDick[NumberThatINeed] == NumberYouSubmit) || (FaultyCheck == true && Faulty[NumberThatINeed] == NumberYouSubmit) || (OtherwiseAM[NumberThatINeed] == NumberYouSubmit)) {
          StopAllCoroutines();
          StartCoroutine(SolvingAids());
        }
        else {
          GetComponent<KMBombModule>().HandleStrike();
          StopAllCoroutines();
          MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
          Anger = false;
          PlacementCheck = false;
          Debug.LogFormat("[More Code #{0}] You submitted 4.{1}. Strike, window-licker. Resetting...", moduleId, NumberYouSubmit);
          StartCoroutine(AidsPicker());
        }
      }
      else {
        if ((Bomb.GetBatteryCount() - 2 >= 0 && BrokenCheck == true && TwoBatAndBroken[NumberThatINeed - 13] == NumberYouSubmit) || (Bomb.GetBatteryCount() - 2 >= 0 && TwoBat[NumberThatINeed - 13] == NumberYouSubmit) || (Bomb.IsIndicatorOn("TRN") && TRN[NumberThatINeed - 13] == NumberYouSubmit) || (OtherwiseNZ[NumberThatINeed - 13] == NumberYouSubmit)) {
          StopAllCoroutines();
          StartCoroutine(SolvingAids());
        }
        else {
          GetComponent<KMBombModule>().HandleStrike();
          StopAllCoroutines();
          MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
          Anger = false;
          PlacementCheck = false;
          Debug.LogFormat("[More Code #{0}] You submitted 4.{1}. Strike, window-licker. Resetting...", moduleId, NumberYouSubmit);
          StartCoroutine(AidsPicker());
        }
      }
    }

    IEnumerator AidsPicker(){
      yield return new WaitForSeconds(.01f);
      NumberThatINeed = UnityEngine.Random.Range(0,AidsWords.Length);
      FuckAids = AidsWords[NumberThatINeed];
      if (FuckAids == "AidsCheck") {
        NumberThatINeed = UnityEngine.Random.Range(0,FakeAidsL.Length);
        FuckAids = FakeAidsL[UnityEngine.Random.Range(0,FakeAidsL.Length)];
        Anger = true;
      }
      Debug.LogFormat("[More Code #{0}] The word that is flashing is {1}.", moduleId, FuckAids);
      if (Anger == true) {
        Debug.LogFormat("[More Code #{0}] The word is not within any list. Submit 4.265.", moduleId);
      }
      else if (Bomb.IsIndicatorOn("BOB") && AEIOUCheck == true && NumberThatINeed <= 12) {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, BobsDickAndVowel[NumberThatINeed]);
      }
      else if (Bomb.IsIndicatorOn("BOB") && NumberThatINeed <= 12) {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, BobsDick[NumberThatINeed]);
      }
      else if (FaultyCheck == true && NumberThatINeed <= 12) {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, Faulty[NumberThatINeed]);
      }
      else if (NumberThatINeed <= 12) {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, OtherwiseAM[NumberThatINeed]);
      }
      else if (Bomb.GetBatteryCount() - 2 >= 0 && BrokenCheck == true) {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, TwoBatAndBroken[NumberThatINeed - 13]);
      }
      else if (Bomb.GetBatteryCount() - 2 >= 0) {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, TwoBat[NumberThatINeed - 13]);
      }
      else if (Bomb.IsIndicatorOn("TRN")) {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, TRN[NumberThatINeed - 13]);
      }
      else {
        Debug.LogFormat("[More Code #{0}] The number you should submit is 4.{1}.", moduleId, OtherwiseNZ[NumberThatINeed - 13]);
      }
      yield return null;
      StartCoroutine(juyhkmghgjmhgnjvmgnhmfjgjhmfnhgjmn());
    }
    IEnumerator juyhkmghgjmhgnjvmgnhmfjgjhmfnhgjmn(){
      for (int i = 0; i < 8; i++) {
        for (int j = 0; j < AlphbetAids.Length; j++) {
          if (FuckAids[i] == AlphbetAids[j]) {
            for (int x = 0; x < AlphabetMoreAids[j].Length; x++) {
              if (AlphabetMoreAids[j][x] == ',') {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                yield return new WaitForSeconds(.2f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                yield return new WaitForSeconds(1f);
              }
              else if (AlphabetMoreAids[j][x] == '.') {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                yield return new WaitForSeconds(.6f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                yield return new WaitForSeconds(1f);
              }
              else if (AlphabetMoreAids[j][x] == '=') {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                yield return new WaitForSeconds(1f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                yield return new WaitForSeconds(1f);
              }
              else if (AlphabetMoreAids[j][x] == '-')  {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                yield return new WaitForSeconds(3f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                yield return new WaitForSeconds(1f);
              }
            }
            MoreAids.GetComponent<MeshRenderer>().material = Aids[2];
            yield return new WaitForSeconds(1f);
            MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
            yield return new WaitForSeconds(1f);
          }
        }
      }
      StartCoroutine(juyhkmghgjmhgnjvmgnhmfjgjhmfnhgjmn());
    }
    IEnumerator SolvingAids(){
      FanfareAids += 1;
      for (int i = 0; i < 5; i++) {
        AidsText[i].text = (UnityEngine.Random.Range(0,10)).ToString();
      }
        MoreAids.GetComponent<MeshRenderer>().material = Aids[UnityEngine.Random.Range(0,4)];
      if (FanfareAids == 100) {
        GetComponent<KMBombModule>().HandlePass();
        moduleSolved = true;
        MoreAids.GetComponent<MeshRenderer>().material = Aids[3];
        for (int i = 0; i < 5; i++) {
          AidsText[i].text = "!";
        }
      }
      else {
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(SolvingAids());
      }
    }
}
