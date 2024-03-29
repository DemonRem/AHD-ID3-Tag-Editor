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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AHD.SM.Formats
{
    public partial class Cl_frameRate : UserControl
    {
        SubtitlesFormat format;
        public Cl_frameRate()
        {
            InitializeComponent();
        }
        public Cl_frameRate(SubtitlesFormat format)
        {
            InitializeComponent();
            this.SubtitlesFormat = format;
        }
        public SubtitlesFormat SubtitlesFormat
        {
            get { return format; }
            set 
            { 
                format = value;
                comboBox1.Items.Clear();
                if (format == null)
                {
                    timeEdit1.Visible = true;
                    comboBox1.Visible = false;
                    return;
                }
                if (format.FrameRates != null)//if the format accepts any frame rate
                {
                    timeEdit1.Visible = false;
                    comboBox1.Visible = true;
                    foreach (double fr in format.FrameRates)
                    {
                        comboBox1.Items.Add(fr);
                    }
                    comboBox1.SelectedItem = format.FrameRate;
                }
                else
                {
                    timeEdit1.Visible = true;
                    comboBox1.Visible = false;
                    timeEdit1.SetTime(format.FrameRate, false);
                }
            }
        }

        private void timeEdit1_TimeChanged(object sender, EventArgs e)
        {
            format.FrameRate = timeEdit1.GetSeconds();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            format.FrameRate = (double)comboBox1.SelectedItem;
        }
    }
}
