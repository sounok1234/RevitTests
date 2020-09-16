using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using System;


namespace AutoAnnotation
{
	class Collector
	{
		//Get exisitng textnotes
		public int GetMaxValue(Document doc)
		{
			FilteredElementCollector collector = new FilteredElementCollector(doc);
			ICollection<Element> TextNotes = collector.OfCategory(BuiltInCategory.OST_TextNotes).WhereElementIsNotElementType().ToElements();

			List<int> Values = new List<int>();
			foreach (Element e in collector)
			{
				TextNote tn = e as TextNote;
				Values.Add(Int32.Parse(tn.Text));
			}

			if (TextNotes.Count > 0)
            {
				return (Values.Max() + 1);
			} else
            {
				return 1;
            }
			
		}


	}
}