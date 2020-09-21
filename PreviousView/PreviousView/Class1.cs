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

namespace PreviousView
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            //List<View> views = new List<View>();
            List<UIView> views = uidoc.GetOpenUIViews() as List<UIView>;

            if (views.Count > 1)
            {
                View view = doc.GetElement(views[1].ViewId) as View;
                uidoc.RequestViewChange(view);

            } else
            {
                ;
            }

            return Result.Succeeded;
        }
        
    }
}
