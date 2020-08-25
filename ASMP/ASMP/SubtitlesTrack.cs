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

namespace AHD.SM.ASMP
{
    /// <summary>
    /// Subtitles Track, a class holds subtitles.
    /// </summary>
    [Serializable()]
    public class SubtitlesTrack
    {
        /// <summary>
        /// Subtitles Track, a class holds subtitles.
        /// </summary>
        public SubtitlesTrack()
        {
            this.name = "";
            subtitles = new List<Subtitle>();
        }
        /// <summary>
        /// Subtitles Track, a class holds subtitles.
        /// </summary>
        /// <param name="name">The name of this track</param>
        public SubtitlesTrack(string name)
        { 
            this.name = name;
            subtitles = new List<Subtitle>();
        }

        string name;
        List<Subtitle> subtitles;
        bool rtl;

        /// <summary>
        /// Get or set the name of this track
        /// </summary>
        public string Name
        { get { return name; } set { name = value;} }
        /// <summary>
        /// Get or set the subtitles collection
        /// </summary>
        public List<Subtitle> Subtitles
        { get { return subtitles; } set { subtitles = value; } }
        /// <summary>
        /// Get or set a value indecate wether to set right to left alignement by default to new subtitle added
        /// </summary>
        public bool RightToLeft
        { get { return rtl; } set { rtl = value; } }
        /// <summary>
        /// Subtitles Track name
        /// </summary>
        /// <returns>The name of this track</returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// Clone this track
        /// </summary>
        /// <returns>An exact copy of this track</returns>
        public SubtitlesTrack Clone()
        {
            SubtitlesTrack newTrack = new SubtitlesTrack(this.name);
            newTrack.rtl = this.rtl;

            foreach (Subtitle sub in this.subtitles)
            {
                newTrack.subtitles.Add(sub.Clone());
            }

            return newTrack;
        }
    }
}
