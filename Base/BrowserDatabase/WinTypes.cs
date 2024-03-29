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
using System.Windows.Forms;
using System.IO;
namespace AHD.ID3.Editor.Base
{
    public class TreeNodeBFolder : TreeNode
    {
        BFolder folder;

        public BFolder BFolder
        { get { return folder; } set { folder = value; RefreshName(); } }

        public void RefreshName()
        {
            this.Text = Path.GetFileName(folder.Path); 
         }

        public void RefreshFolders(int imageIndex,int selectedImageIndex)
        {
            Nodes.Clear();
            foreach (BFolder fol in folder.BFolders)
            {
                TreeNodeBFolder tr = new TreeNodeBFolder();
                tr.BFolder = fol;
                tr.ImageIndex = imageIndex;
                tr.SelectedImageIndex = selectedImageIndex;
                tr.RefreshFolders(imageIndex, selectedImageIndex);
                Nodes.Add(tr);
            }
        }
    }
}
