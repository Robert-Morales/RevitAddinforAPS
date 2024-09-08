using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;
//using Autodesk.Revit.ApplicationServices;

namespace RevitAddinforAPS.Comandos
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Parametros : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region variables globales
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application APP = uiapp.Application;
            Autodesk.Revit.DB.Document doc = uidoc.Document;

            #endregion

            #region accediendo manualmente a elementos

            Selection sel = uidoc.Selection;
            IList<Reference> ListaRefs = sel.PickObjects(ObjectType.Element);
            string general = "";
            foreach (Reference refe in ListaRefs)
            {
                string infoParams = "";
                Element element = doc.GetElement(refe); 
                foreach(Parameter PARAM in element.Parameters)
                {
                    string names = PARAM.Definition.Name;
                    string data = PARAM.StorageType.ToString();

                    infoParams += names + Environment.NewLine + data;
                }
                general += infoParams;
            }
            TaskDialog.Show("informacion parametros", general);


            #endregion

            return Result.Succeeded;
        }
    }
}
