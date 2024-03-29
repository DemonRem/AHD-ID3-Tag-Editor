﻿/* This file is part of AHD Subtitles Maker Professional
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
    public class TimeFormatChecker
    {
        /// <summary>
        /// check if given time format is in Timespan.X form (Timespan.milli or Timespan:frame)
        /// </summary>
        /// <param name="time">The time in Timespan form (Timespan.milli or Timespan.frame)</param>
        /// <returns>True if the time is is in Timespan form (Timespan.milli or Timespan.frame), otherwise false</returns>
        public static bool IsTimeSpanX(string time)
        {
            string[] ent = time.Split(new char[] { ':', ';', '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (ent.Length == 4)
            {
                int x = 0;
                if (int.TryParse(ent[0], out x) & int.TryParse(ent[1], out x)
                  & int.TryParse(ent[2], out x) & int.TryParse(ent[3], out x))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// check if given time format is in Timespan form
        /// </summary>
        /// <param name="time">The time in Timespan form</param>
        /// <returns>True if the time is is in Timespan form, otherwise false</returns>
        public static bool IsTimeSpan(string time)
        {
            string[] ent = time.Split(new char[] { ':', ';', '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (ent.Length == 4)
            {
                int x = 0;
                if (int.TryParse(ent[0], out x) & int.TryParse(ent[1], out x)
                  & int.TryParse(ent[2], out x))
                    return true;
            }
            return false;
        }
    }
}
