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
    public KMColorblindMode Colorblind;
    public TextMesh[] AidsText;
    public TextMesh CBText;
    public GameObject MoreAids;
    public Material[] Aids;

    private List<int> BobsDickAndVowel = new List<int>{248, 243, 201, 238, 200, 291, 297, 268, 275, 269, 232, 211, 296};
    private List<int> BobsDick = new List<int>{276, 242, 274, 213, 214, 212, 280, 258, 271, 204, 210, 246, 227};
    private List<int> Faulty = new List<int>{205, 224, 293, 207, 234, 253, 289, 272, 288, 225, 241, 239, 285};
    private List<int> OtherwiseAM = new List<int>{209, 273, 251, 249, 267, 261, 247, 282, 281, 235, 294, 254, 256};
    private List<int> TwoBatAndBroken = new List<int>{233, 245, 250, 300, 266, 226, 283, 244, 219, 229, 278, 263};
    private List<int> TwoBat = new List<int>{216, 220, 240, 255, 215, 290, 223, 237, 284, 218, 206, 298};
    private List<int> TRN = new List<int>{287, 262, 228, 208, 217, 286, 210, 264, 252, 260, 270, 231};
    private List<int> OtherwiseNZ = new List<int>{222, 257, 295, 279, 259, 236, 292, 277, 299, 202, 203, 230};
    int AEIOUY;
    int Broken;
    int correct;
    int FanfareAids;
    int FaultyButInt;
    int First = 4;
    int Fourth;
    int NumberThatINeed;
    int NumberYouSubmit;
    int Second = 2;
    int Third;

    string[] AidsWords = {"ALLOCATE","BULWARKS","COMPILER","DISPOSAL","ENCIPHER","FORMULAE","GAUNTLET","HUNKERED","ILLUSORY","JOUSTING","KINETICS","LINKWORK","MONOLITH","NANOBOTS","OCTANGLE","POSTSYNC","QUARTICS","REVOLVED","STANZAIC","TOMAHAWK","ULTRAHOT","VENDETTA","WAFFLERS","YOKOZUNA","ZUGZWANG","AidsCheck"};
    string[] FakeAidsL = {"ALLOTYPE","BULKHEAD","COMPUTER","DISPATCH","ENCRYPTS","FORTUNES","GATEWAYS","HUNTRESS","ILLUSION","JUNCTION","KILOBYTE","LINKAGES","MONOGRAM","NANOGRAM","OCTUPLES","POSITRON","QUINTICS","REVEALED","STOCCATA","TOMOGRAM","ULTRARED","VENOMOUS","WEAKENED","XENOLITH","YEASAYER","ZYMOGRAM"};
    //Dit I ,|| Dot O .|| Dah A =|| Dash S -
    string[] AlphabetMoreAids = {".-.,.","=","=,,","=,-",".,.-=","-.-",".",",..","=--=,","=.,",",","=-,=",",,.","--.=","-----","-","==.",",--=","-,==","=.,,","=====","=,",",=","=.","-,",",=-"};
    string AlphbetAids = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string Comma = ".";
    string FuckAids;
    string SerialNumber;

    bool AEIOUCheck;
    bool Anger;
    bool activated;
    bool BrokenCheck;
    bool cbactive;
    bool coma;
    bool FaultyCheck;
    bool PlacementCheck;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;
        cbactive = Colorblind.ColorblindModeActive;
        foreach (KMSelectable Number in AidsNumbers) {
            Number.OnInteract += delegate () { NumberPress(Number); return false; };
        }
        AidsSubmit.OnInteract += delegate () { SubmitPress(); return false; };
        GetComponent<KMBombModule>().OnActivate += OnActivate;
    }

    void Start () {
      SerialNumber = Bomb.GetSerialNumber();
        if (SerialNumber.Contains("AUY")) {
          AEIOUY += 1;
          FaultyButInt += 1;
        }
        if (SerialNumber.Contains("EO")) {
          AEIOUY += 1;
          Broken += 1;
        }
        if (SerialNumber.Contains("I"))
          AEIOUY += 1;
        if (SerialNumber.Contains("FLT"))
          FaultyButInt += 1;
        if (SerialNumber.Contains("BRKN"))
          Broken += 1;
      if (AEIOUY == 1)
        AEIOUCheck = true;
      if (FaultyButInt == 1)
        FaultyCheck = true;
      if (Broken == 2)
        BrokenCheck = true;
    }

    void OnActivate () {
        AidsText[0].text = First.ToString();
        AidsText[1].text = Comma;
        AidsText[2].text = Second.ToString();
        AidsText[3].text = Third.ToString();
        AidsText[4].text = Fourth.ToString();
        StartCoroutine(AidsPicker());
        activated = true;
    }

    void NumberPress (KMSelectable Number) {
  		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Number.transform);
      if (moduleSolved == true || !activated)
        return;
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

    void SubmitPress () {
      AidsSubmit.AddInteractionPunch();
  		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, AidsSubmit.transform);
      if (moduleSolved || !activated)
        return;
      NumberYouSubmit = Second * 100 + Third * 10 + Fourth;
      if (coma) {
        GetComponent<KMBombModule>().HandleStrike();
        StopAllCoroutines();
        Audio.PlaySoundAtTransform("strike", transform);
        MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
        Anger = false;
        PlacementCheck = false;
        Debug.LogFormat("[More Code #{0}] You submitted with a comma. Strike dumbass! Resetting...", moduleId);
        StartCoroutine(AidsPicker());
        return;
      }
      if (First != 4) {
        GetComponent<KMBombModule>().HandleStrike();
        StopAllCoroutines();
        Audio.PlaySoundAtTransform("strike", transform);
        MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
        Anger = false;
        PlacementCheck = false;
        Debug.LogFormat("[More Code #{0}] You submitted without a preceeding 4. Strike, dumbass! Resetting...", moduleId);
        StartCoroutine(AidsPicker());
        return;
      }
      if (Anger && NumberYouSubmit == 265) {
        StopAllCoroutines();
        Audio.PlaySoundAtTransform("solve", transform);
        StartCoroutine(SolvingAids());
      }
      if (Anger && NumberYouSubmit == 265)
        return;
      if (Anger && NumberYouSubmit != 265) {
        GetComponent<KMBombModule>().HandleStrike();
        StopAllCoroutines();
        Audio.PlaySoundAtTransform("strike", transform);
        MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
        Anger = false;
        PlacementCheck = false;
        Debug.LogFormat("[More Code #{0}] You submitted 4.{1}. Strike, window-licker. Resetting...", moduleId, NumberYouSubmit);
        StartCoroutine(AidsPicker());
        return;
      }
      for (int i = 0; i < 13; i++) {
        if (FuckAids[0] == AlphbetAids[i])
          PlacementCheck = true;
      }
      if (PlacementCheck) {
        if ((Bomb.IsIndicatorOn("BOB") && AEIOUCheck == true && BobsDickAndVowel[NumberThatINeed] == NumberYouSubmit) || (Bomb.IsIndicatorOn("BOB") && BobsDick[NumberThatINeed] == NumberYouSubmit) || (FaultyCheck == true && Faulty[NumberThatINeed] == NumberYouSubmit) || (OtherwiseAM[NumberThatINeed] == NumberYouSubmit)) {
          StopAllCoroutines();
          Audio.PlaySoundAtTransform("solve", transform);
          StartCoroutine(SolvingAids());
        }
        else {
          GetComponent<KMBombModule>().HandleStrike();
          StopAllCoroutines();
          Audio.PlaySoundAtTransform("strike", transform);
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
          Audio.PlaySoundAtTransform("solve", transform);
          StartCoroutine(SolvingAids());
        }
        else {
          GetComponent<KMBombModule>().HandleStrike();
          StopAllCoroutines();
          Audio.PlaySoundAtTransform("strike", transform);
          MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
          Anger = false;
          PlacementCheck = false;
          Debug.LogFormat("[More Code #{0}] You submitted 4.{1}. Strike, window-licker. Resetting...", moduleId, NumberYouSubmit);
          StartCoroutine(AidsPicker());
        }
      }
    }

    IEnumerator AidsPicker () {
      yield return new WaitForSeconds(.01f);
      NumberThatINeed = UnityEngine.Random.Range(0,AidsWords.Length);
      FuckAids = AidsWords[NumberThatINeed];
      if (FuckAids == "AidsCheck") {
        NumberThatINeed = UnityEngine.Random.Range(0,FakeAidsL.Length);
        FuckAids = FakeAidsL[UnityEngine.Random.Range(0,FakeAidsL.Length)];
        Anger = true;
      }
      Debug.LogFormat("[More Code #{0}] The word that is flashing is {1}.", moduleId, FuckAids);
      if (Anger) {
        correct = 265;
        Debug.LogFormat("[More Code #{0}] The word is not within any list. Submit 4.265.", moduleId);
      }
      else if (Bomb.IsIndicatorOn("BOB") && AEIOUCheck == true && NumberThatINeed <= 12)
        correct = BobsDickAndVowel[NumberThatINeed];
      else if (Bomb.IsIndicatorOn("BOB") && NumberThatINeed <= 12)
        correct = BobsDick[NumberThatINeed];
      else if (FaultyCheck == true && NumberThatINeed <= 12)
        correct = Faulty[NumberThatINeed];
      else if (NumberThatINeed <= 12)
        correct = OtherwiseAM[NumberThatINeed];
      else if (Bomb.GetBatteryCount() - 2 >= 0 && BrokenCheck == true)
        correct = TwoBatAndBroken[NumberThatINeed - 13];
      else if (Bomb.GetBatteryCount() - 2 >= 0)
        correct = TwoBat[NumberThatINeed - 13];
      else if (Bomb.IsIndicatorOn("TRN"))
        correct = TRN[NumberThatINeed - 13];
      else
        correct = OtherwiseNZ[NumberThatINeed - 13];
      if (correct != 265)
        Debug.LogFormat("[More Code #{0}] The frequency you should submit is 4.{1}.", moduleId, correct);
      yield return null;
      StartCoroutine(juyhkmghgjmhgnjvmgnhmfjgjhmfnhgjmn());
    }

    IEnumerator juyhkmghgjmhgnjvmgnhmfjgjhmfnhgjmn () {
      for (int i = 0; i < 8; i++) {
        for (int j = 0; j < AlphbetAids.Length; j++) {
          if (FuckAids[i] == AlphbetAids[j]) {
            for (int x = 0; x < AlphabetMoreAids[j].Length; x++) {
              if (AlphabetMoreAids[j][x] == ',') {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                if (cbactive)
                  CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                yield return new WaitForSeconds(.2f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                if (cbactive)
                  CBText.text = "";
                yield return new WaitForSeconds(1f);
              }
              else if (AlphabetMoreAids[j][x] == '.') {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                if (cbactive)
                  CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                yield return new WaitForSeconds(.6f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                if (cbactive)
                  CBText.text = "";
                yield return new WaitForSeconds(1f);
              }
              else if (AlphabetMoreAids[j][x] == '=') {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                if (cbactive)
                  CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                yield return new WaitForSeconds(1f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                if (cbactive)
                  CBText.text = "";
                yield return new WaitForSeconds(1f);
              }
              else if (AlphabetMoreAids[j][x] == '-')  {
                MoreAids.GetComponent<MeshRenderer>().material = Aids[1];
                if (cbactive)
                  CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                yield return new WaitForSeconds(3f);
                MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
                if (cbactive)
                  CBText.text = "";
                yield return new WaitForSeconds(1f);
              }
            }
            MoreAids.GetComponent<MeshRenderer>().material = Aids[2];
            if (cbactive)
              CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
            yield return new WaitForSeconds(1f);
            MoreAids.GetComponent<MeshRenderer>().material = Aids[0];
            if (cbactive)
              CBText.text = "";
            yield return new WaitForSeconds(1f);
          }
        }
      }
      StartCoroutine(juyhkmghgjmhgnjvmgnhmfjgjhmfnhgjmn());
    }

    IEnumerator SolvingAids () {
      FanfareAids += 1;
      for (int i = 0; i < 5; i++)
        AidsText[i].text = (UnityEngine.Random.Range(0,10)).ToString();
      if (FanfareAids % 2 == 0) {
        MoreAids.GetComponent<MeshRenderer>().material = Aids[UnityEngine.Random.Range(0, 4)];
        if (cbactive) {
          if (MoreAids.GetComponent<MeshRenderer>().material.name.Equals("black (Instance)"))
            CBText.text = "";
          else
            CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
        }
      }
      if (FanfareAids == 40) {
        GetComponent<KMBombModule>().HandlePass();
        moduleSolved = true;
        MoreAids.GetComponent<MeshRenderer>().material = Aids[3];
        if (cbactive)
          CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
        for (int i = 0; i < 5; i++)
          AidsText[i].text = "!";
      }
      else {
        yield return new WaitForSecondsRealtime(0.05f);
        StartCoroutine(SolvingAids());
      }
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} transmit/trans/tx <frq> [Submits the specified frequency 'frq'] | !{0} colorblind [Toggles colorblind mode]";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand (string command) {
        if (Regex.IsMatch(command, @"^\s*colorblind\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) {
            yield return null;
            if (cbactive) {
                cbactive = false;
                CBText.text = "";
            }
            else {
                cbactive = true;
                if (MoreAids.GetComponent<MeshRenderer>().material.name.Equals("black (Instance)"))
                    CBText.text = "";
                else
                    CBText.text = MoreAids.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
            }
            yield break;
        }
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*transmit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[0], @"^\s*trans\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[0], @"^\s*tx\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) {
            yield return null;
            if (parameters.Length > 2)
                yield return "sendtochaterror Too many parameters!";
            else if (parameters.Length == 2) {
                if (Regex.IsMatch(parameters[1], @"[0-9].[0-9][0-9][0-9]") || Regex.IsMatch(parameters[1], @"[0-9],[0-9][0-9][0-9]")) {
                    for (int i = 0; i < 5; i++) {
                        while (AidsText[i].text != parameters[1][i].ToString()) {
                            AidsNumbers[i].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    AidsSubmit.OnInteract();
                    if (parameters[1].Equals("4." + correct))
                        yield return "solve";
                }
                else
                    yield return "sendtochaterror The specified frequency '" + parameters[1] + "' is invalid!";
            }
            else if (parameters.Length == 1)
                yield return "sendtochaterror Please specify the frequency to submit!";
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve () {
        while (!activated) { yield return true; yield return new WaitForSeconds(0.1f); }
        if (FanfareAids == 0) {
            string corr = "4."+correct;
            for (int i = 0; i < 5; i++) {
                while (AidsText[i].text != corr[i].ToString()) {
                    AidsNumbers[i].OnInteract();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            AidsSubmit.OnInteract();
        }
        while (!moduleSolved) { yield return true; yield return new WaitForSeconds(0.1f); }
    }
}
