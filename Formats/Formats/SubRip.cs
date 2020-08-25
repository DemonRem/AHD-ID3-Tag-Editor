/* This file is part of AHD Subtitles Maker Professional
   A program can create and edit subtitles

   Copyright © Ala Ibrahim Hadid 2009 - 2015

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using AHD.SM.ASMP;

namespace AHD.SM.Formats
{
    public class SubRip : SubtitlesFormat
    {
        public bool EnableSubtitlesFontAndColor = true;

        public override string Name
        {
            get { return "SubRip (*.srt)"; }
        }

        public override string Description
        {
            get { return "SubRip\n\nThis format supports font and color, you should enable font and color via format's options GUI. Please note that the font and color will be writen as HTML tags, not Mplayer tags, these kind of tags are not supported.\nThis format type has this view:\n1\n00:00:01,120 --> 00:00:02,340 \nText1\n\n2\n00:00:04,004 --> 00:00:06,023 \nText2\n......."; }
        }

        public override string[] Extensions
        {
            get { string[] _Extensions = { ".srt" }; return _Extensions; }
        }

        public override bool CheckFile(string filePath, Encoding encoding)
        {
            string[] Lines = File.ReadAllLines(filePath, encoding);
            if (Lines.Length > 2)
            {
                try
                {
                    if (Lines[1].Substring(13, 3) == "-->")
                    { return true; }
                }
                catch { }
            }
            return false;
        }

        public override void Load(string filePath, Encoding encoding)
        {
            if (LoadStarted != null)
                LoadStarted(this, new EventArgs());
            this.FilePath = filePath;
            this.SubtitleTrack = new SubtitlesTrack();
            string[] Lines = File.ReadAllLines(FilePath, encoding);

            for (int i = 0; i < Lines.Length; i++)
            {
                try
                {
                    Subtitle sub = new Subtitle();
                 
                    string[] TextLines = Lines[i].Split(new string[] { "-->" }, StringSplitOptions.RemoveEmptyEntries);
                    if (TextLines.Length == 1)
                        continue;
                    sub.StartTime = TimeFormatConvertor.From_TimeSpan_Milli(TextLines[0]);
                    sub.EndTime = TimeFormatConvertor.From_TimeSpan_Milli(TextLines[1]);
                    sub.Text = new SubtitleText();
                    i++;

                    FontStyle style = FontStyle.Regular;
                    List<Color> colors = new List<Color>();
                    List<string> fontNames = new List<string>();
                    List<float> fontSizes = new List<float>();

                    List<string> resetSeq = new List<string>();
                    while (Lines[i] != "")
                    {
                        SubtitleLine newLine = new SubtitleLine();

                        for (int j = 0; j < Lines[i].Length; j++)
                        {
                            if (Lines[i][j] == '<')
                            {
                                j++;
                                string code = "";
                                while (Lines[i][j] != '>')
                                {
                                    code += Lines[i][j];
                                    j++;
                                }
                                if (code.StartsWith("/"))//this is end of code, reset
                                {
                                    if (code.Length == 2)//style code
                                    {
                                        switch (code.ToLower())
                                        {
                                            case "/b": style &= ~FontStyle.Bold; break;
                                            case "/i": style &= ~FontStyle.Italic; break;
                                            case "/s": style &= ~FontStyle.Strikeout; break;
                                            case "/u": style &= ~FontStyle.Underline; break;
                                        }
                                    }
                                    else if (code.Contains("font"))
                                    {
                                        if (resetSeq.Count == 0)//no chances to take, if there's nothing to reset, reset all
                                        {
                                            if (colors.Count > 0)
                                                colors.RemoveAt(colors.Count - 1);
                                            if (fontNames.Count > 0)
                                                fontNames.RemoveAt(fontNames.Count - 1);
                                            if (fontSizes.Count > 0)
                                                fontSizes.RemoveAt(fontSizes.Count - 1);
                                        }
                                        else
                                        {
                                            string resetCode = resetSeq[resetSeq.Count - 1];
                                            if (resetCode.Contains("color"))
                                            {
                                                //color = Color.White;
                                                if (colors.Count > 0)
                                                    colors.RemoveAt(colors.Count - 1);
                                            }
                                            if (resetCode.Contains("size"))
                                            {
                                                if (fontSizes.Count > 0)
                                                    fontSizes.RemoveAt(fontSizes.Count - 1);
                                            }
                                            if (resetCode.Contains("face"))
                                            {
                                                if (fontNames.Count > 0)
                                                    fontNames.RemoveAt(fontNames.Count - 1);
                                            }
                                            resetSeq.Remove(resetCode);
                                        }
                                    }
                                }
                                else if (code.Length == 1)//style code
                                {
                                    switch (code.ToLower())
                                    {
                                        case "b": style |= FontStyle.Bold; break;
                                        case "i": style |= FontStyle.Italic; break;
                                        case "s": style |= FontStyle.Strikeout; break;
                                        case "u": style |= FontStyle.Underline; break;
                                    }
                                }
                                else if (code.Contains("font"))//font and/or color
                                {
                                    string[] codes = code.Split(new char[] { ' ' });
                                    //the first one should be 'font', so start from second one
                                    for (int c = 1; c < codes.Length; c++)
                                    {
                                        if (codes[c].Contains("color"))
                                        {
                                            string colorCode = codes[c].Replace("color=", "");
                                            colorCode = colorCode.Replace(@"""", "");
                                            if (colorCode.StartsWith("#"))
                                            {
                                                int col = int.Parse(colorCode.Substring(1), System.Globalization.NumberStyles.AllowHexSpecifier);
                                                byte R = (byte)((col & 0xFF0000) >> 16);
                                                byte G = (byte)((col & 0x00FF00) >> 8);
                                                byte B = (byte)((col & 0x0000FF));
                                                //color = System.Drawing.Color.FromArgb(0xFF, R, G, B);
                                                colors.Add(System.Drawing.Color.FromArgb(0xFF, R, G, B));
                                            }
                                            else//the color is a string... try to parse
                                            {
                                                try
                                                {
                                                    //color = Color.FromName(colorCode);
                                                }
                                                catch
                                                {
                                                    //color = Color.White;
                                                    colors.Add(Color.White);
                                                }
                                            }
                                        }
                                        else if (codes[c].Contains("size"))
                                        {
                                            string sizeCode = codes[c].Replace("size=", "");
                                            sizeCode = sizeCode.Replace(@"""", "");

                                            float val = 0;
                                            if (float.TryParse(sizeCode, out val))
                                                fontSizes.Add(val);
                                            else
                                                fontSizes.Add(8);
                                        }
                                        else if (codes[c].Contains("face"))
                                        {
                                            string nameCode = codes[c].Replace("face=", "");
                                            nameCode = nameCode.Replace(@"""", "");

                                            fontNames.Add(nameCode);
                                        }
                                    }
                                    //set this so we will know which to reset later
                                    string resetCode = "";
                                    if (code.Contains("color"))
                                        resetCode = "color";
                                    if (code.Contains("size"))
                                        resetCode += ",size";
                                    if (code.Contains("face"))
                                        resetCode += ",face";
                                    resetSeq.Add(resetCode);
                                }
                            }
                            else if (Lines[i][j] == '{')
                            {

                            }
                            else//text char, add to line
                            {
                                Color color = Color.White;
                                if (colors.Count > 0)
                                    color = colors[colors.Count - 1];
                                string fontName = "Tahoma";
                                if (fontNames.Count > 0)
                                    fontName = fontNames[fontNames.Count - 1];
                                float fontSize = 8;
                                if (fontSizes.Count > 0)
                                    fontSize = fontSizes[fontSizes.Count - 1];
                                newLine.Chars.Add(new SubtitleChar(Lines[i][j], new Font(fontName, fontSize, style), color));
                            }
                        }
                        sub.Text.TextLines.Add(newLine);
                        i++;
                        if (i == Lines.Length)
                            break;
                    }
                    this.SubtitleTrack.Subtitles.Add(sub);
                }
                catch { }
                int x = (100 * i) / Lines.Length;
                if (Progress != null)
                    Progress(this, new ProgressArgs(x, "Loading file ...."));
            }
            if (Progress != null)
                Progress(this, new ProgressArgs(100, "Load Completed."));
            if (LoadFinished != null)
                LoadFinished(this, new EventArgs());
        }

        public override void Save(string filePath, Encoding encoding)
        {
            this.FilePath = filePath;
            if (SaveStarted != null)
                SaveStarted(this, new EventArgs());
            List<string> Lines = new List<string>();
            for (int i = 0; i < this.SubtitleTrack.Subtitles.Count; i++)
            {
                //add number
                Lines.Add((i + 1).ToString());
                //add timecodes and coordinates
                string lin = TimeFormatConvertor.To_TimeSpan_Milli(this.SubtitleTrack.Subtitles[i].StartTime, ",", MillisecondLength.N3) +
                    " --> " +
                     TimeFormatConvertor.To_TimeSpan_Milli(this.SubtitleTrack.Subtitles[i].EndTime, ",", MillisecondLength.N3);
                Lines.Add(lin);
                //just in case fonts and colors are enabled
                string fontName = this.SubtitleTrack.Subtitles[i].Text.TextLines[0].Chars[0].Font.Name;
                float fontSize = this.SubtitleTrack.Subtitles[i].Text.TextLines[0].Chars[0].Font.Size;
                Color fontColor = this.SubtitleTrack.Subtitles[i].Text.TextLines[0].Chars[0].Color;
                FontStyle fontStyle = this.SubtitleTrack.Subtitles[i].Text.TextLines[0].Chars[0].Font.Style;
                //write current values for first time
                int color = (fontColor.R << 16) | (fontColor.G << 8) | (fontColor.B);
                string textLine = @"<font color=""#" + string.Format("{0:X}", color) + @"""" + @" size=""" + fontSize.ToString() + @""""
                    + @" face=""" + fontName + @"""" + ">";
                if ((fontStyle & FontStyle.Bold) == FontStyle.Bold)
                    textLine += "<b>";
                if ((fontStyle & FontStyle.Italic) == FontStyle.Italic)
                    textLine += "<i>";
                if ((fontStyle & FontStyle.Strikeout) == FontStyle.Strikeout)
                    textLine += "<s>";
                if ((fontStyle & FontStyle.Underline) == FontStyle.Underline)
                    textLine += "<u>";
                //add text lines with font options
                for (int j = 0; j < this.SubtitleTrack.Subtitles[i].Text.TextLines.Count; j++)
                {
                    if (!EnableSubtitlesFontAndColor)
                    {
                        Lines.Add(this.SubtitleTrack.Subtitles[i].Text.TextLines[j].ToString());
                    }
                    else//html tags
                    {
                        for (int c = 0; c < this.SubtitleTrack.Subtitles[i].Text.TextLines[j].Chars.Count; c++)
                        {
                            SubtitleChar currentChar = this.SubtitleTrack.Subtitles[i].Text.TextLines[j].Chars[c];
                            //if nothing changed in font, style and color just add the char
                            if (fontStyle == currentChar.Font.Style && fontName == currentChar.Font.Name &&
                                fontSize == currentChar.Font.Size && fontColor == currentChar.Color)
                            {
                                textLine += currentChar.TheChar;
                            }
                            else//something changed, we must do something
                            {
                                //style ?
                                //end previous if it have to
                                if (((fontStyle & FontStyle.Bold) == FontStyle.Bold) &&
                                    ((currentChar.Font.Style & FontStyle.Bold) != FontStyle.Bold))
                                {
                                    fontStyle &= ~FontStyle.Bold;
                                    textLine += "</b>"; 
                                }
                                if (((fontStyle & FontStyle.Italic) == FontStyle.Italic) &&
                                  ((currentChar.Font.Style & FontStyle.Italic) != FontStyle.Italic))
                                {
                                    fontStyle &= ~FontStyle.Italic;
                                    textLine += "</i>"; 
                                }
                                if (((fontStyle & FontStyle.Strikeout) == FontStyle.Strikeout) &&
                                 ((currentChar.Font.Style & FontStyle.Strikeout) != FontStyle.Strikeout))
                                {
                                    fontStyle &= ~FontStyle.Strikeout;
                                    textLine += "</s>"; 
                                }
                                if (((fontStyle & FontStyle.Underline) == FontStyle.Underline) &&
                             ((currentChar.Font.Style & FontStyle.Underline) != FontStyle.Underline))
                                {
                                    fontStyle &= ~FontStyle.Underline;
                                    textLine += "</u>"; 
                                }
                                //start new style if it have to
                                if (((fontStyle & FontStyle.Bold) != FontStyle.Bold) &&
                                 ((currentChar.Font.Style & FontStyle.Bold) == FontStyle.Bold))
                                {
                                    fontStyle |= FontStyle.Bold;
                                    textLine += "<b>"; 
                                }
                                if (((fontStyle & FontStyle.Italic) != FontStyle.Italic) &&
                                  ((currentChar.Font.Style & FontStyle.Italic) == FontStyle.Italic))
                                {
                                    fontStyle |= FontStyle.Italic;
                                    textLine += "<i>"; 
                                }
                                if (((fontStyle & FontStyle.Strikeout) != FontStyle.Strikeout) &&
                                 ((currentChar.Font.Style & FontStyle.Strikeout) == FontStyle.Strikeout))
                                {
                                    fontStyle |= FontStyle.Strikeout;
                                    textLine += "<s>"; 
                                }
                                if (((fontStyle & FontStyle.Underline) != FontStyle.Underline) &&
                             ((currentChar.Font.Style & FontStyle.Underline) == FontStyle.Underline))
                                {
                                    fontStyle |= FontStyle.Underline;
                                    textLine += "<u>"; 
                                }
                                //font
                                if (fontName != currentChar.Font.Name ||
                                  fontSize != currentChar.Font.Size || fontColor != currentChar.Color)
                                {
                                    textLine += "</font>";
                                    //start new values
                                    fontName = currentChar.Font.Name;
                                    fontSize = currentChar.Font.Size;
                                    fontColor = currentChar.Color;
                                    //write current values for first time
                                    color = (fontColor.R << 16) | (fontColor.G << 8) | (fontColor.B);
                                    textLine += @"<font color=""#" + string.Format("{0:X}", color) + @"""" + @" size=""" + fontSize.ToString() + @""""
                                        + @" face=""" + fontName + @"""" + ">";
                                }
                                //add the char
                                textLine += currentChar.TheChar;
                            }
                        }
                        //final ending code
                        if (j == this.SubtitleTrack.Subtitles[i].Text.TextLines.Count - 1)
                        {
                            if ((fontStyle & FontStyle.Bold) == FontStyle.Bold)
                                textLine += "</b>";
                            if ((fontStyle & FontStyle.Italic) == FontStyle.Italic)
                                textLine += "</i>";
                            if ((fontStyle & FontStyle.Strikeout) == FontStyle.Strikeout)
                                textLine += "</s>";
                            if ((fontStyle & FontStyle.Underline) == FontStyle.Underline)
                                textLine += "</u>";
                            textLine += "</font>";
                        }
                        //add it
                        Lines.Add(textLine);
                        textLine = "";
                    }
                }
                int x = (100 * i) / this.SubtitleTrack.Subtitles.Count;
                if (Progress != null)
                    Progress(this, new ProgressArgs(x, "Saving ...."));
                //add sperater
                Lines.Add("");
            }
            File.WriteAllLines(FilePath, Lines.ToArray(), encoding);
            if (Progress != null)
                Progress(this, new ProgressArgs(100, "Save Completed."));
            if (SaveFinished != null)
                SaveFinished(this, new EventArgs());
        }

        public override event EventHandler<ProgressArgs> Progress;

        public override event EventHandler LoadStarted;

        public override event EventHandler LoadFinished;

        public override event EventHandler SaveStarted;

        public override event EventHandler SaveFinished;

        public override bool HasOptions
        {
            get
            {
                return true;
            }
        }
        public override System.Windows.Forms.UserControl OptionsControl
        {
            get
            {
                return new cl_SubRip(this);
            }
        }
    }
}
