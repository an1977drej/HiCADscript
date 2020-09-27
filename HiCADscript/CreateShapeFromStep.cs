using ISD.CAD.Contexts;
using ISD.CAD.Data;
using ISD.CAD.Interface;
using ISD.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiCADscript
{
    static class CreateShapeFromStep
    {
        static UnconstrainedContext Context;
        static EBPart ebPart;
        static frmGUI scriptGUI;

        static CreateShapeFromStep()
        {
            Context = Script.Context;
            scriptGUI = Script.scriptGUI;
        }

        public static Part PlaceShapeInCurrentNode(EBPart EbP)
        {
            AssemblyNode parent = (AssemblyNode)Context.ActiveNode;
            ISD.CAD.IO.StepImportSettings settings = new ISD.CAD.IO.StepImportSettings();
            Part shape = (Part)ISD.CAD.IO.FileIO.Load(@"C:\Users\Andrej\Downloads\STEP-2.14\9666.926(2).stp", settings);
            shape.DisplaceTo(parent);
            scriptGUI.Say(shape.UID);
            Context.EnforceBrowserUpdate();
            Context.EnforceRedraw();
            return shape;
        }
        public static void PlaceShapeOnSelectedPoint(EBPart EbP)
        {
            ebPart = EbP;
            Selection.SelectionEvent += OnPointSelected;
            Selection.StartSelection(SelectionType.Point, "Select a point");
        }

        private static void OnPointSelected(object sender, SelectionEventArgs args)
        {
            Transformation blockTransformation = new Transformation();

            PointOption lastPoint = args.Result[0] as PointOption;

            if (lastPoint != null)
            {
                scriptGUI.Say(lastPoint.Point.ToString());
                blockTransformation.SetTranslation(new Vector3D(lastPoint.Point));
                Part box = PlaceShapeInCurrentNode(ebPart);
                box.Move(blockTransformation);
            }
            Selection.SelectionEvent -= OnPointSelected;
        }

    }
}
