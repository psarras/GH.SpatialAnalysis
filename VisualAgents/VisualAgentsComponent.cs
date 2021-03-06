﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace VisualAgents
{
    public class VisualAgentsComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public VisualAgentsComponent()
          : base("Visual Agents", "VisAgents",
              "Simple example with visual agents",
              "Spatial", "Analysis")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Start", "S", "Start Position", GH_ParamAccess.item);
            pManager.AddVectorParameter("Direction", "D", "Start Direction", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Detail", "D", "Number of samples of the isovist", GH_ParamAccess.item, 20);
            pManager.AddNumberParameter("Field", "F", "Visual field in degrees", GH_ParamAccess.item, 170);

            pManager.AddCurveParameter("Obstacles", "O", "Obstacles in curves", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Reset", "R", "Reset the agent", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Position", "P", "Current Position of the agent", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool reset = false;
            DA.GetData("Reset", ref reset);
            List<Curve> curves = new List<Curve>();
            if (!DA.GetDataList("Obstacles", curves))
                return;
            int detail = 1;
            DA.GetData("Detail", ref detail);
            double field = 1;
            DA.GetData("Field", ref field);
            Point3d P = Point3d.Unset;
            if (!DA.GetData("Start", ref P))
                return;
            Vector3d D = Vector3d.Unset;
            if (!DA.GetData("Direction", ref D))
                return;

            if (agent == null || reset)
            {
                agent = new IsoAgent(detail, field, P, D);
            }

            agent.Observe(curves);
            agent.Update();

            DA.SetData(0, agent.pos);
        }

        public IsoAgent agent;

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7a6757d2-aa56-4a15-977f-0b28a7035fe8"); }
        }
    }
}
