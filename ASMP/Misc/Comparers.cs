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
    /// Comparer used to compare two subtitles
    /// </summary>
    public class SubtitleComparer : IComparer<Subtitle>
    {
        public SubtitleComparer()
        {
            mode = SubtitleCompareType.StartTime;
        }
        public SubtitleComparer(SubtitleCompareType compareMode)
        {
            mode = compareMode;
        }
        SubtitleCompareType mode = SubtitleCompareType.StartTime;
        public int Compare(Subtitle x, Subtitle y)
        { /*
             * Less than zero   : x is less than y. 
             * Zero             : x equals y. 
             * Greater than zero: x is greater than y. 
             */
            switch (mode)
            {
                default:
                case SubtitleCompareType.StartTime:
                    return (int)(x.StartTime - y.StartTime);
                case SubtitleCompareType.EndTime:
                    return (int)(x.EndTime - y.EndTime);
            }
        }
    }
    public enum SubtitleCompareType
    { StartTime, EndTime }
}