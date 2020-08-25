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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AHD.SM.Formats
{
    public partial class cl_SubRip : UserControl
    {
        SubRip format;
        public cl_SubRip(SubRip format)
        {
            InitializeComponent();
            this.format = format;

            checkBox1.Checked = format.EnableSubtitlesFontAndColor;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            format.EnableSubtitlesFontAndColor = checkBox1.Checked;
        }
    }
}
