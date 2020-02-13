﻿using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace VisualAgents
{
    public class VisualAgentsInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "VisualAgents";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("0468b66d-1f38-4274-b6a9-630c49ac2b0b");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
