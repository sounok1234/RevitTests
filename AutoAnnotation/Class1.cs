using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace AutoAnnotation
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get application and documnet objects
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;
            //TextNoteOptions options = new TextNoteOptions();
            ElementId typeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);
            int i = 1;

            IList<Reference> pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Select Multiple Elements");
            List<ElementId> ids = (from Reference r in pickedObjs select r.ElementId).ToList();
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("transaction");
                if (pickedObjs != null && pickedObjs.Count > 0)
                {
                    foreach (ElementId eid in ids)
                    {
                        Element e = doc.GetElement(eid);
                        //string strng = "random";
                        //FamilyInstance faminst = e as FamilyInstance;
                        LocationPoint locPoint = e.Location as LocationPoint;
                        if (null != locPoint)
                        {
                            XYZ point_a = locPoint.Point;
                            TextNote note = TextNote.Create(doc, view.Id, point_a, i.ToString(), typeId);
                        } else
                        {
                            LocationCurve locCurve = e.Location as LocationCurve;
                            XYZ point_a = locCurve.Curve.Evaluate(0.5, true);
                            TextNote note = TextNote.Create(doc, view.Id, point_a, i.ToString(), typeId);
                        }
                        i += 1; 
                    }
                    
                }
                tx.Commit();
            }
            return Result.Succeeded;
        }
    }
}
