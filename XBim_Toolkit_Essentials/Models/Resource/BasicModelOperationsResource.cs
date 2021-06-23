using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using XBim_Toolkit_Essentials.Models.View;

namespace XBim_Toolkit_Essentials.Models.Resource
{
    public class BasicModelOperationsResource
    {
        private string filePath { get; set; } = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\Temp\XBim_Toolkit_Essentials\");
        public string GlobalId { get; set; }
        public BasicModelOperationsViewModel BasicModelOperationsViewModel { get; set; } = new BasicModelOperationsViewModel();
        public List<ConvertXmlViewModel> ListConvertXmlViewModel { get; set; } = new List<ConvertXmlViewModel>();
        public void Retrieve()
        {
            
            string fileName = filePath + "SampleHouse.ifc";
            using (var model = IfcStore.Open(fileName))
            {
                //get all doors in the model (using IFC4 interface of IfcDoor this will work both for IFC2x3 and IFC4)
                var allDoors = model.Instances.OfType<IIfcDoor>();

                //get only doors with defined IIfcTypeObject
                var someDoors = model.Instances.Where<IIfcDoor>(d => d.IsTypedBy.Any());

                //get one single door 
                var theDoor = model.Instances.FirstOrDefault<IIfcDoor>(d => d.GlobalId == this.GlobalId);
                this.BasicModelOperationsViewModel.GlobalId = theDoor.GlobalId;
                this.BasicModelOperationsViewModel.Name = theDoor.Name;

                //get all single-value properties of the door
                var properties = theDoor.IsDefinedBy
                    .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
                    .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                    .OfType<IIfcPropertySingleValue>();

                foreach (var property in properties)
                {
                    this.BasicModelOperationsViewModel.ListProperties.Add(new Properties
                    {
                        Name = property.Name,
                        NominalValue = property.NominalValue
                    });
                }
                    
            }
        }

        public void ConvertXml()
        {
            //open STEP21 file
            string fileName = filePath + "SampleHouse.ifc";
            using (var stepModel = IfcStore.Open(fileName))
            {
                //save as XML
                stepModel.SaveAs(filePath + "SampleHouse.ifcxml");

                //open XML file
                using (var xmlModel = IfcStore.Open(filePath + "SampleHouse.ifcxml"))
                {
                    this.ListConvertXmlViewModel.Add(new ConvertXmlViewModel
                    {
                        TypeName = "All",
                        StepCount = stepModel.Instances.Count(),
                        XmlCount = xmlModel.Instances.Count(),
                    });

                    var ListType = stepModel.Instances.Select(x => x.GetType().Name).Distinct().ToList();
                    foreach (var type in ListType)
                    {
                        this.ListConvertXmlViewModel.Add(new ConvertXmlViewModel
                        {
                            TypeName = type,
                            StepCount = stepModel.Instances.Where(d => type.Contains(d.GetType().Name)).Count(),
                            XmlCount = xmlModel.Instances.Where(d => type.Contains(d.GetType().Name)).Count(),
                        });
                    }
                }
            }

        }
    }
}
