﻿/* This file is part of AHD ID3 Tag Editor (AITE)
 * A program that edit and create ID3 Tag.
 *
 * Copyright © Alaa Ibrahim Hadid 2012 - 2021
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHD.ID3.Editor
{
    public partial class st_ID3V2 : SettingsControl
    {
        public st_ID3V2()
        {
            InitializeComponent();
            comboBox_tagVersion.Items.Add(2);
            comboBox_tagVersion.Items.Add(3);
            comboBox_tagVersion.Items.Add(4);
            comboBox_tagVersion.SelectedItem = Program.Settings.DefaultID3V2Version;

            checkBox_dropExtendedHeader.Checked = Program.Settings.ID3V2_DropExtendedHeader;
            checkBox_footer.Checked = Program.Settings.ID3V2_WriteFooter;
            checkBox_keepPadding.Checked = Program.Settings.ID3V2_KeepPadding;
            checkBox_unsynchronisation.Checked = Program.Settings.ID3V2_UseUnsynchronisation;
        }
        public override string ToString()
        {
            return "ID3 Tag V2";
        }
        public override void SaveSettings()
        {
            Program.Settings.DefaultID3V2Version = (int)comboBox_tagVersion.SelectedItem;
            Program.Settings.ID3V2_DropExtendedHeader = checkBox_dropExtendedHeader.Checked;
            Program.Settings.ID3V2_WriteFooter = checkBox_footer.Checked;
            Program.Settings.ID3V2_KeepPadding = checkBox_keepPadding.Checked;
            Program.Settings.ID3V2_UseUnsynchronisation = checkBox_unsynchronisation.Checked;
            Program.LoadID3V2Settings();// apply for id3v2
        }
        public override void DefaultSettings()
        {
            comboBox_tagVersion.SelectedItem = 3;
            checkBox_dropExtendedHeader.Checked = false;
            checkBox_footer.Checked = false;
            checkBox_keepPadding.Checked = true;
            checkBox_unsynchronisation.Checked = false;
        }
    }
}
