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
   public KMSelectable[] Buttons;
   public KMSelectable SubmitButton;
   public KMColorblindMode Colorblind;
   public TextMesh[] NumberTexts;
   public TextMesh CBText;
   public GameObject FlashDisplay;
   public Material[] Colors;//KORG

   private List<int> BobsDickAndVowel = new List<int> { 248, 243, 201, 238, 200, 291, 297, 268, 275, 269, 232, 211, 296 };
   private List<int> BobsDick = new List<int> { 276, 242, 274, 213, 214, 212, 280, 258, 271, 204, 210, 246, 227 };
   private List<int> Faulty = new List<int> { 205, 224, 293, 207, 234, 253, 289, 272, 288, 225, 241, 239, 285 };
   private List<int> OtherwiseAM = new List<int> { 209, 273, 251, 249, 267, 261, 247, 282, 281, 235, 294, 254, 256 };
   private List<int> TwoBatAndBroken = new List<int> { 233, 245, 250, 300, 266, 226, 283, 244, 219, 229, 278, 263 };
   private List<int> TwoBat = new List<int> { 216, 220, 240, 255, 215, 290, 223, 237, 284, 218, 206, 298 };
   private List<int> TRN = new List<int> { 287, 262, 228, 208, 217, 286, 210, 264, 252, 260, 270, 231 };
   private List<int> OtherwiseNZ = new List<int> { 222, 257, 295, 279, 259, 236, 292, 277, 299, 202, 203, 230 };
   int AEIOUY;
   int Broken;
   int correct;
   int FanfareColors;
   int FaultyButInt;
   int First = 4;
   int Fourth;
   int NumberThatINeed;
   int NumberYouSubmit;
   int Second = 2;
   int Third;

   string[] WordList = { "ALLOCATE", "BULWARKS", "COMPILER", "DISPOSAL", "ENCIPHER", "FORMULAE", "GAUNTLET", "HUNKERED", "ILLUSORY", "JOUSTING", "KINETICS", "LINKWORK", "MONOLITH", "NANOBOTS", "OCTANGLE", "POSTSYNC", "QUARTICS", "REVOLVED", "STANZAIC", "TOMAHAWK", "ULTRAHOT", "VENDETTA", "WAFFLERS", "YOKOZUNA", "ZUGZWANG", "XENOLITH" };
   //Dit I ,|| Dot O .|| Dah A =|| Dash S -
   string[] MoreAlphabet = { ".-.,.", "=", "=,,", "=,-", ".,.-=", "-.-", ".", ",..", "=--=,", "=.,", ",", "=-,=", ",,.", "--.=", "-----", "-", "==.", ",--=", "-,==", "=.,,", "=====", "=,", ",=", "=.", "-,", ",=-" };
   string ActualAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   string Comma = ".";
   string SolutionWord;
   string SerialNumber;

   bool AEIOUCheck;
   bool Activated;
   bool BrokenCheck;
   bool cbactive;
   bool FaultyCheck;
   bool PlacementCheck;

   static int moduleIdCounter = 1;
   int moduleId;
   private bool ModuleSolved;

   void Awake () {
      moduleId = moduleIdCounter++;
      cbactive = Colorblind.ColorblindModeActive;
      foreach (KMSelectable Number in Buttons) {
         Number.OnInteract += delegate () { NumberPress(Number); return false; };
      }
      SubmitButton.OnInteract += delegate () { SubmitPress(); return false; };
      GetComponent<KMBombModule>().OnActivate += OnActivate;
   }

   void Start () {
      SerialNumber = Bomb.GetSerialNumber();
      for (int i = 0; i < 6; i++) {
         if (SerialNumber[i].ToString().Any(x => "AUY".Contains(x))) {
            AEIOUY++;
            FaultyButInt++;
         }
         if (SerialNumber[i].ToString().Any(x => "EO".Contains(x))) {
            AEIOUY++;
            Broken++;
         }
         if (SerialNumber[i].ToString().Any(x => "I".Contains(x))) {
            AEIOUY++;
         }
         if (SerialNumber[i].ToString().Any(x => "FLT".Contains(x))) {
            FaultyButInt++;
         }
         if (SerialNumber[i].ToString().Any(x => "BRKN".Contains(x))) {
            Broken++;
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
   }

   void OnActivate () {
      NumberTexts[0].text = First.ToString();
      NumberTexts[1].text = Comma;
      NumberTexts[2].text = Second.ToString();
      NumberTexts[3].text = Third.ToString();
      NumberTexts[4].text = Fourth.ToString();
      WordPicker();
      Activated = true;
   }

   void NumberPress (KMSelectable Number) {
      GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Number.transform);
      if (ModuleSolved || !Activated) {
         return;
      }
      for (int i = 0; i < 5; i++) {
         if (Number == Buttons[i]) {
            switch (i) {
               case 0:
                  First = (First + 1) % 10;
                  NumberTexts[i].text = First.ToString();
                  break;
               case 1:
                  Comma = Comma == "." ? "," : ".";
                  NumberTexts[i].text = Comma;
                  break;
               case 2:
                  Second = (Second + 1) % 10;
                  NumberTexts[i].text = Second.ToString();
                  break;
               case 3:
                  Third = (Third + 1) % 10;
                  NumberTexts[i].text = Third.ToString();
                  break;
               case 4:
                  Fourth = (Fourth + 1) % 10;
                  NumberTexts[i].text = Fourth.ToString();
                  break;
            }
         }
      }
   }

   void SubmitPress () {
      SubmitButton.AddInteractionPunch();
      GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, SubmitButton.transform);
      if (ModuleSolved || !Activated) {
         return;
      }
      NumberYouSubmit = Second * 100 + Third * 10 + Fourth;
      if (Comma == ",") {
         GetComponent<KMBombModule>().HandleStrike();
         StopAllCoroutines();
         Audio.PlaySoundAtTransform("strike", transform);
         FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
         PlacementCheck = false;
         Debug.LogFormat("[More Code #{0}] You submitted with a comma. Strike dumbass! Resetting...", moduleId);
         WordPicker();
         return;
      }
      if (First != 4) {
         GetComponent<KMBombModule>().HandleStrike();
         StopAllCoroutines();
         Audio.PlaySoundAtTransform("strike", transform);
         FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
         PlacementCheck = false;
         Debug.LogFormat("[More Code #{0}] You submitted without a preceeding 4. Strike, dumbass! Resetting...", moduleId);
         WordPicker();
         return;
      }
      if (SolutionWord == "XENOLITH" && NumberYouSubmit == 265) {
         StopAllCoroutines();
         Audio.PlaySoundAtTransform("solve", transform);
         StartCoroutine(SolvingColors());
         ModuleSolved = true;
         return;
      }
      if (SolutionWord == "XENOLITH" && NumberYouSubmit != 265) {
         GetComponent<KMBombModule>().HandleStrike();
         StopAllCoroutines();
         Audio.PlaySoundAtTransform("strike", transform);
         FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
         PlacementCheck = false;
         Debug.LogFormat("[More Code #{0}] You submitted 4.{1}. Strike, window-licker. Resetting...", moduleId, NumberYouSubmit);
         WordPicker();
         return;
      }
      for (int i = 0; i < 13; i++) {
         if (SolutionWord[0] == ActualAlphabet[i]) {
            PlacementCheck = true;
         }
      }
      if (PlacementCheck) {
         if ((Bomb.IsIndicatorOn("BOB") && AEIOUCheck && BobsDickAndVowel[NumberThatINeed] == NumberYouSubmit) || (Bomb.IsIndicatorOn("BOB") && BobsDick[NumberThatINeed] == NumberYouSubmit) || (FaultyCheck && Faulty[NumberThatINeed] == NumberYouSubmit) || (OtherwiseAM[NumberThatINeed] == NumberYouSubmit)) {
            StopAllCoroutines();
            Audio.PlaySoundAtTransform("solve", transform);
            StartCoroutine(SolvingColors());
         }
         else {
            GetComponent<KMBombModule>().HandleStrike();
            StopAllCoroutines();
            Audio.PlaySoundAtTransform("strike", transform);
            FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
            PlacementCheck = false;
            Debug.LogFormat("[More Code #{0}] You submitted 4.{1}. Strike, window-licker. Resetting...", moduleId, NumberYouSubmit);
            WordPicker();
         }
      }
      else {
         if ((Bomb.GetBatteryCount() - 2 >= 0 && BrokenCheck && TwoBatAndBroken[NumberThatINeed - 13] == NumberYouSubmit) || (Bomb.GetBatteryCount() - 2 >= 0 && TwoBat[NumberThatINeed - 13] == NumberYouSubmit) || (Bomb.IsIndicatorOn("TRN") && TRN[NumberThatINeed - 13] == NumberYouSubmit) || (OtherwiseNZ[NumberThatINeed - 13] == NumberYouSubmit)) {
            StopAllCoroutines();
            Audio.PlaySoundAtTransform("solve", transform);
            StartCoroutine(SolvingColors());
         }
         else {
            GetComponent<KMBombModule>().HandleStrike();
            StopAllCoroutines();
            Audio.PlaySoundAtTransform("strike", transform);
            FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
            PlacementCheck = false;
            Debug.LogFormat("[More Code #{0}] You submitted 4.{1}. Strike, window-licker. Resetting...", moduleId, NumberYouSubmit);
            WordPicker();
         }
      }
   }

   void WordPicker () {
      NumberThatINeed = UnityEngine.Random.Range(0, WordList.Length);
      SolutionWord = WordList[NumberThatINeed];
      Debug.LogFormat("[More Code #{0}] The word that is flashing is {1}.", moduleId, SolutionWord);
      if (SolutionWord == "XENOLITH") {
         correct = 265;
         Debug.LogFormat("[More Code #{0}] The frequency you should submit is 4.265.", moduleId);
      }
      else if (Bomb.IsIndicatorOn("BOB") && AEIOUCheck && NumberThatINeed <= 12) {
         correct = BobsDickAndVowel[NumberThatINeed];
      }
      else if (Bomb.IsIndicatorOn("BOB") && NumberThatINeed <= 12) {
         correct = BobsDick[NumberThatINeed];
      }
      else if (FaultyCheck && NumberThatINeed <= 12) {
         correct = Faulty[NumberThatINeed];
      }
      else if (NumberThatINeed <= 12) {
         correct = OtherwiseAM[NumberThatINeed];
      }
      else if (Bomb.GetBatteryCount() - 2 >= 0 && BrokenCheck) {
         correct = TwoBatAndBroken[NumberThatINeed - 13];
      }
      else if (Bomb.GetBatteryCount() - 2 >= 0) {
         correct = TwoBat[NumberThatINeed - 13];
      }
      else if (Bomb.IsIndicatorOn("TRN")) {
         correct = TRN[NumberThatINeed - 13];
      }
      else {
         correct = OtherwiseNZ[NumberThatINeed - 13];
      }
      if (correct != 265) {
         Debug.LogFormat("[More Code #{0}] The frequency you should submit is 4.{1}.", moduleId, correct);
      }
      StartCoroutine(FlashWord());
   }

   IEnumerator FlashWord () {
      while (true) {
         for (int i = 0; i < 8; i++) {
            for (int j = 0; j < ActualAlphabet.Length; j++) {
               if (SolutionWord[i] == ActualAlphabet[j]) {
                  for (int x = 0; x < MoreAlphabet[j].Length; x++) {
                     if (MoreAlphabet[j][x] == ',') {
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[1];
                        if (cbactive) {
                           CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                        }
                        yield return new WaitForSeconds(.2f);
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
                        if (cbactive) {
                           CBText.text = "";
                        }
                        yield return new WaitForSeconds(1f);
                     }
                     else if (MoreAlphabet[j][x] == '.') {
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[1];
                        if (cbactive) {
                           CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                        }
                        yield return new WaitForSeconds(.6f);
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
                        if (cbactive) {
                           CBText.text = "";
                        }
                        yield return new WaitForSeconds(1f);
                     }
                     else if (MoreAlphabet[j][x] == '=') {
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[1];
                        if (cbactive) {
                           CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                        }
                        yield return new WaitForSeconds(1f);
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
                        if (cbactive) {
                           CBText.text = "";
                        }
                        yield return new WaitForSeconds(1f);
                     }
                     else if (MoreAlphabet[j][x] == '-') {
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[1];
                        if (cbactive) {
                           CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                        }
                        yield return new WaitForSeconds(3f);
                        FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
                        if (cbactive) {
                           CBText.text = "";
                        }
                        yield return new WaitForSeconds(1f);
                     }
                  }
                  FlashDisplay.GetComponent<MeshRenderer>().material = Colors[2];
                  if (cbactive) {
                     CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
                  }
                  yield return new WaitForSeconds(1f);
                  FlashDisplay.GetComponent<MeshRenderer>().material = Colors[0];
                  if (cbactive) {
                     CBText.text = "";
                  }
                  yield return new WaitForSeconds(1f);
               }
            }
         }
      }
   }

   IEnumerator SolvingColors () {
      FanfareColors++;
      for (int i = 0; i < 5; i++) {
         NumberTexts[i].text = (UnityEngine.Random.Range(0, 10)).ToString();
      }
      if (FanfareColors % 2 == 0) {
         FlashDisplay.GetComponent<MeshRenderer>().material = Colors[UnityEngine.Random.Range(0, 4)];
         if (cbactive) {
            if (FlashDisplay.GetComponent<MeshRenderer>().material.name.Equals("black (Instance)")) {
               CBText.text = "";
            }
            else {
               CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
            }
         }
      }
      if (FanfareColors == 40) {
         GetComponent<KMBombModule>().HandlePass();
         ModuleSolved = true;
         FlashDisplay.GetComponent<MeshRenderer>().material = Colors[3];
         if (cbactive) {
            CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
         }
         for (int i = 0; i < 5; i++) {
            NumberTexts[i].text = "!";
         }
      }
      else {
         yield return new WaitForSecondsRealtime(0.05f);
         StartCoroutine(SolvingColors());
      }
   }

   //twitch plays
#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"!{0} transmit/trans/tx <frq> [Submits the specified frequency 'frq'] | !{0} colorblind [Toggles colorblind mode]";
#pragma warning restore 414
   IEnumerator ProcessTwitchCommand (string command) {
      yield return null;
      if (Regex.IsMatch(command, @"^\s*colorblind\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) {
         if (cbactive) {
            cbactive = false;
            CBText.text = "";
         }
         else {
            cbactive = true;
            if (FlashDisplay.GetComponent<MeshRenderer>().material.name.Equals("black (Instance)")) {
               CBText.text = "";
            }
            else {
               CBText.text = FlashDisplay.GetComponent<MeshRenderer>().material.name.Replace(" (Instance)", "");
            }
         }
         yield break;
      }
      string[] parameters = command.Split(' ');
      if (Regex.IsMatch(parameters[0], @"^\s*transmit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[0], @"^\s*trans\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(parameters[0], @"^\s*tx\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) {
         if (parameters.Length > 2) {
            yield return "sendtochaterror Too many parameters!";
         }
         else if (parameters.Length == 2) {
            if (Regex.IsMatch(parameters[1], @"[0-9].[0-9][0-9][0-9]") || Regex.IsMatch(parameters[1], @"[0-9],[0-9][0-9][0-9]")) {
               for (int i = 0; i < 5; i++) {
                  while (NumberTexts[i].text != parameters[1][i].ToString()) {
                     Buttons[i].OnInteract();
                     yield return new WaitForSeconds(0.1f);
                  }
               }
               SubmitButton.OnInteract();
               if (parameters[1].Equals("4." + correct)) {
                  yield return "solve";
               }
            }
            else {
               yield return "sendtochaterror The specified frequency '" + parameters[1] + "' is invalid!";
            }
         }
         else if (parameters.Length == 1) {
            yield return "sendtochaterror Please specify the frequency to submit!";
         }
      }
   }

   IEnumerator TwitchHandleForcedSolve () {
      while (!Activated) {
         yield return true;
      }
      if (FanfareColors == 0) {
         string corr = "4." + correct;
         for (int i = 0; i < 5; i++) {
            while (NumberTexts[i].text != corr[i].ToString()) {
               Buttons[i].OnInteract();
               yield return new WaitForSeconds(0.1f);
            }
         }
         SubmitButton.OnInteract();
      }
      while (!ModuleSolved) {
         yield return true;
      }
   }
}
